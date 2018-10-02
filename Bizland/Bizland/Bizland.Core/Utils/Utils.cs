﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core
{
    public static class Utils
    {
        public static int STATUSCODE_SUCCESS = 1;
        public static int STATUSCODE_FAIL = 0;
        public static int STATUSCODE_UNAUTHORIZE = 2;
        public static int STATUSCODE_SYSTEM_ERROR = -1;

        public static int API_REQUEST_TIMEOUT = 20;

        public static string SubscribeImageFromGallery = "SubscribeImageFromGallery";
        public static string SubscribeImageFromCamera = "SubscribeImageFromCamera";
    }
}
