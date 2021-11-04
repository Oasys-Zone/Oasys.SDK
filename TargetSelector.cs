using Oasys.Common.GameObject;
using Oasys.Common.Logic;

namespace Oasys.SDK
{
    /// <summary>
    /// Standard target selector.
    /// </summary>
    public class TargetSelector
    {
        /// <summary>
        /// Gets the best considered champion to target.
        /// </summary>
        /// <param name="selectedChampion"></param>
        /// <returns></returns>
        public static GameObjectBase GetBestChampionTarget(GameObjectBase selectedChampion = null)
        {
            return Common.Logic.TargetSelector.GetBestHeroTarget(selectedChampion);
        }

        /// <summary>
        /// Gets the amount of attacks left to kill.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static float AttacksLeftToKill(GameObjectBase target)
        {
            return Common.Logic.TargetSelector.AttacksToKill(target);
        }

        /// <summary>
        /// Checks whether if the target is killable.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsKillable(GameObjectBase target)
        {
            return Common.Logic.TargetSelector.IsKillable(target);
        }

        /// <summary>
        /// Checks whether if the player can attack the target.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsAttackable(GameObjectBase target)
        {
            return Common.Logic.TargetSelector.IsAttackable(target);
        }

        /// <summary>
        /// Checks whether if the player can attack the target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="teamCheck"></param>
        /// <returns></returns>
        public static bool IsAttackable(GameObjectBase target, bool teamCheck = true)
        {
            return Common.Logic.TargetSelector.IsAttackable(target, teamCheck);
        }

        /// <summary>
        /// Checks whether if the target is in the player's base(attackable) range.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsInRange(GameObjectBase target)
        {
            return Common.Logic.TargetSelector.IsInRange(target);
        }

        /// <summary>
        /// Checks whether if the target is invulnerable to damage of the specified damagetype, ignore shields default.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damageType"></param>
        /// <param name="ignoreShields">Default true. Ignores spell shields like Sivir E</param>
        /// <returns></returns>
        public static bool IsInvulnerable(GameObjectBase target, DamageType damageType, bool ignoreShields = true)
        {
            return Common.Logic.TargetSelector.IsInvulnerable(target, damageType, ignoreShields);
        }

        /// <summary>
        /// https://leagueoflegends.fandom.com/wiki/Crowd_control#Immunity
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsTotalCCImmune(GameObjectBase target)
        {
            return Common.Logic.TargetSelector.IsTotalCCImmune(target);
        }

        /// <summary>
        /// https://leagueoflegends.fandom.com/wiki/Crowd_control#Displacement_Immunity
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsDisplacementImmune(GameObjectBase target)
        {
            return Common.Logic.TargetSelector.IsDisplacementImmune(target);
        }

        /// <summary>
        /// Checks whether if the target is in the player's base(attackable) range.
        /// </summary>
        /// <param name="minion"></param>
        /// <returns></returns>
        public static bool ShouldAttackMinion(GameObjectBase minion)
        {
            return Common.Logic.TargetSelector.ShouldAttackMinion(minion);
        }
    }
}
