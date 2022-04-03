using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Systems;
public class RecipeSystem : ModSystem
{
    public override void AddRecipeGroups()
    {
        var groupWings = new RecipeGroup(() => "Any Wings", new int[]
        {
            ItemID.DemonWings,
            ItemID.AngelWings,
            ItemID.ButterflyWings,
            ItemID.FairyWings,
            ItemID.HarpyWings,
            ItemID.BoneWings,
            ItemID.FlameWings,
            ItemID.FrozenWings,
            ItemID.GhostWings,
            ItemID.LeafWings,
            ItemID.BatWings,
            ItemID.BeeWings,
            ItemID.TatteredFairyWings,
            ItemID.SpookyWings,
            ItemID.FestiveWings,
            ItemID.BeetleWings,
            ItemID.FinWings,
            ItemID.FishronWings,
            ItemID.WingsNebula,
            ItemID.WingsSolar,
            ItemID.WingsStardust,
            ItemID.WingsVortex,
            ItemID.FinWings,
            ItemID.MothronWings,
            ItemID.BetsyWings,
            ItemID.SteampunkWings
        });
        RecipeGroup.RegisterGroup("AvalonTesting:Wings", groupWings);
    }
}
