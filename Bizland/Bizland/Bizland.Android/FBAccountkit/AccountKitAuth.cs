using Android.App;
using Android.Content;
using Android.Support.V7.App;
using System;
using System.Threading.Tasks;
using AKAccount = Com.Facebook.Accountkit.Account;
using AKAccountKit = Com.Facebook.Accountkit.AccountKit;
using AKAccountKitActivity = Com.Facebook.Accountkit.UI.AccountKitActivity;
using AKAccountKitConfiguration = Com.Facebook.Accountkit.UI.AccountKitConfiguration;
using AKAccountKitError = Com.Facebook.Accountkit.AccountKitError;
using AKAccountKitLoginResult = Com.Facebook.Accountkit.AccountKitLoginResult;
using AKLoginType = Com.Facebook.Accountkit.UI.LoginType;
using IAccountKitAuth = Bizland.Core.IAccountKitService;
using IAKAccountKitCallback = Com.Facebook.Accountkit.IAccountKitCallback;
using IAKAccountKitLoginResult = Com.Facebook.Accountkit.IAccountKitLoginResult;
using LoginAccount = Bizland.Core.LoginAccount;
using LoginResult = Bizland.Core.LoginResult;
using LoginType = Bizland.Core.LoginType;
using ResponseType = Bizland.Core.ResponseType;

namespace Bizland.Droid.FBAccountkit
{
    public class AccountKitAuth : IAccountKitAuth
    {
        readonly Func<Activity> activityInContext;

        public AccountKitAuth(Func<Activity> activityInContext)
        {
            this.activityInContext = activityInContext;
        }


        public Task<LoginAccount> GetCurrentAccount(ResponseType responseType)
        {
            var taskCompletionSource = new TaskCompletionSource<LoginAccount>();

            AKAccountKit.GetCurrentAccount(new InnerAccountKitCallback(taskCompletionSource));

            return taskCompletionSource.Task;
        }

        public Task<LoginResult> LoginWithAccountKit(LoginType loginType, ResponseType responseType)
        {
            var taskCompletionSource = new TaskCompletionSource<LoginResult>();

            Action<IAKAccountKitLoginResult> onAKResult = (e) =>
            {
                if (e == null)
                {
                    taskCompletionSource.SetResult(new LoginResult(false));
                }
                else
                {
                    var tokenOrCode = responseType == ResponseType.AccessToken
                                                                    ? e.AccessToken?.Token
                                                                    : e.AuthorizationCode;
                    var result = new LoginResult(true, false, tokenOrCode);
                    taskCompletionSource.SetResult(result);
                }

                HiddenAccountKitActivity.Completed = null;
            };

            HiddenAccountKitActivity.Completed = onAKResult;

            var context = activityInContext?.Invoke();
            var intent = new Intent(context, typeof(HiddenAccountKitActivity));

            intent.PutExtra(nameof(ResponseType), (int)responseType);
            intent.PutExtra(nameof(LoginType), (int)loginType);

            context?.StartActivity(intent);

            return taskCompletionSource.Task;
        }

        class InnerAccountKitCallback : Java.Lang.Object, IAKAccountKitCallback
        {
            readonly TaskCompletionSource<LoginAccount> taskCompleteionSource;

            public InnerAccountKitCallback(TaskCompletionSource<LoginAccount> taskCompleteionSource)
            {
                this.taskCompleteionSource = taskCompleteionSource;
            }

            public void OnError(AKAccountKitError p0)
            {
                //TODO return exception
                var result = new LoginAccount(true);

                taskCompleteionSource.SetResult(result);
            }

            public void OnSuccess(Java.Lang.Object p0)
            {
                var account = p0 as AKAccount;

                var result = new LoginAccount(phoneNumber: account.PhoneNumber.ToString(), email: account.Email);

                taskCompleteionSource.SetResult(result);
            }
        }
    }

    [Activity(Theme = "@style/MainTheme")]
    public class HiddenAccountKitActivity : AppCompatActivity
    {
        const int APP_REQUEST_CODE = 9999;
        internal static Action<IAKAccountKitLoginResult> Completed;

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var responseType = (ResponseType)Intent.Extras.GetInt(nameof(ResponseType));
            var loginType = (LoginType)Intent.Extras.GetInt(nameof(LoginType));

            var intent = new Intent(this, typeof(AKAccountKitActivity));
            var configurationBuilder =
                new AKAccountKitConfiguration.AccountKitConfigurationBuilder(
                    loginType == LoginType.Phone ? AKLoginType.Phone : AKLoginType.Email,
                    responseType == ResponseType.AuthorizationCode ? AKAccountKitActivity.ResponseType.Code : AKAccountKitActivity.ResponseType.Token);

            intent.PutExtra(
                        AKAccountKitActivity.AccountKitActivityConfiguration,
                        configurationBuilder.Build());

            StartActivityForResult(intent, APP_REQUEST_CODE);

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode != APP_REQUEST_CODE)
            {
                Finish();
                return;
            }


            var result = data?.GetParcelableExtra(AKAccountKitLoginResult.ResultKey).Cast<IAKAccountKitLoginResult>();

            if (result != null && (result.AccessToken != null || result.AuthorizationCode != null))
            {
                Completed?.Invoke(result);

                Finish();
            }
        }
    }

    public static class ObjectTypeHelper
    {
        public static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }
    }
}