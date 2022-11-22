using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Entity;
using Training.Services.IService;

namespace Training.Services.Service
{
    public class SignaIRChatService : Hub
    {
        //解释: ConcurrentDictionary<TKey, TValue>是一个线程安全的字典，它的所有操作都是原子操作，不需要手动加锁。
        private static ConcurrentDictionary<string, string> _sockets = new ConcurrentDictionary<string, string>();

        //解释: 通过这个方法，客户端可以调用服务器端的SendMessage方法，发送消息。
        private static ConcurrentDictionary<string, List<string>> dialogues = new ConcurrentDictionary<string, List<string>>();


        //给所有人发送消息
        public async Task SendMessage(string user, string message)
        {
            string name = Context.ConnectionId;
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        //给指定人发送消息
        public async Task SendMessageToCaller(string user,string Foruser, string message)
        {
            var lst = Context.ConnectionId;
            string connId = GetId(Foruser);
            //给指定用户发消息
            await Clients.Client(connId).SendAsync("ReceiveMessage", user, message);
        }

        //传输图片
        public async Task SendImgToCaller(string user, string Foruser, string Img)
        {
            var lst = Context.ConnectionId;
            string connId = GetId(Foruser);
            //给指定用户发消息
            await Clients.Client(connId).SendAsync("ReceiveImg", user, Img);
        }

        //根据键获取值
        public string GetId(string chatterName)
        {
            //解释：根据键获取值
            return _sockets.FirstOrDefault(x => x.Key.Equals(chatterName)).Value;
        }

        //绑定的字典
        public string ListBindUser(string UserName)
        {
            try
            {
                bool AddJudge = true;
                List<string> a = new List<string>();
                
                
                AddJudge = _sockets.TryAdd(UserName, Context.ConnectionId);//添加进入用户
                if (!(GetId(UserName)==null))
                {
                    //对_sockets数据字典进行更新
                   AddJudge = _sockets.TryUpdate(UserName, Context.ConnectionId, GetId(UserName));
                }

                if (!dialogues.ContainsKey("List"))//查找聊天键,里面放着的是处于聊天页面的用户
                {
                    dialogues.TryAdd("List", a);
                }
                dialogues["List"].Add(UserName);//加入列表页面用户组

                if (AddJudge == true)//判断用户是否绑定成功!
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

        //解绑用户
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

        //根据用户名获取到_sockets对应value
        public string GetConnectionId(string UserName)
        {
            return _sockets.FirstOrDefault(x => x.Key.Equals(UserName)).Value;
        }
    }
}
