using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class VoraylzumKatana : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vorazylcum Katana");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 111;
        Item.autoReuse = true;
        Item.rare = ModContent.RarityType<FireOrangeRarity>();
        Item.width = dims.Width;
        Item.knockBack = 4f;
        Item.useTime = 17;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 10, 90, 0);
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
}

//TODO When the recipe gets put here add like 7 kunzite to it kthxbye
