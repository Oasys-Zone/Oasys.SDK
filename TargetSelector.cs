using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasys.Common.GameObject;

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
        public static GameObjectBase GetBestChampionTarget(GameObjectBase selectedChampion = null) => Common.Logic.TargetSelector.GetBestHeroTarget(selectedChampion);

        /// <summary>
        /// Gets the amount of attacks left to kill.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static float AttacksLeftToKill(GameObjectBase target) => Common.Logic.TargetSelector.AttacksToKill(target);

        /// <summary>
        /// Checks whether if the target is killable.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsKillable(GameObjectBase target) => Common.Logic.TargetSelector.IsKillable(target);

        /// <summary>
        /// Checks whether if the player can attack the target.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsAttackable(GameObjectBase target) => Common.Logic.TargetSelector.IsAttackable(target);

        /// <summary>
        /// Checks whether if the target is in the player's base(attackable) range.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsInRange(GameObjectBase target) => Common.Logic.TargetSelector.IsInRange(target);
        
        /// <summary>
        /// Checks whether if the target is in the player's base(attackable) range.
        /// </summary>
        /// <param name="minion"></param>
        /// <returns></returns>
        public static bool ShouldAttackMinion(GameObjectBase minion) => Common.Logic.TargetSelector.ShouldAttackMinion(minion);
    }
}
