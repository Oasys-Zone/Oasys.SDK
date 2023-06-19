using Oasys.Common.Menu;
using Oasys.Common.Menu.ItemComponents;
using Oasys.Common.Tools;
using Oasys.Common.Tools.Devices;
using Oasys.SDK.InputProviders;
using Oasys.SDK.Tools;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oasys.SDK
{
    public enum PingSlot
    {
        Neutral,
        Retreat,
        Push,
        On_My_Way,
        All_In,
        Assist,
        Hold,
        Enemy_Missing,
        Bait,
        Enemy_No_Vision,
        Vision,
        Request_Vision
    }

    public class Ping
    {
        public PingSlot PingSlot { get; private set; }
        public string Name { get; private set; }

        public Vector2 Position { get; private set; }

        public Ping(PingSlot pingSlot, string name, Vector2 position)
        {
            PingSlot = pingSlot;
            Name = name;
            Position = position;
        }
    }

    public static class PingManager
    {
        internal static double RequestVisionAngle => 0;
        internal static double EnemyNoVisionAngle => 180;

        internal static double OnMyWayAngle => 0;
        internal static double PushAngle => 45;
        internal static double RetreatAngle => 90;
        internal static double BaitAngle => 135;
        internal static double MissingAngle => 180;
        internal static double HoldAngle => 225;
        internal static double AssistAngle => 270;
        internal static double AllInAngle => 315;

        internal static List<Ping> SmartWheelPings => GetPings();
        internal static List<Ping> VisionWheelPings => GetPings(true);
        internal static Vector2 MousePosOnScreen => new(Cursor.Position.X, Cursor.Position.Y);

        internal static Tab Shortcuts = new("Shortcuts");
        internal static Group Vision = new("Vision");
        internal static KeyBinding HoldWhilePressing = new("Hold while pressing Shortcut", Keys.None);
        internal static KeyBinding VisionShortcut = new("Quick Vision Shortcut Key", Keys.H);
        internal static InfoDisplay ShortcutInfo = new() { Title = "Shortcut Info", Information = "Should be same as in Hotkeys" };

        static PingManager()
        {
            MenuManagerProvider.AddTab(Shortcuts);
            Shortcuts.AddGroup(Vision);
            Vision.AddItem(HoldWhilePressing);
            Vision.AddItem(VisionShortcut);
            Vision.AddItem(ShortcutInfo);
        }

        internal static List<Ping> GetPings(bool visionWheel = false)
        {
            var mousePosOnScreen = MousePosOnScreen;
            var points = visionWheel
                ? new List<Ping>()
                {
                    new Ping(PingSlot.Vision, "Enemy Vision", mousePosOnScreen),
                    new Ping(PingSlot.Enemy_No_Vision, "No Vision", GetPointOnCircle(EnemyNoVisionAngle, 200, mousePosOnScreen)),
                    new Ping(PingSlot.Request_Vision, "Request Vision", GetPointOnCircle(RequestVisionAngle, 200, mousePosOnScreen))
                }
                : new List<Ping>()
                {
                    new Ping(PingSlot.Neutral, "Neutral", mousePosOnScreen),
                    new Ping(PingSlot.Retreat, "Retreat", GetPointOnCircle(RetreatAngle, 200, mousePosOnScreen)),
                    new Ping(PingSlot.Push, "Push", GetPointOnCircle(PushAngle, 200, mousePosOnScreen)),
                    new Ping(PingSlot.On_My_Way, "On My Way", GetPointOnCircle(OnMyWayAngle, 200, mousePosOnScreen)),
                    new Ping(PingSlot.All_In, "All In", GetPointOnCircle(AllInAngle, 200, mousePosOnScreen)),
                    new Ping(PingSlot.Assist, "Assist", GetPointOnCircle(AssistAngle, 200, mousePosOnScreen)),
                    new Ping(PingSlot.Hold, "Hold", GetPointOnCircle(HoldAngle, 200, mousePosOnScreen)),
                    new Ping(PingSlot.Enemy_Missing, "Enemy Missing", GetPointOnCircle(MissingAngle, 200, mousePosOnScreen)),
                    new Ping(PingSlot.Bait, "Bait", GetPointOnCircle(BaitAngle, 200, mousePosOnScreen))
                };

            return points;
        }

        public static Vector2 GetPositionOnPingWheel(PingSlot pingSlot, double radius = 200)
        {
            return GetPositionOnPingWheel(pingSlot, radius, MousePosOnScreen);
        }

        public static Vector2 GetPositionOnPingWheel(PingSlot pingSlot, double radius, Vector2 origin)
        {
            return pingSlot switch
            {
                PingSlot.Neutral => origin,
                PingSlot.Retreat => GetPointOnCircle(RetreatAngle, radius, origin),
                PingSlot.Push => GetPointOnCircle(PushAngle, radius, origin),
                PingSlot.On_My_Way => GetPointOnCircle(OnMyWayAngle, radius, origin),
                PingSlot.All_In => GetPointOnCircle(AllInAngle, radius, origin),
                PingSlot.Assist => GetPointOnCircle(AssistAngle, radius, origin),
                PingSlot.Hold => GetPointOnCircle(HoldAngle, radius, origin),
                PingSlot.Enemy_Missing => GetPointOnCircle(MissingAngle, radius, origin),
                PingSlot.Bait => GetPointOnCircle(BaitAngle, radius, origin),
                PingSlot.Vision => origin,
                PingSlot.Enemy_No_Vision => GetPointOnCircle(EnemyNoVisionAngle, radius, origin),
                PingSlot.Request_Vision => GetPointOnCircle(RequestVisionAngle, radius, origin),
                _ => Vector2.Zero
            };
        }

        internal static Vector2 GetPointOnCircle(double angle, double radius, Vector2 origin)
        {
            var X = Math.Abs(origin.X + radius * Math.Cos(angle * (Math.PI / 180)));
            var Y = Math.Abs(origin.Y - radius * Math.Sin(angle * (Math.PI / 180)));
            return new Vector2((float)X, (float)Y);
        }

        internal static bool IsNormalPing(PingSlot ping) => ping == PingSlot.Retreat || ping == PingSlot.Push ||
                                                            ping == PingSlot.On_My_Way || ping == PingSlot.All_In ||
                                                            ping == PingSlot.Assist || ping == PingSlot.Hold ||
                                                            ping == PingSlot.Enemy_Missing || ping == PingSlot.Bait;
        internal static bool IsVisionPing(PingSlot ping) => ping == PingSlot.Enemy_No_Vision || ping == PingSlot.Request_Vision;

        public static void Ping(PingSlot ping)
        {
            PingTo(ping, MousePosOnScreen);
        }

        public static void PingTo(PingSlot ping, Vector2 position)
        {
            PingTo(ping, position, MousePosOnScreen);
        }

        private static object _lock = new();
        public static void PingTo(PingSlot ping, Vector2 position, Vector2 originalPosition)
        {
            if (!GameEngine.IsGameWindowFocused)
            {
                return;
            }

            lock (_lock)
            {
                Mouse.InUse = true;
                NativeImport.BlockInput(true);

                var origMove = Orbwalker.AllowMoving;
                var origAttack = Orbwalker.AllowAttacking;
                var origtype = Orbwalker.OrbwalkingMode;

                Orbwalker.OrbwalkingMode = Orbwalker.OrbWalkingModeType.None;
                Orbwalker.AllowAttacking = false;
                Orbwalker.AllowMoving = false;

                Ping(ping, (int)position.X, (int)position.Y);

                if (MousePosOnScreen != originalPosition)
                {
                    Util.Sleep(5);
                    MouseProvider.SetCursor((int)originalPosition.X, (int)originalPosition.Y);
                }

                NativeImport.BlockInput(false);
                Mouse.InUse = false;

                Orbwalker.AllowAttacking = origAttack;
                Orbwalker.AllowMoving = origMove;
                Orbwalker.OrbwalkingMode = origtype;
            }
        }

        private static void Ping(PingSlot ping, int x, int y)
        {
            // Neutral Ping
            if (ping == PingSlot.Neutral)
            {
                NormalPing(x, y);
            }
            // All Normal Smart Wheel Pings
            else if (IsNormalPing(ping))
            {
                NormalPingWheel(ping, x, y);
            }
            // Enemy has Vision Ping
            else if (ping == PingSlot.Vision)
            {
                VisionPing(x, y);
            }
            // Vision Wheel Pings
            else if (IsVisionPing(ping))
            {
                VisionPingWheel(ping, x, y);
            }
        }

        private static void VisionPingWheel(PingSlot ping, int x, int y)
        {
            if (!(HoldWhilePressing.SelectedKey == Keys.None))
            {
                KeyboardProvider.PressKeyDown(HoldWhilePressing.SelectedKey);
                KeyboardProvider.PressKeyDown(VisionShortcut.SelectedKey);
                DoAction(MouseProvider.LeftDown, x, y);
                Util.Sleep(5);
                var positionOnWheel = GetPositionOnPingWheel(ping, 200);
                DoAction(MouseProvider.LeftUp, (int)positionOnWheel.X, (int)positionOnWheel.Y);
                KeyboardProvider.PressKeyUp(HoldWhilePressing.SelectedKey);
                KeyboardProvider.PressKeyUp(VisionShortcut.SelectedKey);
            }
            else
            {
                KeyboardProvider.PressKeyDown(VisionShortcut.SelectedKey);
                DoAction(MouseProvider.LeftDown, x, y);
                Util.Sleep(5);
                var positionOnWheel = GetPositionOnPingWheel(ping, 200);
                DoAction(MouseProvider.LeftUp, (int)positionOnWheel.X, (int)positionOnWheel.Y);
                KeyboardProvider.PressKeyUp(VisionShortcut.SelectedKey);
            }
        }

        private static void VisionPing(int x, int y)
        {
            DoAction((_1, _2) =>
            {
                if (!(HoldWhilePressing.SelectedKey == Keys.None))
                {
                    KeyboardProvider.PressKeyDown(HoldWhilePressing.SelectedKey);
                    KeyboardProvider.PressKey(VisionShortcut.SelectedKey);
                    KeyboardProvider.PressKeyUp(HoldWhilePressing.SelectedKey);
                }
                else
                {
                    KeyboardProvider.PressKey(VisionShortcut.SelectedKey);
                }
            },
            x, y);
        }

        private static void NormalPingWheel(PingSlot ping, int x, int y)
        {
            Keyboard.SendKeyDown(Keys.LControlKey);
            DoAction(MouseProvider.LeftDown, x, y);
            Util.Sleep(5);
            var positionOnWheel = GetPositionOnPingWheel(ping, 200);
            DoAction(MouseProvider.LeftUp, (int)positionOnWheel.X, (int)positionOnWheel.Y);
            Keyboard.SendKeyUp(Keys.LControlKey);
        }

        private static void NormalPing(int x, int y)
        {
            Keyboard.SendKeyDown(Keyboard.KeyBoardScanCodes.KEY_ALT);
            DoAction(Mouse.LeftClick, x, y);
            Keyboard.SendKeyUp(Keyboard.KeyBoardScanCodes.KEY_ALT);
        }

        private static void DoAction(Action<int, int> action, int x, int y)
        {
            var clock = Stopwatch.StartNew();
            var inAction = true;

            Parallel.Invoke(
            () =>
            {
                Util.Sleep(5);
                action(x, y);
                inAction = false;
            },
            () =>
            {
                var current = clock.ElapsedMilliseconds;
                var i = 0;
                do
                {
                    i++;
                    if (clock.ElapsedMilliseconds > current || i % 20 == 0)
                    {
                        Mouse.SetCursor(x, y);
                        current = clock.ElapsedMilliseconds;
                    }
                }
                while (inAction);
            });
        }
    }
}
