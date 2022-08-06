using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class AlienDevice : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Alien Device");
        Tooltip.SetDefault("Used for crafting the Eye of Oblivion\n'Beep boop'");
        SacrificeTotal = 10;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.DarkRedRarity>();
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 0;
        Item.height = dims.Height;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Beeping");
        Item.useStyle = ItemUseStyleID.Thrust;
    }
}
