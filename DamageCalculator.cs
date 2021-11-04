using Oasys.Common.GameObject;

namespace Oasys.SDK
{
    /// <summary>
    /// Standard damage calculator
    /// </summary>
    public class DamageCalculator
    {
        /// <summary>
        /// Gets the target's health after player's basic attack.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static float GetTargetHealthAfterBasicAttack(GameObjectBase attacker, GameObjectBase target)
        {
            return Common.Logic.DamageCalculator.GetTargetHealthAfterBasicAttack(attacker, target);
        }

        /// <summary>
        /// Gets the player's next attack damage to be dealt to the target.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static float GetNextBasicAttackDamage(GameObjectBase attacker, GameObjectBase target)
        {
            return Common.Logic.DamageCalculator.GetNextBasicAttackDamage(attacker, target);
        }

        public static float GetCombatMagicResist(GameObjectBase attacker, GameObjectBase target)
        {
            var magicResist = target.UnitStats.MagicResist * attacker.UnitStats.MagicPercentPenetration;
            magicResist -= attacker.UnitStats.FlatMagicPenetration;
            return magicResist;
        }

        public static float GetMagicResistMod(GameObjectBase attacker, GameObjectBase target)
        {
            var magicResist = GetCombatMagicResist(attacker, target);
            var damageMod = magicResist > 0
                                ? 100 / (100 + magicResist)
                                : 2 - 100 / (100 + magicResist);
            return damageMod;
        }

        public static float GetCombatArmor(GameObjectBase attacker, GameObjectBase target)
        {
            var armor = target?.Armor ?? 1 * attacker.UnitStats.PercentBonusArmorPenetration;
            armor -= attacker.UnitStats.PhysicalLethality;
            return armor;
        }

        public static float GetArmorMod(GameObjectBase attacker, GameObjectBase target)
        {
            var armor = GetCombatArmor(attacker, target);
            var damageMod = armor > 0
                                ? 100 / (100 + armor)
                                : 2 - 100 / (100 + armor);
            return damageMod;
        }
    }
}
