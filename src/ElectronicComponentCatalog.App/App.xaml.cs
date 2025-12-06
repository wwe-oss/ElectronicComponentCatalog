using Microsoft.Maui.Controls;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App
{
	/// <summary>
	/// Main application entry point.
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new ContentPage
			{
				Content = new Label
				{
					Text = "Electronic Component Catalog — App initialized successfully.",
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions = LayoutOptions.Center,
					TextColor = Colors.White
				},
				BackgroundColor = Colors.Black
			};
		}
	}
}
