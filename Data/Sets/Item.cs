using System.Collections.Generic;
using Avalon.Items.Placeable.Seed;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data.Sets;

public static class Item
{
    public static readonly bool[] HerbSeeds = ItemID.Sets.Factory.CreateBoolSet(
        ItemID.BlinkrootSeeds,
        ItemID.DaybloomSeeds,
        ItemID.WaterleafSeeds,
        ItemID.FireblossomSeeds,
        ItemID.DeathweedSeeds,
        ItemID.MoonglowSeeds,
        ItemID.ShiverthornSeeds,
        ModContent.ItemType<BarfbushSeeds>(),
        ModContent.ItemType<BloodberrySeeds>(),
        ModContent.ItemType<SweetstemSeeds>(),
        ModContent.ItemType<HolybirdSeeds>(),
        ModContent.ItemType<TwilightPlumeSeeds>());

    /// <summary>
    /// not finished
    /// </summary>
    public static readonly List<int> Stations = new List<int>
    {
        TileID.WorkBenches, TileID.TinkerersWorkbench, TileID.Furnaces, TileID.Hellforge, TileID.AdamantiteForge,
        TileID.Anvils, TileID.MythrilAnvil, TileID.HeavyWorkBench, TileID.Sawmill, TileID.Bottles, ModContent.TileType<Tiles.NaquadahAnvil>(),
        ModContent.TileType<Tiles.SolariumAnvil>(), ModContent.TileType<Tiles.TroxiniumForge>(),
        ModContent.TileType<Tiles.NaquadahAnvil>(), TileID.Bookcases, TileID.Loom, TileID.Sinks, TileID.DyeVat
    };

    public static readonly bool[] StackTo2000 = ItemID.Sets.Factory.CreateBoolSet(
        ItemID.WoodenArrow, ItemID.FlamingArrow, ItemID.UnholyArrow, ItemID.JestersArrow, ItemID.MusketBall,
        ItemID.MeteorShot, ItemID.HellfireArrow, ItemID.SilverBullet, ItemID.CrystalBullet, ItemID.HolyArrow,
        ItemID.CursedArrow, ItemID.CursedBullet, ItemID.RocketI, ItemID.RocketII, ItemID.RocketIII,
        ItemID.RocketIV, ItemID.FrostburnArrow, ItemID.ChlorophyteBullet, ItemID.StyngerBolt, ItemID.Nail,
        ItemID.HighVelocityBullet, ItemID.IchorArrow, ItemID.IchorBullet, ItemID.VenomArrow, ItemID.VenomBullet,
        ItemID.PartyBullet, ItemID.NanoBullet, ItemID.ExplodingBullet, ItemID.GoldenBullet, ItemID.BlueSolution,
        ItemID.DarkBlueSolution, ItemID.GreenSolution, ItemID.PurpleSolution, ItemID.RedSolution, ItemID.ChlorophyteArrow,
        ItemID.BoneArrow, ItemID.MoonlordArrow, ItemID.CandyCorn, ItemID.ExplosiveJackOLantern, ItemID.Stake,
        ItemID.MoonlordBullet, ItemID.ClusterRocketI, ItemID.ClusterRocketII, ItemID.WetRocket, ItemID.LavaRocket,
        ItemID.HoneyRocket, ItemID.MiniNukeI, ItemID.MiniNukeII, ItemID.DryRocket, ItemID.TungstenBullet);

