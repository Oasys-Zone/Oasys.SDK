using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Oasys.Common.GameObject.Clients.ExtendedInstances.Spells;

namespace Oasys.SDK.SpellCasting
{
    /// <summary>
    /// Provides summoner spell information.
    /// </summary>
    public class SummonerSpellsProvider
    {
        private static string Slot1_D = string.Empty;
        private static string Slot2_F = string.Empty;

        private static Dictionary<SummonerSpellsEnum, string> SummonerSpellsNameMappedDictionary;

        static SummonerSpellsProvider()
        {
            Slot1_D = UnitManager.MyChampion.GetSpellBook().GetSpellClass(Common.Enums.GameEnums.SpellSlot.Summoner1).SpellData.SpellName;
            Slot2_F = UnitManager.MyChampion.GetSpellBook().GetSpellClass(Common.Enums.GameEnums.SpellSlot.Summoner2).SpellData.SpellName;

            SummonerSpellsNameMappedDictionary = new Dictionary<SummonerSpellsEnum, string>()
            {
                { SummonerSpellsEnum.Barrier, "SummonerBarrier" },
                { SummonerSpellsEnum.Clarity, "SummonerMana" },
                { SummonerSpellsEnum.Cleanse, "SummonerBoost" },
                { SummonerSpellsEnum.Exhaust, "SummonerExhaust" },
                { SummonerSpellsEnum.Flash, "SummonerFlash" },
                { SummonerSpellsEnum.Mark, "SummonerSnowball" },
                { SummonerSpellsEnum.Ghost, "SummonerHaste" },
                { SummonerSpellsEnum.Heal, "SummonerHeal" },
                { SummonerSpellsEnum.Ignite, "SummonerDot" },
                { SummonerSpellsEnum.Smite, "Smite" },
                { SummonerSpellsEnum.PoroToss, "SummonerPoroThrow" },
                { SummonerSpellsEnum.ToTheKing, "SummonerPoroRecall" }
            };
        }

        /// <summary>
        /// Checks whether if I(player) have a given summoner spell on the slot provided.
        /// </summary>
        /// <param name="summSpell"></param>
        /// <param name="summSpellSlot"></param>
        /// <returns></returns>
        public static bool IHaveSpellOnSlot(SummonerSpellsEnum summSpell, SummonerSpellSlot summSpellSlot)
        {
            switch (summSpellSlot)
            {
                case SummonerSpellSlot.First:
                    return SummonerSpellsNameMappedDictionary[summSpell] == Slot1_D;
                case SummonerSpellSlot.Second:
                    return SummonerSpellsNameMappedDictionary[summSpell] == Slot2_F;
            }

            return false;
        }
    }

    public enum SummonerSpellsEnum
    {
        Barrier,
        Clarity,
        Cleanse,
        Exhaust,
        Flash,
        Mark,
        PoroToss,
        Ghost,
        Heal,
        ToTheKing,
        Ignite,
        Smite
    }

    public enum SummonerSpellSlot
    {
        First,
        Second
    }
}
