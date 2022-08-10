using Oasys.Common;
using Oasys.Common.Extensions;
using Oasys.Common.GameObject;
using Oasys.Common.Tools;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;
using System;
using System.Windows.Forms;

namespace Oasys.SDK.Rendering
{
    /// <summary>
    /// Rendering for the core.
    /// </summary>
    public class RenderFactory
    {
        /// <summary>
        /// Gets the render device.
        /// </summary>
        public static Device RenderDevice => Common.RenderFactoryProvider.RenderDevice;

        /// <summary>
        /// Gets the D3D9 Font to draw.
        /// </summary>
        public static Font D3D9Font;

        /// <summary>
        /// Gets the D3D9 Line to draw a line.
        /// </summary>
        public static Line D3D9Line;

        /// <summary>
        /// Gets the D3D9 Line to draw a box.
        /// </summary>
        public static Line D3D9BoxLine;

        static RenderFactory()
        {
            D3D9Font = new Font(RenderDevice, new FontDescription()
            {
                FaceName = "Fixedsys Regular",
                CharacterSet = FontCharacterSet.Default,
                Height = 20,
                Weight = FontWeight.Bold,
                MipLevels = 0,
                OutputPrecision = FontPrecision.Default,
                PitchAndFamily = FontPitchAndFamily.Default,
                Quality = FontQuality.ClearType
            });

            D3D9Line = new Line(RenderDevice);
            D3D9BoxLine = new Line(RenderDevice);
        }

        /// <summary>
        /// Generates a new font with the given FontDescription parameter.
        /// </summary>
        /// <param name="ftDesc"></param>
        /// <returns>SharpDX.Direct3D9.Font</returns>
        public static Font GenerateD3D9Font(FontDescription ftDesc)
        {
            return new Font(RenderDevice, ftDesc);
        }

