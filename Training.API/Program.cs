using Autofac;
using AutoMapper;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Training.EFCore;
using Training.EFCore.Context;
using Training.Services.Service;
using Training.Domain.Entity;
using Training.Domain.Shard;
using Training.API;
using Newtonsoft.Json.Linq;
using Training.Services;
using Training.Services.IService;
using Autofac.Core;
using Aop.Api;
using Hangfire;
using Hangfire.MemoryStorage;
using CSRedis;
using Microsoft.Extensions.Configuration;
using Training.Domain.Entity.Seckill;
using Aop.Api.Domain;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAutofac(new AutofacServiceProviderFactory());

#region JWT
//开启JWT认证
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        //配置JWT认证参数
        //解释TokenValidationParameters 这个类是用来配置Token的验证参数的
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //是否验证发行人
            ValidateIssuer = true,
            //发行人
            ValidIssuer = "http://localhost:5041",
            //是否验证接收人
            ValidateAudience = true,
            //接收人
            ValidAudience = "http://localhost:5041",
            //是否验证安全密钥
            ValidateIssuerSigningKey = true,
            //安全密钥
            //配置文件中的密钥

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AppSetting:Token"])),
            //是否验证过期时间
            ValidateLifetime = true,
            //过期时间
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

//Event事件: 如果Bearer认证失败调用JWTService的方法
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            //如果Token过期
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                //调用JWTService方法 刷新Token
                // var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJWTService>();
                
                //刷新Token
                context.Response.Headers.Add("Authorization", "-1");
                //暴露前端自定义头部字段
                context.Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
            }
            return Task.CompletedTask;
        }
    };
});
// Add services to the container.
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "电商", Description = "API描述" });
    options.OrderActionsBy(o => o.RelativePath);
    options.IncludeXmlComments("NoName.Core.xml", true);
});
builder.Services.AddControllers().AddJsonOptions(p => p.JsonSerializerOptions.PropertyNamingPolicy = null);
//builder.Services.AddCors((x) => { x.AddPolicy("kuayu", d => d.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());});

<<<<<<< HEAD
#region 定时任务
=======
>>>>>>> 7c15e5adcce266222ab5ca86412c2d67cb28f0d3
builder.Services.AddHangfire(configura =>
{
    configura.UseStorage(new MemoryStorage());
    //每隔30分钟执行一次
    //调用SeckillService方法
    RecurringJob.AddOrUpdate<SeckillService>(x => x.SetRedis(), "0 0/1 * * * ? ");
<<<<<<< HEAD
    RecurringJob.AddOrUpdate<SeckillService>(x => x.RedisToDB(), "0 0/30 * * * ? ");
});
#endregion
=======
});
>>>>>>> 7c15e5adcce266222ab5ca86412c2d67cb28f0d3

#region Redis
////注册CSRedis服务
//string strRedis = builder.Configuration["Redis:Host"];
//var cs = new CSRedisClient(strRedis);//实例化CSRedis客户端
//RedisHelper.Initialization(cs);//初始化RedisHelper的实例
#endregion

#region 数据库
//跨域
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
    {
        builder.AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
    });
});

if (builder.Configuration["SQL"] == "MySQL")
{
    builder.Services.AddDbContext<SqlContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
}
else
{
    //使用MySql数据库
    //解释MySqlServerVersion.LatestSupportedServerVersion
    //这个类是用来配置MySql的版本的
    builder.Services.AddDbContext<SqlContext>(x => x.UseMySql(builder.Configuration.GetConnectionString("MySql"), new MySqlServerVersion(new Version(8, 0, 22))));
}
#endregion

#region SignalR
//使用SignalR
builder.Services.AddSignalR();
#endregion

#region 支付宝支付
//支付宝支付
builder.Services.Configure<AlipayConfig>(builder.Configuration.GetSection("Alipay"));
#endregion

#region Autofac/AutoMapper自动注入
//AutoFac自动注册
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    //解释:builder.RegisterAssemblyTypes() 这个方法是用来注册程序集中的类的
    builder.RegisterModule(new AutofacModuleRegister());
});

//autoMapper 自动映射
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "项目名称 V1");

    s.RoutePrefix = "";//请求swagger路径\
});
app.UseCors();

app.UseHangfireDashboard();//配置后台仪表盘

app.UseHangfireServer();//开始使用Hangfire服务

//SignlR
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<SignaIRChat>("/chatHub");
//});

app.MapHub<SignaIRChatService>("/chatHub");

//注意：app.UseAuthentication() 必须在 app.UseAuthorization() 之前调用
app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
