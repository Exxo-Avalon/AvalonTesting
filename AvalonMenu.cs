﻿using System;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Avalon;

public class AvalonMenu : ModMenu
{
    public override Asset<Texture2D> Logo
    {
        get
        {
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                return Mod.Assets.Request<Texture2D>($"{Avalon.TextureAssetsPath}/UI/EAOLogoAprilFools");
            }

            return Mod.Assets.Request<Texture2D>($"{Avalon.TextureAssetsPath}/UI/EAOLogo");
        }
    }

    public override void Load()
    {
        base.Load();

        const string lastSelectedModMenuFieldName = "LastSelectedModMenu";
        FieldInfo? lastSelectedModMenuFieldInfo =
            typeof(MenuLoader).GetField(lastSelectedModMenuFieldName, BindingFlags.NonPublic | BindingFlags.Static);

        if (lastSelectedModMenuFieldInfo != null)
        {
            // Sets the menu to be initially set to Exxo Avalon's on game load
            lastSelectedModMenuFieldInfo.SetValue(null, FullName);
        }
        else
        {
            Avalon.Mod.Logger.Error(
                $"Could not find field with name {lastSelectedModMenuFieldName} in {typeof(MenuLoader)}");
        }
    }
}
