using System.Collections.Generic;
using Avalon.Common;
using Avalon.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Systems;

public class InterfaceLayerSystem : ModSystem
{
    /// <inheritdoc />
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int staminaBarIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Mouse Text");
        if (staminaBarIndex != -1)
        {
            layers.Insert(staminaBarIndex, new LegacyGameInterfaceLayer("Stamina Bar", delegate
            {
                Avalon.Mod.staminaInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                return true;
            }, InterfaceScaleType.UI));
        }
        layers.Insert(0, new LegacyGameInterfaceLayer(
            $"{Mod.DisplayName}: Update Interfaces",
            delegate
            {
                foreach (ModInterfaceLayer modInterfaceLayer in ModInterfaceLayer.RegisteredInterfaceLayers)
                {
                    modInterfaceLayer.Update();
                }
                Avalon.Mod.staminaInterface.Update(Main._drawInterfaceGameTime);
                return true;
            },
            InterfaceScaleType.UI)
        );

        foreach (ModInterfaceLayer modInterfaceLayer in ModInterfaceLayer.RegisteredInterfaceLayers)
        {
            modInterfaceLayer.ModifyInterfaceLayers(layers);
        }
    }
}
