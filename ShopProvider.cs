using Oasys.Common.Enums.GameEnums;
using Oasys.Common.Tools.Devices;
using SharpDX;
using System.Threading;
using System.Windows.Forms;

namespace Oasys.SDK
{
    public static class ShopProvider
    {
        public static void BuyItem(ItemID itemID)
        {
            if (GameEngine.ChatBox.IsChatBoxOpen || !GameEngine.IsGameWindowFocused)
            {
                return;
            }

            if (UnitManager.MyChampion.DistanceTo(new Vector3(14300, 90, 14400)) <= 950 ||
                UnitManager.MyChampion.DistanceTo(new Vector3(405, 95, 425)) <= 950)
            {
                Keyboard.SendKey(Keyboard.KeyBoardScanCodes.KEY_P);
                Thread.Sleep(10);

                Keyboard.SendKeyDown(Keyboard.KeyBoardScanCodes.KEY_CONTROL);
                Keyboard.SendKey(Keyboard.KeyBoardScanCodes.KEY_L);
                Keyboard.SendKeyUp(Keyboard.KeyBoardScanCodes.KEY_CONTROL);
                Thread.Sleep(10);

                var itemName = itemID.ToString();
                itemName = itemName.Replace("_", " ");

                SendKeys.SendWait(" " + itemName + "{ENTER}");
                Thread.Sleep(10);

                Keyboard.SendKey(Keyboard.KeyBoardScanCodes.KEY_ARROWDOWN);
                Keyboard.SendKey(Keyboard.KeyBoardScanCodes.KEY_ARROWUP);
                Thread.Sleep(10);

                Keyboard.SendKey(Keyboard.KeyBoardScanCodes.KEY_ENTER);
                Thread.Sleep(10);

                Keyboard.SendKey(Keyboard.KeyBoardScanCodes.KEY_P);
                Thread.Sleep(10);
            }
        }
    }
}
