using Bizland.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bizland.ViewModels
{
    public class ChatPageViewModel : ViewModelBase
    {

        private IChatServices _chatServices;
        private string _roomName = "PrivateRoom";

        public ChatPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Chat";
        }

        #region ViewModel Properties

        private ObservableCollection<ChatMessageViewModel> _messages;

        public ObservableCollection<ChatMessageViewModel> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                RaisePropertyChanged(() => Messages);
            }
        }

        private ChatMessageViewModel _chatMessage;
        public ChatMessageViewModel ChatMessage
        {
            get { return _chatMessage; }
            set
            {
                _chatMessage = value;
                RaisePropertyChanged(() => ChatMessage);
            }
        }

        #endregion
    }
}