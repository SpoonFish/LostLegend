using LostLegend.Graphics.GUI;
using LostLegend.Graphics.GUI.Interactions;
using LostLegend.Master;
using LostLegend.World;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Statics
{
    static class MapInfo
	{
		public enum MapAreaActions
		{
			Fight,
            Boss,
            Harvest
		};
		public enum MapAreaIDs { 
            EastLithramBeach,
            CentralLithramVillage,
            OuterLithramVillage,
            SeaCaveBeach,
            MainRoom
        };
        public enum MapIDs
        {
            Null,
            Island1,
            WeaponShop
        };

        public static Dictionary<MapIDs, Map> Maps;

        public static void LoadMapInfo(MasterManager master)
        {
            Maps = new Dictionary<MapIDs, Map>
            {
                { MapIDs.Island1, new Map(new Dictionary<MapAreaIDs, MapArea>(), "ISLAND 1", true, new Size(960, 960)) },
                { MapIDs.WeaponShop, new Map(new Dictionary<MapAreaIDs, MapArea>(), "BLACKSMITH", false, new Size(480, 480)) }
            };

            master.worldManager.CurrentMap = Maps[MapIDs.Island1];

            Maps[MapIDs.Island1].MapAreas.Add(
                MapAreaIDs.EastLithramBeach,
                new MapArea(
                        "Lithram Beach",
                        "A quiet beach on the Lithram coast. It is common place for fisherman to carry out their business as this is a rather safe area on the island. Here you can find small crabs and seagulls.",
                        master.storedDataManager.CurrentSaveFile.DiscoveredAreas.IndexOf((int)MapAreaIDs.EastLithramBeach) > -1,
                        ContentLoader.Images["east_lithram_beach"],
                        new Vector2(136, 503),
                        new List<TravelButtonData>()
                        {
                            new TravelButtonData(MapAreaIDs.CentralLithramVillage, new Vector2(182,447), "n")
                        },
                        new List<MapInteractionButton>()
                        {
                            new MapInteractionButton(new Vector2(177,437), new ButtonSignalEvent(), "lithram_house")
                        },
						new List<MapAreaActions>()
						{
							MapAreaActions.Fight,
                            MapAreaActions.Harvest
						}

					)
                );

            Maps[MapIDs.Island1].MapAreas.Add(
                MapAreaIDs.CentralLithramVillage,
                new MapArea(
                        "Lithram Village",
                        "A small but peaceful village that is somewhat accepted by monsters. Everyone seems friendly. This is where you were born and raised.",
                        master.storedDataManager.CurrentSaveFile.DiscoveredAreas.IndexOf((int)MapAreaIDs.CentralLithramVillage) > -1,
                        ContentLoader.Images["central_lithram_village"],
                        new Vector2(160, 309),
                        new List<TravelButtonData>()
                        {
                            new TravelButtonData(MapAreaIDs.EastLithramBeach, new Vector2(182,437), "s"),
                            new TravelButtonData(MapAreaIDs.OuterLithramVillage, new Vector2(269,240), "ne")
                        },
                        new List<MapInteractionButton>()
                        {
                            new MapInteractionButton(new Vector2(215,189), new ButtonSignalEvent(), "lithram_house"),
                            new MapInteractionButton(new Vector2(160,235), new ButtonSignalEvent(), "lithram_fish_shop"),
                            new MapInteractionButton(new Vector2(244,271), new ButtonSignalEvent(), "lithram_food_shop"),
                            new MapInteractionButton(new Vector2(129,324), new ButtonSignalEvent("change_map", "weapon_shop"), "lithram_weapon_shop"),
						},
						new List<MapAreaActions>()
						{
						}
					)
                );

            Maps[MapIDs.Island1].MapAreas.Add(
                MapAreaIDs.OuterLithramVillage,
                new MapArea(
                        "Lithram Village",
                        "This is the outer part of town. It is common to see village guards here. There is a haunted watch-tower.",
                        master.storedDataManager.CurrentSaveFile.DiscoveredAreas.IndexOf((int)MapAreaIDs.OuterLithramVillage) > -1,
                        ContentLoader.Images["outer_lithram_village"],
                        new Vector2(326,195),
                        new List<TravelButtonData>()
                        {
                            new TravelButtonData(MapAreaIDs.SeaCaveBeach, new Vector2(447,242), "e"),
                            new TravelButtonData(MapAreaIDs.CentralLithramVillage, new Vector2(269,240), "sw")
                        },
                        new List<MapInteractionButton>()
                        {
                            new MapInteractionButton(new Vector2(364,203), new ButtonSignalEvent(), "lithram_armour_shop")
                        },
						new List<MapAreaActions>()
						{
							MapAreaActions.Fight,

							MapAreaActions.Fight,

							MapAreaActions.Fight,

							MapAreaActions.Fight
						}
					)
                );

            Maps[MapIDs.Island1].MapAreas.Add(
                MapAreaIDs.SeaCaveBeach,
                new MapArea(
                        "Sea Cave Beach",
                        "A beach at the north of the island named after the famous Sea Cave which lies at the end of the beach. Only the most skilled fishrmen dare to enter it to catch something impressive. There are giant lobsters around the beach that don't seem too friendly",

                        master.storedDataManager.CurrentSaveFile.DiscoveredAreas.IndexOf((int)MapAreaIDs.SeaCaveBeach) > -1,
                        ContentLoader.Images["sea_cave_beach"],
                        new Vector2(548,180),
                        new List<TravelButtonData>()
                        {
                            new TravelButtonData(MapAreaIDs.OuterLithramVillage, new Vector2(447,242), "w")
                        },
                        new List<MapInteractionButton>()
                        {
                        },
						new List<MapAreaActions>()
						{
                            MapAreaActions.Fight
						}
					)
                );

            Maps[MapIDs.WeaponShop].MapAreas.Add(
                MapAreaIDs.MainRoom,
                new MapArea(
                        "Main Room",
                        "Seemingly randomly placed shelves and racks fill the walls of this room with all sorts of weapons. Here you can buy some standard weapons although the owner is not particularly friendly. There is also a mysterious door...",
                        true,
                        ContentLoader.Images["weapon_shop"],
                        new Vector2(239, 346),
                        new List<TravelButtonData>()
                        {
                            new TravelButtonData(MapAreaIDs.CentralLithramVillage, new Vector2(255,404), "s", MapIDs.Island1)
                        },
                        new List<MapInteractionButton>()
                        {
                            new MapInteractionButton(new Vector2(240-105,240-80), new ButtonSignalEvent("npc_interact", "blacksmith"), "blacksmith", new Vector2(32,32))
                        },
						new List<MapAreaActions>()
						{
							MapAreaActions.Fight,
							MapAreaActions.Fight
						}
					)
                ) ;
        }
    }
}
