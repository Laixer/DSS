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
	/// Interaction logic for ResultScrollViewer.xaml. This class is a scroll view which means
	/// that the size of the window isn't much of a concern here. This class mainly passes the right
	/// SeriesCollection objects to the CartesianCharts in the accompanying xaml file. However,
	/// it is also responsible for generating and exporting screenshots of its contents as pdf files.
	/// 
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

		/// <summary>
		/// Gets a bitmap represention a UIElement.
		/// </summary>
		/// <param name="content">The UIElement to get a bitmap representation of</param>
		/// <param name="zoomFactor">The zoom (scaling) factor to render the content with</param>
		/// <returns>The bitmap object set up with the right size.</returns>
		private static RenderTargetBitmap GetBitmap(UIElement content, double zoomFactor)
		{
			double actualHeight = content.RenderSize.Height;
			double actualWidth = content.RenderSize.Width;

			double renderHeight = actualHeight * zoomFactor;
			double renderWidth = actualWidth * zoomFactor;

			return new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96.0, 96.0, PixelFormats.Pbgra32);
		}

		/// <summary>
		/// Since we can't save a bitmap directly as a pdf file,
		/// we need to first encode it as a png file, then
		/// convert that to a pdf file. Then, we open a save file dialog 
		/// to actually save the new dialog.
		/// </summary>
		/// <param name="renderTarget">The bitmap with the contents of the Scroll Viewer</param>
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
