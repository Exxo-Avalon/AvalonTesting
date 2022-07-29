using Avalon.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

public class HadesCross : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hades' Cross");
        Tooltip.SetDefault("Turns the holder into varefolk upon entering lava");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.accessory = true;
        Item.defense = 3;
        Item.value = Item.buyPrice(0, 9, 72);
        Item.rare = ItemRarityID.LightPurple;
        Item.canBePlacedInVanityRegardlessOfConditions = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoBuffPlayer>().AccLavaMerman = true;
        if (hideVisual)
        {
            player.GetModPlayer<ExxoEquipEffectPlayer>().HideVarefolk = true;
        }
        player.lavaImmune = true;
        player.fireWalk = true;
        player.ignoreWater = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.LavaWaders)
            .AddIngredient(ItemID.Hellstone, 20)
            .AddIngredient(ItemID.LavaBucket)
            .AddIngredient(ItemID.SoulofFright, 6)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override bool IsVanitySet(int head, int body, int legs) => true;
}
