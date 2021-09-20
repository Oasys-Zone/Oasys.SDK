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
        /// Enable/disable the orbwalker to being able to attack
        /// </summary>
        public static bool AllowAttacking
        {
            get => Common.Logic.Orbwalker.OrbSettings.AllowAttacking;
            set => Common.Logic.Orbwalker.OrbSettings.AllowAttacking = value;
        }

        /// <summary>
        /// Enable/disable the orbwalker to being able to move
        /// </summary>
        public static bool AllowMoving
        {
            get => Common.Logic.Orbwalker.OrbSettings.AllowMoving;
            set => Common.Logic.Orbwalker.OrbSettings.AllowMoving = value;
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
        public static OrbWalkingModeType OrbwalkingMode
        {
            get => (OrbWalkingModeType)Common.Logic.Orbwalker.OrbSettings.OrbwalkingMode;
            set => Common.Logic.Orbwalker.OrbSettings.OrbwalkingMode = (Common.Logic.OrbwalkingMode)value;
        }

        public enum OrbWalkingModeType
        {
            /// <summary>
            ///     The orbwalker will only last hit minions.
            /// </summary>
            LastHit = Common.Logic.OrbwalkingMode.LastHit,

            /// <summary>
            ///     The orbwalker will alternate between last hitting and auto attacking champions.
            /// </summary>
            Mixed = Common.Logic.OrbwalkingMode.Mixed,

            /// <summary>
            ///     The orbwalker will clear the lane of minions as fast as possible while attempting to get the last hit. But also targetting champions, monsters, turrrets, etc...
            /// </summary>
            LaneClear = Common.Logic.OrbwalkingMode.LaneClear,

            /// <summary>
            ///     The orbwalker will only attack champions.
            /// </summary>
            Combo = Common.Logic.OrbwalkingMode.Combo,

            /// <summary>
            ///     The orbwalker will only last hit minions as late as possible.
            /// </summary>
            Freeze = Common.Logic.OrbwalkingMode.Freeze,

            /// <summary>
            ///     The orbwalker will only move.
            /// </summary>
            Move = Common.Logic.OrbwalkingMode.Move,

            /// <summary>
            ///     The orbwalker does nothing.
            /// </summary>
            None = Common.Logic.OrbwalkingMode.None,

            /// <summary>
            ///     The orbwalker will not attack while evading.
            /// </summary>
            Evade = Common.Logic.OrbwalkingMode.Evade
        }
    }
}
