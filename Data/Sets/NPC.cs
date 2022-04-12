﻿using AvalonTesting.NPCs;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Data.Sets;

public static class NPC
{
    public static readonly bool[] Slimes = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.BlueSlime
        , NPCID.MotherSlime
        , NPCID.LavaSlime
        , NPCID.DungeonSlime
        , NPCID.CorruptSlime
        , NPCID.Slimer
        , NPCID.IlluminantSlime
        , NPCID.IceSlime
        , NPCID.Crimslime
        , NPCID.SpikedIceSlime
        , NPCID.SpikedJungleSlime
        , NPCID.UmbrellaSlime
        , NPCID.RainbowSlime
        , NPCID.SlimeMasked
        , NPCID.SlimeRibbonWhite
        , NPCID.SlimeRibbonYellow
        , NPCID.SlimeRibbonGreen
        , NPCID.SlimeRibbonRed
        , NPCID.SlimeSpiked
        , NPCID.SandSlime,
        ModContent.NPCType<DarkMotherSlime>(),
        ModContent.NPCType<DarkMatterSlime>()
    );


    public static readonly bool[] Toxic = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.Hornet
        , NPCID.ManEater
        , NPCID.GiantTortoise
        , NPCID.AngryTrapper
        , NPCID.MossHornet
        , NPCID.SpikedJungleSlime
        , NPCID.HornetFatty
        , NPCID.HornetHoney
        , NPCID.HornetLeafy
        , NPCID.HornetSpikey
        , NPCID.HornetStingy
        , NPCID.JungleCreeper
    );

    public static readonly bool[] Undead = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.Zombie
        , NPCID.Skeleton
        , NPCID.AngryBones
        , NPCID.DarkCaster
        , NPCID.CursedSkull
        , NPCID.UndeadMiner
        , NPCID.Tim
        , NPCID.DoctorBones
        , NPCID.TheGroom
        , NPCID.ArmoredSkeleton
        , NPCID.Mummy
        , NPCID.Wraith
        , NPCID.SkeletonArcher
        , NPCID.BaldZombie
        , NPCID.PossessedArmor
        , NPCID.VampireBat
        , NPCID.Vampire
        , NPCID.ZombieEskimo
        , NPCID.UndeadViking
        , NPCID.RuneWizard
        , NPCID.PincushionZombie
        , NPCID.SlimedZombie
        , NPCID.SwampZombie
        , NPCID.TwiggyZombie
        , NPCID.ArmoredViking
        , NPCID.FemaleZombie
        , NPCID.HeadacheSkeleton
        , NPCID.MisassembledSkeleton
        , NPCID.PantlessSkeleton
        , NPCID.ZombieRaincoat
        , NPCID.Eyezor
        , NPCID.Reaper
        , NPCID.ZombieMushroom
        , NPCID.ZombieMushroomHat
        , NPCID.ZombieDoctor
        , NPCID.ZombieSuperman
        , NPCID.ZombiePixie
        , NPCID.SkeletonTopHat
        , NPCID.SkeletonAstonaut
        , NPCID.SkeletonAlien
        , NPCID.ZombieXmas
        , NPCID.ZombieSweater
    );

    public static readonly bool[] Fiery = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.FireImp
        , NPCID.LavaSlime
        , NPCID.Hellbat
        , NPCID.Demon
        , NPCID.VoodooDemon
        , NPCID.Lavabat
        , NPCID.RedDevil,
        ModContent.NPCType<Blaze>(),
        ModContent.NPCType<ArmoredHellTortoise>()
    );

    public static readonly bool[] Watery = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.Piranha
        , NPCID.BlueJellyfish
        , NPCID.PinkJellyfish
        , NPCID.Shark
        , NPCID.Crab
        , NPCID.GreenJellyfish
        , NPCID.Arapaima
        , NPCID.SeaSnail
        , NPCID.Squid
        , NPCID.AnglerFish
    );

    public static readonly bool[] Earthen = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.GiantWormHead
        , NPCID.MotherSlime
        , NPCID.ManEater
        , NPCID.CaveBat
        , NPCID.Snatcher
        , NPCID.Antlion
        , NPCID.GiantBat
        , NPCID.DiggerHead
        , NPCID.GiantTortoise
        , NPCID.WallCreeper
        , NPCID.WallCreeperWall
    );

    public static readonly bool[] Flyer = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.DemonEye
        , NPCID.EaterofSouls
        , NPCID.Harpy
        , NPCID.CaveBat
        , NPCID.JungleBat
        , NPCID.Pixie
        , NPCID.WyvernHead
        , NPCID.GiantBat
        , NPCID.Crimera
        , NPCID.CataractEye
        , NPCID.SleepyEye
        , NPCID.DialatedEye
        , NPCID.GreenEye
        , NPCID.PurpleEye
        , NPCID.Moth
        , NPCID.FlyingFish
        , NPCID.FlyingSnake
        , NPCID.AngryNimbus
        , ModContent.NPCType<VampireHarpy>(),
        ModContent.NPCType<Dragonfly>()
    );

    public static readonly bool[] Frozen = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.IceSlime
        , NPCID.IceBat
        , NPCID.IceTortoise
        , NPCID.Wolf
        , NPCID.UndeadViking
        , NPCID.IceElemental
        , NPCID.PigronCorruption
        , NPCID.PigronHallow
        , NPCID.PigronCrimson
        , NPCID.SpikedIceSlime
        , NPCID.SnowFlinx
        , NPCID.IcyMerman
        , NPCID.IceGolem
    );

    public static readonly bool[] Wicked = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.EaterofSouls
        , NPCID.DevourerHead
        , NPCID.CorruptBunny
        , NPCID.CorruptGoldfish
        , NPCID.DarkMummy
        , NPCID.CorruptSlime
        , NPCID.CursedHammer
        , NPCID.Corruptor
        , NPCID.SeekerHead
        , NPCID.Clinger
        , NPCID.Slimer
        , NPCID.PigronCorruption
        , NPCID.Crimera
        , NPCID.Herpling
        , NPCID.CrimsonAxe
        , NPCID.PigronCrimson
        , NPCID.FaceMonster
        , NPCID.FloatyGross
        , NPCID.Crimslime
        , NPCID.BloodCrawler
        , NPCID.BloodCrawlerWall
        , NPCID.BloodFeeder
        , NPCID.BloodJelly
        , NPCID.IchorSticker
        , ModContent.NPCType<GuardianCorruptor>()
        , ModContent.NPCType<Bactus>()
        , ModContent.NPCType<Cougher>()
        , ModContent.NPCType<PyrasiteHead>()
        , ModContent.NPCType<PyrasiteBody>()
        , ModContent.NPCType<PyrasiteTail>()
        , ModContent.NPCType<Viris>()
        , ModContent.NPCType<Ickslime>()
        , ModContent.NPCType<Pigron>(),
        ModContent.NPCType<GrossyFloat>()
    );

    public static readonly bool[] Arcane = NPCID.Sets.Factory.CreateBoolSet(
        NPCID.Pixie
        , NPCID.LightMummy
        , NPCID.EnchantedSword
        , NPCID.Unicorn
        , NPCID.ChaosElemental
        , NPCID.Gastropod
        , NPCID.IlluminantBat
        , NPCID.IlluminantSlime
        , NPCID.PigronHallow
        , NPCID.RainbowSlime
        , ModContent.NPCType<Mime>()
    );
}