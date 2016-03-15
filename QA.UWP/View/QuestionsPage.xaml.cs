using QA.UWP.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QA.UWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuestionsPage : Page
    {
        INavigatable vm;
        public QuestionsPage()
        {
            this.InitializeComponent();
            vm = this.DataContext as INavigatable;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            vm.NavigateFrom(e.Parameter);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            vm.NavigateTo(e.Parameter);
        }
    }
}
