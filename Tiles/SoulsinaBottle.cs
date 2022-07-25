using Avalon.Items.Placeable.Light;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;

namespace Avalon.Tiles;

public class SoulsinaBottle : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        AddMapEntry(new Color(255, 186, 212), LanguageManager.Instance.GetText("Soul in a Bottle"));
        AnimationFrameHeight = 36;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = false;
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
        TileObjectData.addTile(Type);
        DustType = DustID.Glass;
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        frameCounter++;
        if (frameCounter > 6)
        {
            frameCounter = 0;
            frame++;
            if (frame >= 4)
                frame = 0;
        }
    }
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        int itemToDrop = 0;
        switch (frameX / 18)
        {
            case 0:
                itemToDrop = ModContent.ItemType<SoulofBlightinaBottle>();
                break;
            case 1:
                itemToDrop = ModContent.ItemType<SoulofTortureinaBottle>();
                break;
            case 2:
                itemToDrop = ModContent.ItemType<SoulofDelightinaBottle>();
                break;
            case 3:
                itemToDrop = ModContent.ItemType<SoulofIceinaBottle>();
                break;
            case 4:
                itemToDrop = ModContent.ItemType<SoulofTimeinaBottle>();
                break;
            case 5:
                itemToDrop = ModContent.ItemType<SoulofHumidityinaBottle>();
                break;
        }
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 32, itemToDrop);
    }
    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        switch (Main.tile[i, j].TileFrameX / 18)
        {
            case 0:
                r = 226f / 255f;
                g = 226f / 255f;
                b = 226f / 255f;
                break;
            case 1:
                r = 1f;
                g = 140f / 255f;
                b = 140f / 255f;
                break;
            case 2:
                r = 225f / 255f;
                g = 201f / 255f;
                b = 1f;
                break;
            case 3:
                r = 116f / 255f;
                g = 208f / 255f;
                b = 245f / 255f;
                break;
            case 4:
                r = 252f / 255f;
                g = 229f / 255f;
                b = 87f / 255f;
                break;
            case 5:
                r = 122f / 255f;
                g = 1f;
                b = 110f / 255f;
                break;
        }
    }
}
