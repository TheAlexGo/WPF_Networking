using System.Windows;
using System.Net;
using System.IO;

namespace WebRequestMain
{
	public partial class MainWindow : Window{
		const string newLine = "\r\n";

		public MainWindow() => InitializeComponent();

		void RequestToUri(object sender, RoutedEventArgs e){
			if (uriIn.Text == "")
			{
				MessageBox.Show("Укажите ссылку на сайт, с которого хотите получить информацию", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			else
            {
				try
				{
					WebRequest webRequest = WebRequest.Create(uriIn.Text);
					WebResponse webResponse = webRequest.GetResponse();
					using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
					{
						string readLine;
						if ((readLine = streamReader.ReadLine()) != null)
							contentOut.Text = readLine;
						while ((readLine = streamReader.ReadLine()) != null)
							contentOut.AppendText(newLine + readLine);
					}
					descriptionOut.Text = $"URI запроса: {webRequest.RequestUri}{newLine}Метод запроса: {webRequest.Method}{newLine}Тип данных ответа: {webResponse.ContentType}{newLine}Длина ответа: {webResponse.ContentLength}{newLine}Заголовки:";
					WebHeaderCollection webHeaderCollection = webResponse.Headers;
					foreach (string key in webHeaderCollection.AllKeys)
					{
						descriptionOut.AppendText($"{newLine}{key}:");
						foreach (string value in webHeaderCollection.GetValues(key))
							descriptionOut.AppendText($" {value}");
					}
				}
				catch
				{
					MessageBox.Show("Ссылка не действительна", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		void ReadFile(object sender, RoutedEventArgs e){
			if (fileUriIn.Text == "")
			{
				MessageBox.Show("Укажите ссылку на сайт, с которого хотите получить информацию", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			else
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(((FileWebRequest)WebRequest.Create(fileUriIn.Text)).GetResponse().GetResponseStream()))
						fileContentOut.Text = streamReader.ReadToEnd();
				}
				catch
				{
					MessageBox.Show("Ссылка не действительна", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		void WriteFile(object sender, RoutedEventArgs e){
			if (fileUriIn.Text == "")
			{
				MessageBox.Show("Укажите ссылку на сайт, с которого хотите получить информацию", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
			} 
			else
            {
				try
				{
					FileWebRequest fileWebRequest = (FileWebRequest)WebRequest.Create(fileUriIn.Text);
					fileWebRequest.Method = "PUT";
					using (StreamWriter streamWriter = new StreamWriter(fileWebRequest.GetRequestStream()))
						streamWriter.Write(fileContentOut.Text);
				}
				catch
				{
					MessageBox.Show("Ссылка не действительна", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			
		}
	}
}