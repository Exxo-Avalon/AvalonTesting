using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class UndeadShard : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Undead Shard");
        Tooltip.SetDefault("'A fragment of undead creatures'");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 6, 0);
        Item.height = dims.Height;
    }
}
