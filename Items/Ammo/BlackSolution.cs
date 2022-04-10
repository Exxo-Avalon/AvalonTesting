using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

internal class BlackSolution : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Black Solution");
        Tooltip.SetDefault("Used by the Clentaminator\nSpreads the Dark Matter");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.ammo = AmmoID.Solution;
        Item.rare = ItemRarityID.Orange;
        Item.consumable = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.BlackSolution>() - ProjectileID.PureSpray;
        Item.value = Item.buyPrice(0, 0, 25);
        Item.maxStack = 2000;
    }
}
