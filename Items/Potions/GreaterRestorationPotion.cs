using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class GreaterRestorationPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Greater Restoration Potion");
        Tooltip.SetDefault("Reduced potion cooldown");
        SacrificeTotal = 30;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 17;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.healLife = 110;
        Item.maxStack = 99;
        Item.value = 4000;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.RestorationPotion)
            .AddIngredient(ItemID.SoulofLight)
            .AddIngredient(ItemID.Daybloom)
            .AddIngredient(ItemID.PinkGel, 3)
            .AddTile(TileID.Bottles)
            .Register();
    }
    public override bool CanUseItem(Player player)
    {
        return player.statLife < player.statLifeMax2 && !player.HasBuff(BuffID.PotionSickness);
    }
    public override bool? UseItem(Player player)
    {
        int seconds = 45;
        if (player.pStone) seconds = (int)(seconds * 0.75); 
        player.AddBuff(BuffID.PotionSickness, seconds * 60 * 60);
        return true;
    }
}
