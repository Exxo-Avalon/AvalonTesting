using AvalonTesting.Dusts;
using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AvalonTesting.Tiles;

public class LivingLightning : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        SoundType = SoundID.Dig;
        ItemDrop = ModContent.ItemType<LivingLightningBlock>();
        AddMapEntry(new Color(196, 142, 238));
        AnimationFrameHeight = 90;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = false;
        Main.tileFrameImportant[Type] = false;
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 1;
        TileObjectData.newTile.Origin = new Point16(0, 0);
        TileObjectData.newTile.CoordinateHeights = new int[1] {16};
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.HookCheckIfCanPlace =
            new PlacementHook(CanPlaceAlter, -1, 0, processedCoordinates: true);
        TileObjectData.newTile.UsesCustomCanPlace = true;
        TileObjectData.newTile.HookPostPlaceMyPlayer =
            new PlacementHook(AfterPlacement, -1, 0, processedCoordinates: false);
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<LightningDust>();
    }

    public int CanPlaceAlter(int i, int j, int type, int style, int direction, int alternate)
    {
        return 1;
    }

    public static int AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            NetMessage.SendTileSquare(Main.myPlayer, i, j, 1, 1);
        }

        return 1;
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
        frame = Main.tileFrame[TileID.LivingFire];
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 0.6f;
        g = 0.1f;
        b = 0.6f;
    }
}