        /// <summary>
        /// Draws a text on the screen.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="FontSize"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="centered"></param>
        public static void DrawText(string text, int FontSize, Vector2 position, Color color, bool centered = true)
        {
            try
            {
                var _font = D3D9Font;
                var FontDescription = _font.Description;
                FontDescription.Height = FontSize;

                var fontDimension = _font.MeasureText(null, text, new Rectangle((int)position.X, (int)position.Y, 0, 0), centered ? (FontDrawFlags.Center | FontDrawFlags.VerticalCenter) : FontDrawFlags.Left);
                _font.DrawText(null, text, fontDimension, FontDrawFlags.Center | FontDrawFlags.VerticalCenter, color);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Draws a text on the screen.
        /// </summary>
        /// <param name="providedFont"></param>
        /// <param name="text"></param>
        /// <param name="FontSize"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="centered"></param>
        public static void DrawText(Font providedFont, string text, int FontSize, Vector2 position, Color color, bool centered = true)
        {
            var fontDimension = providedFont.MeasureText(null, text, new Rectangle((int)position.X, (int)position.Y, 0, 0), centered ? (FontDrawFlags.Center | FontDrawFlags.VerticalCenter) : FontDrawFlags.Left);
            providedFont.DrawText(null, text, fontDimension, FontDrawFlags.Center | FontDrawFlags.VerticalCenter, color);
        }

        /// <summary>
        /// Draws a line on the screen.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="w"></param>
        /// <param name="Color"></param>
        public static void DrawLine(float x1, float y1, float x2, float y2, float w, Color Color)
        {
            D3D9Line.Width = w;
            D3D9Line.Antialias = false;
            D3D9Line.GLLines = false;

            RawVector2[] vertices =
            {
                new RawVector2(x1, y1),
                new RawVector2(x2, y2)
            };

            D3D9Line.Begin();
            D3D9Line.Draw(vertices, new RawColorBGRA(Color.B, Color.G, Color.R, Color.A));
            D3D9Line.End();
        }



        /// <summary>
        /// Draws a straight hollowed boxed line.
        /// </summary>
        /// <param name="sPos"></param>
        /// <param name="ePos"></param>
        /// <param name="sWidth"></param>
        /// <param name="cl"></param>
        public static void DrawSpellBoxedLine(Vector2 sPos, Vector2 ePos, float sWidth, Color cl)
        {
            var posDir = new float[2] { sPos.X - ePos.X, sPos.Y - ePos.Y };

            var dist = System.Math.Sqrt(posDir[0] * posDir[0] + posDir[1] * posDir[1]);

            posDir[0] /= (float)dist; //dirX /=
            posDir[1] /= (float)dist; //dirY /=

            var sLeft = new Vector2(sPos.X + sWidth / 2 * posDir[1], sPos.Y - sWidth / 2 * posDir[0]);
            var sRight = new Vector2(sPos.X - sWidth / 2 * posDir[1], sPos.Y + sWidth / 2 * posDir[0]);

            var eLeft = new Vector2(ePos.X + sWidth / 2 * posDir[1], ePos.Y - sWidth / 2 * posDir[0]);
            var eRight = new Vector2(ePos.X - sWidth / 2 * posDir[1], ePos.Y + sWidth / 2 * posDir[0]);

            DrawLine(sLeft.X, sLeft.Y, sRight.X, sRight.Y, 3, cl); //Base start
            DrawLine(eLeft.X, eLeft.Y, eRight.X, eRight.Y, 3, cl); //Base end
            DrawLine(sLeft.X, sLeft.Y, eLeft.X, eLeft.Y, 3, cl); // Left line
            DrawLine(sRight.X, sRight.Y, eRight.X, eRight.Y, 3, cl); //Right line
        }

        /// <summary>
        /// Draws a box/rectangle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="color"></param>
        public static void DrawBox(float x, float y, float w, float h, Color color)
        {
            Vector2[] vertices =
            {
                new Vector2(x, y),
                new Vector2(x + w, y),
                new Vector2(x + w, y + h),
                new Vector2(x, y + h),
                new Vector2(x, y)
            };

            D3D9BoxLine.Begin();
            D3D9BoxLine.Draw(vertices, color);
            D3D9BoxLine.End();
        }

        /// <summary>
        /// Draws a box/rectangle from the middle point of the vector position given.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public static void DrawBoxMidPoint(Vector2 pos, float width, float height, Color color)
        {
            DrawBox(pos.X - width, pos.Y - height, width * 2, height * 2, color);
        }

        /// <summary>
        /// Draws a native circle relative to the game position and viewmatrix.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="filled"></param>
        public static void DrawNativeCircle(Vector3 position, float radius, Color color, float thickness, bool filled = false)
        {
            Common.RenderFactoryProvider.DrawCircle(position, radius, color, thickness, filled);
        }

        /// <summary>
        /// Draw circle on screen position
        /// </summary>
        public static void DrawCircle(Vector2 mapPos, float radius, Color color, float thickness)
        {
            var line = new Vector2[360];

            for (var i = 0; i < 360; i++)
            {
                var rad = i * Math.PI / 180;
                var radCos = Math.Cos(rad);
                var radSin = Math.Sin(rad);
                line[i].X = mapPos.X + (float)radCos * radius;
                line[i].Y = mapPos.Y + (float)radSin * radius;
            }

            D3D9Line.Width = thickness;
            D3D9Line.Begin();
            D3D9Line.Draw(line, new RawColorBGRA(color.B, color.G, color.R, color.A));
            D3D9Line.End();
        }

        /// <summary>
        /// Draw circle with boundaries that will be drawn if the circle hits the boundary.
        /// StartingPos is top-left pos of the box.
        /// MaxSize is the box size down-right from StartingPos.
        /// </summary>
        public static void DrawCircle(Vector2 mapPos, float radius, Color color, float thickness, Vector2 startingPos, Vector2 maxSize)
        {
            var line = new Vector2[360];

            var minimumPos = new Vector2(startingPos.X + maxSize.X, startingPos.Y + maxSize.Y);
            for (var i = 0; i < 360; i++)
            {
                var rad = i * Math.PI / 180;
                var radCos = Math.Cos(rad);
                var radSin = Math.Sin(rad);
                line[i].X = mapPos.X + (float)radCos * radius;
                line[i].Y = mapPos.Y + (float)radSin * radius;
                if (startingPos.X > line[i].X)
                {
                    line[i].X = startingPos.X;
                }
                if (minimumPos.X < line[i].X)
                {
                    line[i].X = minimumPos.X;
                }
                if (startingPos.Y > line[i].Y)
                {
                    line[i].Y = startingPos.Y;
                }
                if (minimumPos.Y < line[i].Y)
                {
                    line[i].Y = minimumPos.Y;
                }
            }

            D3D9Line.Width = thickness;
            D3D9Line.Begin();
            D3D9Line.Draw(line, new RawColorBGRA(color.B, color.G, color.R, color.A));
            D3D9Line.End();
        }

        /// <summary>
        /// Draw image
        /// </summary>
        public static void DrawImage(Image image)
        {
            image.Sprite.Begin();
            RenderFactoryProvider.DrawTransformSprite(image.Sprite, image.Texture, image.Color, image.Position, image.Scale);
            image.Sprite.End();
        }

        /// <summary>
        /// Draw HP bar damage.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damage"></param>
        public static void DrawHPBarDamage(GameObjectBase target, float damage)
        {
            var col = new Color
            {
                A = 155,
                R = 255,
                G = 165,
                B = 0
            };

            DrawHPBarDamage(target, damage, col);
        }

        /// <summary>
        /// Draw HP bar damage.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damage"></param>
        public static void DrawHPBarDamage(GameObjectBase target, float damage, Color color)
        {
            if (target is null || !target.W2S.IsValid() || !target.IsVisible || damage <= 1)
            {
                return;
            }

            var resolution = Screen.PrimaryScreen.Bounds;
            var isHero = target.IsObject(Common.Enums.GameEnums.ObjectTypeFlag.AIHeroClient);
            var isJungle = false;
            var isJungleBuff = false;
            var isCrab = false;
            var isDragon = false;
            var isBaron = false;
            var isHerald = false;
            if (target.UnitComponentInfo.SkinName.Contains("SRU_Krug", StringComparison.OrdinalIgnoreCase) ||
                target.UnitComponentInfo.SkinName.Contains("SRU_Gromp", StringComparison.OrdinalIgnoreCase) ||
                target.UnitComponentInfo.SkinName.Equals("SRU_Murkwolf", StringComparison.OrdinalIgnoreCase) ||
                target.UnitComponentInfo.SkinName.Contains("Super", StringComparison.OrdinalIgnoreCase) ||
                target.UnitComponentInfo.SkinName.Equals("SRU_Razorbeak", StringComparison.OrdinalIgnoreCase))
            {
                isJungle = true;
            }
            if (target.UnitComponentInfo.SkinName.Contains("SRU_Red", StringComparison.OrdinalIgnoreCase) ||
                target.UnitComponentInfo.SkinName.Contains("SRU_Blue", StringComparison.OrdinalIgnoreCase))
            {
                isJungleBuff = true;
            }
            if (target.UnitComponentInfo.SkinName.Contains("SRU_Crab", StringComparison.OrdinalIgnoreCase))
            {
                isCrab = true;
            }
            if (target.UnitComponentInfo.SkinName.Contains("SRU_Baron", StringComparison.OrdinalIgnoreCase))
            {
                isBaron = true;
            }
            if (target.UnitComponentInfo.SkinName.Contains("SRU_Dragon", StringComparison.OrdinalIgnoreCase))
            {
                isDragon = true;
            }
            if (target.UnitComponentInfo.SkinName.Contains("SRU_RiftHerald", StringComparison.OrdinalIgnoreCase))
            {
                isHerald = true;
            }

            var barWidth = resolution.Width switch
            {
                >= 2560 => isHero ? 125 :
                           isBaron ? 200 :
                           isDragon ? 170 :
                           isHerald ? 170 :
                           isCrab ? 170 :
                           isJungleBuff ? 170 :
                           isJungle ? 110 :
                           75,
                >= 1920 => isHero ? 105 :
                           isBaron ? 175 :
                           isDragon ? 150 :
                           isHerald ? 150 :
                           isCrab ? 150 :
                           isJungleBuff ? 145 :
                           isJungle ? 95 :
                           60,
                _ =>       isHero ? 105 :
                           isBaron ? 175 :
                           isDragon ? 150 :
                           isHerald ? 150 :
                           isCrab ? 150 :
                           isJungleBuff ? 145 :
                           isJungle ? 95 :
                           60,
            };
            var barHeight = resolution.Height switch
            {
                >= 1440 => isHero ? 13 :
                           isBaron ? 13 :
                           isDragon ? 13 :
                           isHerald ? 13 :
                           isCrab ? 13 :
                           isJungleBuff ? 13 :
                           isJungle ? 4 :
                           4,
                >= 1080 => isHero ? 10 :
                           isBaron ? 10 :
                           isDragon ? 10 :
                           isHerald ? 10 :
                           isCrab ? 10 :
                           isJungleBuff ? 10 :
                           isJungle ? 3 :
                           3,
                _ =>       isHero ? 10 :
                           isBaron ? 10 :
                           isDragon ? 10 :
                           isHerald ? 10 :
                           isCrab ? 10 :
                           isJungleBuff ? 10 :
                           isJungle ? 3 :
                           3,
            };
            var healthBarOffset = resolution.Width switch
            {
                >= 2560 => isHero ? new Vector2(-14, -22) :
                           isBaron ? new Vector2(-60, -28) :
                           isDragon ? new Vector2(-32, -70) :
                           isHerald ? new Vector2(-45, -22) :
                           isCrab ? new Vector2(-45, -22) :
                           isJungleBuff ? new Vector2(-45, -22) :
                           isJungle ? new Vector2(-14, -4) :
                           new Vector2(3, -4),
                >= 1920 => isHero ? new Vector2(-6, -18) :
                           isBaron ? new Vector2(-50, -22) :
                           isDragon ? new Vector2(-32, -55) :
                           isHerald ? new Vector2(-35, -18) :
                           isCrab ? new Vector2(-35, -18) :
                           isJungleBuff ? new Vector2(-35, -18) :
                           isJungle ? new Vector2(-6, -4) :
                           new Vector2(8, -4),
                _ =>       isHero ? new Vector2(-6, -18) :
                           isBaron ? new Vector2(-50, -22) :
                           isDragon ? new Vector2(-32, -55) :
                           isHerald ? new Vector2(-35, -18) :
                           isCrab ? new Vector2(-35, -18) :
                           isJungleBuff ? new Vector2(-35, -18) :
                           isJungle ? new Vector2(-6, -4) :
                           new Vector2(8, -4),
            };

            var pos = target.HealthBarScreenPosition;
            pos += healthBarOffset;

            var end = pos;
            end.X += barWidth * Math.Max(float.Epsilon, target.Health / target.MaxHealth);

            var start = pos;
            var dmgPercent = Math.Max(float.Epsilon, target.Health - damage) / target.MaxHealth;
            start.X += barWidth * dmgPercent;

            //var length = pos.Extend(end, barWidth);
            //DrawLine(pos.X, pos.Y, length.X, length.Y, barHeight, color);
            DrawLine(start.X, start.Y, end.X, end.Y, barHeight, color);
        }
    }
}
