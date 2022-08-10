using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class FieryBladeofGrass : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Fiery Blade of Grass");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 42;
        Item.damage = 33;
        Item.useTurn = Item.autoReuse = true;
        Item.scale = 1f;
        Item.rare = ItemRarityID.LightRed;
        Item.useTime = 23;
        Item.knockBack = 3.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 54, 0);
        Item.useAnimation = 23;
        Item.UseSound = SoundID.Item1;
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        target.AddBuff(BuffID.Poisoned, 60 * 6);
        target.AddBuff(BuffID.OnFire, 60 * 6);
    }
    public override void OnHitPvp(Player player, Player target, int damage, bool crit)
    {
        target.AddBuff(BuffID.Poisoned, 60 * 6);
        target.AddBuff(BuffID.OnFire, 60 * 6);
    }
}
