using AvalonTesting.Systems;
using AvalonTesting.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AvalonTesting.Tiles;

public class HallowedAltar : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(255, 216, 0), LanguageManager.Instance.GetText("Hallowed Altar"));
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.CoordinateHeights = new[] {16, 18};
        TileObjectData.addTile(Type);
        Main.tileHammer[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        DustType = DustID.HallowedWeapons;
        TileID.Sets.PreventsTileRemovalIfOnTopOfIt[Type] = true;
        TileID.Sets.InteractibleByNPCs[Type] = true;
        HitSound = new SoundStyle("AvalonTesting/Sounds/Item/HallowedAltarHit");
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        float brightness = Main.rand.Next(-5, 6) * 0.0025f;
        r = 1f + brightness;
        g = 0.9f + (brightness * 2f);
        b = 0f;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        if (!ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && !Main.hardMode)
        {
            blockDamaged = false;
        }

        return ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && Main.hardMode;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        if (ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && Main.hardMode)
        {
            SmashHallowAltar(i, j);
            SoundEngine.PlaySound(new SoundStyle("AvalonTesting/Sounds/Item/HallowedAltarBreak"), new Vector2(i * 16, j * 16));
            //var R = new Rectangle(i * 16 - 16, j * 16 - 16, 72, 72);
            //var R2 = new Rectangle(i * 16, j * 16 - 8, 24, 24);
            //var R3 = new Rectangle(i * 16 + 16, j * 16 + 24, 16, 16);
            //int C = 30;
            //float vR = .2f;

            //Vector2 wholeCookiePos = new Vector2(i * 16 - 16, j * 16 - 16);
            //Vector2 chip1Pos = new Vector2(i * 16, j * 16 - 8);
            //Vector2 chip2Pos = new Vector2(i * 16 + 16, j * 16 + 24);

            //for (int i2 = 1; i2 <= C; i2++)
            //{
            //    int D2 = Dust.NewDust(wholeCookiePos, R.Width, R.Height, DustID.Sand, 0, 0, 100, new Color(), 2f);
            //    Main.dust[D2].noGravity = true;
            //    Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (wholeCookiePos.X + 12));
            //    Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (wholeCookiePos.Y + 8));
            //}

            //for (int i2 = 1; i2 <= 15; i2++)
            //{
            //    int D2 = Dust.NewDust(chip1Pos, R2.Width, R2.Height, DustID.Mud, 0, 0, 100, new Color(), 2f);
            //    Main.dust[D2].noGravity = true;
            //    Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (chip1Pos.X + 12));
            //    Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (chip1Pos.Y + 8));
            //}
            //for (int i2 = 1; i2 <= 15; i2++)
            //{
            //    int D2 = Dust.NewDust(chip2Pos, R3.Width, R3.Height, DustID.Mud, 0, 0, 100, new Color(), 2f);
            //    Main.dust[D2].noGravity = true;
            //    Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (chip2Pos.X + 12));
            //    Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (chip2Pos.Y + 8));
            //}

            var R = new Rectangle(i * 16 + 16, j * 16 + 8, 8, 8 * 5);
            var R2 = new Rectangle(i * 16 + 16, j * 16 + 8, 24, 8);
            var R3 = new Rectangle(i * 16 + 16, j * 16 + 8, 24, 40);
            int C = 30;
            float vR = .2f;
            Vector2 pinkPos = new Vector2(i * 16 + 10, j * 16);
            Vector2 horizPos = new Vector2(i * 16 + 10, j * 16 + 16);
            Vector2 vertPos = new Vector2(i * 16 + 26, j * 16);
            for (int i2 = 1; i2 <= C; i2++)
            {
                int D2 = Dust.NewDust(pinkPos, R3.Width, R3.Height, DustID.Enchanted_Pink, 0, 0, 100, new Color(), 2f);
                Main.dust[D2].noGravity = true;
                Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (pinkPos.X + 12));
                Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (pinkPos.Y + 8));
            }

            for (int i2 = 1; i2 <= C; i2++)
            {
                int D = Dust.NewDust(vertPos, R.Width, R.Height, DustID.Enchanted_Gold, 0, 0, 100, new Color(), 2f);
                Main.dust[D].noGravity = true;
                Main.dust[D].velocity.X = vR * (Main.dust[D].position.X - (vertPos.X + 12));
                Main.dust[D].velocity.Y = vR * (Main.dust[D].position.Y - (vertPos.Y + 8));
            }

            for (int i2 = 1; i2 <= C; i2++)
            {
                int D2 = Dust.NewDust(horizPos, R2.Width, R2.Height, DustID.Enchanted_Gold, 0, 0, 100, new Color(), 2f);
                Main.dust[D2].noGravity = true;
                Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (horizPos.X + 12));
                Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (horizPos.Y + 8));
            }
        }
    }

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (Main.rand.Next(60) == 1)
        {
            int num162 = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.HallowedWeapons, 0f, 0f, 0, default,
                1.5f);
            Main.dust[num162].noGravity = true;
            Main.dust[num162].velocity *= 1f;
        }
    }

    public static void SmashHallowAltar(int i, int j)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            return;
        }

        if (!ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && !Main.hardMode)
        {
            return;
        }

        if (WorldGen.noTileActions)
        {
            return;
        }

        if (WorldGen.gen)
        {
            return;
        }

        int num = ModContent.GetInstance<ExxoWorldGen>().HallowedAltarCount % 2;
        int num2 = (ModContent.GetInstance<ExxoWorldGen>().HallowedAltarCount / 2) + 1;
        float num3 = Main.maxTilesX / 4200;
        int num4 = 1 - num;
        num3 = (num3 * 310f) - (85 * num);
        num3 *= 0.85f;
        num3 /= num2;
        if (num == 0)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                if (ModContent.GetInstance<ExxoWorldGen>().SHMTier1Ore == ExxoWorldGen.SHMTier1Variant.Tritanorium)
                {
                    Main.NewText("Your world has been invigorated with Tritanorium!", 117, 158, 107);
                }
                else
                {
                    Main.NewText("Your world has been melted with Pyroscoric!", 187, 35, 0);
                }
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                if (ModContent.GetInstance<ExxoWorldGen>().SHMTier1Ore == ExxoWorldGen.SHMTier1Variant.Tritanorium)
                {
                    ChatHelper.BroadcastChatMessage(
                        NetworkText.FromLiteral("Your world has been invigorated with Tritanorium!"),
                        new Color(117, 158, 107));
                }
                else
                {
                    ChatHelper.BroadcastChatMessage(
                        NetworkText.FromLiteral("Your world has been melted with Pyroscoric!"), new Color(187, 35, 0));
                }
            }

            num = ModContent.GetInstance<ExxoWorldGen>().SHMTier1Ore.GetSHMTier1VariantTileOre();
            num3 *= 1.05f;
        }
        else if (num == 1)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                if (ModContent.GetInstance<ExxoWorldGen>().SHMTier2Ore == ExxoWorldGen.SHMTier2Variant.Unvolandite)
                {
                    Main.NewText("Your world has been blessed with Unvolandite!", 171, 119, 75);
                }
                else
                {
                    Main.NewText("Your world has been blessed with Vorazylcum!", 123, 95, 126);
                }
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                if (ModContent.GetInstance<ExxoWorldGen>().SHMTier2Ore == ExxoWorldGen.SHMTier2Variant.Unvolandite)
                {
                    ChatHelper.BroadcastChatMessage(
                        NetworkText.FromLiteral("Your world has been blessed with Unvolandite!"),
                        new Color(171, 119, 75));
                }
                else
                {
                    ChatHelper.BroadcastChatMessage(
                        NetworkText.FromLiteral("Your world has been blessed with Vorazylcum!"),
                        new Color(123, 95, 126));
                }
            }

            num = ModContent.GetInstance<ExxoWorldGen>().SHMTier2Ore.GetSHMTier2VariantTileOre();
        }

        int num11 = 0;
        while (num11 < num3)
        {
            int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double num12 = Main.worldSurface;
            if (num == 1)
            {
                num12 = Main.rockLayer;
            }

            if (num == 2 || num == 3)
            {
                num12 = (Main.rockLayer + Main.rockLayer + Main.maxTilesY) / 3.0;
            }

            int j2 = WorldGen.genRand.Next((int)num12, Main.maxTilesY - 150);
            switch (num)
            {
                case 0:
                    num = ModContent.TileType<TritanoriumOre>();
                    break;
                case 1:
                    num = ModContent.TileType<PyroscoricOre>();
                    break;
                case 2:
                    num = ModContent.TileType<UnvolanditeOre>();
                    break;
                case 3:
                    num = ModContent.TileType<VorazylcumOre>();
                    break;
            }

            WorldGen.OreRunner(i2, j2, WorldGen.genRand.Next(5, 9 + num4), WorldGen.genRand.Next(5, 9 + num4),
                (ushort)num);
            num11++;
        }

        ModContent.GetInstance<ExxoWorldGen>().HallowedAltarCount++;
    }
}
