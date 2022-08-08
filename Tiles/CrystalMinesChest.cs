using Avalon.Common;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class CrystalMinesChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.CrystalMinesChest>();
    protected override int ChestKeyItemId => ModContent.ItemType<Items.Other.CrystalMinesKey>();
    protected override bool CanBeLocked => true;
    protected override Color LockedMapColor => new(188, 119, 247);
    protected override Color UnlockedMapColor => new(188, 119, 247);

    public override bool IsLockedChest(int i, int j)
    {
        return Main.tile[i, j].TileFrameX >= 36;
    }
    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Crystal Mines Chest");
        DustType = ModContent.DustType<CrystalDust>();
        base.SetStaticDefaults();
    }
    public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
    {
        if (Main.tile[i, j].TileFrameX >= 36)
        {
            frameXAdjustment = -36;
            return true;
        }
        return false;
    }
}
