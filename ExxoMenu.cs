using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace AvalonTesting;
public class ExxoMenu : ModMenu
{
    public override Asset<Texture2D> Logo
    {
        get
        {
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                return ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogoAprilFools");
            }
            return ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogo");
        }
    }
}
