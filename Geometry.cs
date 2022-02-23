using Oasys.Common.Extensions;
using SharpDX;
using System;

namespace Oasys.SDK
{
    public static class Geometry
    {
        public static double DistanceFromPointToLine(Vector2 point, Vector2[] Line)
        {
            var l1 = Line[0];
            var l2 = Line[1];

            return Math.Abs((l2.X - l1.X) * (l1.Y - point.Y) - (l1.X - point.X) * (l2.Y - l1.Y)) /
                    Math.Sqrt(Math.Pow(l2.X - l1.X, 2) + Math.Pow(l2.Y - l1.Y, 2));
        }
    }
}
