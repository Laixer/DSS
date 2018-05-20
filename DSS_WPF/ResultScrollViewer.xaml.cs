using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for ResultScrollViewer.xaml
	/// </summary>
	public partial class ResultScrollViewer : System.Windows.Controls.UserControl
	{
		public ResultScrollViewer()
		{
			InitializeComponent();
		}

		private void Export(object sender, RoutedEventArgs e)
		{
			RenderTargetBitmap renderTargetBitmap =
			new RenderTargetBitmap((int)(this.Width * (300.0 / 96)), (int)(this.Height * (300.0 / 96)), 300, 300, PixelFormats.Pbgra32);
			renderTargetBitmap.Render(this);
			PngBitmapEncoder pngImage = new PngBitmapEncoder();

			pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
			String filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\file.png";
			using (Stream fileStream = File.Create(filePath))
			{
				pngImage.Save(fileStream);
			}

			Uri uri = new Uri(filePath);
			Image image = Image.GetInstance(uri);
			Debug.WriteLine("width " + image.Width + " height " + image.Height);

			Document doc = new Document();
			Rectangle pageSize = new Rectangle((float)renderTargetBitmap.Width, (float)renderTargetBitmap.Height);
			doc.SetPageSize(pageSize);
			PdfWriter.GetInstance(doc, new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\file.pdf", FileMode.Create));
			doc.Open();
			image.SetAbsolutePosition(0, 0);

			doc.Add(image);
			doc.Close();
		}
	}
}
