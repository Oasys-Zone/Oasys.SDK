using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Mathematics.Interop;
using SharpDX.Direct3D9;

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
            var _font = D3D9Font;
            var FontDescription = _font.Description;
            FontDescription.Height = FontSize;

            var fontDimension = _font.MeasureText(null, text, new Rectangle((int)position.X, (int)position.Y, 0, 0), centered ? (FontDrawFlags.Center | FontDrawFlags.VerticalCenter) : FontDrawFlags.Left);
            _font.DrawText(null, text, fontDimension, FontDrawFlags.Center | FontDrawFlags.VerticalCenter, color);
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
            float[] posDir = new float[2] { (sPos.X - ePos.X), (sPos.Y - ePos.Y) };

            var dist = System.Math.Sqrt(posDir[0] * posDir[0] + posDir[1] * posDir[1]);

            posDir[0] /= (float)dist; //dirX /=
            posDir[1] /= (float)dist; //dirY /=

            Vector2 sLeft = new Vector2(sPos.X + (sWidth / 2) * posDir[1], sPos.Y - (sWidth / 2) * posDir[0]);
            Vector2 sRight = new Vector2(sPos.X - (sWidth / 2) * posDir[1], sPos.Y + (sWidth / 2) * posDir[0]);

            Vector2 eLeft = new Vector2(ePos.X + (sWidth / 2) * posDir[1], ePos.Y - (sWidth / 2) * posDir[0]);
            Vector2 eRight = new Vector2(ePos.X - (sWidth / 2) * posDir[1], ePos.Y + (sWidth / 2) * posDir[0]);

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
        public static void DrawNativeCircle(Vector3 position, float radius, Color color, float thickness, bool filled = false) => Common.RenderFactoryProvider.DrawCircle(position, radius, color, thickness, filled);
    }
}
