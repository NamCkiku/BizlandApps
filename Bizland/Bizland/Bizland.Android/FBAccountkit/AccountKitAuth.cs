using Android.App;
using Android.Content;
using Android.Support.V7.App;
using Bizland.Droid.FBAccountkit;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using AKAccount = Com.Facebook.Accountkit.Account;
using AKAccountKit = Com.Facebook.Accountkit.AccountKit;
using AKAccountKitActivity = Com.Facebook.Accountkit.UI.AccountKitActivity;
using AKAccountKitConfiguration = Com.Facebook.Accountkit.UI.AccountKitConfiguration;
using AKAccountKitError = Com.Facebook.Accountkit.AccountKitError;
using AKAccountKitLoginResult = Com.Facebook.Accountkit.AccountKitLoginResult;
using AKLoginType = Com.Facebook.Accountkit.UI.LoginType;
using IAKAccountKitCallback = Com.Facebook.Accountkit.IAccountKitCallback;
using IAKAccountKitLoginResult = Com.Facebook.Accountkit.IAccountKitLoginResult;
using INXAccountKitAuth = Bizland.Core.IAccountKitService;
using NXLoginAccount = Bizland.Core.LoginAccount;
using NXLoginResult = Bizland.Core.LoginResult;
using NXLoginType = Bizland.Core.LoginType;
using NXResponseType = Bizland.Core.ResponseType;

[assembly: Dependency(typeof(AccountKitAuth))]
namespace Bizland.Droid.FBAccountkit
{
    public class AccountKitAuth : INXAccountKitAuth
    {
        readonly Func<Activity> activityInContext;

        public AccountKitAuth(Func<Activity> activityInContext)
        {
            this.activityInContext = activityInContext;
        }


        public Task<NXLoginAccount> GetCurrentAccount(NXResponseType responseType)
        {
            var taskCompletionSource = new TaskCompletionSource<NXLoginAccount>();

            AKAccountKit.GetCurrentAccount(new InnerAccountKitCallback(taskCompletionSource));

            return taskCompletionSource.Task;
        }

        public Task<NXLoginResult> LoginWithAccountKit(NXLoginType loginType, NXResponseType responseType)
        {
            var taskCompletionSource = new TaskCompletionSource<NXLoginResult>();

            Action<IAKAccountKitLoginResult> onAKResult = (e) =>
            {
                if (e == null)
                {
                    taskCompletionSource.SetResult(new NXLoginResult(false));
                }
                else
                {
                    var tokenOrCode = responseType == NXResponseType.AccessToken
                                                                    ? e.AccessToken?.Token
                                                                    : e.AuthorizationCode;
                    var result = new NXLoginResult(true, false, tokenOrCode);
                    taskCompletionSource.SetResult(result);
                }

                HiddenAccountKitActivity.Completed = null;
            };

            HiddenAccountKitActivity.Completed = onAKResult;

            var context = activityInContext?.Invoke();
            var intent = new Intent(context, typeof(HiddenAccountKitActivity));

            intent.PutExtra(nameof(NXResponseType), (int)responseType);
            intent.PutExtra(nameof(NXLoginType), (int)loginType);

            context?.StartActivity(intent);

            return taskCompletionSource.Task;
        }

        class InnerAccountKitCallback : Java.Lang.Object, IAKAccountKitCallback
        {
            readonly TaskCompletionSource<NXLoginAccount> taskCompleteionSource;

            public InnerAccountKitCallback(TaskCompletionSource<NXLoginAccount> taskCompleteionSource)
            {
                this.taskCompleteionSource = taskCompleteionSource;
            }

            public void OnError(AKAccountKitError p0)
            {
                //TODO return exception
                var result = new NXLoginAccount(true);

                taskCompleteionSource.SetResult(result);
            }

            public void OnSuccess(Java.Lang.Object p0)
            {
                var account = p0 as AKAccount;

                var result = new NXLoginAccount(phoneNumber: account.PhoneNumber.ToString(), email: account.Email);

                taskCompleteionSource.SetResult(result);
            }
        }
    }

    [Activity(Theme = "@style/MainTheme")]
    public class HiddenAccountKitActivity : AppCompatActivity
    {
        const int APP_REQUEST_CODE = 99;
        internal static Action<IAKAccountKitLoginResult> Completed;

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var responseType = (NXResponseType)Intent.Extras.GetInt(nameof(NXResponseType));
            var loginType = (NXLoginType)Intent.Extras.GetInt(nameof(NXLoginType));

            var intent = new Intent(this, typeof(AKAccountKitActivity));
            var configurationBuilder =
                new AKAccountKitConfiguration.AccountKitConfigurationBuilder(
                    loginType == NXLoginType.Phone ? AKLoginType.Phone : AKLoginType.Email,
                    responseType == NXResponseType.AuthorizationCode ? AKAccountKitActivity.ResponseType.Code : AKAccountKitActivity.ResponseType.Token);

            intent.PutExtra(
                        AKAccountKitActivity.AccountKitActivityConfiguration,
                        configurationBuilder.Build());

            StartActivityForResult(intent, APP_REQUEST_CODE);

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            if (requestCode != APP_REQUEST_CODE)
            {
                base.OnActivityResult(requestCode, resultCode, data);

                Finish();
                return;
            }

            var result = (IAKAccountKitLoginResult)data?.GetParcelableExtra(AKAccountKitLoginResult.ResultKey);

            Completed?.Invoke(result);

            Finish();
        }
    }
}