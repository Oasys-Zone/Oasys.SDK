using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasys.Common.Tools.Devices;

namespace Oasys.SDK.InputProviders
{
    /// <summary>
    /// Provides keyboard input emulation.
    /// </summary>
    public class KeyboardProvider
    {
        /// <summary>
        /// Checks whether if a key is pressed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyPressed(System.Windows.Forms.Keys key) => Keyboard.IsKeyPressed(key);

        /// <summary>
        /// Checks whether if control is pressed.
        /// </summary>
        /// <returns></returns>
        public static bool IsControlPressed() => Keyboard.IsControlPressed();

        /// <summary>
        /// Checks whether if shift is pressed.
        /// </summary>
        /// <returns></returns>
        public static bool IsShiftPressed() => Keyboard.IsShiftPressed();

        /// <summary>
        ///  Presses a key down.
        /// </summary>
        /// <param name="keyBoardScanCode"></param>
        public static void PressKeyDown(short keyBoardScanCode) => Keyboard.SendKeyDown(keyBoardScanCode);

        /// <summary>
        /// Presses a key down.
        /// </summary>
        /// <param name="keyBoardScanCode"></param>
        public static void PressKeyDown(KeyBoardScanCodes keyBoardScanCode) => PressKeyDown((short)keyBoardScanCode);

        /// <summary>
        /// Presses a key up.
        /// </summary>
        /// <param name="keyBoardScanCode"></param>
        public static void PressKeyUp(short keyBoardScanCode) => Keyboard.SendKeyUp(keyBoardScanCode);

        /// <summary>
        /// Presses a key up.
        /// </summary>
        /// <param name="keyBoardScanCode"></param>
        public static void PressKeyUp(KeyBoardScanCodes keyBoardScanCode) => PressKeyUp((short)keyBoardScanCode);

        /// <summary>
        /// Simulates a key press.
        /// </summary>
        /// <param name="keyboardScanCode"></param>
        public static void PressKey(short keyboardScanCode) => Keyboard.SendKey(keyboardScanCode);

        /// <summary>
        /// Simulates a key press.
        /// </summary>
        /// <param name="keyboardScanCode"></param>
        public static void PressKey(KeyBoardScanCodes keyboardScanCode) => PressKey((short)keyboardScanCode);

        public enum KeyBoardScanCodes : short
        {
            ESC = 0x01,
            KEY_1 = 0x02,
            KEY_2 = 0x03,
            KEY_3 = 0x04,
            KEY_4 = 0x05,
            KEY_5 = 0x06,
            KEY_6 = 0x07,
            KEY_7 = 0x08,
            KEY_8 = 0x09,
            KEY_9 = 0x0A,
            KEY_0 = 0x0B,
            KEY_MINUS = 0x0C,
            KEY_EQUAL = 0x0D,
            KEY_BACKSPACE = 0x0E,
            KEY_TAB = 0x0F,
            KEY_Q = 0x10,
            KEY_W = 0x11,
            KEY_E = 0x12,
            KEY_R = 0x13,
            KEY_T = 0x14,
            KEY_Y = 0x15,
            KEY_U = 0x16,
            KEY_I = 0x17,
            KEY_O = 0x18,
            KEY_P = 0x19,
            KEY_OPENING_BRACKETS = 0x1A,
            KEY_CLOSENING_BRACKETS = 0x1B,
            KEY_ENTER = 0x1C,
            KEY_CONTROL = 0x1D,
            KEY_A = 0x1E,
            KEY_S = 0x1F,
            KEY_D = 0x20,
            KEY_F = 0x21,
            KEY_G = 0x22,
            KEY_H = 0x23,
            KEY_J = 0x24,
            KEY_K = 0x25,
            KEY_L = 0x26,
            KEY_NUMPAD0 = 0x52,
            KEY_NUMPAD1 = 0x4F,
            KEY_NUMPAD2 = 0x50,
            KEY_NUMPAD3 = 0x51,
            KEY_NUMPAD4 = 0x4B,
            KEY_NUMPAD5 = 0x4C,
            KEY_NUMPAD6 = 0x4D,
            KEY_NUMPAD7 = 0x47,
            KEY_NUMPAD8 = 0x48,
            KEY_NUMPAD9 = 0x49
        }
    }
}
