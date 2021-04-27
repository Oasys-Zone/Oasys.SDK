using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static float GetTargetHealthAfterBasicAttack(GameObjectBase attacker, GameObjectBase target) => Common.Logic.DamageCalculator.GetTargetHealthAfterBasicAttack(attacker, target);

        /// <summary>
        /// Gets the player's next attack damage to be dealt to the target.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static float GetNextBasicAttackDamage(GameObjectBase attacker, GameObjectBase target) => Common.Logic.DamageCalculator.GetNextBasicAttackDamage(attacker, target);
    }
}
