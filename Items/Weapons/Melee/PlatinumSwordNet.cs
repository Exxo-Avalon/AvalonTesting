﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

internal class PlatinumSwordNet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Platinum Sword Net");
        ItemID.Sets.CatchingTool[Item.type] = true;
        ItemID.Sets.LavaproofCatchingTool[Item.type] = true;
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 15;
        Item.autoReuse = true;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.knockBack = 5f;
        Item.useTime = 23;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.buyPrice(0, 1);
        Item.useAnimation = 23;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes() => CreateRecipe().AddIngredient(ItemID.PlatinumBroadsword)
        .AddIngredient(ItemID.BugNet).AddTile(TileID.Anvils).Register();
}
