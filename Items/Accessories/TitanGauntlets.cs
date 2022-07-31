using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
class TitanGauntlets : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Titan Gauntlets");
        Tooltip.SetDefault("Melee attacks inflict Frostburn and increases melee damage and speed by 9%\n" +
            "Absorbs 25% of damage done to players on your team when above 25% life\n" +
            "Puts a shell around the owner when below 50% life that reduces damage by 25%\n" +
            "Provides 15 defense, 3 life regeneration, and 15% damage when below 33% life\n" +
            "Increases knockback and enables auto swing for melee weapons\n" +
            "Increases the size of melee weapons");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 14;
        Item.rare = ModContent.RarityType<Rarities.RainbowRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 30, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<TitanShield>())
            .AddIngredient(ModContent.ItemType<FrostGauntlet>())
            .AddTile(TileID.TinkerersWorkbench).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.FrozenShield)
            .AddIngredient(ItemID.FireGauntlet)
            .AddIngredient(ModContent.ItemType<AegisofAges>())
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.statLife <= player.statLifeMax2 * 0.25)
        {
            player.AddBuff(62, 5, true);
        }
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().FrostGauntlet = true;
        player.kbGlove = true;
        player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetDamage(DamageClass.Ranged) += 0.1f;
        player.frostArmor = true;
        player.autoReuseGlove = true;
        player.meleeScaleGlove = true;
        if (player.statLife <= player.statLifeMax2 * 0.33)
        {
            player.statDefense += 20;
            player.lifeRegen += 5;
            player.GetDamage(DamageClass.Generic) += 0.2f;
        }
        player.noKnockback = true;
        if (player.statLife > player.statLifeMax2 * 0.25)
        {
            if (player == Main.player[Main.myPlayer])
            {
                player.hasPaladinShield = true;
            }
            else if (player.miscCounter % 5 == 0)
            {
                var myPlayer = Main.myPlayer;
                if (Main.player[myPlayer].team == player.team && player.team != 0)
                {
                    var num45 = player.position.X - Main.player[myPlayer].position.X;
                    var num46 = player.position.Y - Main.player[myPlayer].position.Y;
                    var num47 = (float)Math.Sqrt(num45 * num45 + num46 * num46);
                    if (num47 < 800f)
                    {
                        Main.player[myPlayer].AddBuff(43, 10, true);
                    }
                }
            }
        }
    }
}
