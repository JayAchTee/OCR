using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

namespace OCR
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				OCR myApp = new OCR(args);

				string result = null;

				var task = Task.Run(async () =>
				{
					result = await myApp.Run();
				});

				task.Wait();

				if (result == null)
				{
					Environment.ExitCode = 1;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Unhandled exception in {0}: {1}", System.IO.Path.GetFileName(Environment.GetCommandLineArgs()[0]), e);

				Environment.ExitCode = 2;
			}
		}
	}

	class OCR
	{
		private string[] arguments = null;

		public OCR(string[] Arguments)
		{
			this.arguments = Arguments;
		}

		public async Task<string> Run()
		{
			foreach (string argument in arguments)
			{
				if (!System.IO.File.Exists(argument))
				{
					continue;
				}

				System.IO.FileInfo info = new System.IO.FileInfo(argument);

				if (!info.Exists)
				{
					continue;
				}

				StorageFile file = await StorageFile.GetFileFromPathAsync(info.FullName);

				IRandomAccessStreamWithContentType stream = await file.OpenReadAsync();

				var decoder = await BitmapDecoder.CreateAsync(stream);

				SoftwareBitmap image = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Rgba8, BitmapAlphaMode.Premultiplied);

				OcrEngine engine = OcrEngine.TryCreateFromUserProfileLanguages();

				OcrResult result = await engine.RecognizeAsync(image);

				Console.WriteLine(result.Text);

				return (result.Text);
			}

			return (null);
		}
	}
}
