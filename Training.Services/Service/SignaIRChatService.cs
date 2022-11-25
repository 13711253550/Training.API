using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TencentCloud.Ccc.V20200210.Models;
using Training.Domain.Entity;
using Training.Domain.Entity.UserEntity;
using Training.EFCore;
using Training.Services.IService;

namespace Training.Services.Service
{
    public class SignaIRChatService : Hub
    {
        public IRespotry<Clinical_Reception> Clinical_Reception;

        public SignaIRChatService(IRespotry<Clinical_Reception> Clinical_Reception)
        {
            this.Clinical_Reception = Clinical_Reception;
        }

        //解释: ConcurrentDictionary<TKey, TValue>是一个线程安全的字典，它的所有操作都是原子操作，不需要手动加锁。
        private static ConcurrentDictionary<string, string> _sockets = new ConcurrentDictionary<string, string>();

        //解释: 通过这个方法，客户端可以调用服务器端的SendMessage方法，发送消息。
        private static ConcurrentDictionary<string, List<string>> dialogues = new ConcurrentDictionary<string, List<string>>();


        /// <summary>
        /// 给所有人发送消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            string name = Context.ConnectionId;
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// 给指定人发送消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageToCaller(string user, string message)
        {
            string connId = "";
            int Cid = 0;
            //如果user是Did开头
            if (user.StartsWith("Did"))
            {
                //截取user除了Did的部分
                user = user.Substring(3);
                Cid = Clinical_Reception.GetList().Where(x => x.Did == Convert.ToInt32(user)).FirstOrDefault().Uid;
                connId = GetId(Cid.ToString());
                //给指定用户发消息
                await Clients.Client(connId).SendAsync("ReceiveMessage", user, message);
            }
            else
            {
                Cid = Clinical_Reception.GetList().Where(x => x.Uid == Convert.ToInt32(user)).FirstOrDefault().Did;
                string ConnId = "Did" + Cid;
                var lst = Context.ConnectionId;
                connId = GetId(ConnId);
                //给指定用户发消息
                await Clients.Client(connId).SendAsync("ReceiveMessage", user, message);
            }
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// 传输图片
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Foruser"></param>
        /// <param name="Img"></param>
        /// <returns></returns>
        public async Task SendImgToCaller(string user, object Img)
        {
            string connId = "";
            int Cid = 0;
            //如果user是Did开头
            if (user.StartsWith("Did"))
            {
                //截取user除了Did的部分
                user = user.Substring(3);
                Cid = Clinical_Reception.GetList().Where(x => x.Did == Convert.ToInt32(user)).FirstOrDefault().Uid;
                connId = GetId(Cid.ToString());
                //给指定用户发消息
                await Clients.Client(connId).SendAsync("ReceiveImg", user, Img);
            }
            else
            {
                Cid = Clinical_Reception.GetList().Where(x => x.Uid == Convert.ToInt32(user)).FirstOrDefault().Did;
                string ConnId = "Did" + Cid;
                var lst = Context.ConnectionId;
                connId = GetId(ConnId);
                //给指定用户发消息
                await Clients.Client(connId).SendAsync("ReceiveImg", user, Img);
            }
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveImg", user, Img);
        }
        /// <summary>
        /// 传输音频
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mp3"></param>
        /// <returns></returns>
        public async Task SendYinToCaller(string user, string mp3)
        {
            string connId = "";
            int Cid = 0;
            //如果user是Did开头
            if (user.StartsWith("Did"))
            {
                //截取user除了Did的部分
                user = user.Substring(3);
                Cid = Clinical_Reception.GetList().Where(x => x.Did == Convert.ToInt32(user)).FirstOrDefault().Uid;
                connId = GetId(Cid.ToString());
                //给指定用户发消息
                await Clients.Client(connId).SendAsync("ReceiveMp3", user, mp3);
            }
            else
            {
                Cid = Clinical_Reception.GetList().Where(x => x.Uid == Convert.ToInt32(user)).FirstOrDefault().Did;
                string ConnId = "Did" + Cid;
                var lst = Context.ConnectionId;
                connId = GetId(ConnId);
                //给指定用户发消息
                await Clients.Client(connId).SendAsync("ReceiveMp3", user, mp3);
            }
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMp3", user, mp3);
        }

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="chatterName"></param>
        /// <returns></returns>
        public string GetId(string chatterName)
        {
            //解释：根据键获取值
            return _sockets.FirstOrDefault(x => x.Key.Equals(chatterName)).Value;
        }

        /// <summary>
        /// 绑定的字典
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string ListBindUser(string UserName)
        {
            try
            {
                bool AddJudge = true;
                bool UptJudge = true;
                List<string> a = new List<string>();
                
                if (!(GetId(UserName)==null))
                {
                    //对_sockets数据字典进行更新
                    UptJudge = _sockets.TryUpdate(UserName, Context.ConnectionId, GetId(UserName));
                }
                
                AddJudge = _sockets.TryAdd(UserName, Context.ConnectionId);//添加进入用户
                
                if (!dialogues.ContainsKey("List"))//查找聊天键,里面放着的是处于聊天页面的用户
                {
                    dialogues.TryAdd("List", a);
                }
                dialogues["List"].Add(UserName);//加入列表页面用户组

                if (AddJudge == true|| UptJudge==true)//判断用户是否绑定成功!
                {
                    return "绑定成功!";
                }
                else 
                { 
                    return "绑定失败!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 解绑用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public String ListUnBindUser(string UserName)
        {
            try
            {
                //解释这段代码的意思是：如果当前用户已经登录了，那么就把当前用户的连接ID和用户名存到字典里面。
                int port = _sockets.Count;//长度
                string ConnId = Context.ConnectionId;
                _sockets.TryRemove(UserName, out ConnId);//移除用户
                dialogues["List"].Remove(UserName);//移除列表页面用户组
                if (_sockets.Count < port)//判断用户是否绑定成功!
                { return "解绑成功!"; }
                else { return "解绑失败!"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 根据用户名获取到_sockets对应value
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string GetConnectionId(string UserName)
        {
            return _sockets.FirstOrDefault(x => x.Key.Equals(UserName)).Value;
        }
        
    }
}
