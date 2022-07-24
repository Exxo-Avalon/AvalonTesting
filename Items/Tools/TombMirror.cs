using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Avalon.Items.Tools;

public class TombMirror : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Tomb Mirror");
        Tooltip.SetDefault("Use to return to where you last died\n'Gazing into the mirror, you see yourself in a past life'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 1;
        Item.consumable = true;
        Item.width = 20;
        Item.height = 22;
        Item.rare = ItemRarityID.LightRed;
        Item.useStyle = ItemHoldStyleID.HoldUp;
        Item.useTime = 32;
        Item.useAnimation = 32;
        Item.consumable = false;
        Item.UseSound = SoundID.Item6;
        Item.autoReuse = false;
    }
    public override bool CanUseItem(Player player)
    {
        return player.showLastDeath;
    }
    public override bool? UseItem(Player player)
    {
        Vector2 newPos = new Vector2(player.lastDeathPostion.X - 16f, player.lastDeathPostion.Y - 24f);
        player.Teleport(newPos);
        return true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.RecallPotion, 3)
            .AddRecipeGroup("AvalonTesting:Tombstones", 10)
            .AddRecipeGroup("AvalonTesting:Herbs", 5)
            .AddTile(TileID.Furnaces)
            .Register();
    }
}
