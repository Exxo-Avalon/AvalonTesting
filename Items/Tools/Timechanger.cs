using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class Timechanger : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Timechanger");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = 30;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Timechanger>();
        Item.value = Item.sellPrice(0, 2, 70, 0);
    }

    //public override bool? UseItem(Player player)
    //{
    //    if (Main.dayTime) Main.time = 53999;
    //    else Main.time = 32399;

    //    if (Main.netMode == NetmodeID.SinglePlayer)
    //    {
    //        Main.NewText(String.Format("It is now {0}.", Main.dayTime ? "Night" : "Day"), 50, 255, 130);
    //    }
    //    else if (Main.netMode == NetmodeID.Server)
    //    {
    //        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(String.Format("It is now {0}.", Main.dayTime ? "Night" : "Day")), new Color(50, 255, 130));
    //    }

    //    return true;
    //}

    public override void AddRecipes() => CreateRecipe().AddRecipeGroup(RecipeGroup.recipeGroupIDs["AvalonTesting:GoldBar"], 30).AddIngredient(ItemID.SoulofLight, 15).AddIngredient(ItemID.SoulofNight, 15).AddRecipeGroup(RecipeGroup.recipeGroupIDs["AvalonTesting:Tier3Watch"], 1).AddTile(TileID.MythrilAnvil).Register();
}
