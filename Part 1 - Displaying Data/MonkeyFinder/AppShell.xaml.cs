namespace MonkeyFinder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// nameof(DetailPage == "DetailsPage"
		Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
	}
}