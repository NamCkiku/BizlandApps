using System;

namespace Bizland.Droid.FBAccountkit
{
    public class AccountKitAuth
    {
        public static int APP_REQUEST_CODE = 99;

        public IntPtr Handle => throw new NotImplementedException();

        public void LoginWithAccountKit(/*LoginType type, AccountKitActivity.ResponseType responseType*/)
        {
            //var intent = new Intent(Forms.Context, typeof(AccountKitActivity));

            //if (intent != null)
            //{
            //    var configurationBuilder = new AccountKitConfiguration.AccountKitConfigurationBuilder(
            //         type,
            //         responseType);

            //    intent.PutExtra(
            //    AccountKitActivityBase.AccountKitActivityConfiguration,
            //    configurationBuilder.Build());
            //}

            //Forms.Context.StartActivity(intent);


        }


        public void GetCurrentAccount()
        {
            // AccountKit.GetCurrentAccount(this);
        }


        public void Logout()
        {
            // AccountKit.LogOut();
        }




        //public void OnError(AccountKitError p0)
        //{

        //}

        public void OnSuccess(Java.Lang.Object p0)
        {
            //var account = Android.Runtime.Extensions.JavaCast<Account>(p0);
        }

        public void Dispose()
        {
        }
    }
}