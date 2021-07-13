using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasys.Common;
using Oasys.Common.GameInstances;
using Oasys.Common.GameObject.Clients;
using Oasys.Common.Enums.GameEnums;
using Oasys.Common.Tools.Devices;

namespace Oasys.SDK
{
    /// <summary>
    /// Provides information related to the game engine.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// Gets the current game time in milliseconds format.
        /// </summary>
        public static float GameTime => EngineManager.GameTime;

        /// <summary>
        /// Gets the current game version.
        /// </summary>
        public static string GameVersion => EngineManager.GameVersion;

        /// <summary>
        /// Gets the game object that is underneath the mouse.
        /// It will return last cached game object if no object is found.
        /// </summary>
        public static AIBaseClient HoveredGameObjectUnderMouse => EngineManager.ObjectUnderMouse;
        
        /// <summary>
        /// Gets the current game status.
        /// </summary>
        public static GameStatusFlag GameStatus => EngineManager.GameStatus;

        /// <summary>
        /// Gets information about the current game match.
        /// </summary>
        public static MissionInfo InGameInfo => EngineManager.MissionInfo;

        /// <summary>
        /// Gets information about the chatbox.
        /// </summary>
        public static ChatClient ChatBox => EngineManager.ChatClient;

        /// <summary>
        /// Gets information about the minimap.
        /// </summary>
        public static MinimapInfo Minimap => EngineManager.MinimapInfo;

        /// <summary>
        /// Checks whether the given vector3 position is a wall.
        /// </summary>
        /// <param name="vec3Pos"></param>
        /// <returns></returns>
        public static bool IsWall(SharpDX.Vector3 vec3Pos) => EngineManager.IsWall(vec3Pos);

        /// <summary>
        /// Position of the cursor in game world projection.
        /// </summary>
        public static SharpDX.Vector3 WorldMousePosition => EngineManager.WorldMousePosition;

        /// <summary>
        /// Position of the cursor in screen projection relative to the world position.
        /// </summary>
        public static SharpDX.Vector2 ScreenMousePosition => EngineManager.ScreenMousePosition;

        /// <summary>
        /// Current zoom value in game.
        /// </summary>
        public static float ZoomValue => EngineManager.ZoomValue;

        /// <summary>
        /// Maz zoomable value in game.
        /// </summary>
        public static float MaxZoomValue => EngineManager.MaxZoomValue;

        /// <summary>
        /// Issue an order for the player(me) act on.
        /// </summary>
        /// <param name="issuedOrder"></param>
        /// <param name="positionOrdered"></param>
        public static void IssueOrder(OrderType issuedOrder, SharpDX.Vector2 positionOrdered) => EngineManager.IssueOrder((EngineManager.OrderType)issuedOrder, positionOrdered);

        /// <summary>
        /// Issue an order for the player(me) act on.
        /// </summary>
        /// <param name="issuedOrder"></param>
        /// <param name="positionOrdered"></param>
        public static void IssueOrder(OrderType issuedOrder, SharpDX.Vector3 positionOrdered) => EngineManager.IssueOrder((EngineManager.OrderType)issuedOrder, positionOrdered);
        
        public enum OrderType
        {
            Stop = EngineManager.OrderType.Stop,
            MoveTo = EngineManager.OrderType.MoveTo
        }
    }
}
