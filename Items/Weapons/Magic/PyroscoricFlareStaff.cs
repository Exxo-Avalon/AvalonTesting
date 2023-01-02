using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Rarities;

namespace Avalon.Items.Weapons.Magic;

class PyroscoricFlareStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pyroscoric Flare Staff");
        Tooltip.SetDefault("Fires a fiery ball that bursts into fiery sparks\n'Oxygen Devourer'");
        SacrificeTotal = 1;
        Item.staff[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SapphireStaff);
        Item.staff[Item.type] = true;
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.damage = 145;
        Item.autoReuse = true;
        Item.shootSpeed = 6f;
        Item.mana = 19;
        Item.UseSound = SoundID.Item45;
        Item.rare = ModContent.RarityType<MagentaRarity>();
        Item.knockBack = 6f;
        Item.useTime = 32;
        Item.useAnimation = 32;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.SolarBolt>();
        Item.value = Item.sellPrice(0, 10, 0, 0);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.PyroscoricBar>(), 20)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
