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
        public List<IGuiButton> Nodes;
        public List<TextInput> TextInputs;
        public List<FadingImage> FadingImages;

        public GuiScreen()
        {
            Nodes = new List<IGuiButton>();
            BackgroundComponents = new List<IGuiComponent>();
            BasicComponents = new List<IGuiComponent>();
            Buttons = new List<IGuiButton>();
            TextInputs = new List<TextInput>();
            FadingImages = new List<FadingImage>();
        }
    }
}
