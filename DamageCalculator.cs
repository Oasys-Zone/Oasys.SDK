using Oasys.Common.GameObject;

namespace Oasys.SDK
{
    /// <summary>
    /// Standard damage calculator
    /// </summary>
    public class DamageCalculator : Common.Logic.DamageCalculator
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
            var armor = target?.Armor ?? 1 * attacker.UnitStats.ArmorPercentPentration;
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

        public static float GetPhysicalDamageReductionModifier<T>(T target) where T : GameObjectBase
        {
            return Common.Logic.DamageCalculator.GetPhysicalDamageReductionModifier(target);
        }

        public static float GetMagicDamageReductionModifier<T>(T target) where T : GameObjectBase
        {
            return Common.Logic.DamageCalculator.GetMagicDamageReductionModifier(target);
        }

        public static float GetGeneralDamageReductionModifier<T>(T target) where T : GameObjectBase
        {
            return Common.Logic.DamageCalculator.GetGeneralDamageReductionModifier(target);
        }

        public static float CalculateActualDamage<TObject>(TObject attacker, TObject target, float physicalDamage, float magicDamage, float trueDamage)
            where TObject : GameObjectBase
        {
            return CalculateActualDamage(GetArmorMod(attacker, target), GetPhysicalDamageReductionModifier(target),
                                         GetMagicResistMod(attacker, target), GetMagicDamageReductionModifier(target),
                                         physicalDamage, magicDamage,
                                         trueDamage, GetGeneralDamageReductionModifier(target),
                                         target.BuffManager.HasActiveBuff("PressTheAttack/PressTheAttackDamageAmp.lua"), attacker.Level);
        }

        public static float CalculateActualDamage<TObject>(TObject attacker, TObject target, float physicalDamage)
            where TObject : GameObjectBase
        {
            return CalculateActualDamage(GetArmorMod(attacker, target), GetPhysicalDamageReductionModifier(target),
                                         GetMagicResistMod(attacker, target), GetMagicDamageReductionModifier(target),
                                         physicalDamage, 0,
                                         0, GetGeneralDamageReductionModifier(target),
                                         target.BuffManager.HasActiveBuff("PressTheAttack/PressTheAttackDamageAmp.lua"), attacker.Level);
        }

        public static float CalculateActualDamage<TDamageInfo, TObject>(TDamageInfo damageInfo)
            where TDamageInfo : DamageInfo<TObject>
            where TObject : GameObjectBase
        {
            return CalculateActualDamage(GetArmorMod(damageInfo.attacker, damageInfo.target), GetPhysicalDamageReductionModifier(damageInfo.target),
                                         GetMagicResistMod(damageInfo.attacker, damageInfo.target), GetMagicDamageReductionModifier(damageInfo.target),
                                         damageInfo.physicalDamage, damageInfo.magicDamage,
                                         damageInfo.trueDamage, GetGeneralDamageReductionModifier(damageInfo.target),
                                         damageInfo.target.BuffManager.HasActiveBuff("PressTheAttack/PressTheAttackDamageAmp.lua"), damageInfo.attacker.Level);
        }

        public record DamageInfo<TObject>(TObject attacker, TObject target, float physicalDamage, float magicDamage, float trueDamage)
            where TObject : GameObjectBase;

        public record PhysicalDamageInfo<TObject>(TObject attacker, TObject target, float physicalDamage)
            : DamageInfo<TObject>(attacker, target, physicalDamage, 0, 0)
            where TObject : GameObjectBase;

        public record MagicDamageInfo<TObject>(TObject attacker, TObject target, float magicDamage)
            : DamageInfo<TObject>(attacker, target, 0, magicDamage, 0)
            where TObject : GameObjectBase;

        public record TrueDamageInfo<TObject>(TObject attacker, TObject target, float trueDamage)
            : DamageInfo<TObject>(attacker, target, 0, 0, trueDamage)
            where TObject : GameObjectBase;

        public record MixedDamageInfo<TObject>(TObject attacker, TObject target, float physicalDamage, float magicDamage)
            : DamageInfo<TObject>(attacker, target, physicalDamage, magicDamage, 0)
            where TObject : GameObjectBase;
    }
}
