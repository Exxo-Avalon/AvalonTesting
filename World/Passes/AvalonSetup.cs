using System;
using Avalon.Items.Placeable.Bar;
using Avalon.Systems;
using Avalon.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.World.Passes;

public class AvalonSetup : GenPass
{
    public AvalonSetup()
        : base("AvalonSetup", 10)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Setting up Avalonian World Gen";

        ModContent.GetInstance<ExxoWorldGen>().CopperOre ??=
            Enum.GetValues<ExxoWorldGen.CopperVariant>().GetRandomValue();

        switch (ModContent.GetInstance<ExxoWorldGen>().CopperOre)
        {
            case ExxoWorldGen.CopperVariant.Copper:
                WorldGen.SavedOreTiers.Copper = TileID.Copper;
                WorldGen.copperBar = ItemID.CopperBar;
                break;

            case ExxoWorldGen.CopperVariant.Tin:
                WorldGen.SavedOreTiers.Copper = TileID.Tin;
                WorldGen.copperBar = ItemID.TinBar;
                break;

            case ExxoWorldGen.CopperVariant.Bronze:
                WorldGen.SavedOreTiers.Copper = (ushort)ModContent.TileType<BronzeOre>();
                WorldGen.copperBar = ModContent.ItemType<BronzeBar>();
                break;
        }

        ModContent.GetInstance<ExxoWorldGen>().IronOre ??=
            Enum.GetValues<ExxoWorldGen.IronVariant>().GetRandomValue();

        switch (ModContent.GetInstance<ExxoWorldGen>().IronOre)
        {
            case ExxoWorldGen.IronVariant.Iron:
                WorldGen.SavedOreTiers.Iron = TileID.Iron;
                WorldGen.ironBar = ItemID.IronBar;
                break;

            case ExxoWorldGen.IronVariant.Lead:
                WorldGen.SavedOreTiers.Iron = TileID.Lead;
                WorldGen.ironBar = ItemID.LeadBar;
                break;

            case ExxoWorldGen.IronVariant.Nickel:
                WorldGen.SavedOreTiers.Iron = (ushort)ModContent.TileType<NickelOre>();
                WorldGen.ironBar = ModContent.ItemType<NickelBar>();
                break;
        }

        ModContent.GetInstance<ExxoWorldGen>().SilverOre ??=
            Enum.GetValues<ExxoWorldGen.SilverVariant>().GetRandomValue();

        switch (ModContent.GetInstance<ExxoWorldGen>().SilverOre)
        {
            case ExxoWorldGen.SilverVariant.Silver:
                WorldGen.SavedOreTiers.Silver = TileID.Silver;
                WorldGen.silverBar = ItemID.SilverBar;
                break;

            case ExxoWorldGen.SilverVariant.Tungsten:
                WorldGen.SavedOreTiers.Silver = TileID.Tungsten;
                WorldGen.silverBar = ItemID.TungstenBar;
                break;

            case ExxoWorldGen.SilverVariant.Zinc:
                WorldGen.SavedOreTiers.Silver = (ushort)ModContent.TileType<ZincOre>();
                WorldGen.silverBar = ModContent.ItemType<ZincBar>();
                break;
        }

        ModContent.GetInstance<ExxoWorldGen>().GoldOre ??=
            Enum.GetValues<ExxoWorldGen.GoldVariant>().GetRandomValue();

        switch (ModContent.GetInstance<ExxoWorldGen>().GoldOre)
        {
            case ExxoWorldGen.GoldVariant.Gold:
                WorldGen.SavedOreTiers.Gold = TileID.Gold;
                WorldGen.goldBar = ItemID.GoldBar;
                break;

            case ExxoWorldGen.GoldVariant.Platinum:
                WorldGen.SavedOreTiers.Gold = TileID.Platinum;
                WorldGen.goldBar = ItemID.PlatinumBar;
                break;

            case ExxoWorldGen.GoldVariant.Bismuth:
                WorldGen.SavedOreTiers.Gold = (ushort)ModContent.TileType<BismuthOre>();
                WorldGen.goldBar = ModContent.ItemType<BismuthBar>();
                break;
        }

        ModContent.GetInstance<ExxoWorldGen>().RhodiumOre ??=
            Enum.GetValues<ExxoWorldGen.RhodiumVariant>().GetRandomValue();

        ModContent.GetInstance<ExxoWorldGen>().CobaltOre ??=
            Enum.GetValues<ExxoWorldGen.CobaltVariant>().GetRandomValue();

        switch (ModContent.GetInstance<ExxoWorldGen>().CobaltOre)
        {
            case ExxoWorldGen.CobaltVariant.Cobalt:
                WorldGen.SavedOreTiers.Cobalt = TileID.Cobalt;
                break;

            case ExxoWorldGen.CobaltVariant.Palladium:
                WorldGen.SavedOreTiers.Cobalt = TileID.Palladium;
                break;

            case ExxoWorldGen.CobaltVariant.Duratanium:
                WorldGen.SavedOreTiers.Cobalt = (ushort)ModContent.TileType<DurataniumOre>();
                break;
        }

        ModContent.GetInstance<ExxoWorldGen>().MythrilOre ??=
            Enum.GetValues<ExxoWorldGen.MythrilVariant>().GetRandomValue();

        switch (ModContent.GetInstance<ExxoWorldGen>().MythrilOre)
        {
            case ExxoWorldGen.MythrilVariant.Mythril:
                WorldGen.SavedOreTiers.Mythril = TileID.Mythril;
                break;

            case ExxoWorldGen.MythrilVariant.Orichalcum:
                WorldGen.SavedOreTiers.Mythril = TileID.Orichalcum;
                break;

            case ExxoWorldGen.MythrilVariant.Naquadah:
                WorldGen.SavedOreTiers.Mythril = (ushort)ModContent.TileType<NaquadahOre>();
                ;
                break;
        }

        ModContent.GetInstance<ExxoWorldGen>().AdamantiteOre ??=
            Enum.GetValues<ExxoWorldGen.AdamantiteVariant>().GetRandomValue();

        switch (ModContent.GetInstance<ExxoWorldGen>().AdamantiteOre)
        {
            case ExxoWorldGen.AdamantiteVariant.Adamantite:
                WorldGen.SavedOreTiers.Adamantite = TileID.Adamantite;
                break;

            case ExxoWorldGen.AdamantiteVariant.Titanium:
                WorldGen.SavedOreTiers.Adamantite = TileID.Titanium;
                break;

            case ExxoWorldGen.AdamantiteVariant.Troxinium:
                WorldGen.SavedOreTiers.Adamantite = (ushort)ModContent.TileType<TroxiniumOre>();
                ;
                break;
        }

        ModContent.GetInstance<ExxoWorldGen>().SHMTier1Ore ??=
            Enum.GetValues<ExxoWorldGen.SHMTier1Variant>().GetRandomValue();

        ModContent.GetInstance<ExxoWorldGen>().SHMTier2Ore ??=
            Enum.GetValues<ExxoWorldGen.SHMTier2Variant>().GetRandomValue();
    }
}
