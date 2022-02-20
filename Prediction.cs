using Oasys.Common.GameObject;
using Oasys.Common.Menu;
using Oasys.Common.Menu.ItemComponents;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oasys.SDK
{
    /// <summary>
    /// Spell Prediction.
    /// </summary>
    public class Prediction
    {
        public class MenuSelected
        {
            public enum HitChance
            {
                Impossible = -2,
                OutOfRange = -1,
                Unknown = 0,
                Low = 1,
                Medium = 2,
                High = 3,
                VeryHigh = 4,
                Dashing = 5,
                Immobile = 6,
            }

            public enum PredictionMode
            {
                LS,
                EB
            }

            public enum PredictionType
            {
                Line,
                Circle,
                Cone
            }

            /// <summary>
            /// The prediction output.
            /// </summary>
            /// <param name="UnitPosition">The position of unit.</param>
            /// <param name="CastPosition">The position to cast.</param>
            /// <param name="HitChance">The hitchance.</param>
            /// <param name="CollisionObjects">The collision objects.</param>
            public record PredictionOutput(Vector3 UnitPosition, Vector3 CastPosition, HitChance HitChance, List<GameObjectBase> CollisionObjects)
            {
                /// <summary>
                /// CollisionObjects has any items.
                /// </summary>
                public bool Collision => CollisionObjects?.Count > 0;
            }

            /// <summary>
            /// The prediction input.
            /// </summary>
            /// <param name="Type">The prediction type.</param>
            /// <param name="Target">The target to predict.</param>
            /// <param name="Range">The spell range.</param>
            /// <param name="Radius">The spell width/radius/angle.</param>
            /// <param name="Delay">The delay/cast time/animation time in seconds.</param>
            /// <param name="Speed">The speed in units per second.</param>
            /// <param name="SourcePosition">The source position. Ex: Victor's E can be placed somewhere else. Normally source position is Localplayer position.</param>
            /// <param name="CollisionCheck">The CollisionCheck.</param>
            public record PredictionInput(PredictionType Type, GameObjectBase Target, float Range, float Radius, float Delay, float Speed, Vector3 SourcePosition, bool CollisionCheck);

            private static PredictionMode MenuMode
            {
                get => (PredictionMode)Enum.Parse(typeof(PredictionMode), MenuManagerProvider.GetTab("SDK Prediction").GetItem<ModeDisplay>("Mode").SelectedModeName);
                set => MenuManagerProvider.GetTab("SDK Prediction").GetItem<ModeDisplay>("Mode").SelectedModeName = value.ToString();
            }

            /// <summary>
            /// Get a prediction result based on prediction input and the menu selected SDK prediction mode.
            /// </summary>
            /// <param name="type">The prediction type.</param>
            /// <param name="target">The target to predict.</param>
            /// <param name="range">The spell range.</param>
            /// <param name="radius">The spell width/radius/angle.</param>
            /// <param name="delay">The delay/cast time/animation time in seconds.</param>
            /// <param name="speed">The speed in units per second.</param>
            /// <param name="collisionCheck">The CollisionCheck.</param>
            /// <returns>The prediction output.</returns>
            public static PredictionOutput GetPrediction(PredictionType type, GameObjectBase target, float range, float radius, float delay, float speed, bool collisionCheck = true)
                => GetPrediction(new PredictionInput(type, target, range, radius, delay, speed, UnitManager.MyChampion.AIManager.ServerPosition, collisionCheck));

            /// <summary>
            /// Get a prediction result based on prediction input and the menu selected SDK prediction mode.
            /// </summary>
            /// <param name="type">The prediction type.</param>
            /// <param name="target">The target to predict.</param>
            /// <param name="range">The spell range.</param>
            /// <param name="radius">The spell width/radius/angle.</param>
            /// <param name="delay">The delay/cast time/animation time in seconds.</param>
            /// <param name="speed">The speed in units per second.</param>
            /// <param name="sourcePosition">The source position. Ex: Victor's E can be placed somewhere else. Normally source position is Localplayer position.</param>
            /// <param name="collisionCheck">The CollisionCheck.</param>
            /// <returns>The prediction output.</returns>
            public static PredictionOutput GetPrediction(PredictionType type, GameObjectBase target, float range, float radius, float delay, float speed, Vector3 sourcePosition, bool collisionCheck = true)
                => GetPrediction(new PredictionInput(type, target, range, radius, delay, speed, sourcePosition, collisionCheck));

            /// <summary>
            /// Get a prediction result based on prediction input and the menu selected SDK prediction mode.
            /// </summary>
            /// <param name="input">The prediction input.</param>
            /// <returns>The prediction output.</returns>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public static PredictionOutput GetPrediction(PredictionInput input) => MenuMode switch
            {
                PredictionMode.LS => GetLSPrediction(input),
                PredictionMode.EB => GetEBPrediction(input),
                _ => throw new ArgumentOutOfRangeException(nameof(MenuMode)),
            };

            private static PredictionOutput GetLSPrediction(PredictionInput input)
            {
                var lsPredictionType = input.Type switch
                {
                    PredictionType.Line => Common.Logic.LS.SkillshotType.SkillshotLine,
                    PredictionType.Circle => Common.Logic.LS.SkillshotType.SkillshotCircle,
                    PredictionType.Cone => Common.Logic.LS.SkillshotType.SkillshotCone,
                    _ => Common.Logic.LS.SkillshotType.SkillshotLine,
                };
                var predictionOutput = LS.GetPrediction(new Common.Logic.LS.PredictionInput
                {
                    Type = lsPredictionType,
                    Unit = input.Target,
                    Range = input.Range,
                    Speed = input.Speed,
                    Radius = input.Radius,
                    Delay = input.Delay,
                    From = input.SourcePosition
                });
                var hitChance = predictionOutput.Hitchance switch
                {
                    Common.Logic.LS.HitChance.Immobile => HitChance.Immobile,
                    Common.Logic.LS.HitChance.Dashing => HitChance.Dashing,
                    Common.Logic.LS.HitChance.VeryHigh => HitChance.VeryHigh,
                    Common.Logic.LS.HitChance.High => HitChance.High,
                    Common.Logic.LS.HitChance.Medium => HitChance.Medium,
                    Common.Logic.LS.HitChance.Low => HitChance.Low,
                    Common.Logic.LS.HitChance.Impossible => HitChance.Impossible,
                    Common.Logic.LS.HitChance.OutOfRange => HitChance.OutOfRange,
                    _ => HitChance.Unknown,
                };
                return new PredictionOutput(predictionOutput.UnitPosition, predictionOutput.CastPosition, hitChance, predictionOutput.CollisionObjects);
            }

            private static PredictionOutput GetEBPrediction(PredictionInput input)
            {
                var ebPredictionType = input.Type switch
                {
                    PredictionType.Line => Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Linear,
                    PredictionType.Circle => Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular,
                    PredictionType.Cone => Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Cone,
                    _ => Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Linear,
                };
                var predictionData = new Common.Logic.EB.Prediction.Position.PredictionData(ebPredictionType, (int)input.Range, (int)input.Radius, (int)input.Radius, (int)(input.Delay * 1000f), (int)input.Speed, 0, input.SourcePosition);
                var predictionOutput = EB.GetPrediction(input.Target, predictionData);
                var hitChance = predictionOutput.HitChance switch
                {
                    Common.Logic.EB.HitChance.Unknown => HitChance.Unknown,
                    Common.Logic.EB.HitChance.Impossible => HitChance.Impossible,
                    Common.Logic.EB.HitChance.AveragePoint => HitChance.Medium,
                    Common.Logic.EB.HitChance.Immobile => HitChance.Immobile,
                    Common.Logic.EB.HitChance.Dashing => HitChance.Dashing,
                    Common.Logic.EB.HitChance.High => HitChance.VeryHigh,
                    Common.Logic.EB.HitChance.Medium => HitChance.High,
                    Common.Logic.EB.HitChance.Low => HitChance.Low,
                    _ => HitChance.Unknown,
                };
                return new PredictionOutput(predictionOutput.UnitPosition, predictionOutput.CastPosition, hitChance, predictionOutput.CollisionObjects.ToList());
            }
        }

        /// <summary>
        /// Ported LeagueSharp Prediction.
        /// </summary>
        public static class LS
        {
            /// <summary>
            /// Predict linear cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictLinearMissile(GameObjectBase target, float range, int radius, float delay, float speed, Vector3 sourcePosition)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotLine, Unit = target, Range = range, Radius = radius, Delay = delay, Speed = speed, From = sourcePosition };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict linear cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictLinearMissile(GameObjectBase target, float range, float radius, float delay, float speed, Vector3 sourcePosition)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotLine, Unit = target, Range = range, Radius = radius, Delay = delay, Speed = speed, From = sourcePosition };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict linear cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictLinearMissile(GameObjectBase target, float range, int radius, float delay, float speed)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotLine, Unit = target, Range = range, Radius = radius, Delay = delay, Speed = speed };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict linear cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictLinearMissile(GameObjectBase target, float range, float radius, float delay, float speed)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotLine, Unit = target, Range = range, Radius = radius, Delay = delay, Speed = speed };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict circular cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictCircularMissile(GameObjectBase target, float range, int radius, float delay, float speed, Vector3 sourcePosition)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotCircle, Unit = target, Range = range, Radius = radius, Delay = delay, Speed = speed, From = sourcePosition };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict circular cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictCircularMissile(GameObjectBase target, float range, float radius, float delay, float speed, Vector3 sourcePosition)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotCircle, Unit = target, Range = range, Radius = radius, Delay = delay, Speed = speed, From = sourcePosition };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict circular cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictCircularMissile(GameObjectBase target, float range, int radius, float delay, float speed)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotCircle, Unit = target, Range = range, Radius = radius, Delay = delay, Speed = speed };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict circular cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictCircularMissile(GameObjectBase target, float range, float radius, float delay, float speed)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotCircle, Unit = target, Range = range, Radius = radius, Delay = delay, Speed = speed };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict cone cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictConeSpell(GameObjectBase target, float range, int angle, float delay, float speed, Vector3 sourcePosition)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotCone, Unit = target, Range = range, Radius = angle, Delay = delay, Speed = speed, From = sourcePosition };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict cone cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictConeSpell(GameObjectBase target, float range, float angle, float delay, float speed, Vector3 sourcePosition)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotCone, Unit = target, Range = range, Radius = angle, Delay = delay, Speed = speed, From = sourcePosition };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict cone cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictConeSpell(GameObjectBase target, float range, int angle, float delay, float speed)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotCone, Unit = target, Range = range, Radius = angle, Delay = delay, Speed = speed };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            /// Predict cone cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput PredictConeSpell(GameObjectBase target, float range, float angle, float delay, float speed)
            {
                var input = new Common.Logic.LS.PredictionInput() { Type = Common.Logic.LS.SkillshotType.SkillshotCone, Unit = target, Range = range, Radius = angle, Delay = delay, Speed = speed };
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }

            /// <summary>
            ///     Gets the prediction.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput GetPrediction(GameObjectBase target, float delay)
            {
                return GetPrediction(new Common.Logic.LS.PredictionInput { Unit = target, Delay = delay });
            }

            /// <summary>
            ///     Gets the prediction.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput GetPrediction(GameObjectBase target, float delay, float radius)
            {
                return GetPrediction(new Common.Logic.LS.PredictionInput { Unit = target, Delay = delay, Radius = radius });
            }

            /// <summary>
            ///     Gets the prediction.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput GetPrediction(GameObjectBase target, float delay, float radius, float speed)
            {
                return GetPrediction(new Common.Logic.LS.PredictionInput { Unit = target, Delay = delay, Radius = radius, Speed = speed });
            }

            /// <summary>
            ///     Gets the prediction.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="collisionable">The collisionable object types.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput GetPrediction(GameObjectBase target, float delay, float radius, float speed, Common.Logic.LS.CollisionableObjects[] collisionable)
            {
                return GetPrediction(new Common.Logic.LS.PredictionInput { Unit = target, Delay = delay, Radius = radius, Speed = speed, CollisionObjects = collisionable });
            }

            /// <summary>
            ///     Gets the prediction.
            /// </summary>
            /// <param name="input">The prediction data to predict.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.LS.PredictionOutput GetPrediction(Common.Logic.LS.PredictionInput input)
            {
                return Common.Logic.LS.Prediction.GetPrediction(input);
            }
        }

        /// <summary>
        /// Ported EloBuddy Prediction.
        /// </summary>
        public class EB
        {
            /// <summary>
            /// Predict cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="data">The prediction data to predict.</param>
            /// <param name="skipCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult GetPrediction(GameObjectBase target, Common.Logic.EB.Prediction.Position.PredictionData data, bool skipCollision = false)
            {
                return Common.Logic.EB.Prediction.Position.GetPrediction(target, data, skipCollision);
            }

            /// <summary>
            /// Predict linear cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in milliseconds/ticks.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="allowedCollisionCount">The allowed collision count.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictLinearMissile(GameObjectBase target, float range, int radius, int delay, float speed, int allowedCollisionCount = 0, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Linear, (int)range, radius, 0, delay, (int)speed, allowedCollisionCount, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predict linear cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="allowedCollisionCount">The allowed collision count.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictLinearMissile(GameObjectBase target, float range, int radius, float delay, float speed, int allowedCollisionCount = 0, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Linear, (int)range, radius, 0, delay, (int)speed, allowedCollisionCount, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predict linear cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="allowedCollisionCount">The allowed collision count.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictLinearMissile(GameObjectBase target, float range, float radius, float delay, float speed, int allowedCollisionCount = 0, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Linear, (int)range, (int)radius, 0, delay, (int)speed, allowedCollisionCount, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predict circular cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in milliseconds/ticks.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictCircularMissile(GameObjectBase target, float range, int radius, int delay, float speed, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predict circular cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictCircularMissile(GameObjectBase target, float range, int radius, float delay, float speed, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predict circular cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictCircularMissile(GameObjectBase target, float range, float radius, float delay, float speed, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, (int)radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predict cone cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in milliseconds/ticks.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictConeSpell(GameObjectBase target, float range, int angle, int delay, float speed, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Cone, (int)range, 0, angle, delay, (int)speed, -1, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predict cone cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictConeSpell(GameObjectBase target, float range, int angle, float delay, float speed, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Cone, (int)range, 0, angle, delay, (int)speed, -1, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predict cone cast position and collision for a specific target.
            /// </summary>
            /// <param name="target">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <param name="ignoreCollision">Skip collision checks.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult PredictConeSpell(GameObjectBase target, float range, float angle, float delay, float speed, Vector3? sourcePosition = null, bool ignoreCollision = false)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Cone, (int)range, 0, (int)angle, delay, (int)speed, -1, sourcePosition);
                return GetPrediction(target, data, ignoreCollision);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets"> The targets to predict. If null then the enemy heroes will be chosen instead.</param>
            /// <param name="data"> The prediction data to predict.</param>
            public static Common.Logic.EB.PredictionResult[] GetPredictionAoe(GameObjectBase[] targets, Common.Logic.EB.Prediction.Position.PredictionData data)
            {
                return Common.Logic.EB.Prediction.Position.GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in milliseconds/ticks.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictLinearMissileAoe(GameObjectBase[] targets, float range, int radius, int delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Linear, (int)range, radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictLinearMissileAoe(GameObjectBase[] targets, float range, int radius, float delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Linear, (int)range, radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictLinearMissileAoe(GameObjectBase[] targets, float range, float radius, float delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Linear, (int)range, (int)radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in milliseconds/ticks.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictCircularMissileAoe(GameObjectBase[] targets, float range, int radius, int delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictCircularMissileAoe(GameObjectBase[] targets, float range, int radius, float delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The unit that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="radius"> The skillshot width's radius or the angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictCircularMissileAoe(GameObjectBase[] targets, float range, float radius, float delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, (int)radius, 0, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The units that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in milliseconds/ticks.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictConeSpellAoe(GameObjectBase[] targets, float range, int angle, int delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, 0, angle, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The units that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictConeSpellAoe(GameObjectBase[] targets, float range, int angle, float delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, 0, angle, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }

            /// <summary>
            /// Predicts all the possible positions to hit as many targets as possible from a predifined group of targets.
            /// </summary>
            /// <param name="targets">The units that the prediction will made for.</param>
            /// <param name="range">The skillshot range in units.</param>
            /// <param name="angle"> The skillshot angle in case of the cone skillshots.</param>
            /// <param name="delay">The skillshot delay in seconds.</param>
            /// <param name="speed">The skillshot speed in units per second.</param>
            /// <param name="sourcePosition">The position from where the skillshot missile gets fired.</param>
            /// <returns>The output after calculating the prediction.</returns>
            public static Common.Logic.EB.PredictionResult[] PredictConeSpellAoe(GameObjectBase[] targets, float range, float angle, float delay, float speed, Vector3? sourcePosition = null)
            {
                var data = new Common.Logic.EB.Prediction.Position.PredictionData(Common.Logic.EB.Prediction.Position.PredictionData.PredictionType.Circular, (int)range, 0, (int)angle, delay, (int)speed, -1, sourcePosition);
                return GetPredictionAoe(targets, data);
            }
        }
    }
}