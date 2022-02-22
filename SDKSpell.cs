using System;
using System.Collections.Generic;
using System.Linq;
using Oasys.Common.Enums.GameEnums;
using Oasys.Common.Extensions;
using Oasys.Common.GameObject;
using Oasys.Common.GameObject.Clients.ExtendedInstances.Spells;
using Oasys.Common.Logic;
using Oasys.SDK.SpellCasting;
using SharpDX;
using System.Diagnostics;
using static Oasys.SDK.Prediction.MenuSelected;
using Oasys.SDK.Tools;

namespace Oasys.SDK
{
    /// <summary>
    /// SDKSpell instance.
    /// </summary>
    public class SDKSpell
    {
        /// <summary>
        /// Raised when a spell is casted by Oasys.
        /// </summary>
        public static event Action<SDKSpell, GameObjectBase> OnSpellCast;

        /// <summary>
        /// SDKSpell instance.
        /// </summary>
        /// <param name="castSlot">The castslot.</param>
        /// <param name="spellSlot">The spellslot.</param>
        public SDKSpell(CastSlot castSlot, SpellSlot spellSlot)
        {
            CastSlot = castSlot;
            SpellSlot = spellSlot;
        }

        /// <summary>
        /// The spell class from spellbook.
        /// </summary>
        public SpellClass SpellClass => UnitManager.MyChampion.GetSpellBook().GetSpellClass(SpellSlot);

        /// <summary>
        /// The castslot.
        /// </summary>
        public CastSlot CastSlot { get; }

        /// <summary>
        /// The spellslot.
        /// </summary>
        public SpellSlot SpellSlot { get; }

        /// <summary>
        /// Defines if a spell is ready. Standard value = spellClass.IsSpellReady && UnitManager.MyChampion.Mana >= spellClass.SpellData.ResourceCost && UnitManager.MyChampion.Mana >= minimumMana.
        /// </summary>
        public Func<SpellClass, float, int, bool> IsSpellReady { get; set; }
            = (spellClass, minimumMana, minimumCharges)
            => spellClass.IsSpellReady &&
               UnitManager.MyChampion.Mana >= spellClass.SpellData.ResourceCost &&
               UnitManager.MyChampion.Mana >= minimumMana &&
               spellClass.Charges >= minimumCharges;

        /// <summary>
        /// The minimum mana for a spell to be used. Standard value = 0.
        /// </summary>
        public Func<float> MinimumMana { get; set; } = () => 0f;

        /// <summary>
        /// The minimum charges for a spell to be used. Standard value = 0.
        /// </summary>
        public Func<int> MinimumCharges { get; set; } = () => 0;

        /// <summary>
        /// Defines if a spell is enabled. Fx: enabled by menu. Standard value = true.
        /// </summary>
        public Func<bool> IsEnabled { get; set; } = () => true;

        /// <summary>
        /// Allow to cast the spell in a direction. Standard value = false.
        /// </summary>
        public Func<bool> AllowCastInDirection { get; set; } = () => false;

        /// <summary>
        /// Allow to cast the spell on minimap. Standard value = false.
        /// </summary>
        public Func<bool> AllowCastOnMap { get; set; } = () => false;

        /// <summary>
        /// Is a targetted spell. Standard value = false.
        /// </summary>
        public Func<bool> IsTargetted { get; set; } = () => false;

        /// <summary>
        /// Is a charge spell. Standard value = false.
        /// </summary>
        public Func<bool> IsCharge { get; set; } = () => false;

        /// <summary>
        /// The spell range. Standard value = 0.
        /// </summary>
        public Func<float> Range { get; set; } = () => 0f;

        /// <summary>
        /// The spell speed. Standard value = 0.
        /// </summary>
        public Func<float> Speed { get; set; } = () => 0f;

        /// <summary>
        /// The spell radius/width/angle. Standard value = 0.
        /// </summary>
        public Func<float> Radius { get; set; } = () => 0f;

        /// <summary>
        /// The spell delay/animation time/cast time. Standard value = 0.25.
        /// </summary>
        public Func<float> Delay { get; set; } = () => 0.25f;

        /// <summary>
        /// The position to cast from. Standard value = UnitManager.MyChampion.AIManager.ServerPosition.
        /// </summary>
        public Func<Vector3> From { get; set; } = () => UnitManager.MyChampion.AIManager.ServerPosition;

