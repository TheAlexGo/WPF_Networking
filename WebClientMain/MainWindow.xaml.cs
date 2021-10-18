using System.Windows;
using System.Net;
using System.IO;

namespace WebClientMain {
	public partial class MainWindow : Window{
		const string newLine = "\r\n";

		public MainWindow() => InitializeComponent();

		void ContentFromUri(object sender, RoutedEventArgs e) {
			if (uriIn.Text == "")
			{
				MessageBox.Show("Укажите ссылку на сайт, с которого хотите получить информацию", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			else
			{
				try
                {
					using (StreamReader streamReader = new StreamReader(new WebClient().OpenRead(uriIn.Text)))
					{
						string readLine;
						if ((readLine = streamReader.ReadLine()) != null)
							contentOut.Text = readLine;
						while ((readLine = streamReader.ReadLine()) != null)
							contentOut.AppendText(newLine + readLine);
					}
				}
				catch
                {
					MessageBox.Show("Ссылка не действительна", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
	}
}