using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasys.Common.EventsProvider;

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
        /// This event is raised whenever the main input(space key for default) is registered.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.CoreEventTemplate OnCoreMainInput
        {
            add => Common.EventsProvider.CoreEvents.OnCoreMainInput += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreMainInput -= value;
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
        /// This event is raised whenever the rendering occurs. If you want to draw, this is the event you want to subscribe to.
        /// </summary>
        public static event Common.EventsProvider.CoreEvents.CoreEventTemplate OnCoreRender
        {
            add => Common.EventsProvider.CoreEvents.OnCoreRender += value;
            remove => Common.EventsProvider.CoreEvents.OnCoreRender -= value;
        }
    }
}
