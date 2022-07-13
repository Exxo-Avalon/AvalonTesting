using AvalonTesting.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

internal class LimeGreenSolution : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Lime Solution");
        Tooltip.SetDefault("Used by the Clentaminator\nSpreads the Jungle");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.ammo = AmmoID.Solution;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.consumable = true;
        Item.shoot = ModContent.ProjectileType<LimeSolution>() - ProjectileID.PureSpray;
        Item.value = Item.buyPrice(0, 0, 25);
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) =>
        player.itemAnimation >= player.HeldItem.useAnimation - 3;
}
