using System;

namespace ver1
{
	public class Copier : BaseDevice, IPrinter, IScanner
	{
		public int PrintCounter { get; set; } = 0;
		public int ScanCounter { get; set; } = 0;
		public new int Counter { get; set; }
		public IPrinter printer;
		public IScanner scanner;
		private IDevice.State printerState = IDevice.State.off;
		private IDevice.State scannerState = IDevice.State.off;
		public int PrintInRowCounter { get; private set; } = 0;
		public int ScanInRowCounter { get; private set; } = 0;

		public void Print(in IDocument document)
		{
			if (GetState() == IDevice.State.off)
				return;

			if (state == IDevice.State.on)
			{
				Console.WriteLine("{0} Print: {1}", DateTime.Now, document.GetFileName());
				PrintCounter++;
			}
		}

		public void PowerOn()
		{
			if (GetState() == IDevice.State.off)
				Counter++;

			base.PowerOn();
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
		public void ScanAndPrint()
		{
			Scan(out IDocument document, IDocument.FormatType.JPG);
			Print(document);
		}

		public Copier()
		{
			printer = this;
			scanner = this;
		}
		public IDevice.State GetState()
		{
			if (scanner.GetState() == printer.GetState())
			{
				return scanner.GetState();
			}
			return IDevice.State.on;
		}

		IDevice.State IPrinter.GetState()
		{
			return printerState;
		}
		IDevice.State IScanner.GetState()
		{
			return scannerState;
		}
		void IPrinter.SetState(IDevice.State state)
		{
			printerState = state;
		}

		void IScanner.SetState(IDevice.State state)
		{
			scannerState = state;
		}
		void IDevice.SetState(IDevice.State state)
		{
			printer.SetState(state);
			scanner.SetState(state);
		}
		public void CopierPowerOn()
		{
			if (GetState() == IDevice.State.off)
			{
				Counter++;
				((IDevice)this).PowerOn();
			}
		}
		public void CopierPowerOff()
		{
			if (GetState() != IDevice.State.off)
			{
				((IDevice)this).PowerOff();
			}
		}
		public void CopierStandbyOn()
		{
			if (GetState() != IDevice.State.standby)
			{
				((IDevice)this).StandbyOn();
			}
		}
		public void CopierStandbyOff()
		{
			if (GetState() == IDevice.State.standby)
			{
				((IDevice)this).StandbyOff();
			}
		}
	}

}
