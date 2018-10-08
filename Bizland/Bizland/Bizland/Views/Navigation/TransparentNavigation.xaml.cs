using Xamarin.Forms;

namespace Bizland.Views
{
    public partial class TransparentNavigation : NavigationPage
    {
        public TransparentNavigation() : base()
        {
            InitializeComponent();
        }

        public TransparentNavigation(Page root) : base(root)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}
