using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class Elektriwave : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Elektriwave");
        Tooltip.SetDefault("Has a chance to inflict Electrified");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 106;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ModContent.RarityType<TealRarity>();
        Item.useTime = 15;
        Item.knockBack = 6f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 616000;
        Item.useAnimation = 15;
        Item.UseSound = SoundID.Item15;
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        if(Main.rand.NextBool(3))
        target.AddBuff(BuffID.Electrified, 180);
    }
    public override void OnHitPvp(Player player, Player target, int damage, bool crit)
    {
        if (Main.rand.NextBool(3))
            target.AddBuff(BuffID.Electrified, 180);
    }
}
