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
			Colours.Add("lightbronze", new Color(194, 142, 123));
			Colours.Add("bronze", new Color(174,122,103));
			Colours.Add("main", new Color(109,129,148));
            Colours.Add("white", new Color(255, 255, 255));
            Colours.Add("gray", new Color(75, 75, 75));
            Colours.Add("lightgray", new Color(175, 175, 175));
            Colours.Add("lightred", new Color(255, 205, 205));
            Colours.Add("red", new Color(255, 85, 85));
            Colours.Add("lightgreen", new Color(205, 255, 205));
            Colours.Add("green", new Color(85, 255, 85));
            Colours.Add("lightyellow", new Color(235, 235, 205));
            Colours.Add("lightblue", new Color(205, 205, 255));
        }
        public static void LoadTextureDict(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            string imageData =
//name , frames, speed, ani types
@"
Character/head_tone1,4,1000,1
Character/body_tone1,4,1000,1
Character/head_tone2,4,1000,1
Character/body_tone2,4,1000,1
Character/head_tone3,4,1000,1
Character/body_tone3,4,1000,1
Character/head_tone4,4,1000,1
Character/body_tone4,4,1000,1
Character/head_tone5,4,1000,1
Character/body_tone5,4,1000,1
Character/head_tone6,4,1000,1
Character/body_tone6,4,1000,1
Character/eyes_blue,4,1000,1
Character/eyes_hazel,4,1000,1
Character/eyes_brown,4,1000,1
Character/eyes_yellow,4,1000,1
Character/eyes_green,4,1000,1
Character/eyes_red,4,1000,1
Character/eyes_purple,4,1000,1
Character/eyes_yellowgreen,4,1000,1
Character/eyes_bluebrown,4,1000,1
Character/eyes_turquoise,4,1000,1
Character/hair2_red,4,1000,1
Character/hair2_ginger,4,1000,1
Character/hair2_black,4,1000,1
Character/hair2_brown,4,1000,1
Character/hair2_dark_brown,4,1000,1
Character/hair2_brown2,4,1000,1
Character/hair2_gray,4,1000,1
Character/hair2_blond,4,1000,1

Items/palm_log,1,0,1

test,1,0,1
test2,1,0,1
ancient_pond,1,0,1
lostlegend_logo,1,0,1
spoonfishstudios_logo,1,0,1
GUI/PanelStyles/no_box,1,0,1
GUI/PanelStyles/bronze,1,0,1
GUI/PanelStyles/category_outline,1,0,1
GUI/PanelStyles/bronze_thin,1,0,1
GUI/PanelStyles/bronze_thick,1,0,1
GUI/PanelStyles/bronze_outline,1,0,1
GUI/PanelStyles/bronze_outline_round,1,0,1
GUI/PanelStyles/blue_outline_round,1,0,1
GUI/PanelStyles/bronze_outline_round_light,1,0,1

GUI/Menu/blackout,1,0,1

GUI/Npc/blacksmith,1,0,1

GUI/Icons/misc_category,1,0,1
GUI/Icons/key_category,1,0,1
GUI/Icons/armour_category,1,0,1
GUI/Icons/weapon_category,1,0,1
GUI/Icons/backpack,1,0,1
GUI/Icons/burger_bar,1,0,1
GUI/Icons/plus,1,0,1
GUI/Icons/magnify,1,0,1
GUI/Icons/minus,1,0,1
GUI/Icons/n_arrow,1,0,1
GUI/Icons/equipment,1,0,1
GUI/Icons/ne_arrow,1,0,1
GUI/Icons/e_arrow,1,0,1
GUI/Icons/se_arrow,1,0,1
GUI/Icons/s_arrow,1,0,1
GUI/Icons/sw_arrow,1,0,1
GUI/Icons/w_arrow,1,0,1
GUI/Icons/blacksmith_icon,1,0,1
GUI/Icons/nw_arrow,1,0,1
GUI/Icons/lithram_house_icon,1,0,1
GUI/Icons/lithram_food_shop_icon,1,0,1
GUI/Icons/lithram_weapon_shop_icon,1,0,1
GUI/Icons/lithram_armour_shop_icon,1,0,1
GUI/Icons/lithram_fish_shop_icon,1,0,1


Map/weapon_shop,1,0,1
Map/Island1/island1_background,1,0,1
Map/Island1/east_lithram_beach,1,0,1
Map/Island1/central_lithram_village,1,0,1
Map/Island1/outer_lithram_village,1,0,1
Map/Island1/sea_cave_beach,1,0,1
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
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!?.,:+-=%()/><' ";
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
                else if (letter == '>')
                    currentWidth = 9;
                else if (letter == '<')
                    currentWidth = 9;
                else if (letter == '\'')
                    currentWidth = 5;

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

        public static AnimatedTexture UniqueImage(AnimatedTexture image, int frame = -1)
        {
            AnimatedTexture newImage = new AnimatedTexture(image.Texture, image.Frames, image.FrameDuration, image.Types);
            if (frame == -1)
                newImage.RandomiseFrame();
            else
                newImage.CurrentFrame = frame;
            return newImage;
        }
    }
}

