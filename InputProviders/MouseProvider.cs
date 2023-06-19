﻿using Oasys.Common.Tools;
using Oasys.Common.Tools.Devices;
using System;

namespace Oasys.SDK.InputProviders
{
    /// <summary>
    /// Provides mouse input emulation.
    /// </summary>
    public class MouseProvider
    {
        /// <summary>
        /// Sets the cursor position.
        /// Note that using SetCursor over SetPosition will not send any input stream events that we might want for certain scenarios.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetCursor(int x, int y)
        {
            Mouse.SetCursor(x, y);
        }

        /// <summary>
        /// Sets the cursor position.
        /// Note that using SetCursor over SetPosition will not send any input stream events that we might want for certain scenarios.
        /// </summary>
        /// <param name="p"></param>
        public static void SetCursor(Pos p)
        {
            SetCursor(p.X, p.Y);
        }

        /// <summary>
        /// Changes the cursor position relatively to its current position.
        /// Note that using SetCursor over SetPosition will not send any input stream events that we might want for certain scenarios.
        /// </summary>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        public static void SetCursorRelative(int xOffset, int yOffset)
        {
            Mouse.SetCursorRelative(xOffset, yOffset);
        }

        /// <summary>
        /// Changes the cursor position relatively to its current position.
        /// Note that using SetCursor over SetPosition will not send any input stream events that we might want for certain scenarios.
        /// </summary>
        /// <param name="p"></param>
        public static void SetCursorRelative(Pos p)
        {
            SetCursorRelative(p.X, p.Y);
        }

        /// <summary>
        /// Sets the mouse position using the native mouse_event call.
        /// This will also stream all information of mouse movement into the input handler.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetPosition(int x, int y)
        {
            Mouse.SetPosition(x, y);
        }

        /// <summary>
        /// Sets the mouse position using the native mouse_event call.
        /// This will also stream all information of mouse movement into the input handler.
        /// </summary>
        /// <param name="p"></param>
        public static void SetPosition(Pos p)
        {
            SetPosition(p.X, p.Y);
        }

        /// <summary>
        /// Changes the mouse position relatively to its current position using the native mouse_event call.
        /// This will also stream all information of mouse movement into the input handler.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetPositionRelative(int x, int y)
        {
            Mouse.SetPositionRelative(x, y);
        }

        /// <summary>
        /// Changes the mouse position relatively to its current position using the native mouse_event call.
        /// This will also stream all information of mouse movement into the input handler.
        /// </summary>
        /// <param name="p"></param>
        public static void SetPositionRelative(Pos p)
        {
            SetPositionRelative(p.X, p.Y);
        }

        /// <summary>
        /// Clicks on target position, and after [delay]ms bounces back to origin location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delay"></param>
        /// <param name="left"></param>
        /// <param name="callback">what to do instead of a right click in case we need a key press or something</param>
        /// <returns>has clicked</returns>
        public static bool ClickAndBounce(int x, int y, int delay, bool left = false, Action callback = null)
        {
            return Mouse.ClickAndBounce(x, y, delay, left, callback) < float.MaxValue;
        }

        /// <summary>
        /// Clicks on target position, and after [delay]ms bounces back to origin location.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="delay"></param>
        /// <param name="left"></param>
        /// <param name="callback">what to do instead of a right click in case we need a key press or something</param>
        /// <returns>has clicked</returns>
        public static bool ClickAndBounce(Pos p, int delay, bool left = false, Action callback = null)
        {
            return ClickAndBounce(p.X, p.Y, delay, left, callback);
        }

        /// <summary>
        /// Left click mouse down
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void LeftDown(int x, int y)
        {
            Mouse.LeftDown(x, y);
        }

        /// <summary>
        /// Left click mouse up
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void LeftUp(int x, int y)
        {
            Mouse.LeftUp(x, y);
        }

        /// <summary>
        /// Right click mouse down
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void RightDown(int x, int y)
        {
            Mouse.RightDown(x, y);
        }

        /// <summary>
        /// Right click mouse up
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void RightUp(int x, int y)
        {
            Mouse.RightUp(x, y);
        }

        public static bool InUse => Mouse.InUse;

        public static Pos RestorePosition
        {
            get => Mouse.Restore;
            set => Mouse.Restore = value;
        }
    }
}
