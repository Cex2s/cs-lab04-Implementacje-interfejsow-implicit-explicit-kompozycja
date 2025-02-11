﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
	public class Scanner : IScanner
	{
		public int ScanCounter { get; set; } = 0;
		public new int Counter { get; set; } = 0;

		public IDevice.State state = IDevice.State.off;
		public IDevice.State GetState()
		{
			return state;
		}


		public void PowerOn()
		{
			state = IDevice.State.on;
			Counter++;
		}
		public void PowerOff()
		{
			state = IDevice.State.off;
		}


		public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
		{
			string type = "";


			switch (formatType)
			{
				case IDocument.FormatType.TXT:
					type = "Text";
					break;
				case IDocument.FormatType.PDF:
					type = "PDF";
					break;
				case IDocument.FormatType.JPG:
					type = "Image";
					break;
				default:
					throw new Exception("Invalid type!");

			}

			string nameDocument = string.Format($"{type}{ScanCounter + 1}.{formatType.ToString().ToLower()}");

			if (formatType == IDocument.FormatType.PDF)
			{
				document = new PDFDocument(nameDocument);
			}
			else if (formatType == IDocument.FormatType.TXT)
			{
				document = new TextDocument(nameDocument);
			}
			else
			{
				document = new ImageDocument(nameDocument);
			}

			if (state == IDevice.State.on)
			{
				ScanCounter++;
				Console.WriteLine("{0}, Scan: {1}", DateTime.Now, document.GetFileName());
			}


		}

	}
}
