using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Placeable.Tile;

class CompressedExtractBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Compressed Extractination Block");
        Tooltip.SetDefault("Stick it in the Extractinator!\nNot currently functional");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 40;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 40;
        Item.height = dims.Height;
    }
}
