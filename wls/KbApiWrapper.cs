using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace wls
{
    /// <summary>
    /// Class which incapsulate all API calls
    /// </summary>
    public static class KbApiWrapper
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(
            [In] IntPtr hWnd,
            [Out, Optional] IntPtr lpdwProcessId
            );

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern ushort GetKeyboardLayout(
            [In] int idThread
            );

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Int32 vKey);

        [DllImport("User32.dll")]
        public static extern int GetWindowText(int hwnd, StringBuilder s, int nMaxCount);


        /// <summary>
        /// get keyboard layout id (codepage)
        /// </summary>
        public static ushort GetKeyboardLayout()
        {
            return GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero));
        }

        public static string GetAppTitle()
        {
            int hwnd = GetForegroundWindow().ToInt32();
            var sbTitle = new StringBuilder(1024);
            int intLength = GetWindowText(hwnd, sbTitle, sbTitle.Capacity);
            if ((intLength <= 0) || (intLength > sbTitle.Length)) return "unknown";
            string title = sbTitle.ToString();
            return title;
        }

        public static List<int> KeysPressed()
        {
            var result = new List<int>(10);
            result.AddRange(Enum.GetValues(typeof(Keys)).Cast<int>().Where(i => GetAsyncKeyState(i) == -32767));
            return result;
        }


        public static class ToggleKeys
        {
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
}
