using System;
using System.Collections.Generic;
using System.Reflection;
using Avalon.Players;
using Avalon.UI.Herbology;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Systems;

public class InterfaceLayerSystem : ModSystem
{
    private UIState? herbologyState;
    private UserInterface? herbologyUserInterface;

    /// <inheritdoc />
    public override void Load()
    {
        herbologyState = new HerbologyUIState();
        herbologyUserInterface = new UserInterface();
    }

    /// <inheritdoc />
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        layers.Insert(0, new LegacyGameInterfaceLayer(
            "Avalon: Update Interfaces",
            delegate
            {
                if (Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().DisplayHerbologyMenu && Main.playerInventory &&
                    herbologyUserInterface?.CurrentState == null)
                {
                    herbologyUserInterface?.SetState(herbologyState);
                    typeof(UserInterface)
                        .GetField("_clickDisabledTimeRemaining", BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.SetValue(herbologyUserInterface, 0);
                }
                else if (!(Main.playerInventory &&
                           Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().DisplayHerbologyMenu) &&
                         herbologyUserInterface?.CurrentState != null)
                {
                    herbologyUserInterface?.SetState(null);
                }

                herbologyUserInterface?.Update(Main._drawInterfaceGameTime);
                //ExxoAvalonOrigins.Mod.staminaInterface.Update(Main._drawInterfaceGameTime);
                return true;
            },
            InterfaceScaleType.UI)
        );

        int inventoryIndex =
            layers.FindIndex(layer => layer.Name.Equals("Vanilla: Radial Hotbars", StringComparison.Ordinal));
        if (inventoryIndex >= 0)
        {
            // layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
            //     "ExxoAvalonOrigins: Tome Slot",
            //     delegate
            //     {
            //         ExxoAvalonOrigins.Mod.tomeSlot.DrawTomes(Main.spriteBatch);
            //         return true;
            //     },
            //     InterfaceScaleType.UI)
            // );

            layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                "Avalon: Herbology Bench",
                delegate
                {
                    herbologyUserInterface?.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }

        // int resourceBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        // if (resourceBarsIndex >= 0)
        // {
        //     layers.Insert(resourceBarsIndex, new LegacyGameInterfaceLayer(
        //         "ExxoAvalonOrigins: Stamina Bar",
        //         delegate
        //         {
        //             ExxoAvalonOrigins.Mod.staminaInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
        //             return true;
        //         },
        //         InterfaceScaleType.UI)
        //     );
        // }
    }
}
