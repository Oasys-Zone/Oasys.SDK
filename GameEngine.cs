using Oasys.Common;
using Oasys.Common.Enums.GameEnums;
using Oasys.Common.GameInstances;
using Oasys.Common.GameObject.Clients;
using Oasys.Common.Logic.Helpers;
using System.Collections.Generic;

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
        /// Gets the current game tick. This is equal to gametime * 1000
        /// </summary>
        public static int GameTick => EngineManager.GameTick;

        /// <summary>
        /// Gets a boolean value presenting if the game is in focus by windows.
        /// </summary>
        public static bool IsGameWindowFocused => EngineManager.IsGameWindowFocused;

        /// <summary>
        /// The local ping to server.
        /// </summary>
        public static int GamePing => EngineManager.GamePing;

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
        /// Gets information about the frame rate (FPS).
        /// </summary>
        public static FrameClockFacade FrameInformation => EngineManager.FrameInformation;

        /// <summary>
        /// Checks whether the given vector3 position is a wall.
        /// </summary>
        /// <param name="vec3Pos"></param>
        /// <returns></returns>
        public static bool IsWall(SharpDX.Vector3 vec3Pos)
        {
            return EngineManager.IsWall(vec3Pos);
        }

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
        public static void IssueOrder(OrderType issuedOrder, SharpDX.Vector2 positionOrdered)
        {
            EngineManager.IssueOrder((EngineManager.OrderType)issuedOrder, positionOrdered);
        }

        /// <summary>
        /// Issue an order for the player(me) act on.
        /// </summary>
        /// <param name="issuedOrder"></param>
        /// <param name="positionOrdered"></param>
        public static void IssueOrder(OrderType issuedOrder, SharpDX.Vector3 positionOrdered)
        {
            EngineManager.IssueOrder((EngineManager.OrderType)issuedOrder, positionOrdered);
        }

        public enum OrderType
        {
            Stop = EngineManager.OrderType.Stop,
            MoveTo = EngineManager.OrderType.MoveTo,
            Recall = EngineManager.OrderType.Recall,
        }

        public static Common.Logic.Helpers.GameData.AllGameData AllGameData => Common.Logic.Helpers.GameData.AllGameData.Instance;

        public static Common.Logic.Helpers.ActivePlayerData.ActivePlayer ActivePlayer => Common.Logic.Helpers.ActivePlayerData.ActivePlayer.Instance;

        public static IEnumerable<AllPlayerData.PlayerData> AllPlayerData => Common.Logic.Helpers.AllPlayerData.AllPlayers;
    }
}
