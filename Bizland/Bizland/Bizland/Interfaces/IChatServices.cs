using Bizland.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bizland.Interfaces
{
    public interface IChatServices
    {
        Task Connect();
        Task Send(ChatMessage message, string roomName);
        Task JoinRoom(string roomName);
        event EventHandler<ChatMessage> OnMessageReceived;
    }
}
