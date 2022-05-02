using System;

namespace ver1
{
    public interface IDevice
    {
        enum State { on, off, standby };


        State GetState(); // zwraca aktualny stan urządzenia

        int Counter { get; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                              // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
        abstract protected void SetState(State state);
        public void PowerOn() { SetState(State.on); }
        public void PowerOff() { SetState(State.off); }
        public void StandbyOn() { SetState(State.standby); }
        public void StandbyOff() { SetState(State.on); }

    }

    public abstract class BaseDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void PowerOn()
        {
            state = IDevice.State.on;
            Console.WriteLine("Device is on ...");
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        public void Print(in IDocument document);
        public new void PowerOn() { SetState(State.on); }
        public new void PowerOff() { SetState(State.off); }
        public new void StandbyOn() { SetState(State.standby); }
        public new void StandbyOff() { SetState(State.on); }
        new State GetState();
        new void SetState(State state);

    }

    public interface IScanner : IDevice
    {
        public void Scan(out IDocument document, IDocument.FormatType formatType);
        public new void PowerOn() { SetState(State.on); }
        public new void PowerOff() { SetState(State.off); }
        public new void StandbyOn() { SetState(State.standby); }
        public new void StandbyOff() { SetState(State.on); }
        new State GetState();
        new void SetState(State state);

    }

}
