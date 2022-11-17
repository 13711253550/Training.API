using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Hubs
{
    public class SignaIRChat:Hub
    {
        //解释: ConcurrentDictionary<TKey, TValue>是一个线程安全的字典，它的所有操作都是原子操作，不需要手动加锁。
        private static ConcurrentDictionary<string, string> _sockets = new ConcurrentDictionary<string, string>();
        //解释: 重写Hub类的OnConnectedAsync方法，当客户端连接到服务器时，会调用这个方法。
        public override Task OnConnectedAsync()
        {
            //解释: 通过Context.ConnectionId获取客户端的连接ID，通过Context.User.Identity.Name获取客户端的用户名。
            _sockets.TryAdd(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }

        //解释: 重写Hub类的OnDisconnectedAsync方法，当客户端断开连接时，会调用这个方法。
        public override Task OnDisconnectedAsync(Exception exception)
        {
            //解释: 通过Context.ConnectionId获取客户端的连接ID，通过Context.User.Identity.Name获取客户端的用户名。
            _sockets.TryRemove(Context.ConnectionId, out string name);
            return base.OnDisconnectedAsync(exception);
        }
        
        //解释: 通过这个方法，客户端可以调用服务器端的SendMessage方法，发送消息。
        private static ConcurrentDictionary<string, List<string>> dialogues = new ConcurrentDictionary<string, List<string>>();

         
    }
}
