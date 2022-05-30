using System;
using System.Reflection;
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
                return Mod.Assets.Request<Texture2D>($"{AvalonTesting.TextureAssetsPath}/UI/EAOLogoAprilFools");
            }

            return Mod.Assets.Request<Texture2D>($"{AvalonTesting.TextureAssetsPath}/UI/EAOLogo");
        }
    }

    public override void Load()
    {
        base.Load();

        // Sets the menu to be initially set to Exxo Avalon's on game load
        typeof(MenuLoader)
            .GetField("LastSelectedModMenu", BindingFlags.NonPublic | BindingFlags.Static)
            ?.SetValue(null, FullName);
    }
}
