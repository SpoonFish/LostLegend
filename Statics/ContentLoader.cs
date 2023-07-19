using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostLegend.Graphics;
using Microsoft.Xna.Framework;
using LostLegend.Graphics.GUI.Text;

namespace LostLegend.Statics
{
    static class ContentLoader
    {
        public static Dictionary<string, Color> Colours = new Dictionary<string,Color>();

        public static Dictionary<string, AnimatedTexture> Images = new Dictionary<string, AnimatedTexture>();
        public static Dictionary<char, TextLetter> FontDict = new Dictionary<char, TextLetter>();

        //load all ContentLoader. in the content from this string which has extra data about the animations
        public static void LoadColours()
        {
            Colours.Add("main", new Color(109,129,148));
            Colours.Add("white", new Color(255, 255, 255));
            Colours.Add("lightred", new Color(255, 205, 205));
            Colours.Add("lightyellow", new Color(235, 235, 205));
            Colours.Add("lightblue", new Color(205, 205, 255));
        }
        public static void LoadTextureDict(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            string imageData =
//name , frames, speed, ani types
@"
test,1,0,1
test2,1,0,1
lostlegend_logo,1,0,1
spoonfishstudios_logo,1,0,1
GUI/PanelStyles/no_box,1,0,1
GUI/PanelStyles/bronze,1,0,1
GUI/Menu/blackout,1,0,1
";
            string[] imageDataList = imageData.Split("\r\n");
            foreach (string image in imageDataList)
            {
                if (image == "")
                    continue;
                string[] splitImage = image.Split(',');
                splitImage[^1].Replace("\n", "");//remove newline

                string imgName = splitImage[0];
                int aniFrames = int.Parse(splitImage[1]);
                double aniSpeed = double.Parse(splitImage[2]);
                int aniTypes = int.Parse(splitImage[3]);

                Images.Add(imgName.Split("/")[^1], new AnimatedTexture(content.Load<Texture2D>("Images/" + imgName), aniFrames, aniSpeed, aniTypes, imgName.Split("/")[^1]));
            }
        }

        public static void LoadFont(Microsoft.Xna.Framework.Content.ContentManager content, GraphicsDevice graphicsDevice)
        {
            int totalWidth = 0;
            Texture2D fontImage = content.Load<Texture2D>("Images/font");
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!?.,:+-=%()/ ";
            for (int i = 0; i < letters.Length; i++)
            {
                char letter = letters[i];
                int currentWidth = 10;
                int spacing = 1;
                if (letter == 'M')//some letters have different sizes
                    currentWidth= 13;
                else if (letter == 'W')
                    currentWidth = 13;
                else if (letter == 'i')
                    currentWidth = 6;
                else if (letter == 't')
                    currentWidth = 8;
                else if (letter == 'f')
                    currentWidth = 9;
                else if (letter == 'l')
                    currentWidth = 6;
                else if (letter == '1')
                    currentWidth = 8;
                else if (letter == '!')
                    currentWidth = 4;
                else if (letter == '.')
                    currentWidth = 4;
                else if (letter == ',')
                    currentWidth = 5;
                else if (letter == ':')
                    currentWidth = 4;
                else if (letter == '(')
                    currentWidth = 6;
                else if (letter == ')')
                    currentWidth = 6;

                Rectangle sourceRectangle = new Rectangle(totalWidth, 0, currentWidth, 20);
                totalWidth += currentWidth;
                
                Texture2D letterImage = new Texture2D(graphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
                Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
                fontImage.GetData(0, sourceRectangle, data, 0, data.Length);
                letterImage.SetData(data);

                TextLetter textLetter = new TextLetter(letterImage, spacing, letter);
                FontDict.Add(letter, textLetter);
            }
        }

        public static void UpdateTextures(double timePassed)
        {
            foreach (AnimatedTexture texture in Images.Values)
            {
                texture.Update(timePassed);
            }
        }

        public static AnimatedTexture UniqueImage(AnimatedTexture image)
        {
            AnimatedTexture newImage = new AnimatedTexture(image.Texture, image.Frames, image.FrameDuration, image.Types);
            newImage.RandomiseFrame();
            return newImage;
        }
    }
}

