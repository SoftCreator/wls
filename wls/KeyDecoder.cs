using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace wls
{
    public class KeyDecoder
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

        /// <summary>
        /// Вернёт Id раскладки.
        /// </summary>
        public static ushort GetKeyboardLayout()
        {
            return GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero));
        }

        private Dictionary<int, string> _baseKeysCode =
            new Dictionary<int, string>
                {
                    {(int)Keys.LButton, "<LMouse>"},
                    {(int)Keys.RButton, "<RMouse>"},
                    {(int)Keys.Back, "<Backspace>"},
                    {(int)Keys.Space, " "},
                    {(int)Keys.Return, "<Enter>"},
                    {(int)Keys.Delete, "<Del>"},
                    {(int)Keys.Insert, "<Ins>"},
                    {(int)Keys.Home, "<Home>"},
                    {(int)Keys.End, "<End>"},
                    {(int)Keys.Tab, "<Tab>"},
                    {(int)Keys.Prior, "<Page Up>"},
                    {(int)Keys.PageDown, "<Page Down>"},
                    {(int)Keys.Home, "<Home>"},
                    {(int)Keys.LWin, "<Win>"},
                    {(int)Keys.RWin, "<Win>"}
                };

        public string GetKey(int keyCode, bool shiftKey)
        {
            return "";
        }


    }
}
