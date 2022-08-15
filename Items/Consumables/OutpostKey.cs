using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class OutpostKey : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Outpost Key");
        Tooltip.SetDefault("Opens the tuhrtl outpost door");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.maxStack = 999;
        Item.width = dims.Width;
        Item.value = 0;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Lime;
    }
}
