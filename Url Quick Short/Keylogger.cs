/*
 *	ArticleKeyLog - Basic Keystroke Mining
 *
 *	Date:	05/12/2005
 *
 *	Author:	Alexander Kent
 *
 *	Description:	Sample Application for the Code Project (www.codeproject.com)
 */

using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Url_Quick_Short
{
    /// <summary>
    /// Summary description for Keylogger
    /// 
    /// Timer Intervals are in ms, examples:
    ///	60000ms = 1 minute
    ///	1800000ms = 30 minutes
    /// 36000000ms = 60 minutes
    /// 
    /// </summary>
    /// 	
    public class Keylogger
    {
        /// <summary>
        /// The GetAsyncKeyState function determines whether a key is up or down at the time 
        /// the function is called, and whether the key was pressed after a previous call 
        /// to GetAsyncKeyState.
        /// </summary>
        /// <param name="vKey">Specifies one of 256 possible virtual-key codes. </param>
        /// <returns>If the function succeeds, the return value specifies whether the key 
        /// was pressed since the last call to GetAsyncKeyState, and whether the key is 
        /// currently up or down. If the most significant bit is set, the key is down, 
        /// and if the least significant bit is set, the key was pressed after 
        /// the previous call to GetAsyncKeyState. </returns>
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(
            System.Windows.Forms.Keys vKey); // Keys enumeration

        [DllImport("user32.dll")]
        static extern short GetKeyState(VirtualKeyStates nVirtKey);


        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(
            System.Int32 vKey);

        private System.String keyBuffer;
        private System.Timers.Timer timerKeyMine;
        private System.Timers.Timer timerBufferFlush;
        private string keydumpPath;
        private bool saveToFile;

        public Keylogger(string keydumpPath, bool saveToFile = true)
        {
            this.keydumpPath = keydumpPath;
            this.saveToFile = saveToFile;

            //
            // keyBuffer
            //
            keyBuffer = "";

            // 
            // timerKeyMine
            // 
            this.timerKeyMine = new System.Timers.Timer();
            this.timerKeyMine.Enabled = true;
            this.timerKeyMine.Elapsed += new System.Timers.ElapsedEventHandler(this.timerKeyMine_Elapsed);
            this.timerKeyMine.Interval = 10;

            // 
            // timerBufferFlush
            //
            this.timerBufferFlush = new System.Timers.Timer();
            this.timerBufferFlush.Enabled = true;
            this.timerBufferFlush.Elapsed += new System.Timers.ElapsedEventHandler(this.timerBufferFlush_Elapsed);
            this.timerBufferFlush.Interval = 60000; // 1 minute
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        public static byte code(Keys key)
        {
            return (byte)((int)key & 0xFF);
        }

        public static bool IsKeyPressed(VirtualKeyStates testKey)
        {
            bool keyPressed = false;
            short result = GetKeyState(testKey);

            switch (result)
            {
                case 0:
                    // Not pressed and not toggled on.
                    keyPressed = false;
                    break;

                case 1:
                    // Not pressed, but toggled on
                    keyPressed = false;
                    break;

                default:
                    // Pressed (and may be toggled on)
                    keyPressed = true;
                    break;
            }

            return keyPressed;
        }

        private static void Invoke(Action action)
        {
            frmMain.Instance.Invoke(action);
        }

        /// <summary>
        /// Itrerating thru the entire Keys enumeration; downed key names are stored in keyBuffer 
        /// (space delimited).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerKeyMine_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (int i in Enum.GetValues(typeof(KeysWeCareAbout)))
            {
                if (IsKeyBeingPressed(i))
                {
                    string key = Enum.GetName(typeof(KeysWeCareAbout), i);

                    keyBuffer += key + " ";

                    if (keyBuffer.EndsWith(frmMain.Instance.SystemSettings.TriggerKey + " "))
                    {
                        bool isShiftBeingPressed = IsKeyPressed(VirtualKeyStates.VK_SHIFT);
                        bool isCtrlBeingPressed = IsKeyPressed(VirtualKeyStates.VK_CONTROL);

                        Debug.WriteLine($"Shift: {isShiftBeingPressed}, Ctrl: {isCtrlBeingPressed}");

                        if (frmMain.Instance.SystemSettings.TriggerUseShift && !isShiftBeingPressed)
                        {
                            break;
                        }
                        if (frmMain.Instance.SystemSettings.TriggerUseCtrl && !isCtrlBeingPressed)
                        {
                            break;
                        }
                        //if (frmMain.SystemSettings.TriggerUseAlt && IsKeyPressed(VirtualKeyStates.VK_ALT))
                        //{
                        //    break;
                        //}

                        Task.Run(ProcessTriggerKeyFound);
                        break;
                    }
                }
            }
        }

        private async Task ProcessTriggerKeyFound()
        {
            try
            {
                string value = string.Empty;

                Invoke(() =>
                {
                    value = Clipboard.GetText();
                });

                List<string> urlsFound = new List<string>();
                foreach (Match m in Regex.Matches(value, @"((mailto\:|(news|(ht|f)tp(s?))\://){1}\S+)"))
                {
                    if (m.Success)
                    {
                        urlsFound.Add(m.Value);
                    }
                }

                if (urlsFound.Count > 0)
                {
                    urlsFound = urlsFound.Distinct().ToList();

                    Invoke(() =>
                    {
                        frmMain.Instance.notifyIcon1.ShowBalloonTip(10000, "Url Quick Short", $"{urlsFound.Count} urls found!", ToolTipIcon.Info);
                    });
                    
                    foreach (var url in urlsFound)
                    {
                        string shortUri = await frmMain.Instance.SystemSettings.CurrentProvider.ShortenUrl(url, frmMain.Instance.SystemSettings.GetAuthenticationData());
                        value = value.Replace(url, shortUri);
                    }
                    Invoke(() =>
                    {
                        Clipboard.SetText(value);
                    });
                }
            }
            catch (Exception ex)
            {
                Invoke(() =>
                {
                    frmMain.Instance.notifyIcon1.ShowBalloonTip(10000, "Url Quick Short", ex.Message, ToolTipIcon.Error);
                });
            }
        }

        private bool CheckForOemKey(string key, string checkFor, string ifShiftDown, string ifNotShiftDown)
        {
            if (key == checkFor)
            {
                if (IsKeyPressed(VirtualKeyStates.VK_SHIFT))
                {
                    keyBuffer += ifShiftDown + " ";
                }
                else
                {
                    keyBuffer += ifNotShiftDown + " ";
                }
                return true;
            }
            return false;
        }

        private static bool IsKeyBeingPressed(int i)
        {
            return GetAsyncKeyState(i) == -32767;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBufferFlush_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Flush2File();
        }


        /// <summary>
        /// Transfers key stroke data from temporary buffer storage to permanent memory. 
        /// If no exception gets thrown the key stroke buffer resets.
        /// </summary>
        /// <param name="file">The complete file path to write to.</param>
        /// <param name="append">Determines whether data is to be appended to the file. 
        /// If the files exists and append is false, the file is overwritten. 
        /// If the file exists and append is true, the data is appended to the file. 
        /// Otherwise, a new file is created.</param>
        public void Flush2File(bool forced = false)
        {
            try
            {
                if (keyBuffer.Length > 1000 || forced)
                {
                    int length = Math.Min(keyBuffer.Length, 1000);
                    string write = keyBuffer;
                    if (write.Length > length)
                    {
                        write = write.Remove(length);
                    }
                    keyBuffer = keyBuffer.Remove(0, length);
                    if (this.saveToFile && frmMain.Instance.SystemSettings.LogKeysDebug)
                    {
                        StreamWriter sw = new StreamWriter(this.keydumpPath, true);
                        sw.Write(write);
                        sw.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #region Properties
        public bool Enabled
        {
            get
            {
                return timerKeyMine.Enabled && timerBufferFlush.Enabled;
            }
            set
            {
                timerKeyMine.Enabled = timerBufferFlush.Enabled = value;
            }
        }

        public double FlushInterval
        {
            get
            {
                return timerBufferFlush.Interval;
            }
            set
            {
                timerBufferFlush.Interval = value;
            }
        }

        public double MineInterval
        {
            get
            {
                return timerKeyMine.Interval;
            }
            set
            {
                timerKeyMine.Interval = value;
            }
        }
        #endregion

        public enum VirtualKeyStates : int
        {
            VK_LBUTTON = 0x01,
            VK_RBUTTON = 0x02,
            VK_CANCEL = 0x03,
            VK_MBUTTON = 0x04,
            //
            VK_XBUTTON1 = 0x05,
            VK_XBUTTON2 = 0x06,
            //
            VK_BACK = 0x08,
            VK_TAB = 0x09,
            //
            VK_CLEAR = 0x0C,
            VK_RETURN = 0x0D,
            //
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_PAUSE = 0x13,
            VK_CAPITAL = 0x14,
            //
            VK_KANA = 0x15,
            VK_HANGEUL = 0x15, /* old name - should be here for compatibility */
            VK_HANGUL = 0x15,
            VK_JUNJA = 0x17,
            VK_FINAL = 0x18,
            VK_HANJA = 0x19,
            VK_KANJI = 0x19,
            //
            VK_ESCAPE = 0x1B,
            //
            VK_CONVERT = 0x1C,
            VK_NONCONVERT = 0x1D,
            VK_ACCEPT = 0x1E,
            VK_MODECHANGE = 0x1F,
            //
            VK_SPACE = 0x20,
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_END = 0x23,
            VK_HOME = 0x24,
            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,
            VK_SELECT = 0x29,
            VK_PRINT = 0x2A,
            VK_EXECUTE = 0x2B,
            VK_SNAPSHOT = 0x2C,
            VK_INSERT = 0x2D,
            VK_DELETE = 0x2E,
            VK_HELP = 0x2F,
            //
            VK_LWIN = 0x5B,
            VK_RWIN = 0x5C,
            VK_APPS = 0x5D,
            //
            VK_SLEEP = 0x5F,
            //
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 0x64,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_MULTIPLY = 0x6A,
            VK_ADD = 0x6B,
            VK_SEPARATOR = 0x6C,
            VK_SUBTRACT = 0x6D,
            VK_DECIMAL = 0x6E,
            VK_DIVIDE = 0x6F,
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7A,
            VK_F12 = 0x7B,
            VK_F13 = 0x7C,
            VK_F14 = 0x7D,
            VK_F15 = 0x7E,
            VK_F16 = 0x7F,
            VK_F17 = 0x80,
            VK_F18 = 0x81,
            VK_F19 = 0x82,
            VK_F20 = 0x83,
            VK_F21 = 0x84,
            VK_F22 = 0x85,
            VK_F23 = 0x86,
            VK_F24 = 0x87,
            //
            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            //
            VK_OEM_NEC_EQUAL = 0x92, // '=' key on numpad
                                     //
            VK_OEM_FJ_JISHO = 0x92, // 'Dictionary' key
            VK_OEM_FJ_MASSHOU = 0x93, // 'Unregister word' key
            VK_OEM_FJ_TOUROKU = 0x94, // 'Register word' key
            VK_OEM_FJ_LOYA = 0x95, // 'Left OYAYUBI' key
            VK_OEM_FJ_ROYA = 0x96, // 'Right OYAYUBI' key
                                   //
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,
            VK_LCONTROL = 0xA2,
            VK_RCONTROL = 0xA3,
            VK_LMENU = 0xA4,
            VK_RMENU = 0xA5,
            //
            VK_BROWSER_BACK = 0xA6,
            VK_BROWSER_FORWARD = 0xA7,
            VK_BROWSER_REFRESH = 0xA8,
            VK_BROWSER_STOP = 0xA9,
            VK_BROWSER_SEARCH = 0xAA,
            VK_BROWSER_FAVORITES = 0xAB,
            VK_BROWSER_HOME = 0xAC,
            //
            VK_VOLUME_MUTE = 0xAD,
            VK_VOLUME_DOWN = 0xAE,
            VK_VOLUME_UP = 0xAF,
            VK_MEDIA_NEXT_TRACK = 0xB0,
            VK_MEDIA_PREV_TRACK = 0xB1,
            VK_MEDIA_STOP = 0xB2,
            VK_MEDIA_PLAY_PAUSE = 0xB3,
            VK_LAUNCH_MAIL = 0xB4,
            VK_LAUNCH_MEDIA_SELECT = 0xB5,
            VK_LAUNCH_APP1 = 0xB6,
            VK_LAUNCH_APP2 = 0xB7,
            //
            VK_OEM_1 = 0xBA, // ';:' for US
            VK_OEM_PLUS = 0xBB, // '+' any country
            VK_OEM_COMMA = 0xBC, // ',' any country
            VK_OEM_MINUS = 0xBD, // '-' any country
            VK_OEM_PERIOD = 0xBE, // '.' any country
            VK_OEM_2 = 0xBF, // '/?' for US
            VK_OEM_3 = 0xC0, // '`~' for US
                             //
            VK_OEM_4 = 0xDB, // '[{' for US
            VK_OEM_5 = 0xDC, // '\|' for US
            VK_OEM_6 = 0xDD, // ']}' for US
            VK_OEM_7 = 0xDE, // ''"' for US
            VK_OEM_8 = 0xDF,
            //
            VK_OEM_AX = 0xE1, // 'AX' key on Japanese AX kbd
            VK_OEM_102 = 0xE2, // "<>" or "\|" on RT 102-key kbd.
            VK_ICO_HELP = 0xE3, // Help key on ICO
            VK_ICO_00 = 0xE4, // 00 key on ICO
                              //
            VK_PROCESSKEY = 0xE5,
            //
            VK_ICO_CLEAR = 0xE6,
            //
            VK_PACKET = 0xE7,
            //
            VK_OEM_RESET = 0xE9,
            VK_OEM_JUMP = 0xEA,
            VK_OEM_PA1 = 0xEB,
            VK_OEM_PA2 = 0xEC,
            VK_OEM_PA3 = 0xED,
            VK_OEM_WSCTRL = 0xEE,
            VK_OEM_CUSEL = 0xEF,
            VK_OEM_ATTN = 0xF0,
            VK_OEM_FINISH = 0xF1,
            VK_OEM_COPY = 0xF2,
            VK_OEM_AUTO = 0xF3,
            VK_OEM_ENLW = 0xF4,
            VK_OEM_BACKTAB = 0xF5,
            //
            VK_ATTN = 0xF6,
            VK_CRSEL = 0xF7,
            VK_EXSEL = 0xF8,
            VK_EREOF = 0xF9,
            VK_PLAY = 0xFA,
            VK_ZOOM = 0xFB,
            VK_NONAME = 0xFC,
            VK_PA1 = 0xFD,
            VK_OEM_CLEAR = 0xFE
        }

        public enum KeysWeCareAbout
        {
            //
            // Summary:
            //     The 0 key.
            D0 = 48,
            //
            // Summary:
            //     The 1 key.
            D1 = 49,
            //
            // Summary:
            //     The 2 key.
            D2 = 50,
            //
            // Summary:
            //     The 3 key.
            D3 = 51,
            //
            // Summary:
            //     The 4 key.
            D4 = 52,
            //
            // Summary:
            //     The 5 key.
            D5 = 53,
            //
            // Summary:
            //     The 6 key.
            D6 = 54,
            //
            // Summary:
            //     The 7 key.
            D7 = 55,
            //
            // Summary:
            //     The 8 key.
            D8 = 56,
            //
            // Summary:
            //     The 9 key.
            D9 = 57,
            //
            // Summary:
            //     The A key.
            A = 65,
            //
            // Summary:
            //     The B key.
            B = 66,
            //
            // Summary:
            //     The C key.
            C = 67,
            //
            // Summary:
            //     The D key.
            D = 68,
            //
            // Summary:
            //     The E key.
            E = 69,
            //
            // Summary:
            //     The F key.
            F = 70,
            //
            // Summary:
            //     The G key.
            G = 71,
            //
            // Summary:
            //     The H key.
            H = 72,
            //
            // Summary:
            //     The I key.
            I = 73,
            //
            // Summary:
            //     The J key.
            J = 74,
            //
            // Summary:
            //     The K key.
            K = 75,
            //
            // Summary:
            //     The L key.
            L = 76,
            //
            // Summary:
            //     The M key.
            M = 77,
            //
            // Summary:
            //     The N key.
            N = 78,
            //
            // Summary:
            //     The O key.
            O = 79,
            //
            // Summary:
            //     The P key.
            P = 80,
            //
            // Summary:
            //     The Q key.
            Q = 81,
            //
            // Summary:
            //     The R key.
            R = 82,
            //
            // Summary:
            //     The S key.
            S = 83,
            //
            // Summary:
            //     The T key.
            T = 84,
            //
            // Summary:
            //     The U key.
            U = 85,
            //
            // Summary:
            //     The V key.
            V = 86,
            //
            // Summary:
            //     The W key.
            W = 87,
            //
            // Summary:
            //     The X key.
            X = 88,
            //
            // Summary:
            //     The Y key.
            Y = 89,
            //
            // Summary:
            //     The Z key.
            Z = 90,
            //
            // Summary:
            //     The 0 key on the numeric keypad.
            NumPad0 = 96,
            //
            // Summary:
            //     The 1 key on the numeric keypad.
            NumPad1 = 97,
            //
            // Summary:
            //     The 2 key on the numeric keypad.
            NumPad2 = 98,
            //
            // Summary:
            //     The 3 key on the numeric keypad.
            NumPad3 = 99,
            //
            // Summary:
            //     The 4 key on the numeric keypad.
            NumPad4 = 100,
            //
            // Summary:
            //     The 5 key on the numeric keypad.
            NumPad5 = 101,
            //
            // Summary:
            //     The 6 key on the numeric keypad.
            NumPad6 = 102,
            //
            // Summary:
            //     The 7 key on the numeric keypad.
            NumPad7 = 103,
            //
            // Summary:
            //     The 8 key on the numeric keypad.
            NumPad8 = 104,
            //
            // Summary:
            //     The 9 key on the numeric keypad.
            NumPad9 = 105,
            //
            // Summary:
            //     The multiply key.
            Multiply = 106,
            //
            // Summary:
            //     The add key.
            Add = 107,
            //
            // Summary:
            //     The subtract key.
            Subtract = 109,
            //
            // Summary:
            //     The decimal key.
            Decimal = 110,
            //
            // Summary:
            //     The divide key.
            Divide = 111,
            //
            // Summary:
            //     The F1 key.
            F1 = 112,
            //
            // Summary:
            //     The F2 key.
            F2 = 113,
            //
            // Summary:
            //     The F3 key.
            F3 = 114,
            //
            // Summary:
            //     The F4 key.
            F4 = 115,
            //
            // Summary:
            //     The F5 key.
            F5 = 116,
            //
            // Summary:
            //     The F6 key.
            F6 = 117,
            //
            // Summary:
            //     The F7 key.
            F7 = 118,
            //
            // Summary:
            //     The F8 key.
            F8 = 119,
            //
            // Summary:
            //     The F9 key.
            F9 = 120,
            //
            // Summary:
            //     The F10 key.
            F10 = 121,
            //
            // Summary:
            //     The F11 key.
            F11 = 122,
            //
            // Summary:
            //     The F12 key.
            F12 = 123,
        }
    }
}
