using System;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();
            IDocument doc = new PDFDocument("aaaa.pdf");
            device.Print(in doc);

            IDocument doc2;
            device.Scan(out doc2);
            device.ScanAndPrint();

            device.sendFax(doc, "352352");
            device.ScanAndSend("352352");

            Console.WriteLine(device.Counter);
            Console.WriteLine(device.FaxCounter);
            Console.WriteLine(device.PrintCounter);
            Console.WriteLine(device.ScanCounter);
        }
    }
}
