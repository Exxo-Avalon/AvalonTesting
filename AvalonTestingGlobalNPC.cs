using System.Collections.Generic;
using System.Linq;
using AvalonTesting.Buffs;
using AvalonTesting.Buffs.AdvancedBuffs;
using AvalonTesting.DropConditions;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Armor;
using AvalonTesting.Items.Consumables;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Other;
using AvalonTesting.Items.Placeable.Painting;
using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Items.Potions;
using AvalonTesting.Items.Tokens;
using AvalonTesting.Items.Tools;
using AvalonTesting.Items.Vanity;
using AvalonTesting.Items.Weapons.Magic;
using AvalonTesting.Items.Weapons.Melee;
using AvalonTesting.NPCs;
using AvalonTesting.NPCs.Bosses;
using AvalonTesting.Players;
using AvalonTesting.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting;

public class AvalonTestingGlobalNPC : GlobalNPC
{
    private const int UncommonChance = 50;
    private const int RareChance = 700;
    private const int VeryRareChance = 1000;
    public static int boogerBoss = 0;
    public static float endoSpawnRate = 0.25f;
    public static bool savedIceman = false;

    public static List<int> shmMobs = new()
    {
        NPCID.Creeper,
        NPCID.Pumpking,
        NPCID.SantaNK1,
        ModContent.NPCType<AegisHallowor>(),
        ModContent.NPCType<ArmageddonSlime>(),
        ModContent.NPCType<ArmoredHellTortoise>(),
        ModContent.NPCType<ArmoredWraith>(),
        ModContent.NPCType<BactusMinion>(), // remove later
        ModContent.NPCType<BombBones>(),
        ModContent.NPCType<NPCs.BombSkeleton>(),
        ModContent.NPCType<CloudBat>(),
        ModContent.NPCType<CometTail>(),
        ModContent.NPCType<CrystalBones>(),
        ModContent.NPCType<CrystalSpectre>(),
        ModContent.NPCType<CursedMagmaSkeleton>(),
        ModContent.NPCType<DarkMatterSlime>(),
        ModContent.NPCType<DarkMotherSlime>(),
        ModContent.NPCType<Dragonfly>(),
        ModContent.NPCType<DragonLordBody>(),
        ModContent.NPCType<DragonLordBody2>(),
        ModContent.NPCType<DragonLordBody3>(),
        ModContent.NPCType<DragonLordHead>(),
        ModContent.NPCType<DragonLordLegs>(),
        ModContent.NPCType<DragonLordTail>(),
        ModContent.NPCType<Ectosphere>(),
        ModContent.NPCType<EyeBones>(),
        ModContent.NPCType<GuardianBones>(),
        ModContent.NPCType<GuardianCorruptor>(),
        ModContent.NPCType<ImpactWizard>(),
        ModContent.NPCType<Juggernaut>(),
        ModContent.NPCType<JuggernautSorcerer>(),
        ModContent.NPCType<MatterMan>(),
        ModContent.NPCType<MechanicalDiggerBody>(),
        ModContent.NPCType<MechanicalDiggerHead>(),
        ModContent.NPCType<MechanicalDiggerTail>(),
        ModContent.NPCType<Mechasting>(),
        ModContent.NPCType<ProtectorWheel>(),
        ModContent.NPCType<QuickCaribe>(),
        ModContent.NPCType<RedAegisBonesHelmet>(),
        ModContent.NPCType<RedAegisBonesHorned>(),
        ModContent.NPCType<RedAegisBonesSparta>(),
        ModContent.NPCType<RedAegisBonesSpike>(),
        ModContent.NPCType<UnstableAnomaly>(),
        ModContent.NPCType<UnvolanditeMite>(),
        ModContent.NPCType<UnvolanditeMiteDigger>(),
        ModContent.NPCType<Valkyrie>(),
        ModContent.NPCType<VampireHarpy>(),
        ModContent.NPCType<VorazylcumMite>(),
        ModContent.NPCType<VorazylcumMiteDigger>()
    };

