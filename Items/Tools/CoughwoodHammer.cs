using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class CoughwoodHammer : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Coughwood Hammer");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.UseSound = SoundID.Item1;
        Item.damage = 7;
        Item.autoReuse = true;
        Item.hammer = 42;
        Item.useTurn = true;
        Item.scale = 1.2f;
        Item.useTime = 20;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 50;
        Item.useAnimation = 30;
    }
}
