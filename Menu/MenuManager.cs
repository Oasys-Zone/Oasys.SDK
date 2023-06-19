using System;

namespace Oasys.SDK.Menu
{
    public class MenuManager
    {
        /// <summary>
        /// Adds the tab to the menu.
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>Index of tab</returns>
        /// <example>
        ///    <code>
        ///           MenuManager.AddTab(new Tab("Tab1"));
        ///    </code>
        /// </example>
        public static int AddTab(Common.Menu.Tab tab)
        {
            return Common.Menu.MenuManagerProvider.AddTab(tab);
        }

        /// <summary>
        /// Gets a tab with the name from the menu.
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        /// <example>
        ///    <code>
        ///           MenuManager.GetTab("Tab1");
        ///    </code>
        /// </example>
        public static Common.Menu.Tab GetTab(string tabName)
        {
            return Common.Menu.MenuManagerProvider.GetTab(tabName);
        }

        /// <summary>
        /// Gets a tab by a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Requested item</returns>
        /// <example>
        ///    <code>
        ///            <![CDATA[ 
        ///            var tab1 = MenuManager.GetTab("Tab1");
        ///            tab1.GetItem<Counter>(x => x.Title == "sometitle");
        ///            ]]>
        ///    </code>
        /// </example>
        public static Common.Menu.Tab GetTab(Func<Common.Menu.Tab, bool> predicate)
        {
            return Common.Menu.MenuManagerProvider.GetTab(predicate);
        }

        /// <summary>
        /// Gets a tab by its index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Requested item</returns>
        /// <example>
        ///    <code>
        ///            var tab1 = MenuManager.GetTab(1);
        ///    </code>
        /// </example>
        public static Common.Menu.Tab GetTab(int index)
        {
            return Common.Menu.MenuManagerProvider.GetTab(index);
        }
    }
}