    public static readonly int[] Hornets =
    {
        NPCID.Hornet, NPCID.MossHornet, NPCID.HornetFatty, NPCID.HornetHoney, NPCID.HornetLeafy, NPCID.HornetSpikey,
        NPCID.HornetStingy
    };

    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress)
        {
            pool.Clear();
            pool.Add(ModContent.NPCType<Valkyrie>(), 0.6f);
            pool.Add(ModContent.NPCType<CloudBat>(), 0.9f);
        }
        if (spawnInfo.player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && !spawnInfo.player.InPillarZone())
        {
            pool.Clear();
            pool.Add(ModContent.NPCType<Bactus>(), 1f);
            pool.Add(ModContent.NPCType<PyrasiteHead>(), 0.1f);
            if (Main.hardMode)
            {
                pool.Add(ModContent.NPCType<Cougher>(), 0.8f);
                pool.Add(ModContent.NPCType<Ickslime>(), 0.7f);
                if (spawnInfo.player.ZoneRockLayerHeight)
                {
                    pool.Add(ModContent.NPCType<Viris>(), 1f);
                    pool.Add(ModContent.NPCType<GrossyFloat>(), 0.6f);
                }
                if (spawnInfo.player.ZoneDesert)
                {
                    pool.Add(NPCID.DarkMummy, 0.3f);
                    pool.Add(ModContent.NPCType<EvilVulture>(), 0.4f);
                }
            }
        }
        if (spawnInfo.player.GetModPlayer<ExxoBiomePlayer>().ZoneCaesium)
        {
            if (Main.hardMode)
            {
                pool.Clear();
                pool.Add(ModContent.NPCType<CaesiumBrute>(), 1f);
                pool.Add(ModContent.NPCType<CaesiumSeekerHead>(), 0.05f);
                pool.Add(ModContent.NPCType<CaesiumStalker>(), 0.9f);
            }
        }
        if (spawnInfo.player.GetModPlayer<ExxoBiomePlayer>().ZoneDarkMatter && !spawnInfo.player.InPillarZone())
        {
            pool.Clear();
            pool.Add(ModContent.NPCType<DarkMotherSlime>(), 0.5f);
            pool.Add(ModContent.NPCType<DarkMatterSlime>(), 0.9f);
            pool.Add(ModContent.NPCType<VampireHarpy>(), 0.9f);
            pool.Add(ModContent.NPCType<MatterMan>(), 0.9f);
            pool.Add(ModContent.NPCType<UnstableAnomaly>(), 0.9f);
        }
        if (spawnInfo.player.GetModPlayer<ExxoBiomePlayer>().ZoneHellcastle)
        {
            pool.Clear();
            pool.Add(NPCID.Demon, 0.2f);
            pool.Add(NPCID.RedDevil, 0.2f);
            pool.Add(ModContent.NPCType<EctoHand>(), 0.3f);
            pool.Add(ModContent.NPCType<HellboundLizard>(), 1f);
            pool.Add(ModContent.NPCType<Gargoyle>(), 1f);
            if (ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode && Main.hardMode)
            {
                pool.Add(ModContent.NPCType<ArmoredHellTortoise>(), 1f);
            }
        }
    }


    /// <summary>
    ///     A method to choose a random Town NPC death messages.
    /// </summary>
    /// <param name="Type">The Town NPC's type.</param>
    /// <returns>The string containing the death message.</returns>
    public static string TownDeathMSG(int Type)
    {
        string result = "";
        if (Type == NPCID.Merchant)
        {
            int r = Main.rand.Next(5);
            if (r == 0)
            {
                result += " tried to sell torches to a zombie.";
            }

            if (r == 1)
            {
                result += " made a grave error...";
            }

            if (r == 2)
            {
                result += " was slain...";
            }

            if (r == 3)
            {
                result += " was hanged with a bug net.";
            }

            if (r == 4)
            {
                result += " tried gold dust for the first time.";
            }
        }
        else if (Type == NPCID.Nurse)
        {
            int r = Main.rand.Next(5);
            if (r == 0)
            {
                result += " couldn't heal herself in a timely manner.";
            }

            if (r == 1)
            {
                result += " was expected to make a full recovery.";
            }

            if (r == 2)
            {
                result += "'s face wasn't sewn on well enough.";
            }

            if (r == 3)
            {
                result += " encountered a complication.";
            }

            if (r == 4)
            {
                result += "'s surgical strike was in error.";
            }
        }
        else if (Type == NPCID.OldMan)
        {
            int r = Main.rand.Next(2);
            if (r == 0)
            {
                result += " died of old age.";
            }

            if (r == 1)
            {
                result += " was slain...";
            }
        }
        else if (Type == NPCID.ArmsDealer)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += "'s gun jammed.";
            }

            if (r == 1)
            {
                result += " fired a meteor bullet...";
            }

            if (r == 2)
            {
                result += " ran out of ammo.";
            }

            if (r == 3)
            {
                result += " was slain...";
            }

            if (r == 4)
            {
                result += " shot himself.";
            }

            if (r == 5)
            {
                if (!Main.dayTime)
                {
                    result += " was caught.";
                }
                else
                {
                    result += " shot himself.";
                }
            }
        }
        else if (Type == NPCID.Dryad)
        {
            int r = Main.rand.Next(7);
            if (r == 0)
            {
                result += "'s time was up...";
            }

            if (r == 1)
            {
                result += " choked on an herb.";
            }

            if (r == 2)
            {
                result += " turned into grass.";
            }

            if (r == 3)
            {
                result += " died.";
            }

            if (r == 4)
            {
                result += " was reduced to a fine paste...";
            }

            if (r == 5)
            {
                result += " was mauled by an unknown creature.";
            }

            if (r == 6)
            {
                result += " tripped on a corrupt vine.";
            }
        }
        else if (Type == NPCID.Guide)
        {
            int r = Main.rand.Next(7);
            if (r == 0)
            {
                result += " let too many zombies in.";
            }

            if (r == 1)
            {
                result += " was reading a history book...";
            }

            if (r == 2)
            {
                result += " was slain...";
            }

            if (r == 3)
            {
                result += " got impaled...";
            }

            if (r == 4)
            {
                result += " was the victim of dark magic.";
            }

            if (r == 5)
            {
                result += " was voodoo'd.";
            }

            if (r == 6)
            {
                result += " opened a door.";
            }
        }
        else if (Type == NPCID.Demolitionist)
        {
            int r = Main.rand.Next(7);
            if (r == 0)
            {
                result += " blew up.";
            }

            if (r == 1)
            {
                result += " threw a bomb at a mob, but it bounced back.";
            }

            if (r == 2)
            {
                result += "'s explosives were a little too sensitive...";
            }

            if (r == 3)
            {
                result += " made a dirty bomb.";
            }

            if (r == 4)
            {
                result += " was bombed.";
            }

            if (r == 5)
            {
                result += " went out with a bang.";
            }

            if (r == 6)
            {
                result += " became a true martyr.";
            }
        }
        else if (Type == NPCID.Clothier)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " was unknowningly cursed...";
            }

            if (r == 1)
            {
                result += " died from unknown causes.";
            }

            if (r == 2)
            {
                result += " was unravelled...";
            }

            if (r == 3)
            {
                result += " was eviscerated...";
            }

            if (r == 4)
            {
                result += " went back to the dungeon.";
            }

            if (r == 5)
            {
                result += " stitched himself up.";
            }
        }
        else if (Type == NPCID.GoblinTinkerer)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += "'s contraption exploded.";
            }

            if (r == 1)
            {
                result += " tinkered with his life.";
            }

            if (r == 2)
            {
                result += " was crushed...";
            }

            if (r == 3)
            {
                result += " rocketed into the ceiling.";
            }

            if (r == 4)
            {
                result += " died from platinum poisoning.";
            }

            if (r == 5)
            {
                result += " was approached from the north.";
            }
        }
        else if (Type == NPCID.Wizard)
        {
            int r = Main.rand.Next(7);
            if (r == 0)
            {
                result += " forgot how to live.";
            }

            if (r == 1)
            {
                result += " cast too many spells...";
            }

            if (r == 2)
            {
                result += " was crushed...";
            }

            if (r == 3)
            {
                result += " watched his innards become outards...";
            }

            if (r == 4)
            {
                result += " made himself 'disappear.'";
            }

            if (r == 5)
            {
                result += " became a frog.";
            }

            if (r == 6)
            {
                result += "'s low armor class failed him.";
            }
        }
        else if (Type == NPCID.SantaClaus)
        {
            int r = Main.rand.Next(2);
            if (r == 0)
            {
                result += " had too much milk and cookies.";
            }

            if (r == 1)
            {
                result += " wasn't believed in.";
            }
        }
        else if (Type == NPCID.Mechanic)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += "'s spine cracked...";
            }

            if (r == 1)
            {
                result += "'s engine broke down.";
            }

            if (r == 2)
            {
                result += " ran out of wire...";
            }

            if (r == 3)
            {
                result += " died.";
            }

            if (r == 4)
            {
                result += " dropped her contacts...";
            }

            if (r == 5)
            {
                result += " was removed from " + Main.worldName + ".";
            }
        }
        else if (Type == NPCID.Truffle)
        {
            int r = Main.rand.Next(4);
            if (r == 0)
            {
                result += " bit himself.";
            }

            if (r == 1)
            {
                result += " was eaten.";
            }

            if (r == 2)
            {
                result += " was slain...";
            }

            if (r == 3)
            {
                result += " is no longer a fun guy.";
            }
        }
        else if (Type == NPCID.Steampunker)
        {
            int r = Main.rand.Next(5);
            if (r == 0)
            {
                result += "'s time machine created a paradox...";
            }

            if (r == 1)
            {
                result += " slipped a cog.";
            }

            if (r == 2)
            {
                result += " died from unknown causes.";
            }

            if (r == 3)
            {
                result += " fell off an airship.";
            }

            if (r == 4)
            {
                result += " used her teleporter too fast.";
            }
        }
        else if (Type == NPCID.DyeTrader)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " fell into his dye.";
            }

            if (r == 1)
            {
                result += " was killed.";
            }

            if (r == 2)
            {
                result += " died from unknown causes.";
            }

            if (r == 3)
            {
                result += " was decapitated.";
            }

            if (r == 4)
            {
                result += " has dyed.";
            }

            if (r == 5)
            {
                result += " was dyed a deep chartreuse.";
            }
        }
        else if (Type == NPCID.PartyGirl)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " partied too hard.";
            }

            if (r == 1)
            {
                result += " inhaled too much confetti...";
            }

            if (r == 2)
            {
                result += " was crushed.";
            }

            if (r == 3)
            {
                result += " left the party.";
            }

            if (r == 4)
            {
                result += " was eaten.";
            }

            if (r == 5)
            {
                result += " was dissolved into the punch.";
            }
        }
        else if (Type == NPCID.Cyborg)
        {
            int r = Main.rand.Next(7);
            if (r == 0)
            {
                result += " was assimilated.";
            }

            if (r == 1)
            {
                result += "'s mechanisms were damaged beyond repair.";
            }

            if (r == 2)
            {
                result += " was crushed.";
            }

            if (r == 3)
            {
                result += " is '404 not found.'";
            }

            if (r == 4)
            {
                result += "'s implants were ripped out.";
            }

            if (r == 5)
            {
                result += " short-circuited.";
            }

            if (r == 6)
            {
                result += " malfunctioned.";
            }
        }
        else if (Type == NPCID.Painter)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " inhaled paint fumes.";
            }

            if (r == 1)
            {
                result += " tried to paint himself.";
            }

            if (r == 2)
            {
                result += "'s body was mangled.";
            }

            if (r == 3)
            {
                result += "'s paint was ripped off the canvas...";
            }

            if (r == 4)
            {
                result += "'s paint cracked.";
            }

            if (r == 5)
            {
                result += " inhaled asbestos.";
            }
        }
        else if (Type == NPCID.WitchDoctor)
        {
            int r = Main.rand.Next(5);
            if (r == 0)
            {
                result += "'s practice lead to his demise.";
            }

            if (r == 1)
            {
                result += " tried to embody life.";
            }

            if (r == 2)
            {
                result += "'s body was mangled.";
            }

            if (r == 3)
            {
                result += " was chopped up.";
            }

            if (r == 4)
            {
                result += " was eaten by an exotic insect.";
            }
        }
        else if (Type == NPCID.Pirate)
        {
            int r = Main.rand.Next(5);
            if (r == 0)
            {
                result += " choked on his gold tooth.";
            }

            if (r == 1)
            {
                result += " was hit by a cannonball.";
            }

            if (r == 2)
            {
                result += " was eviscerated.";
            }

            if (r == 3)
            {
                result += "'s other eye was removed.";
            }

            if (r == 4)
            {
                result += " lost his peg leg.";
            }
        }
        else if (Type == NPCID.Stylist)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " attempted to give a spider a trim.";
            }

            if (r == 1)
            {
                result += " was tied up.";
            }

            if (r == 2)
            {
                result += " was eviscerated.";
            }

            if (r == 3)
            {
                result += " got a little too snippy.";
            }

            if (r == 4)
            {
                result += " tried to give Cousin It a trim.";
            }

            if (r == 5)
            {
                result += " couldn't take the sight of spiders anymore.";
            }
        }
        else if (Type == NPCID.TravellingMerchant)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " has departed...";
            }

            if (r == 1)
            {
                result += " had INTERPOL problems.";
            }

            if (r == 2)
            {
                result += " was vaporized.";
            }

            if (r == 3)
            {
                result += " did not need a monster there.";
            }

            if (r == 4)
            {
                result += " tried to sell an exotic pitcher plant...";
            }

            if (r == 5)
            {
                result += " failed to make any sales.";
            }
        }
        else if (Type == NPCID.Angler)
        {
            int r = Main.rand.Next(9);
            if (r == 0)
            {
                result += " has left!";
            }

            if (r == 1)
            {
                result += " caught a fish!";
            }

            if (r == 2)
            {
                result += " fell into a ditch!";
            }

            if (r == 3)
            {
                result += " failed a test!";
            }

            if (r == 4)
            {
                result += " lost the game!";
            }

            if (r == 5)
            {
                result += " took off!";
            }

            if (r == 6)
            {
                result += " died.";
            }

            if (r == 7)
            {
                result += " found someone better to harass.";
            }

            if (r == 8)
            {
                if (NPC.AnyNPCs(NPCID.Pirate))
                {
                    result += " was thrown overboard by " + Main.npc[FindATypeOfNPC(NPCID.Pirate)].GivenName + ".";
                }
                else
                {
                    result += " died.";
                }
            }
        }
        else if (Type == NPCID.TaxCollector)
        {
            int r = Main.rand.Next(5);
            if (r == 0)
            {
                result += " went to the wrong house.";
            }

            if (r == 1)
            {
                result += " collected counterfeit money.";
            }

            if (r == 2)
            {
                result += " had a hole in his pocket.";
            }

            if (r == 3)
            {
                result += " saw the ghost of " + Main.worldName + "'s past.";
            }

            if (r == 4)
            {
                result += " overtaxed himself.";
            }
        }
        else if (Type == NPCID.DD2Bartender)
        {
            int r = Main.rand.Next(5);
            if (r == 0)
            {
                result += " found himself on the wrong side of the bar.";
            }

            if (r == 1)
            {
                result += "'s sentries stopped working.";
            }

            if (r == 2)
            {
                result += " spontaneously combusted.";
            }

            if (r == 3)
            {
                result += " went through the wrong portal.";
            }

            if (r == 4)
            {
                result += " drank his feelings away.";
            }
        }
        else if (Type == NPCID.Princess)
        {
            int r = Main.rand.Next(4);
            if (r == 0)
            {
                result += " was taken away by a dragon.";
            }

            if (r == 1)
            {
                result += " was chained up in a tower.";
            }

            if (r == 2)
            {
                result += " swallowed her scepter.";
            }

            if (r == 3)
            {
                result += " tripped on her dress.";
            }
        }
        else if (Type == NPCID.Golfer)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " didn't yell \"fore!\"";
            }

            if (r == 1)
            {
                result += " was hit in the head by a golfball.";
            }

            if (r == 2)
            {
                result += " got a triple bogey.";
            }

            if (r == 3)
            {
                result += " went over par.";
            }

            if (r == 4)
            {
                result += " fell in a hole.";
            }

            if (r == 5)
            {
                result += "'s club hit back.";
            }
        }
        else if (Type == NPCID.BestiaryGirl)
        {
            int r = Main.rand.Next(3);
            if (r == 0)
            {
                result += " got rabies.";
            }

            if (r == 1)
            {
                result += " was put down.";
            }

            if (r == 2)
            {
                result += " got too comfortable with an exotic beast.";
            }
        }
        else if (Type == ModContent.NPCType<Iceman>())
        {
            int r = Main.rand.Next(7);
            if (r == 0) result += " froze.";
            if (r == 1) result += " melted.";
            if (r == 2) result += " has died.";
            if (r == 3) result += "'s ice was cracked.";
            if (r == 4)
            {
                if (NPC.AnyNPCs(NPCID.ArmsDealer))
                {
                    result += " was used to cool " + Main.npc[FindATypeOfNPC(NPCID.ArmsDealer)].GivenName + "'s drink.";
                }
                else result += " fell into a crevasse.";
            }
            if (r == 5) result += " fell into a crevasse";
            if (r == 6) result += " slipped.";
        }
        else if (Type == ModContent.NPCType<Librarian>())
        {
            int r = Main.rand.Next(4);
            if (r == 0) result += " was nuked by a full squad.";
            if (r == 1) result += " fell victim to toxic world chat.";
            if (r == 2) result += " couldn't afford grade eighteen.";
            if (r == 3) result += " was slain by a boss cone attack.";
        }
        else
        {
            result += " was slain...";
        }

        return result;
    }

    /// <summary>
    ///     Finds a type of NPC.
    /// </summary>
    /// <param name="type">The type of NPC to find.</param>
    /// <returns>The index of the found NPC in the Main.npc[] array.</returns>
    public static int FindATypeOfNPC(int type)
    {
        for (int i = 0; i < 200; i++)
        {
            if (type == Main.npc[i].type && Main.npc[i].active)
            {
                return i;
            }
        }

        return 0;
    }

    public override bool CheckDead(NPC npc)
    {
        if (npc.townNPC && npc.life <= 0)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(npc.FullName + TownDeathMSG(npc.type), new Color(178, 0, 90));
                npc.life = 0;
                npc.HitEffect();
                npc.active = false;
                npc.NPCLoot();
                SoundEngine.PlaySound(SoundID.NPCKilled, (int)npc.position.X, (int)npc.position.Y);
            }
            else
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(npc.FullName + TownDeathMSG(npc.type)),
                    new Color(178, 0, 90));
                NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, npc.whoAmI, -1);
                int t = 0;
                int s = 1;
                switch (npc.type)
                {
                    case NPCID.Guide:
                        if (npc.GivenName == "Andrew")
                        {
                            t = ItemID.GreenCap;
                        }

                        break;
                    case NPCID.DyeTrader:
                        if (Main.rand.Next(8) == 0)
                        {
                            t = ItemID.DyeTradersScimitar;
                        }

                        break;
                    case NPCID.Painter:
                        if (Main.rand.Next(10) == 0)
                        {
                            t = ItemID.PainterPaintballGun;
                        }

                        break;
                    case NPCID.DD2Bartender:
                        if (Main.rand.Next(8) == 0)
                        {
                            t = ItemID.AleThrowingGlove;
                        }

                        break;
                    case NPCID.Stylist:
                        if (Main.rand.Next(8) == 0)
                        {
                            t = ItemID.StylistKilLaKillScissorsIWish;
                        }

                        break;
                    case NPCID.Clothier:
                        t = ItemID.RedHat;
                        break;
                    case NPCID.PartyGirl:
                        if (Main.rand.Next(4) == 0)
                        {
                            t = ItemID.PartyGirlGrenade;
                            s = Main.rand.Next(30, 61);
                        }

                        break;
                    case NPCID.TaxCollector:
                        if (Main.rand.Next(8) == 0)
                        {
                            t = 3351;
                        }

                        break;
                    case NPCID.TravellingMerchant:
                        t = ItemID.PeddlersHat;
                        break;
                    case NPCID.Princess:
                        t = ItemID.PrincessWeapon;
                        break;
                }

                if (t > 0)
                {
                    int a = Item.NewItem(npc.GetItemSource_Loot(), npc.position, 16, 16, t, s);
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a);
                }

                //Main.npc[npc.whoAmI].NPCLoot();
                SoundEngine.PlaySound(SoundID.NPCKilled, (int)npc.position.X, (int)npc.position.Y);
            }

            return false;
        }

        return base.CheckDead(npc);
    }

    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
        if (player.GetModPlayer<ExxoBuffPlayer>().AdvancedBattle)
        {
            spawnRate = (int)(spawnRate * AdvBattle.RateMultiplier);
            maxSpawns = (int)(maxSpawns * AdvBattle.SpawnMultiplier);
        }

        if (player.HasBuff<AdvCalming>())
        {
            spawnRate = (int)(spawnRate * AdvCalming.RateMultiplier);
            maxSpawns = (int)(maxSpawns * AdvCalming.SpawnMultiplier);
        }
    }

    public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
    {
        if (target.HasBuff<BeeSweet>() && Hornets.Contains(npc.type))
        {
            return false;
        }

        return base.CanHitPlayer(npc, target, ref cooldownSlot);
    }

    public override void OnKill(NPC npc)
    {
        if (npc.type == NPCID.SkeletronHead && !NPC.downedBoss3)
        {
            AvalonTestingWorld.GenerateSulphur();
        }

        if (npc.type == NPCID.DungeonSpirit && Main.rand.Next(15) == 0 &&
            Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon)
        {
            int proj = Projectile.NewProjectile(npc.GetSpawnSource_ForProjectile(), npc.position, npc.velocity,
                ModContent.ProjectileType<Projectiles.SpiritPoppy>(), 0, 0, Main.myPlayer);
            Main.projectile[proj].velocity.Y = -3.5f;
            Main.projectile[proj].velocity.X = Main.rand.Next(-45, 46) * 0.1f;
        }
    }

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        var hardModeCondition = new HardmodeOnly();
        var preHardModeCondition = new Invert(hardModeCondition);
        var superHardModeCondition = new Superhardmode();
        var hardmodePreSuperHardmodeCondition =
            new Combine(true, null, hardModeCondition, new Invert(new Superhardmode()));
        var notExpertCondition = new Conditions.NotExpert();
        var notFromStatueCondition = new Conditions.NotFromStatue();
        var zoneRockLayerCondition = new ZoneRockLayer();
        var contagionCondition = new ZoneContagion();
        var undergroundContagionCondition = new Combine(true, "Drops in the underground contagion", contagionCondition,
            zoneRockLayerCondition);
        var undergroundHardmodeContagionCondition = new Combine(true, undergroundContagionCondition.GetConditionDescription(), undergroundContagionCondition,
            hardModeCondition);
        var dungeonCondition = new ZoneDungeon();
        var hardmodeDungeonCondition = new Combine(true, dungeonCondition.GetConditionDescription(), hardModeCondition,
            dungeonCondition);

        #region individual

        switch (npc.type)
        {
            case NPCID.Golem:
            {
                // Get main drops and duplicate
                var oneFromRulesRule = new OneFromRulesRule(1);
                foreach (IItemDropRule rule in npcLoot.Get(false))
                {
                    if (rule is LeadingConditionRule rule1)
                    {
                        foreach (IItemDropRuleChainAttempt chain in rule1.ChainedRules)
                        {
                            if (chain is Chains.TryIfSucceeded chain1 && chain1.RuleToChain is OneFromRulesRule ruleMain)
                            {
                                oneFromRulesRule.options = ruleMain.options;
                                break;
                            }
                        }

                        if ((oneFromRulesRule.options?.Length ?? 0) != 0)
                        {
                            break;
                        }
                    }
                }

                var condition = new Combine(true, null, new FirstTimeKillingGolem(), notExpertCondition);
                npcLoot.Add(ItemDropRule.ByCondition(condition, ItemID.Picksaw));
                npcLoot.Add(oneFromRulesRule.HideFromBestiary());
                break;
            }
            case NPCID.WallofFlesh:
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<NullEmblem>()));
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<FleshyTendril>(),
                    1,
                    13, 19));
                break;
            case NPCID.AngryBones or NPCID.AngryBonesBig or NPCID.AngryBonesBigHelmet
                or NPCID.AngryBonesBigMuscle:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlackWhetstone>(), 50));
                break;
            case NPCID.KingSlime:
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<BandofSlime>(),
                    3));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BirthofaMonster>(), 9));
                break;
            case NPCID.Duck or NPCID.Duck2 or NPCID.DuckWhite or NPCID.DuckWhite2:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Quack>(), VeryRareChance));
                break;
            case NPCID.EaterofSouls:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RottenEye>(), 7));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EvilOuroboros>(), RareChance));
                break;
            case NPCID.DialatedEye:
                npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 40));
                break;
            case NPCID.UndeadMiner:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersPickaxe>(), 30));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MinersSword>(), 30));
                break;
            case NPCID.FloatyGross:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Patella>(), 5, 1, 2));
                break;
            case NPCID.RaggedCaster:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SixHundredWattLightbulb>(), UncommonChance));
                break;
            case NPCID.ChaosElemental:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosDust>(), 7, 2, 4));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosCharm>(), 30));
                break;
            case NPCID.BoneSerpentHead:
                npcLoot.Add(ItemDropRule.Common(ItemID.Sunfury, 20));
                break;
            case NPCID.WyvernHead:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MysticalTotem>(), 2));
                break;
            case NPCID.Clown:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.Throw.ClownBomb>(), 3, 2, 6));
                break;
            case NPCID.Harpy:
                npcLoot.Add(ItemDropRule.ByCondition(hardModeCondition, ItemID.ShinyRedBalloon, 50));
                break;
            case NPCID.Vulture:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Beak>(), 3));
                break;
            case NPCID.QueenBee:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FightoftheBumblebee>(), 8));
                break;
            case NPCID.Plantera:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PlanterasRage>(), 15));
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<LifeDew>(), 1, 10,
                    18));
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ItemID.ChlorophyteOre, 1, 60, 120));
                break;
            case NPCID.GoblinSorcerer:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosTome>(), 40));
                break;
            case NPCID.RedDevil:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ForsakenRelic>(), 20));
                break;
            case NPCID.PossessedArmor:
                npcLoot.Add(ItemDropRule.OneFromOptions(80, ModContent.ItemType<PossessedArmorHelmet>(),
                    ModContent.ItemType<PossessedArmorChainmail>(), ModContent.ItemType<PossessedArmorGreaves>()));
                break;
            case NPCID.IchorSticker:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoldenShield>(), 70));
                break;
            case NPCID.DungeonSpirit:
                npcLoot.Add(ItemDropRule.OneFromOptions(33, ModContent.ItemType<PhantomMask>(),
                    ModContent.ItemType<PhantomShirt>(), ModContent.ItemType<PhantomPants>()));
                npcLoot.Add(ItemDropRule.Common(ItemID.Ectoplasm, 5));
                break;
            case NPCID.Mothron:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenVigilanteTome>(), 5));
                break;
            case NPCID.AngryNimbus:
                NPCLoader.blockLoot.Add(ItemID.NimbusRod);
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LivingLightningBlock>(), 1, 8, 16));
                npcLoot.Add(ItemDropRule.Common(ItemID.Cloud, 1, 10, 16));
                npcLoot.Add(ItemDropRule.Common(ItemID.RainCloud, 1, 2, 6));
                break;
            case NPCID.EyeofCthulhu:
            {
                npcLoot.Add(ItemDropRule.ByCondition(preHardModeCondition,
                    ModContent.ItemType<Items.Consumables.BloodyAmulet>(),
                    10, 1, 1, 3));

                npcLoot.Add(ItemDropRule.ByCondition(hardmodePreSuperHardmodeCondition,
                    ModContent.ItemType<Items.Consumables.BloodyAmulet>(),
                    100, 1, 1, 15));

                npcLoot.Add(ItemDropRule.ByCondition(superHardModeCondition,
                    ModContent.ItemType<Items.Consumables.BloodyAmulet>(),
                    100, 1, 1, 7));

                break;
            }
            case NPCID.Shark:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DivingSuit>(), 60));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DivingPants>(), 40));
                break;
        }

        #endregion

        #region group

        if (npc.type is NPCID.Crimera or NPCID.FaceMonster or NPCID.BloodCrawler or NPCID.BloodCrawlerWall)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Patella>(), 7));
        }

        if (npc.type is NPCID.PincushionZombie or NPCID.SlimedZombie or NPCID.SwampZombie or NPCID.TwiggyZombie
            or NPCID.Zombie
            or NPCID.ZombieEskimo or NPCID.FemaleZombie or NPCID.ZombieRaincoat)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RottenFlesh>(), 7));
        }

        if (npc.type is NPCID.Clinger or NPCID.Spazmatism)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GreekExtinguisher>(), UncommonChance));
        }

        if (npc.type is NPCID.HellArmoredBones or NPCID.HellArmoredBonesSpikeShield or NPCID.HellArmoredBonesMace
            or NPCID.HellArmoredBonesSword)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(55, ModContent.ItemType<HellArmoredHelmet>(),
                ModContent.ItemType<HellBlazingChestplate>(), ModContent.ItemType<HellArmoredGreaves>()));
        }

        if (npc.type is NPCID.ManEater or NPCID.Snatcher or NPCID.AngryTrapper)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notFromStatueCondition, ModContent.ItemType<DewOrb>(), 25, 1, 1, 4));
        }

        if (npc.type is NPCID.GiantTortoise or NPCID.IceTortoise or NPCID.Vulture or NPCID.FlyingFish
            or NPCID.Unicorn)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementDust>(), 7));
        }

        if (npc.type is NPCID.Corruptor or NPCID.SeekerHead)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RottenEye>(), 3, 1, 2));
        }

        if (npc.type is NPCID.Harpy or NPCID.CaveBat or NPCID.GiantBat or NPCID.JungleBat)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RubybeadHerb>(), 7));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MysticalClaw>(), 9));
        }

        if (npc.type is NPCID.Hornet or NPCID.BlackRecluse or NPCID.MossHornet or NPCID.HornetFatty
            or NPCID.HornetHoney
            or NPCID.HornetLeafy or NPCID.HornetSpikey or NPCID.HornetStingy or NPCID.JungleCreeper
            or NPCID.JungleCreeperWall or NPCID.BlackRecluseWall)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StrongVenom>(), 7));
        }

        if (npc.type is NPCID.Retinazer or NPCID.Spazmatism or NPCID.SkeletronPrime or NPCID.TheDestroyer)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScrollofTome>(), 3));
        }

        if (npc.type is NPCID.CorruptSlime or NPCID.Gastropod or NPCID.IlluminantSlime or NPCID.ToxicSludge
            or NPCID.Crimslime
            or NPCID.RainbowSlime or NPCID.FloatyGross)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notFromStatueCondition, ModContent.ItemType<DewofHerbs>(),
                25, 1, 1, 4));
        }

        if (npc.type is NPCID.Corruptor or NPCID.IchorSticker or NPCID.ChaosElemental or NPCID.IceElemental
            or NPCID.AngryNimbus or NPCID.GiantTortoise or NPCID.RedDevil)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosCrystal>(), 100));
        }

        if (npc.type is NPCID.CrimsonAxe or NPCID.CursedHammer or NPCID.EnchantedSword)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Clash>(), 100));
        }

        if (npc.type is NPCID.SkeletonArcher or NPCID.LavaSlime or NPCID.MeteorHead or NPCID.FireImp
            or NPCID.Hellbat
            or NPCID.Demon or NPCID.HellArmoredBones or NPCID.HellArmoredBonesSpikeShield
            or NPCID.HellArmoredBonesMace or NPCID.HellArmoredBonesSword)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Vortex>(), 200));
        }

        if (!NPCID.Sets.CountsAsCritter[npc.type] && !npc.townNPC)
        {
            ItemDropRule.ByCondition(undergroundHardmodeContagionCondition, ItemID.SoulofNight, 5);
            ItemDropRule.ByCondition(hardmodeDungeonCondition, ItemID.CobaltShield, 120);
            npcLoot.Add(ItemDropRule.OneFromOptions(600, ItemID.EndurancePotion, ItemID.GravitationPotion,
                ItemID.InfernoPotion,
                ModContent.ItemType<StarbrightPotion>(), ModContent.ItemType<StrengthPotion>(),
                ModContent.ItemType<CrimsonPotion>(), ItemID.IronskinPotion, ItemID.SwiftnessPotion,
                ModContent.ItemType<ShockwavePotion>(), ItemID.MiningPotion, ItemID.ObsidianSkinPotion,
                ItemID.NightOwlPotion, ItemID.RagePotion, ItemID.RegenerationPotion, ItemID.SpelunkerPotion,
                ItemID.SonarPotion, ItemID.WrathPotion, ItemID.SummoningPotion, ItemID.HunterPotion,
                ItemID.FlipperPotion, ModContent.ItemType<GPSPotion>(), ItemID.GillsPotion).HideFromBestiary());
        }

        if (Data.Sets.NPC.Slimes[npc.type])
        {
            ItemDropRule.ByCondition(hardModeCondition, ItemID.RoyalGel, 500);
        }

        if (Data.Sets.NPC.Undead[npc.type])
        {
            ItemDropRule.ByCondition(hardModeCondition, ModContent.ItemType<UndeadTalisman>(), 550);
        }

        if (npc.lifeMax >= 100)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new SoulofHumidityDrop(), ModContent.ItemType<SoulofHumidity>(), 9));
        }

        if (Data.Sets.NPC.Toxic[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToxinShard>(), 8));
        }

        if (Data.Sets.NPC.Undead[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UndeadShard>(), 11));
        }

        if (Data.Sets.NPC.Fiery[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireShard>(), 8));
        }

        if (Data.Sets.NPC.Watery[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterShard>(), 8));
        }

        if (Data.Sets.NPC.Earthen[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthShard>(), 12));
        }

        if (Data.Sets.NPC.Flyer[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BreezeShard>(), 13));
        }

        if (Data.Sets.NPC.Frozen[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostShard>(), 10));
        }

        if (Data.Sets.NPC.Wicked[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CorruptShard>(), 9));
        }

        if (Data.Sets.NPC.Arcane[npc.type])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcaneShard>(), 7));
        }

        if (npc.type is NPCID.ChaosElemental or NPCID.IceElemental or NPCID.IchorSticker or NPCID.Corruptor ||
            npc.type == ModContent.NPCType<Viris>())
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElementDiamond>(), 6));
        }

        if (npc.boss)
        {
            npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<StaminaCrystal>(), 4));
        }

        #endregion group

        if (AvalonTesting.Mod.ImkSushisMod != null)
        {
            if (!NPCID.Sets.CountsAsCritter[npc.type] && !npc.townNPC)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new PostPhantasmHellcastleDrop(),
                    ModContent.ItemType<HellcastleToken>(), 15));
                npcLoot.Add(ItemDropRule.ByCondition(new SuperhardmodePreArmaDrop(),
                    ModContent.ItemType<SuperhardmodeToken>(), 15));
                npcLoot.Add(ItemDropRule.ByCondition(new PostArmageddonDrop(), ModContent.ItemType<DarkMatterToken>(),
                    15));
                npcLoot.Add(ItemDropRule.ByCondition(new PostMechastingDrop(), ModContent.ItemType<MechastingToken>(),
                    15));
                npcLoot.Add(ItemDropRule.ByCondition(new ZoneTropics(), ModContent.ItemType<TropicsToken>(), 15));
                npcLoot.Add(ItemDropRule.ByCondition(undergroundHardmodeContagionCondition, ModContent.ItemType<ContagionToken>(), 15));
            }
        }
    }

    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        if (AvalonTesting.Mod.ImkSushisMod != null)
        {
            if (ModContent.GetInstance<DownedBossSystem>().DownedPhantasm)
            {
                NPCLoader.blockLoot.Add(AvalonTesting.Mod.ImkSushisMod.Find<ModItem>("PostMartiansLootToken").Type);
                NPCLoader.blockLoot.Add(AvalonTesting.Mod.ImkSushisMod.Find<ModItem>("PostPlanteraLootToken").Type);
                NPCLoader.blockLoot.Add(AvalonTesting.Mod.ImkSushisMod.Find<ModItem>("HardmodeLootToken").Type);
            }
        }

        NPCLoader.blockLoot.Add(ItemID.RangerEmblem);
        NPCLoader.blockLoot.Add(ItemID.SummonerEmblem);
        NPCLoader.blockLoot.Add(ItemID.WarriorEmblem);
        NPCLoader.blockLoot.Add(ItemID.SorcererEmblem);
        
        var hardModeCondition = new HardmodeOnly();
        var superHardModeCondition = new Superhardmode();
        var zoneRockLayerCondition = new ZoneRockLayer();
        var contagionCondition = new ZoneContagion();
        var undergroundContagionCondition = new Combine(true, "Drops in the underground contagion", contagionCondition,
            zoneRockLayerCondition);
        var undergroundHardmodeContagionCondition = new Combine(true, undergroundContagionCondition.GetConditionDescription(), undergroundContagionCondition,
            hardModeCondition);
        var dungeonCondition = new ZoneDungeon();
        var hardmodeDungeonCondition = new Combine(true, dungeonCondition.GetConditionDescription(), hardModeCondition,
            dungeonCondition);
        var desertPostBeakCondition = new DesertPostBeakDrop();
        var snowCondition = new ZoneSnow();
        var undergroundSnow = new Combine(true, "Drops in the underground snow", snowCondition, zoneRockLayerCondition);
        var undergroundHardmodeSnow = new Combine(true, undergroundSnow.GetConditionDescription(), undergroundSnow, hardModeCondition);
        var bloodMoonAndNotFromStatueCondition = new Conditions.IsBloodMoonAndNotFromStatue();
        var eclipseCondition = new IsEclipse();

        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumHeadgear>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumPlateMail>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumGreaves>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(undergroundHardmodeSnow, ModContent.ItemType<SoulofIce>(), 10));
        globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<Rock>(), 600));
        globalLoot.Add(ItemDropRule.ByCondition(hardModeCondition, ModContent.ItemType<PointingLaser>(), 650).HideFromBestiary());
        globalLoot.Add(ItemDropRule.ByCondition(superHardModeCondition, ModContent.ItemType<AlienDevice>(), 700).HideFromBestiary());
        globalLoot.Add(ItemDropRule.ByCondition(undergroundHardmodeContagionCondition, ModContent.ItemType<RingofDisgust>(), RareChance));
        globalLoot.Add(ItemDropRule.ByCondition(hardmodeDungeonCondition, ModContent.ItemType<Trespassing>(), RareChance));
        globalLoot.Add(ItemDropRule.ByCondition(bloodMoonAndNotFromStatueCondition, ModContent.ItemType<BloodyWhetstone>(), 50));
        globalLoot.Add(ItemDropRule.ByCondition(hardmodeDungeonCondition, ModContent.ItemType<ACometHasStruckGround>(), RareChance));
        globalLoot.Add(ItemDropRule.ByCondition(eclipseCondition, ModContent.ItemType<EclipseofDoom>(), RareChance));
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (npc.HasBuff(ModContent.BuffType<AstralCurse>()))
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit);
        }
        if (npc.HasBuff(ModContent.BuffType<BacteriaInfection>()))
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.ScourgeOfTheCorruptor);
        }
    }
}
