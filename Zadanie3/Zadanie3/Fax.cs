using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public class Fax : IFax
    {
        public int Counter { get; private set; } = 0;
        protected IDevice.State state = IDevice.State.off;
        public int SendFaxCounter { get; private set; } = 0;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
        }

        public void PowerOn()
        {
            state = IDevice.State.on;
        }
        public void sendFax(IDocument document, string faxNumber)
        {
            if (state == IDevice.State.on)
            {
                SendFaxCounter++;
                Console.WriteLine($"{DateTime.Today} Sent: {document.GetFileName()} Fax: {document.GetFileName()} to: {faxNumber}");
            }
        }
    }
}
