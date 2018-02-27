using Bizland.Interfaces;
using Bizland.Model;
using Bizland.Service;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland
{
    public class ChatServices : IChatServices
    {
        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;

        public event EventHandler<ChatMessage> OnMessageReceived;

        public ChatServices()
        {
            _connection = new HubConnection("http://10.1.11.131:8010/");
            _proxy = _connection.CreateHubProxy("ChatHub");
        }

        #region IChatServices implementation

        public async Task Connect()
        {
            await _connection.Start();

            _proxy.On("GetMessage", (string name, string message) => OnMessageReceived(this, new ChatMessage
            {
                Name = name,
                Message = message
            }));
        }

        public async Task Send(ChatMessage message, string roomName)
        {
            _proxy.Invoke("SendMessage", message.Name, message.Message, roomName);
        }

        public async Task JoinRoom(string roomName)
        {
            _proxy.Invoke("JoinRoom", roomName);
        }

        #endregion
    }
}
