﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace AvalonTesting.Projectiles.Torches;

public class UltrabrightTorch : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ultrabright Torch");
    }

    public override void SetDefaults()
    {
        Projectile.width = 6;
        Projectile.height = 14;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.light = 1f;
        Projectile.damage = 0;
        Projectile.DamageType = DamageClass.Ranged;// projectile.tileCollide = true /* tModPorter - this is redundant, for more info see https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide#damage-classes */ ;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 10);
        if (Projectile.aiStyle == 1)
        {
            int it = ItemID.UltrabrightTorch;
            int style = 12;
            int TileX = (int)(Projectile.position.X + Projectile.width * 0.5f) / 16;
            int TileY = (int)(Projectile.position.Y + Projectile.height * 0.5f) / 16;

            if (TileX < 0 || TileX >= Main.maxTilesX || TileY < 0 || TileY >= Main.maxTilesY)
            {
                Projectile.active = false;
                return;
            }
            if (!Main.tile[TileX, TileY].HasTile)
            {
                WorldGen.PlaceTile(TileX, TileY, 4, false, true, -1, 0);
                // not sure if PlaceTile calls TileFrame
                WorldGen.TileFrame(TileX, TileY);
                Main.tile[TileX, TileY].TileFrameY = (short)(style * 22);
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    NetMessage.SendData(MessageID.TileChange, -1, -1, null, 1, TileX, TileY, 4, style);
                }
                Projectile.active = false;
            }
            else
            {
                Item.NewItem(Projectile.GetItemSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, 16, 16, it);
                Projectile.active = false;
            }

            if (!Main.tile[TileX, TileY].HasTile && (Main.tile[TileX + 1, TileY + 1].HasTile || Main.tile[TileX - 1, TileY + 1].HasTile || Main.tile[TileX + 1, TileY - 1].HasTile || Main.tile[TileX - 1, TileY - 1].HasTile) && !Main.tile[TileX, TileY + 1].HasTile)
            {
                Item.NewItem(Projectile.GetItemSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, 16, 16, it);
                Projectile.active = false;
            }
            if (Main.tile[TileX, TileY].LiquidAmount > 0)
            {
                Item.NewItem(Projectile.GetItemSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, 16, 16, it);
                Projectile.active = false;
            }
            if (Main.tile[TileX, TileY + 1].Slope != SlopeType.Solid || Main.tile[TileX, TileY + 1].IsHalfBlock)
            {
                Item.NewItem(Projectile.GetItemSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, 16, 16, it);
                Projectile.active = false;
            }
            Projectile.active = false;
        }
    }
}