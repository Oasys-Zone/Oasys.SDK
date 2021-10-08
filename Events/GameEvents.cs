namespace Oasys.SDK.Events
{
    /// <summary>
    /// Game related events.
    /// </summary>
    public class GameEvents
    {
        /// <summary>
        /// This event is raised when the loading of the game finishes, where a user is inside the lobby. 
        /// If the game has already started, then it is raised immediately after the core has initialized.
        /// </summary>
        public static event Common.EventsProvider.GameEvents.GameEventTemplate OnGameLoadComplete
        {
            add => Common.EventsProvider.GameEvents.OnGameLoadComplete += value;
            remove => Common.EventsProvider.GameEvents.OnGameLoadComplete -= value;
        }

        /// <summary>
        /// This event is raised when a game match finishes.
        /// </summary>
        public static event Common.EventsProvider.GameEvents.GameEventTemplate OnGameMatchComplete
        {
            add => Common.EventsProvider.GameEvents.OnGameMatchComplete += value;
            remove => Common.EventsProvider.GameEvents.OnGameMatchComplete -= value;
        }

        /// <summary>
        /// This event is raised whenever enemy champion casts a spell.
        /// </summary>
        public static event Common.EventsProvider.GameEvents.SpellEventTemplate OnProcessSpell
        {
            add => Common.EventsProvider.GameEvents.OnGameProcessSpell += value;
            remove => Common.EventsProvider.GameEvents.OnGameProcessSpell -= value;
        }

        /// <summary>
        /// This event is raised whenever enemy champion recalls.
        /// </summary>
        public static event Common.EventsProvider.GameEvents.RecallEventTemplate OnProcessRecall
        {
            add => Common.EventsProvider.GameEvents.OnGameProcessRecall += value;
            remove => Common.EventsProvider.GameEvents.OnGameProcessRecall -= value;
        }

        /// <summary>
        /// This event is raised whenever there is an object that is created by the game.
        /// To activate this event, please refer to Common.Settings.Core.UseNativeObjectManagerCaching (It is disabled by default)
        /// </summary>
        public static event Common.EventsProvider.GameEvents.ObjectManagerCallbackDelegate OnCreateObject
        {
            add => Common.EventsProvider.GameEvents.OnCreateObject += value;
            remove => Common.EventsProvider.GameEvents.OnCreateObject -= value;
        }

        /// <summary>
        /// This event is raised whenever there is an object that is deleted by the game.
        /// To activate this event, please refer to Common.Settings.Core.UseNativeObjectManagerCaching (It is disabled by default)
        /// </summary>
        public static event Common.EventsProvider.GameEvents.ObjectManagerCallbackDelegate OnDeleteObject
        {
            add => Common.EventsProvider.GameEvents.OnDeleteObject += value;
            remove => Common.EventsProvider.GameEvents.OnDeleteObject -= value;
        }

        /// <summary>
        /// This event is raised whenever a hero object buys an item.
        /// </summary>
        public static event Common.EventsProvider.GameEvents.HeroItemEventTemplate OnBuyItem
        {
            add => Common.EventsProvider.GameEvents.OnBuyItem += value;
            remove => Common.EventsProvider.GameEvents.OnBuyItem -= value;
        }

        /// <summary>
        /// This event is raised whenever a hero object sells an item.
        /// </summary>
        public static event Common.EventsProvider.GameEvents.HeroItemEventTemplate OnSellItem
        {
            add => Common.EventsProvider.GameEvents.OnSellItem += value;
            remove => Common.EventsProvider.GameEvents.OnSellItem -= value;
        }

        /// <summary>
        /// This event is raised whenever a hero object upgrades an item.
        /// </summary>
        public static event Common.EventsProvider.GameEvents.HeroItemEventTemplate OnItemUpgrade
        {
            add => Common.EventsProvider.GameEvents.OnItemUpgrade += value;
            remove => Common.EventsProvider.GameEvents.OnItemUpgrade -= value;
        }

        /// <summary>
        /// This event is raised whenever a hero object changes the slot of an item.
        /// </summary>
        public static event Common.EventsProvider.GameEvents.HeroItemEventTemplate OnItemSlotChanged
        {
            add => Common.EventsProvider.GameEvents.OnItemSlotChanged += value;
            remove => Common.EventsProvider.GameEvents.OnItemSlotChanged -= value;
        }
    }
}
