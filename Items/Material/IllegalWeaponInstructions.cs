using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Material;

class IllegalWeaponInstructions : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Illegal Weapon Instructions");
        Tooltip.SetDefault("'Read if you dare'");
        SacrificeTotal = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<DarkRedRarity>();
        Item.width = dims.Width;
        Item.value = 50;
        Item.maxStack = 99;
        Item.height = dims.Height;
    }
}
