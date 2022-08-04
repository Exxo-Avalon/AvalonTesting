using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class Rock : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Rock");
        Tooltip.SetDefault("Used for crafting the Eye of Oblivion\nThrowable\n'Don't throw it at a window!'");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 0;
        Item.height = dims.Height;
        Item.shoot = ModContent.ProjectileType<Projectiles.ThrowingRock>();
        Item.shootSpeed = 7f;
        Item.useTime = 25;
        Item.noUseGraphic = true;
        Item.useAnimation = 25;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
    }
}
