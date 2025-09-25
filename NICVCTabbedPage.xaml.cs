using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace NICVC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NICVCTabbedPage : Microsoft.Maui.Controls.TabbedPage
    {
        public NICVCTabbedPage()
        {
            InitializeComponent();
            Lbl_Header.Text = App.GetLabelByKey("nicvdconf");        

            On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            Children.Add(new Dashboard_Page());
            Children.Add(new ReserveNicDashboardPage());
            Children.Add(new FeedbackPage());
            Children.Add(new AboutUsPage());          
            CurrentPage = Children[App.CurrentTabpageIndex];

        }

    }
}