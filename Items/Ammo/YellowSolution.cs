using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

internal class YellowSolution : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Yellow Solution");
        Tooltip.SetDefault("Used by the Clentaminator\nSpreads the Contagion");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.ammo = AmmoID.Solution;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.consumable = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.YellowSolution>() - ProjectileID.PureSpray;
        Item.value = Item.buyPrice(0, 0, 25);
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) =>
        player.itemAnimation >= player.HeldItem.useAnimation - 3;
}
