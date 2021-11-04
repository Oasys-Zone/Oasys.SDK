using Oasys.Common;
using Oasys.Common.Extensions;
using SharpDX;
using System.Linq;

namespace Oasys.SDK
{
    public static class Collision
    {
        public static bool MinionCollision(Vector2 targetPosition, int width)
        {
            foreach (var minion in UnitManager.EnemyMinions.Where(x => x.IsAlive && !x.W2S.IsZero))
            {
                if (IsLineCollision(minion.W2S, new Vector2[] { UnitManager.MyChampion.W2S, targetPosition }, width) && minion.W2S.Distance(targetPosition) < UnitManager.MyChampion.W2S.Distance(targetPosition))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool MinionCollision(Vector3 targetPosition, int width)
        {
            foreach (var minion in UnitManager.EnemyMinions.Where(x => x.IsAlive && !x.W2S.IsZero))
            {
                var w2s = LeagueNativeRendererManager.WorldToScreen(minion.Position);
                if (IsLineCollision(minion.W2S, new Vector2[] { UnitManager.MyChampion.W2S, w2s }, width) && minion.Position.Distance(targetPosition) < UnitManager.MyChampion.Position.Distance(targetPosition))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsLineCollision(Vector2 pos1, Vector2[] line, int spellWidth)
        {
            return Geometry.DistanceFromPointToLine(pos1, line) <= spellWidth;
        }
    }
}
