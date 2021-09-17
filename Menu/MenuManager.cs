namespace Oasys.SDK.Menu
{
    public class MenuManager
    {
        /// <summary>
        /// Adds the tab to the menu.
        /// </summary>
        /// <param name="tab"></param>
        /// <example>
        ///    <code>
        ///           MenuManager.AddTab(new Tab("Tab1"));
        ///    </code>
        /// </example>
        public static void AddTab(Common.Menu.Tab tab)
        {
            Common.Menu.MenuManagerProvider.AddTab(tab);
        }

        /// <summary>
        /// Gets the tab with the name from the menu.
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
    }
}
