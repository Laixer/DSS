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

namespace Dss
{

	/// <summary>
	/// Interaction logic for ResultScrollViewer.xaml
	/// </summary>
	public partial class ResultScrollViewer : System.Windows.Controls.UserControl
	{
		private const double zoomFactor = 4.0;

		public ResultScrollViewer(int resultNumber)
		{
			InitializeComponent();

			ResultNumber = resultNumber;
			Formatter = value => Math.Pow(10, value).ToString("N", CultureInfo.CreateSpecificCulture("nl"));
			Base = 10;

			ShearStrainHorizontalStress = new SeriesCollection();
			NormalStressShearStress = new SeriesCollection();
			TimeAxialStrain = new SeriesCollection();
			ShearStrainNormalStressAndShearStrainPorePressure = new SeriesCollection();
			HorizontalStrainSecantGModulus = new SeriesCollection();


			DataContext = this;
		}

		private int ResultNumber;
		public Func<double, string> Formatter { get; set; }
		public double Base { get; set; }

		public SeriesCollection ShearStrainHorizontalStress { get; }
		public SeriesCollection NormalStressShearStress { get; }
		public SeriesCollection TimeAxialStrain { get; }
		public SeriesCollection ShearStrainNormalStressAndShearStrainPorePressure { get; }
		public SeriesCollection HorizontalStrainSecantGModulus { get; }

		private void Export(object sender, RoutedEventArgs e)
		{
			ExportButton.Visibility = Visibility.Hidden;

			System.Windows.Controls.ScrollViewer scrollViewer = (System.Windows.Controls.ScrollViewer)this.Content;
			System.Windows.Controls.Grid content = (System.Windows.Controls.Grid)scrollViewer.Content;
			RenderTargetBitmap renderTarget = GetBitmap(content, zoomFactor);
			VisualBrush sourceBrush = new VisualBrush(content);

			DrawingVisual drawingVisual = new DrawingVisual();
			DrawingContext drawingContext = drawingVisual.RenderOpen();

			using (drawingContext)
			{
				drawingContext.PushTransform(new ScaleTransform(zoomFactor, zoomFactor));
				drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(content.RenderSize.Height, content.RenderSize.Width)));
			}
			renderTarget.Render(drawingVisual);

			SaveBitmap(renderTarget);
		}

		private static RenderTargetBitmap GetBitmap(UIElement content, double zoomFactor)
		{
			double actualHeight = content.RenderSize.Height;
			double actualWidth = content.RenderSize.Width;

			double renderHeight = actualHeight * zoomFactor;
			double renderWidth = actualWidth * zoomFactor;

			return new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96.0, 96.0, PixelFormats.Pbgra32);
		}

		private void SaveBitmap(RenderTargetBitmap renderTarget)
		{
			PngBitmapEncoder encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(renderTarget));
			using (MemoryStream fs = new MemoryStream())
			{
				encoder.Save(fs);
				Image image = Image.GetInstance(fs.ToArray()); // this is an iTextSharp image, not a wpf image or something

				using (Document doc = new Document())
				{
					Rectangle pageSize = new Rectangle((float)renderTarget.Width, (float)renderTarget.Height);
					doc.SetPageSize(pageSize);

					ShowSaveFileDialog(doc, image);
				}
			}
		}

		private void ShowSaveFileDialog(Document document, Image image)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				OverwritePrompt = true,
				FileName = "Proefstuk " + ResultNumber,
				AddExtension = true,
				Filter = "PDF file (*.pdf)|*.pdf"
			};
			if (dialog.ShowDialog() == true)
			{
				using (FileStream stream = new FileStream(dialog.FileName, FileMode.Create))
				{
					PdfWriter.GetInstance(document, stream);
					document.Open();
					image.SetAbsolutePosition(0, 0);

					document.Add(image);
					document.Close();
				}	
			}
			else
			{
				return;
			}
		}
	}
}
