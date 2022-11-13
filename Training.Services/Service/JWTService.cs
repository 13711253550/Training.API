using CSRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Training.Domain.DTO;
using Training.Domain.Entity;
using Training.Domain.Entity.JWT;
using Training.Domain.Shard;
using Training.EFCore;
using Training.Services.IService;

namespace Training.Services.Service
{
    public class JWTService : IJWTService
    {
        public IRespotry<User> User;
        public IConfiguration Configuration;
        public JWTService(IRespotry<User> User, IConfiguration configuration)
        {
            this.User = User;
            this.Configuration = configuration;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Result<string> Login(LoginDTO login)
        {
            //查询用户
            var user = User.GetList().Where(x => x.account == login.account && x.password == login.password).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            JWT jwt = new JWT()
            {
                Uid = user.Id,
                verification_JWT = GetNewJWT(user, 1),
                renovation_JWT = GetNewJWT(user, 300)
            };


            var Res = AddRedis(jwt, user.Id);
            return new Result<string>()
            {
                code = stateEnum.Success,
                message = "登录成功",
                data = jwt.verification_JWT
            };
        }

        /// <summary>
        /// 解密Token获取到Id
        /// </summary>
        /// <param name="Token">Token用户信息</param>
        /// <returns></returns>
        public int GetUserName(string Token)
        {
            var jwt = new JwtSecurityToken(Token);
            //解释jwt.Payload["Uid"] 为用户ID
            var uid = jwt.Payload["Uid"].ToString();
            return int.Parse(uid);
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        private string GetNewJWT(User user, int Time)
        {
            //解释JwtSecurityTokenHandler类型: JWT安全令牌处理程序类
            var tokenHand = new JwtSecurityTokenHandler();
            //解释Encoding.ASCII.GetBytes 这个方法是将字符串转换成字节数组
            //解释1111111111111111 这个字符串是加密的key，你可以随便写，但是要记住，这里的key要和验证的时候的key一样
            var key = Encoding.ASCII.GetBytes(Configuration["AppSetting:Token"]);
            //解释SecurityTokenDescriptor 这个类是用来描述Token的一些基本信息的，比如说，谁颁发的，谁接收的，什么时候颁发的，什么时候过期的，加密的时候使用的key等等
            var tokenDescript = new SecurityTokenDescriptor()
            {
                //解释Subject 这个属性是用来描述这个Token的接收者的，一般是用户名
                //解释ClaimsIdentity  这个类是用来描述用户的一些基本信息的，比如说，用户名，用户ID，用户邮箱等等
                Subject = new ClaimsIdentity(new Claim[]
                 {
                     new Claim(ClaimDTO.Uid, user.Id.ToString()),
                     new Claim(ClaimDTO.Uname, user.account),
                 }),
                //解释Expires 这个属性是用来设置Token的过期时间的
                Expires = DateTime.Now.AddMinutes(Time),
                //解释Issuer 这个属性是用来设置Token的颁发者的
                Issuer = "http://localhost:5041",
                //解释Audience 这个属性是用来设置Token的接收者的
                Audience = "http://localhost:5041",
                //解释SigningCredentials 这个属性是用来设置Token的加密方式的
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //解释tokenHand.CreateToken 这个方法是用来生成Token的
            var token = tokenHand.CreateToken(tokenDescript);
            //解释tokenHand.WriteToken 这个方法是用来将Token转换成字符串的
            return tokenHand.WriteToken(token);
        }

        /// <summary>
        /// 将JWT放入Redis
        /// </summary>
        /// <param name="jwt"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        private string AddRedis(JWT jwt, int uid)
        {
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6379");
            RedisHelper.Initialization(redis);
            redis.Set(uid.ToString(), jwt);
            return "插入成功";
        }

        /// <summary>
        /// 判断验证Token和刷新Token是否过期
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetToken(string token)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            //反编译token
            if (token == null || token == "0")
            {
                return "0";
            }
            var jwt = new JwtSecurityToken(token);

            //payload:获取到当前token的负载
            //解释exp 为过期时间
            var exp = jwt.Payload["exp"].ToString();
            //格式转换exp
            var expTime = startTime.AddSeconds(long.Parse(exp));
            if (expTime < DateTime.Now)
            {
                CSRedisClient redis = new CSRedisClient("127.0.0.1:6379");
                RedisHelper.Initialization(redis);
                var uid = jwt.Payload["Uid"].ToString();
                var jwtNow = redis.Get<JWT>(uid);
                var jwt2 = new JwtSecurityToken(jwtNow.renovation_JWT);
                var renovation_exp = jwt2.Payload["exp"].ToString();
                var renovation_expTime = startTime.AddSeconds(long.Parse(renovation_exp));
                if (renovation_expTime > DateTime.Now && token == jwtNow.verification_JWT)
                {
                    User user = new User()
                    {
                        Id = Convert.ToInt32(jwt.Payload["Uid"]),
                        account = jwt.Payload["Uname"].ToString(),
                    };
                    JWT newjwt = new JWT();
                    newjwt.verification_JWT = GetNewJWT(user, 1);
                    newjwt.renovation_JWT = GetNewJWT(user, 300);
                    redis.Set(uid.ToString(), newjwt);
                    return newjwt.verification_JWT;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return token;
            }
        }
    }
}
