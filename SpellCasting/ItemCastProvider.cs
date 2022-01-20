using Oasys.Common.Enums.GameEnums;
using Oasys.Common.GameObject;
using Oasys.Common.Tools;
using Oasys.SDK.InputProviders;
using SharpDX;
using System.Threading;

namespace Oasys.SDK.SpellCasting
{
    /// <summary>
    /// Provides item casting emulation.
    /// </summary>
    public class ItemCastProvider
    {
        /// <summary>
        /// Casts an item at/towards the position of the mouse, or self casts.
        /// </summary>
        /// <param name="iID"></param>
        public static void CastItem(ItemID iID)
        {
            var playerSpellbook = UnitManager.MyChampion.GetSpellBook();
            var playerInventory = UnitManager.MyChampion.Inventory;

            if (playerInventory.HasItem(iID))
            {
                switch (playerInventory.GetItemByID(iID).Slot)
                {
                    case ItemSlot.One:
                        if (playerSpellbook.GetSpellClass(SpellSlot.Item1).IsSpellReady)
                        {
                            KeyboardProvider.PressKey(KeyboardProvider.KeyBoardScanCodes.KEY_1);
                        }

                        break;
                    case ItemSlot.Two:
                        if (playerSpellbook.GetSpellClass(SpellSlot.Item2).IsSpellReady)
                        {
                            KeyboardProvider.PressKey(KeyboardProvider.KeyBoardScanCodes.KEY_2);
                        }

                        break;
                    case ItemSlot.Three:
                        if (playerSpellbook.GetSpellClass(SpellSlot.Item3).IsSpellReady)
                        {
                            KeyboardProvider.PressKey(KeyboardProvider.KeyBoardScanCodes.KEY_3);
                        }

                        break;
                    case ItemSlot.Trinket:
                        if (playerSpellbook.GetSpellClass(SpellSlot.Trinket).IsSpellReady)
                        {
                            KeyboardProvider.PressKey(KeyboardProvider.KeyBoardScanCodes.KEY_4);
                        }

                        break;
                    case ItemSlot.Five:
                        if (playerSpellbook.GetSpellClass(SpellSlot.Item4).IsSpellReady)
                        {
                            KeyboardProvider.PressKey(KeyboardProvider.KeyBoardScanCodes.KEY_5);
                        }

                        break;
                    case ItemSlot.Six:
                        if (playerSpellbook.GetSpellClass(SpellSlot.Item5).IsSpellReady)
                        {
                            KeyboardProvider.PressKey(KeyboardProvider.KeyBoardScanCodes.KEY_6);
                        }

                        break;
                    case ItemSlot.Seven:
                        if (playerSpellbook.GetSpellClass(SpellSlot.Item6).IsSpellReady)
                        {
                            KeyboardProvider.PressKey(KeyboardProvider.KeyBoardScanCodes.KEY_7);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Casts an item at/towards the given vector3 position parameter
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="objectPosition"></param>
        public static void CastItem(ItemID iID, Vector3 objectPosition)
        {
            var currMousePos = System.Windows.Forms.Cursor.Position;
            var w2sPos = Common.LeagueNativeRendererManager.WorldToScreen(objectPosition);

            MouseProvider.SetCursor((int)w2sPos.X, (int)w2sPos.Y);
            CastItem(iID);

            Util.Sleep(2);
            MouseProvider.SetCursor(currMousePos);
        }

        /// <summary>
        /// Casts an item at/towards the given vector3 position parameter
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="screenPosition"></param>
        public static void CastItem(ItemID iID, Vector2 screenPosition)
        {
            var currMousePos = System.Windows.Forms.Cursor.Position;

            MouseProvider.SetCursor((int)screenPosition.X, (int)screenPosition.Y);
            CastItem(iID);

            Util.Sleep(2);
            MouseProvider.SetCursor(currMousePos);
        }

        /// <summary>
        /// Casts an item at/towards the given target.
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="target"></param>
        public static void CastItem(ItemID iID, GameObjectBase target)
        {
            CastItem(iID, target.W2S);
        }
    }
}
