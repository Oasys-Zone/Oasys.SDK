using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasys.Common;
using Oasys.Common.GameInstances;
using Oasys.Common.GameObject.Clients;
using Oasys.Common.Enums.GameEnums;

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
    }
}
