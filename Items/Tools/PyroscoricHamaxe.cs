using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

public class PyroscoricHamaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pyroscoric Hamaxe");
        SacrificeTotal = 1;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 40;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.15f;
        Item.axe = 35;
        Item.hammer = 125;
        Item.rare = ModContent.RarityType<MagentaRarity>();
        Item.width = dims.Width;
        Item.useTime = 13;
        Item.knockBack = 3.5f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 2;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 416000;
        Item.useAnimation = 13;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void HoldItem(Player player)
    {
        if (player.inventory[player.selectedItem].type == Item.type)
        {
            player.pickSpeed -= 0.25f;
        }
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(2) == 0)
        {
            int num162 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, 0f, 0f, 0, default(Color), 2f);
            Main.dust[num162].noGravity = true;
        }
    }
}