    public static readonly bool[] StackTo999 = ItemID.Sets.Factory.CreateBoolSet(
        ItemID.Mushroom, ItemID.Torch, ItemID.GoldBar, ItemID.CopperBar, ItemID.SilverBar, ItemID.IronBar,
        ItemID.Acorn, ItemID.LifeCrystal, ItemID.Bottle, ItemID.Lens, ItemID.DemoniteBar, ItemID.CorruptSeeds,
        ItemID.VileMushroom, ItemID.GrassSeeds, ItemID.Sunflower, ItemID.PurificationPowder, ItemID.VilePowder,
        ItemID.RottenChunk, ItemID.WormTooth, ItemID.FallenStar, ItemID.ShadowScale,
        ItemID.ManaCrystal, ItemID.MeteoriteBar, ItemID.Hook, ItemID.Book, ItemID.Bomb, ItemID.Grenade,
        ItemID.HellstoneBar, ItemID.MushroomGrassSeeds, ItemID.JungleGrassSeeds, ItemID.Stinger, ItemID.Vine,
        ItemID.StickyBomb, ItemID.BlackLens, ItemID.BlackThread, ItemID.GreenThread, ItemID.Leather,
        ItemID.Glowstick, ItemID.StickyGlowstick, ItemID.DaybloomSeeds, ItemID.MoonglowSeeds,
        ItemID.BlinkrootSeeds, ItemID.DeathweedSeeds, ItemID.WaterleafSeeds, ItemID.FireblossomSeeds,
        ItemID.Daybloom, ItemID.Moonglow, ItemID.Blinkroot, ItemID.Deathweed, ItemID.Waterleaf,
        ItemID.Fireblossom, ItemID.SharkFin, ItemID.Feather, ItemID.AntlionMandible, ItemID.IllegalGunParts,
        ItemID.GoldenKey, ItemID.JungleSpores, ItemID.TatteredCloth, ItemID.HallowedSeeds, ItemID.CobaltBar,
        ItemID.MythrilBar, ItemID.AdamantiteBar, ItemID.BlueTorch, ItemID.RedTorch, ItemID.GreenTorch,
        ItemID.PurpleTorch, ItemID.WhiteTorch, ItemID.YellowTorch, ItemID.DemonTorch, ItemID.PixieDust,
        ItemID.CursedFlame, ItemID.CursedTorch, ItemID.UnicornHorn, ItemID.DarkShard, ItemID.LightShard,
        ItemID.TinBar, ItemID.LeadBar, ItemID.TungstenBar, ItemID.PlatinumBar, ItemID.StickyGrenade,
        ItemID.Marshmallow, ItemID.IceTorch, ItemID.PinkThread, ItemID.ChlorophyteBar, ItemID.TealMushroom,
        ItemID.GreenMushroom, ItemID.SkyBlueFlower, ItemID.YellowMarigold, ItemID.BlueBerries, ItemID.LimeKelp,
        ItemID.PinkPricklyPear, ItemID.OrangeBloodroot, ItemID.RedHusk, ItemID.CyanHusk, ItemID.VioletHusk,
        ItemID.PurpleMucos, ItemID.BlackInk, ItemID.TempleKey, ItemID.PalladiumBar, ItemID.OrichalcumBar,
        ItemID.TitaniumBar, ItemID.HallowedBar, ItemID.ChlorophyteArrow, ItemID.OrangeTorch, ItemID.CrimtaneBar,
        ItemID.LifeFruit, ItemID.LihzahrdPowerCell, ItemID.TurtleShell, ItemID.TissueSample, ItemID.Vertebrae,
        ItemID.Ichor, ItemID.IchorTorch, ItemID.VialofVenom, ItemID.Ectoplasm, ItemID.GiantHarpyFeather, ItemID.BoneFeather,
        ItemID.FireFeather, ItemID.IceFeather, ItemID.BrokenBatWing, ItemID.TatteredBeeWing, ItemID.JungleKey,
        ItemID.CorruptionKey, ItemID.CrimsonKey, ItemID.HallowedKey, ItemID.FrozenKey, ItemID.ShroomiteBar,
        ItemID.BrokenHeroSword, ItemID.ButterflyDust, ItemID.GlassPlatform, ItemID.GoodieBag,
        ItemID.JungleKeyMold, ItemID.CorruptionKeyMold, ItemID.CrimsonKeyMold, ItemID.HallowedKeyMold,
        ItemID.FrozenKeyMold, ItemID.BlackFairyDust, ItemID.PumpkinSeed, ItemID.SpiderFang,
        ItemID.SpookyTwig, ItemID.Holly, ItemID.Coal, ItemID.CrimsonSeeds, ItemID.BeetleHusk,
        ItemID.UltrabrightTorch, ItemID.ShiverthornSeeds, ItemID.Shiverthorn, ItemID.BeeWax);

    public static readonly bool[] StackTo100 = ItemID.Sets.Factory.CreateBoolSet(
        ItemID.BottledWater, ItemID.Dynamite, ItemID.ObsidianSkinPotion, ItemID.RegenerationPotion,
        ItemID.SwiftnessPotion, ItemID.GillsPotion, ItemID.IronskinPotion, ItemID.ManaRegenerationPotion,
        ItemID.MagicPowerPotion, ItemID.FeatherfallPotion, ItemID.SpelunkerPotion, ItemID.InvisibilityPotion,
        ItemID.ShinePotion, ItemID.NightOwlPotion, ItemID.BattlePotion, ItemID.ThornsPotion,
        ItemID.WaterWalkingPotion, ItemID.ArcheryPotion, ItemID.HunterPotion, ItemID.GravitationPotion,
        ItemID.GreaterManaPotion, ItemID.CookedMarshmallow, ItemID.FlaskofVenom, ItemID.FlaskofCursedFlames,
        ItemID.FlaskofFire, ItemID.FlaskofGold, ItemID.FlaskofIchor, ItemID.FlaskofNanites, ItemID.FlaskofParty,
        ItemID.FlaskofPoison, ItemID.MiningPotion, ItemID.HeartreachPotion, ItemID.CalmingPotion,
        ItemID.BuilderPotion, ItemID.TitanPotion, ItemID.FlipperPotion, ItemID.SummoningPotion,
        ItemID.TrapsightPotion, ItemID.AmmoReservationPotion, ItemID.LifeforcePotion, ItemID.EndurancePotion,
        ItemID.RagePotion, ItemID.InfernoPotion, ItemID.WrathPotion, ItemID.RecallPotion,
        ItemID.TeleportationPotion, ItemID.LovePotion, ItemID.StinkPotion, ItemID.FishingPotion,
        ItemID.SonarPotion, ItemID.CratePotion, ItemID.WarmthPotion, ItemID.GenderChangePotion, ItemID.WormholePotion,
        ItemID.LuckPotion, ItemID.LuckPotionGreater, ItemID.LuckPotionLesser, ItemID.PotionOfReturn, ItemID.RedPotion);
}
