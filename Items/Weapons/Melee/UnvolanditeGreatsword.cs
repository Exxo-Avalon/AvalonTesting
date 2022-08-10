using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class UnvolanditeGreatsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Unvolandite Greatsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 52;
        Item.height = 54;
        Item.damage = 109;
        Item.autoReuse = true;
        Item.rare = ModContent.RarityType<FireOrangeRarity>();
        Item.knockBack = 7f;
        Item.useTime = 22;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 9, 87, 65);
        Item.useAnimation = 22;
        Item.UseSound = SoundID.Item1;
    }
}
