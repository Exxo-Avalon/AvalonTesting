using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

public class AlchemicalSkull : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Alchemical Skull");
        Tooltip.SetDefault("Increases spawn rate and Attackers also take damage"
                           + "\nThe wearer can walk on water and lava"
                           + "\nGrants immunity to knockback and fire blocks");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 150000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.ObsidianShield)
            .AddIngredient(ItemID.IronskinPotion, 10)
            .AddIngredient(ItemID.BattlePotion, 15)
            .AddIngredient(ItemID.ThornsPotion, 15)
            .AddIngredient(ItemID.WaterWalkingPotion, 10)
            .AddIngredient(ItemID.Bone, 99)
            .AddTile(TileID.Hellforge).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.waterWalk = player.waterWalk2 = player.enemySpawns = player.noKnockback = player.fireWalk = true;
        player.thorns = 1f;
    }
}
