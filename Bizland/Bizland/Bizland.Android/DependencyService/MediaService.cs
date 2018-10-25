using Android.App;
using Android.Content;
using Bizland.Core;
using Bizland.Droid.DependencyService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace Bizland.Droid.DependencyService
{
    public class MediaService : IMediaService
    {
        private readonly List<ImageSource> _pickAsyncResult;
        private readonly EventWaitHandle _waitHandle = new AutoResetEvent(false);

        public Task<List<ImageSource>> PickImageAsync()
        {
            var imageIntent = new Intent(
            Intent.ActionPick);
            imageIntent.SetType("image/*");
            imageIntent.PutExtra(Intent.ExtraAllowMultiple, true);
            imageIntent.SetAction(Intent.ActionGetContent);
            ((Activity)Forms.Context).StartActivityForResult(
                Intent.CreateChooser(imageIntent, "Select photo"), 0);

            return Task.Run(() =>
            {
                return new List<ImageSource>();
            });
        }
    }
}