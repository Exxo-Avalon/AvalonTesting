using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class TheBanhammer : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("The Banhammer");
        Tooltip.SetDefault("Strong enough to destroy Hallowed Altars");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 30;
        Item.autoReuse = true;
        Item.hammer = 100;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.knockBack = 12f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Pwnhammer)
            .AddIngredient(ItemID.HallowedBar, 35)
            .AddIngredient(ItemID.SoulofMight, 10)
            .AddIngredient(ModContent.ItemType<Material.SoulofBlight>())
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
