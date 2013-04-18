using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace wls
{
    public static class ToggleKeys
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        public static bool Ctrl
        {
            get { return Convert.ToBoolean(GetAsyncKeyState(Keys.ControlKey) & 0x8000); }
        } // ControlKey
        public static bool Shift
        {
            get { return Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey) & 0x8000); }
        } // ShiftKey
        public static bool CapsLock
        {
            get { return Convert.ToBoolean(GetAsyncKeyState(Keys.CapsLock) & 0x8000); }
        } // CapsLock

        public static bool Alt
        {
            get { return Convert.ToBoolean(GetAsyncKeyState(Keys.Menu) & 0x8000); }
        } // AltKey
    }
}
