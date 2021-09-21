using Oasys.Common;
using Oasys.Common.GameObject;
using Oasys.Common.GameObject.Clients;
using Oasys.Common.GameObject.ObjectClass;
using System.Collections.Generic;
using System.Linq;

namespace Oasys.SDK
{
    /// <summary>
    /// Manages the units.
    /// </summary>
    public class UnitManager
    {
        /// <summary>
        /// Simply the champion the player is playing as.
        /// </summary>
        public static AIHeroClient MyChampion = ObjectManagerExport.LocalPlayer;

        public static List<AIBaseClient> AllObjects => ObjectManagerExport.CollectedNativeObjects.Select(x => x.Value).ToList();

        /// <summary>
        /// Gets all the enemy game objects. This includes champions, minions, jungle mobs, towers and inhibitors.
        /// </summary>
        public static List<GameObjectBase> Enemies
        {
            get
            {
                var aibCliList = new List<GameObjectBase>() { };
                aibCliList.AddRange(EnemyChampions);
                aibCliList.AddRange(EnemyMinions);
                aibCliList.AddRange(EnemyJungleMobs);
                aibCliList.AddRange(EnemyTowers);
                aibCliList.AddRange(EnemyInhibitors);

                return aibCliList;
            }
        }

        /// <summary>
        /// Gets all the ally game objects. This includes champions, minions, jungle mobs, towers and inhibitors.
        /// </summary>
        public static List<GameObjectBase> Allies
        {
            get
            {
                var aibCliList = new List<GameObjectBase>() { };
                aibCliList.AddRange(AllyChampions);
                aibCliList.AddRange(AllyMinions);
                aibCliList.AddRange(AllyJungleMobs);
                aibCliList.AddRange(AllyTowers);
                aibCliList.AddRange(AllyInhibitors);

                return aibCliList;
            }
        }


        public static List<AIPlacementClient> PlacementObjects => ObjectManagerExport.PlacementCollection.Values.ToList();

        /// <summary>
        /// Gets all the wards present in game.
        /// </summary>
        public static List<AIPlacementClient> Wards => PlacementObjects
                                                  .Where(x => x.Health < 10 && x.MaxHealth < 10 && !x.UnitComponentInfo.SkinName.ToLower().Contains("minion") &&
                                                             (x.Name.Equals("SightWard") || x.Name.Equals("JammerDevice")))
                                                  .ToList();

        /// <summary>
        /// Gets all nexuses in game if UseNativeObjectManagerCaching is enabled
        /// </summary>
        public static List<Nexus> Nexuses => ObjectManagerExport.NexusCollection.Values.ToList();

        /// <summary>
        /// Gets enemy nexus if UseNativeObjectManagerCaching is enabled
        /// </summary>
        public static Nexus EnemyNexus => Nexuses.FirstOrDefault(x => x.IsEnemy);

        /// <summary>
        /// Gets ally nexus if UseNativeObjectManagerCaching is enabled
        /// </summary>
        public static Nexus AllyNexus => Nexuses.FirstOrDefault(x => x.OnMyTeam);

        /// <summary>
        /// Gets all the enemy champions.
        /// </summary>
        public static List<Hero> EnemyChampions => ObjectManagerExport.HeroCollection.Where(kvp => kvp.Value.Team != MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the ally champions, including the player champion.
        /// </summary>
        public static List<Hero> AllyChampions => ObjectManagerExport.HeroCollection.Where(kvp => kvp.Value.Team == MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the enemy minions.
        /// </summary>
        public static List<Minion> EnemyMinions => ObjectManagerExport.MinionCollection.Where(kvp => kvp.Value.Team != MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the ally minions.
        /// </summary>
        public static List<Minion> AllyMinions => ObjectManagerExport.MinionCollection.Where(kvp => kvp.Value.Team == MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the enemy towers.
        /// </summary>
        public static List<Turret> EnemyTowers => ObjectManagerExport.TurretCollection.Where(kvp => kvp.Value.Team != MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the ally towers.
        /// </summary>
        public static List<Turret> AllyTowers => ObjectManagerExport.TurretCollection.Where(kvp => kvp.Value.Team == MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the enemy inhibitors.
        /// </summary>
        public static List<Inhibitor> EnemyInhibitors => ObjectManagerExport.InhibCollection.Where(kvp => kvp.Value.Team != MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the ally inhibitors.
        /// </summary>
        public static List<Inhibitor> AllyInhibitors => ObjectManagerExport.InhibCollection.Where(kvp => kvp.Value.Team == MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the enemy jungle mobs.
        /// </summary>
        public static List<JungleMob> EnemyJungleMobs => ObjectManagerExport.JungleObjectCollection.Where(kvp => kvp.Value.Team != MyChampion.Team).Select(a => a.Value).ToList();

        /// <summary>
        /// Gets all the ally jungle mobs.
        /// </summary>
        public static List<JungleMob> AllyJungleMobs => ObjectManagerExport.JungleObjectCollection.Where(kvp => kvp.Value.Team == MyChampion.Team).Select(a => a.Value).ToList();
    }
}
