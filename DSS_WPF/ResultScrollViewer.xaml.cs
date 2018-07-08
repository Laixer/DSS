using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
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
	/// Interaction logic for ResultScrollViewer.xaml. This class is a scroll view which means
	/// that the size of the window isn't much of a concern here. This class mainly passes the right
	/// SeriesCollection objects to the CartesianCharts in the accompanying xaml file. However,
	/// it is also responsible for generating and exporting screenshots of its contents as pdf files.
	/// 
	public partial class ResultScrollViewer : System.Windows.Controls.UserControl
	{
		public ResultScrollViewer(int resultNumber)
		{
			InitializeComponent();

			ResultNumber = resultNumber;
			LogarithmicFormatter = value => Math.Pow(10, value).ToString("N", CultureInfo.InvariantCulture);
			Base = 10;
			InvertedAxisFormatter = value => (-value).ToString("N", CultureInfo.InvariantCulture);

			ShearStrainHorizontalStress = new SeriesCollection();
			NormalStressShearStress = new SeriesCollection();
			TimeAxialStrain = new SeriesCollection();
			ShearStrainNormalStressAndShearStrainPorePressure = new SeriesCollection();
			HorizontalStrainSecantGModulus = new SeriesCollection();

			DataContext = this;
		}

		private int ResultNumber;
		public Func<double, string> LogarithmicFormatter { get; set; }
		public Func<double, string> InvertedAxisFormatter { get; set; }
		public double Base { get; set; }

		public SeriesCollection ShearStrainHorizontalStress { get; }
		public SeriesCollection NormalStressShearStress { get; }
		public SeriesCollection TimeAxialStrain { get; }
		public SeriesCollection ShearStrainNormalStressAndShearStrainPorePressure { get; }
		public SeriesCollection HorizontalStrainSecantGModulus { get; }

		/// <summary>
		/// Event handler for the "Export" button in the lower right corner of the Scroll Viewer.
		/// Gets a bitmap representation of the contents of this Scroll Viewer, renders them into
		/// a context and saves the result to disk using a SaveFileDialog.
		/// </summary>
		private void Export(object sender, RoutedEventArgs e)
		{
			ExportButton.Visibility = Visibility.Hidden;

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

			ExportButton.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Shows a SaveFileDialog with a certain document and the image to add to it.
		/// </summary>
		/// <param name="document">The document to save</param>
		/// <param name="image">The image to add to the document</param>

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
