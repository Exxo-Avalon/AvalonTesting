using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

class GreenPhasecleaver : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Green Phasecleaver");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 80;
        Item.autoReuse = true;
        Item.scale = 1.2f;
        Item.useTurn = true;
        Item.rare = ModContent.RarityType<TealRarity>();
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.knockBack = 5.25f;
        Item.DamageType = DamageClass.Melee;
        Item.value = 70000;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0.05f, 0.5f, 0.1f);
    }
}
