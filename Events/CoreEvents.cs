
namespace Oasys.SDK.Events
{
    /// <summary>
    /// Core related events
    /// </summary>
    public class CoreEvents
    {
        /// <summary>
        /// This event is raised multiple times per second. To be specific, it is raised every 10ms. 
        /// This event is useful for custom caching and calculations executing every each of tick.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.CoreEventTemplate OnCoreMainTick
        {
            add => Common.EventsProvider.CoreEvents.OnCoreMainTick += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreMainTick -= value;
        }

        /// <summary>
        /// This event is raised whenever the main input(space key for default) is released.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.CoreEventTemplate OnCoreMainInputRelease
        {
            add => Common.EventsProvider.CoreEvents.OnCoreMainInputRelease += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreMainInputRelease -= value;
        }

        /// <summary>
        /// This event is raised whenever the main input(space key for default) is pressed.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.AsyncEventHandler OnCoreMainInputAsync
        {
            add => Common.EventsProvider.CoreEvents.OnCoreMainInputAsync += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreMainInputAsync -= value;
        }

        /// <summary>
        /// This event is raised whenever the main input(space key for default) is pressed before trying to basic attack.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.AsyncEventHandler OnCoreMainInputBeforeBasicAttackAsync
        {
            add => Common.EventsProvider.CoreEvents.OnCoreMainInputBeforeBasicAttackAsync += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreMainInputBeforeBasicAttackAsync -= value;
        }

        /// <summary>
        /// This event is raised whenever the harass input(x key for default) is pressed.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.AsyncEventHandler OnCoreHarassInputAsync
        {
            add => Common.EventsProvider.CoreEvents.OnCoreHarassInputAsync += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreHarassInputAsync -= value;
        }

        /// <summary>
        /// This event is raised whenever the laneclear input(v key for default) is pressed.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.AsyncEventHandler OnCoreLaneclearInputAsync
        {
            add => Common.EventsProvider.CoreEvents.OnCoreLaneclearInputAsync += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreLaneclearInputAsync -= value;
        }

        /// <summary>
        /// This event is raised whenever the laasthit input(c key for default) is pressed.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.AsyncEventHandler OnCoreLasthitInputAsync
        {
            add => Common.EventsProvider.CoreEvents.OnCoreLasthitInputAsync += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreLasthitInputAsync -= value;
        }

        /// <summary>
        /// This event is raised whenever the rendering occurs. If you want to draw, this is the event you want to subscribe to.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.RenderEventTemplate OnCoreRender
        {
            add => Common.EventsProvider.CoreEvents.OnCoreRender += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreRender -= value;
        }
    }
}