        /// <summary>
        /// The minimum hit chance. Standard value = HitChance.High.
        /// </summary>
        public Func<HitChance> MinimumHitChance { get; set; } = () => HitChance.High;

        /// <summary>
        /// The prediction type/mode. Standard value = PredictionType.Line.
        /// </summary>
        public Func<PredictionType> PredictionMode { get; set; } = () => PredictionType.Line;

        /// <summary>
        /// Should allow collision or not. Standard value = true.
        /// </summary>
        public Func<GameObjectBase, IEnumerable<GameObjectBase>, bool> AllowCollision { get; set; } = (target, collisions) => true;

        /// <summary>
        /// Should cast spell. Standard value = target is not null.
        /// </summary>
        public Func<Orbwalker.OrbWalkingModeType, GameObjectBase, SpellClass, float, bool> ShouldCast { get; set; } = (mode, target, spellClass, damage) => target is not null;

        /// <summary>
        /// Target selection. Standard value = null.
        /// </summary>
        public Func<Orbwalker.OrbWalkingModeType, GameObjectBase> TargetSelect { get; set; } = (mode) => null;

        /// <summary>
        /// Damage calculation. Standard value = 0.
        /// </summary>
        public Func<GameObjectBase, SpellClass, float> Damage { get; set; } = (target, spellClass) => 0f;

        /// <summary>
        /// Render method. Fx: Karthus ult can kill x, y, z.
        /// </summary>
        public Action RenderSpellUsage { get; set; }

        /// <summary>
        /// Chargetimer to control chargespell time.
        /// </summary>
        public virtual Stopwatch ChargeTimer { get; } = new();

        /// <summary>
        /// Returns true if ChargeTimer is running.
        /// </summary>
        public virtual bool IsCharging() => ChargeTimer.IsRunning;

        /// <summary>
        /// Get prediction.
        /// </summary>
        /// <param name="target">The target to predict.</param>
        /// <returns>Prediction result.</returns>
        public virtual PredictionOutput GetPrediction(GameObjectBase target) => Prediction.MenuSelected.GetPrediction(PredictionMode(), target, Range(), Radius(), Delay(), Speed(), From());

        /// <summary>
        /// Get available targets that is possible to hit based on spell data.
        /// </summary>
        /// <param name="mode">Input mode.</param>
        /// <param name="predicate">Predicate to check for each target.</param>
        /// <returns>Available targets.</returns>
        public virtual IEnumerable<GameObjectBase> GetTargets(Orbwalker.OrbWalkingModeType mode, Func<GameObjectBase, bool> predicate = null)
        {
            var enemies = new List<GameObjectBase>();
            if (mode == Orbwalker.OrbWalkingModeType.Combo)
            {
                enemies.AddRange(UnitManager.EnemyChampions);
            }
            else if (mode == Orbwalker.OrbWalkingModeType.LaneClear || mode == Orbwalker.OrbWalkingModeType.Mixed)
            {
                enemies.AddRange(UnitManager.EnemyMinions);
                enemies.AddRange(UnitManager.EnemyJungleMobs);
                enemies.AddRange(UnitManager.EnemyChampions);
            }
            else if (mode == Orbwalker.OrbWalkingModeType.LastHit || mode == Orbwalker.OrbWalkingModeType.Freeze)
            {
                enemies.AddRange(UnitManager.EnemyMinions);
            }

            return enemies.Where(x => x.IsAlive && x.DistanceTo(From()) <= Range() && IsTargetable(x) && (IsTargetted() || IsPossibleToHit(x)))
                          .Where(predicate ?? basePredicate);
        }

        /// <summary>
        /// Determines if a target is targetable.
        /// </summary>
        /// <param name="target">Target to check.</param>
        /// <returns>True if target is targetable.</returns>
        public virtual bool IsTargetable(GameObjectBase target)
        {
            return target.NetworkID >= 0 &&
                    target.Position.IsValid() &&
                    target.Index >= 0 &&
                    target.MaxHealth > 1 &&
                    target.Health > 0 &&
                    target.IsVisible &&
                    target.IsAlive &&
                    target.IsTargetable;
        }

        private static readonly Func<GameObjectBase, bool> basePredicate = (x) => true;

        /// <summary>
        /// Is possible to hit.
        /// </summary>
        /// <param name="target">Target to check.</param>
        /// <returns>True if possible to hit, false if not.</returns>
        public virtual bool IsPossibleToHit(GameObjectBase target) => IsPossibleToHit(target, GetPrediction(target));

