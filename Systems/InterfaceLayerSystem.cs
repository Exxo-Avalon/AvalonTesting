using System.Collections.Generic;
using Avalon.Common;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Systems;

public class InterfaceLayerSystem : ModSystem
{
    /// <inheritdoc />
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        layers.Insert(0, new LegacyGameInterfaceLayer(
            $"{Mod.DisplayName}: Update Interfaces",
            delegate
            {
                foreach (ModInterfaceLayer modInterfaceLayer in ModInterfaceLayer.RegisteredInterfaceLayers)
                {
                    modInterfaceLayer.Update();
                }

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
