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

			Formatter = value => Math.Pow(10, value).ToString("N", CultureInfo.CreateSpecificCulture("nl"));
			Base = 10;

			DataContext = this;
		}

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
			PdfWriter.GetInstance(doc, new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\file.pdf", FileMode.Create));
			doc.Open();
			image.SetAbsolutePosition(0, 0);

			doc.Add(image);
			doc.Close();
		}
	}
}
