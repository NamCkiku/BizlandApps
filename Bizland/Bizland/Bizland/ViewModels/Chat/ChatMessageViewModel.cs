﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.ViewModels
{
    public class ChatMessageViewModel : ExtendedBindableObject
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);

            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        private string _image;

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        private bool _isMine;

        public bool IsMine
        {
            get { return _isMine; }
            set
            {
                _isMine = value;
                RaisePropertyChanged(() => IsMine);
            }
        }
    }
}
