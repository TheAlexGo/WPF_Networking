using System.Windows;
using System.Net;

namespace DNS {
	public partial class MainWindow : Window{
		const string newLine = "\r\n";

		public MainWindow() => InitializeComponent();

		private void dnsThrough(object sender, RoutedEventArgs e){
			dnsOut.Text = $"IP-адреса домена {dnsIn.Text}:";
			IPHostEntry ipHostEntry = Dns.GetHostEntry(dnsIn.Text);
			foreach(IPAddress ipAddress in ipHostEntry.AddressList)
				dnsOut.AppendText(newLine + ipAddress.ToString());
			dnsOut.AppendText(newLine + "Альтернативные имена домена:");
			foreach(string alias in ipHostEntry.Aliases)
				dnsOut.AppendText(newLine + alias);
			dnsOut.AppendText($"{newLine}Имя домена: {ipHostEntry.HostName}");
		}
	}
}