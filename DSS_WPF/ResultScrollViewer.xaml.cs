using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using LiveCharts;
using System.Globalization;
using Microsoft.Win32;

namespace DSS_WPF
{
	/// <summary>
	/// Interaction logic for ResultScrollViewer.xaml
	/// </summary>
	public partial class ResultScrollViewer : System.Windows.Controls.UserControl
	{
		public ResultScrollViewer(int resultNumber)
		{
			InitializeComponent();

			ResultNumber = resultNumber;
			Formatter = value => Math.Pow(10, value).ToString("N", CultureInfo.CreateSpecificCulture("nl"));
			Base = 10;

			DataContext = this;
		}

		private int ResultNumber;
		public Func<double, string> Formatter { get; set; }
		public double Base { get; set; }

		public SeriesCollection ShearStrainHorizontalStress { get; set; }
		public SeriesCollection NormalStressShearStress { get; set; }
		public SeriesCollection TimeAxialStrain { get; set; }
		public SeriesCollection ShearStrainNormalStressAndShearStrainPorePressure { get; set; }
		public SeriesCollection HorizontalStrainSecantGModulus { get; set; }

		private void Export(object sender, RoutedEventArgs e)
		{
			System.Windows.Controls.ScrollViewer scrollViewer = (System.Windows.Controls.ScrollViewer)this.Content;
			System.Windows.Controls.Grid content = (System.Windows.Controls.Grid)scrollViewer.Content;
			double actualHeight = content.RenderSize.Height;
			double actualWidth = content.RenderSize.Width;
			double zoom = 4.0;

			double renderHeight = actualHeight * zoom;
			double renderWidth = actualWidth * zoom;

			RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96.0, 96.0, PixelFormats.Pbgra32);
			VisualBrush sourceBrush = new VisualBrush(content);

			DrawingVisual drawingVisual = new DrawingVisual();
			DrawingContext drawingContext = drawingVisual.RenderOpen();

			using (drawingContext)
			{
				drawingContext.PushTransform(new ScaleTransform(zoom, zoom));
				drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
			}
			renderTarget.Render(drawingVisual);

			PngBitmapEncoder encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(renderTarget));
			MemoryStream fs = new MemoryStream();
			encoder.Save(fs);

			Image image = Image.GetInstance(fs.ToArray()); // this is an iTextSharp image, not a wpf image or something
			
			Document doc = new Document();
			Rectangle pageSize = new Rectangle((float)renderTarget.Width, (float)renderTarget.Height);
			doc.SetPageSize(pageSize);

			showSaveFileDialog(doc, image);
		}

		private void showSaveFileDialog(Document document, Image image)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.OverwritePrompt = true;
			dialog.FileName = "Proefstuk " + ResultNumber;
			dialog.AddExtension = true;
			dialog.Filter = "PDF file (*.pdf)|*.pdf";
			if (dialog.ShowDialog() == true)
			{
				FileStream stream = new FileStream(dialog.FileName, FileMode.Create);
				PdfWriter.GetInstance(document, stream);
				document.Open();
				image.SetAbsolutePosition(0, 0);

				document.Add(image);
				document.Close();
			}
			else
			{
				return;
			}
		}
	}
}
