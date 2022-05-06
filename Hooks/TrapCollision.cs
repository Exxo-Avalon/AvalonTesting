using AvalonTesting.Common;
using AvalonTesting.Tiles;
using Microsoft.Xna.Framework;
using On.Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Hooks;

[Autoload(Side = ModSide.Both)]
public class TrapCollision : ModHook
{
    protected override void Apply() => Collision.HurtTiles += OnHurtTiles;

    private static Vector2 OnHurtTiles(Collision.orig_HurtTiles orig, Vector2 position,
                                       Vector2 velocity, int width, int height, bool fireImmune = false)
    {
        Vector2 output = orig(position, velocity, width, height, fireImmune);
        Vector2 vector = position;
        int num = (int)(position.X / 16f) - 1;
        int num2 = (int)((position.X + width) / 16f) + 2;
        int num3 = (int)(position.Y / 16f) - 1;
        int num4 = (int)((position.Y + height) / 16f) + 2;
        if (num < 0)
        {
            num = 0;
        }

        if (num2 > Terraria.Main.maxTilesX)
        {
            num2 = Terraria.Main.maxTilesX;
        }

        if (num3 < 0)
        {
            num3 = 0;
        }

        if (num4 > Terraria.Main.maxTilesY)
        {
            num4 = Terraria.Main.maxTilesY;
        }

        for (int i = num; i < num2; i++)
        {
            for (int j = num3; j < num4; j++)
            {
                if (Terraria.Main.tile[i, j].Slope == SlopeType.Solid &&
                    !Terraria.Main.tile[i, j].IsActuated && Terraria.Main.tile[i, j].HasTile &&
                    (Terraria.Main.tile[i, j].TileType == ModContent.TileType<VenomSpike>() ||
                     Terraria.Main.tile[i, j].TileType == TileID.Spikes ||
                     Terraria.Main.tile[i, j].TileType == TileID.WoodenSpikes))
                {
                    Vector2 vector2;
                    vector2.X = i * 16;
                    vector2.Y = j * 16;
                    int num5 = 0;
                    int type = Terraria.Main.tile[i, j].TileType;
                    int num6 = 16;
                    if (Terraria.Main.tile[i, j].IsHalfBlock)
                    {
                        vector2.Y += 8f;
                        num6 -= 8;
                    }

                    if (vector.X + width >= vector2.X && vector.X <= vector2.X + 16f &&
                        vector.Y + height >= vector2.Y && vector.Y <= vector2.Y + num6 + 0.01)
                    {
                        int num9 = 1;
                        if (vector.X + (width / 2f) < vector2.X + 8f)
                        {
                            num9 = -1;
                        }

                        if (!fireImmune && (type == 37 || type == 58 || type == 76))
                        {
                            num5 = 20;
                        }

                        if (!Terraria.Main.LocalPlayer.Avalon().trapImmune &&
                            !Terraria.Main.LocalPlayer.Avalon().spikeImmune && type == TileID.Spikes)
                        {
                            num5 = 40;
                        }

                        if (!Terraria.Main.LocalPlayer.Avalon().trapImmune &&
                            !Terraria.Main.LocalPlayer.Avalon().spikeImmune && type == TileID.WoodenSpikes)
                        {
                            num5 = 60;
                        }

                        // if (!Main.LocalPlayer.Avalon().trapImmune && !Main.LocalPlayer.Avalon().spikeImmune && type == ModContent.TileType<Tiles.PoisonSpike>())
                        // {
                        //    num5 = 35;
                        //    Main.player[Main.myPlayer].AddBuff(BuffID.Poisoned, 180, true);
                        // }
                        if (!Terraria.Main.LocalPlayer.Avalon().trapImmune &&
                            !Terraria.Main.LocalPlayer.Avalon().spikeImmune &&
                            type == ModContent.TileType<VenomSpike>())
                        {
                            num5 = 90;
                            Terraria.Main.player[Terraria.Main.myPlayer].AddBuff(BuffID.Venom, 180);
                        }

                        return new Vector2(num9, num5);
                    }
                }
            }
        }

        return output;
    }
}
