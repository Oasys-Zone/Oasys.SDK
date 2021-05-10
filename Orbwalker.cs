
using Oasys.Common.GameObject;

namespace Oasys.SDK
{
    public class Orbwalker
    {
        /// <summary>
        /// Can my champion basic attack
        /// </summary>
        public static bool CanBasicAttack => Common.Logic.Orbwalker.OrbSettings.CanBasicAttack;

        /// <summary>
        /// Reset attack timer
        /// </summary>
        public static void AttackReset()
        {
            Common.Logic.Orbwalker.OrbSettings.LastBasicAttack = 0;
        }

        /// <summary>
        /// Can my champion move after basic attack
        /// </summary>
        public static bool CanMove => Common.Logic.Orbwalker.OrbSettings.CanMove;

        /// <summary>
        /// Maximum movement commands per minute
        /// </summary>
        public static float MovementAPM
        {
            set => Common.Logic.Orbwalker.OrbSettings.MovementAPM = value;
        }

        /// <summary>
        /// Remaining time in seconds before basic attack is possible
        /// </summary>
        public static float AttackCooldownRemaining => Common.Logic.Orbwalker.OrbSettings.AttackCooldownRemaining;


        /// <summary>
        /// Last basic attack in game time
        /// </summary>
        public static float LastBasicAttack => Common.Logic.Orbwalker.OrbSettings.LastBasicAttack;

        /// <summary>
        /// Last move in game time
        /// </summary>
        public static float LastMove => Common.Logic.Orbwalker.OrbSettings.LastMove;

        /// <summary>
        /// Previous attacked minion with orbwalker
        /// </summary>
        public static GameObjectBase PreviousMinion => Common.Logic.Orbwalker.OrbSettings.PreviousMinion;

        /// <summary>
        /// Manually overrided hero targetselection
        /// </summary>
        public static GameObjectBase SelectedHero
        {
            get => Common.Logic.Orbwalker.OrbSettings.SelectedHero;
            set => Common.Logic.Orbwalker.OrbSettings.SelectedHero = value;
        }

        /// <summary>
        /// Manually overrided target 
        /// </summary>
        public static GameObjectBase SelectedTarget
        {
            get => Common.Logic.Orbwalker.OrbSettings.SelectedTarget;
            set => Common.Logic.Orbwalker.OrbSettings.SelectedTarget = value;
        }

        /// <summary>
        /// Current selected laneclear target
        /// </summary>
        public static GameObjectBase LaneClearTarget => Common.Logic.Orbwalker.OrbSettings.LaneClearTarget;

        /// <summary>
        /// Is target champions only being pressed default value F10 while holding space
        /// </summary>
        public static bool TargetChampionsOnly => Common.Logic.Orbwalker.OrbSettings.TargetChampionsOnly;

        /// <summary>
        /// Current selected hero target
        /// </summary>
        public static GameObjectBase TargetHero => Common.Logic.Orbwalker.OrbSettings.TargetHero;

        /// <summary>
        /// Orbwalking mode
        /// </summary>
        public static Common.Logic.OrbwalkingMode OrbwalkingMode
        {
            get => Common.Logic.Orbwalker.OrbSettings.OrbwalkingMode;
            set => Common.Logic.Orbwalker.OrbSettings.OrbwalkingMode = value;
        }

    }
}
