using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostLegend.Graphics.GUI
{
    class GuiScreen
    {
        public List<IGuiComponent> BasicComponents;
        public List<IGuiComponent> BackgroundComponents;
        public List<IGuiButton> Buttons;
		public List<GuiEntity> Entities;
		public List<MapInteractionButton> InteractionButtons;
        public List<TextInput> TextInputs;
        public List<FadingImage> FadingImages;
		public Vector2 CurrentScroll;
        public GuiScreen()
        {
            InteractionButtons = new List<MapInteractionButton>();
            Entities = new List<GuiEntity>();
            BackgroundComponents = new List<IGuiComponent>();
            BasicComponents = new List<IGuiComponent>();
            Buttons = new List<IGuiButton>();
            TextInputs = new List<TextInput>();
            FadingImages = new List<FadingImage>();
        }
    }
}