        /// <summary>
        /// Is possible to hit.
        /// </summary>
        /// <param name="target">Target to check.</param>
        /// <param name="predictionOutput">Prediction output to validate.</param>
        /// <returns>True if possible to hit, false if not.</returns>
        public virtual bool IsPossibleToHit(GameObjectBase target, PredictionOutput predictionOutput) => predictionOutput.HitChance >= MinimumHitChance() && (!predictionOutput.Collision || AllowCollision(target, predictionOutput.CollisionObjects));

        /// <summary>
        /// Execute spell.
        /// </summary>
        /// <param name="mode">Input mode.</param>
        /// <returns>True if spell is casted/rendered/started to charge/released charge.</returns>
        public virtual bool ExecuteCastSpell(Orbwalker.OrbWalkingModeType mode = Orbwalker.OrbWalkingModeType.Combo)
        {
            try
            {
                if (UnitManager.MyChampion.IsAlive && (IsCharge() || !UnitManager.MyChampion.IsCastingSpell))
                {
                    if (!IsSpellReady(SpellClass, MinimumMana(), MinimumCharges()))
                    {
                        return false;
                    }
                    if (!IsEnabled())
                    {
                        return false;
                    }
                    var target = TargetSelect(mode);
                    if (!ShouldCast(mode, target, SpellClass, Damage(target, SpellClass)))
                    {
                        return false;
                    }
                    if (RenderSpellUsage != default)
                    {
                        RenderSpellUsage?.Invoke();
                        return true;
                    }
                    if (target == default && Delay() == default && SpellCastProvider.CastSpell(CastSlot))
                    {
                        OnSpellCast?.Invoke(this, target);
                        return true;
                    }
                    else if (target == default && SpellCastProvider.CastSpell(CastSlot, Delay()))
                    {
                        OnSpellCast?.Invoke(this, target);
                        return true;
                    }
                    else if (target != null)
                    {
                        if (IsTargetted())
                        {
                            var w2s = target.W2S;
                            var castPos = w2s.IsValid()
                                            ? w2s
                                            : AllowCastOnMap()
                                                ? target.WorldToMap
                                                : Vector2.Zero;
                            if (castPos.IsValid() && (IsCharge()
                                ? ChargeSpellAtPos(CastSlot, castPos, Delay())
                                : SpellCastProvider.CastSpell(CastSlot, castPos, Delay())))
                            {
                                OnSpellCast?.Invoke(this, target);
                                return true;
                            }
                        }
                        else
                        {
                            var predictResult = GetPrediction(target);
                            var w2s = predictResult.CastPosition.ToW2S();
                            var castPos = w2s.IsValid()
                                ? w2s
                                : AllowCastOnMap()
                                    ? predictResult.CastPosition.ToWorldToMap()
                                    : AllowCastInDirection()
                                        ? From().Extend(From() + (predictResult.CastPosition - From()).Normalized(), 50).ToW2S()
                                        : Vector2.Zero;
                            if (UnitManager.MyChampion.Position.Distance(predictResult.CastPosition) <= Range() &&
                                IsPossibleToHit(target, predictResult) &&
                                castPos.IsValid() &&
                                (IsCharge()
                                ? ChargeSpellAtPos(CastSlot, castPos, Delay())
                                : SpellCastProvider.CastSpell(CastSlot, castPos, Delay())))
                            {
                                OnSpellCast?.Invoke(this, target);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Release spell at position or start charging spell.
        /// </summary>
        /// <param name="castSlot">The cast slot.</param>
        /// <param name="pos">The w2s cast position.</param>
        /// <param name="castTime">The cast time in seconds.</param>
        /// <returns>True if release spell at position or start charging spell.</returns>
        public virtual bool ChargeSpellAtPos(CastSlot castSlot, Vector2 pos, float castTime)
        {
            if (IsCharging())
            {
                if (SpellCastProvider.ReleaseChargeSpell((SpellCastSlot)castSlot, pos, castTime))
                {
                    ChargeTimer.Stop();
                    return true;
                }
            }
            else
            {
                ChargeTimer.Restart();
                return SpellCastProvider.StartChargeSpell((SpellCastSlot)CastSlot);
            }

            return false;
        }
    }
}
