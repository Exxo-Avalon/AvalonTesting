// using AvalonTesting.Dusts;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using ReLogic.Content;
// using Terraria;
// using Terraria.ModLoader;
//
// namespace AvalonTesting.Biomes;
//
// public class ContagionWaterStyle : ModWaterStyle
// {
//     public override int ChooseWaterfallStyle()
//     {
//         return Mod.Find<ModWaterfallStyle>("ContagionWaterfallStyle").Slot;
//     }
//
//     public override int GetSplashDust()
//     {
//         return ModContent.DustType<ContagionWaterSplash>();
//     }
//
//     public override int GetDropletGore()
//     {
//         return Mod.Find<ModGore>("ContagionDroplet").Type;
//     }
//
//     public override void LightColorMultiplier(ref float r, ref float g, ref float b)
//     {
//         r = 1f;
//         g = 1f;
//         b = 1f;
//     }
//
//     public override Color BiomeHairColor()
//     {
//         return Color.LimeGreen;
//     }
//
//     public override byte GetRainVariant()
//     {
//         return (byte)Main.rand.Next(3);
//     }
//
//     public override Asset<Texture2D> GetRainTexture()
//     {
//         return Mod.Assets.Request<Texture2D>("Biomes/ContagionRain");
//     }
// }


