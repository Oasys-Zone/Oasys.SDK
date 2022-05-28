using System.Collections.Generic;

namespace Oasys.SDK.SpellCasting
{
    /// <summary>
    /// Provides summoner spell information.
    /// </summary>
    public class SummonerSpellsProvider
    {
        private static readonly string Slot1_D = string.Empty;
        private static readonly string Slot2_F = string.Empty;

        private static readonly Dictionary<SummonerSpellsEnum, string> SummonerSpellsNameMappedDictionary;

        static SummonerSpellsProvider()
        {
            Slot1_D = UnitManager.MyChampion.GetSpellBook().GetSpellClass(Common.Enums.GameEnums.SpellSlot.Summoner1).SpellData.SpellName;
            Slot2_F = UnitManager.MyChampion.GetSpellBook().GetSpellClass(Common.Enums.GameEnums.SpellSlot.Summoner2).SpellData.SpellName;

            if (Slot1_D.Contains("smite", System.StringComparison.OrdinalIgnoreCase))
            {
                Slot1_D = "SummonerSmite";
            }
            if (Slot2_F.Contains("smite", System.StringComparison.OrdinalIgnoreCase))
            {
                Slot2_F = "SummonerSmite";
            }

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
                { SummonerSpellsEnum.Smite, "SummonerSmite" },
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
