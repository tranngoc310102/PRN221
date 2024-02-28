using System.Diagnostics;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;

namespace DemoAsynschonus3
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		private readonly HttpClient client = new HttpClient
		{
			MaxResponseContentBufferSize = 1_000_000
		};


		private List<string> UrlList = new List<string>();
		private void getUrl()
		{
			string url = txtUrl.Text;
			UrlList.Clear();
			UrlList.Add(url);
		}
		private async void OnStartButtonClick(object sender, RoutedEventArgs e)
		{

			btnStartButton.IsEnabled = false;
			getUrl();
			txtResults.Clear();
			await SumPageSizesAsync();
			txtResults.Text += $"\nControl returned to {nameof(OnStartButtonClick)}.";
			btnStartButton.IsEnabled = true;
		}

		private async Task SumPageSizesAsync()
		{
			var stopwatch = Stopwatch.StartNew();
			int total = 0;
			foreach (string url in UrlList)
			{
				int contentLength = await ProcessUrlAsync(url, client);
				total += contentLength;
			}
			stopwatch.Stop();
			txtResults.Text += $"\nTotal bytes returned: {total:#,#}";
			txtResults.Text += $"\nElapsed time:      {stopwatch.Elapsed}\n";
		}
		//-
		private async Task<int> ProcessUrlAsync(string url, HttpClient client)
		{
			byte[] content = await client.GetByteArrayAsync(url);
			DisplayResults(url, content);
			return content.Length;
		}

		private void DisplayResults(string url, byte[] content) =>
		txtResults.Text += $"{url,-60} {content.Length,10:#,#}\n";
		protected override void OnClosed(EventArgs e) => client.Dispose();
	}//end class
}