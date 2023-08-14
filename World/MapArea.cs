using LostLegend.Graphics;
using LostLegend.Graphics.GUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.World
{
    class MapArea
    {
        public string Name;
        public string Description;
        public bool IsDiscovered;
        public bool IsActive;
        public Vector2 CenterPos;
        public AnimatedTexture Texture;
        public List<TravelButtonData> TravelButtons;
        public List<MapInteractionButton> InteractionButtons;

        public MapArea(string name, string desc, bool discovered, AnimatedTexture texture, Vector2 centerPos, List<TravelButtonData> travelButtons, List<MapInteractionButton> interactionButtons) 
        {
            InteractionButtons = interactionButtons;
            TravelButtons = travelButtons;
            IsActive = false;
            Name = name;
            Description = desc;
            Texture = texture;
            IsDiscovered = discovered;
            CenterPos = centerPos;
            //ZOOMPOS
        }

    }
}
