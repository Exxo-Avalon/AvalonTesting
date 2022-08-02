using System.Collections.Generic;
using System.Linq;
using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.DropConditions;
using Avalon.Items.Accessories;
using Avalon.Items.Armor;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Other;
using Avalon.Items.Placeable.Painting;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Potions;
using Avalon.Items.Tokens;
using Avalon.Items.Tools;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic;
using Avalon.Items.Weapons.Melee;
using Avalon.Items.Weapons.Throw;
using Avalon.NPCs;
using Avalon.Players;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon;

public class AvalonGlobalNPC : GlobalNPC
{
    public static readonly int[] Hornets =
    {
        NPCID.Hornet, NPCID.MossHornet, NPCID.HornetFatty, NPCID.HornetHoney, NPCID.HornetLeafy, NPCID.HornetSpikey,
        NPCID.HornetStingy,
    };
    public static int BleedTime = 60 * 7;

    public static List<int> SHMMobs = new List<int>
    {
        NPCID.Creeper,
        NPCID.Pumpking,
        NPCID.SantaNK1,
        ModContent.NPCType<AegisHallowor>(),
        ModContent.NPCType<NPCs.Bosses.ArmageddonSlime>(),
        ModContent.NPCType<ArmoredHellTortoise>(),
        ModContent.NPCType<ArmoredWraith>(),
        ModContent.NPCType<BactusMinion>(), // remove later
        ModContent.NPCType<BombBones>(),
        ModContent.NPCType<BombSkeleton>(),
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
        ModContent.NPCType<NPCs.Bosses.Mechasting>(),
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
        ModContent.NPCType<VorazylcumMiteDigger>(),
    };

    private const int RareChance = 700;
    private const int UncommonChance = 50;
    private const int VeryRareChance = 1000;

    public static int BoogerBoss { get; set; }
    public static float EndoSpawnRate { get; set; } = 0.25f;
    public static bool SavedIceman { get; set; }

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
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {

    }
    /// <summary>
    ///     A method to choose a random Town NPC death messages.
    /// </summary>
    /// <param name="type">The Town NPC's type.</param>
    /// <returns>The string containing the death message.</returns>
    public static string TownDeathMsg(int type)
    {
        string result = string.Empty;
        if (type == NPCID.Merchant)
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
        else if (type == NPCID.Nurse)
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
        else if (type == NPCID.OldMan)
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
        else if (type == NPCID.ArmsDealer)
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
        else if (type == NPCID.Dryad)
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
        else if (type == NPCID.Guide)
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
        else if (type == NPCID.Demolitionist)
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
        else if (type == NPCID.Clothier)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " was unknowingly cursed...";
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
        else if (type == NPCID.GoblinTinkerer)
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
        else if (type == NPCID.Wizard)
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
        else if (type == NPCID.SantaClaus)
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
        else if (type == NPCID.Mechanic)
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
        else if (type == NPCID.Truffle)
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
        else if (type == NPCID.Steampunker)
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
        else if (type == NPCID.DyeTrader)
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
        else if (type == NPCID.PartyGirl)
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
        else if (type == NPCID.Cyborg)
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
        else if (type == NPCID.Painter)
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
        else if (type == NPCID.WitchDoctor)
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
        else if (type == NPCID.Pirate)
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
        else if (type == NPCID.Stylist)
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
        else if (type == NPCID.TravellingMerchant)
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
        else if (type == NPCID.Angler)
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
        else if (type == NPCID.TaxCollector)
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
        else if (type == NPCID.DD2Bartender)
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
        else if (type == NPCID.Princess)
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
        else if (type == NPCID.Golfer)
        {
            int r = Main.rand.Next(6);
            if (r == 0)
            {
                result += " didn't yell \"fore!\"";
            }

            if (r == 1)
            {
                result += " was hit in the head by a golf ball.";
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
        else if (type == NPCID.BestiaryGirl)
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
        else if (type == ModContent.NPCType<Iceman>())
        {
            int r = Main.rand.Next(7);
            if (r == 0)
            {
                result += " froze.";
            }

            if (r == 1)
            {
                result += " melted.";
            }

            if (r == 2)
            {
                result += " has died.";
            }

            if (r == 3)
            {
                result += "'s ice was cracked.";
            }

            if (r == 4)
            {
                if (NPC.AnyNPCs(NPCID.ArmsDealer))
                {
                    result += " was used to cool " + Main.npc[FindATypeOfNPC(NPCID.ArmsDealer)].GivenName + "'s drink.";
                }
                else
                {
                    result += " fell into a crevasse.";
                }
            }

            if (r == 5)
            {
                result += " fell into a crevasse";
            }

            if (r == 6)
            {
                result += " slipped.";
            }
        }
        else if (type == ModContent.NPCType<Librarian>())
        {
            int r = Main.rand.Next(4);
            if (r == 0)
            {
                result += " was nuked by a full squad.";
            }

            if (r == 1)
            {
                result += " fell victim to toxic world chat.";
            }

            if (r == 2)
            {
                result += " couldn't afford grade eighteen.";
            }

            if (r == 3)
            {
                result += " was slain by a boss cone attack.";
            }
        }
        else
        {
            result += " was slain...";
        }

        return result;
    }
    /// <summary>
    /// Spawns the Wall of Steel at the given position.
    /// </summary>
    /// <param name="pos">The position to spawn the boss at.</param>
    /// <param name="essence">Whether or not the method will broadcast the "has awoken!" message.</param>
    public static void SpawnWOS(Vector2 pos, bool essence = false)
    {
        if (pos.Y / 16f < Main.maxTilesY - 205)
        {
            return;
        }
        if (AvalonWorld.WallOfSteel >= 0 || Main.wofNPCIndex >= 0)
        {
            return;
        }
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            return;
        }
        int num = 1;
        if (pos.X / 16f > Main.maxTilesX / 2)
        {
            num = -1;
        }
        bool flag = false;
        int num2 = (int)pos.X;
        while (!flag)
        {
            flag = true;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && Main.player[i].position.X > num2 - 1200 && Main.player[i].position.X < num2 + 1200)
                {
                    num2 -= num * 16;
                    flag = false;
                }
            }
            if (num2 / 16 < 20 || num2 / 16 > Main.maxTilesX - 20)
            {
                flag = true;
            }
        }
        int num3 = (int)pos.Y;
        int num4 = num2 / 16;
        int num5 = num3 / 16;
        int num6 = 0;
        try
        {
            while (WorldGen.SolidTile(num4, num5 - num6) || Main.tile[num4, num5 - num6].LiquidAmount >= 100)
            {
                if (!WorldGen.SolidTile(num4, num5 + num6) && Main.tile[num4, num5 + num6].LiquidAmount < 100)
                {
                    num5 += num6;
                    goto IL_162;
                }
                num6++;
            }
            num5 -= num6;
        }
        catch
        {
        }
        IL_162:
        num3 = num5 * 16;
        int num7 = NPC.NewNPC(NPC.GetBossSpawnSource(Player.FindClosest(pos, 32, 32)), num2, num3, ModContent.NPCType<NPCs.Bosses.WallofSteel>(), 0);
        if (Main.netMode == NetmodeID.Server && num7 < 200)
        {
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, NetworkText.Empty, num7);
        }
        //if (Main.npc[num7].displayName == "")
        //{
        //    Main.npc[num7].DisplayName = "Wall of Steel";
        //}
        if (!essence)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText("Wall of Steel has awoken!", 175, 75, 255);
                return;
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Wall of Steel has awoken!"), new Color(175, 75, 255));
            }
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
    public override void SetupShop(int type, Chest shop, ref int nextSlot)
    {
        if (type == NPCID.Steampunker)
        {
            if (Main.bloodMoon || Main.eclipse)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Ammo.YellowSolution>());
                nextSlot++;
            }
            if (Main.LocalPlayer.ZoneJungle)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Ammo.LimeGreenSolution>());
                nextSlot++;
            }
            if (ModContent.GetInstance<DownedBossSystem>().DownedMechasting)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Ammo.BlackSolution>());
                shop.item[nextSlot].value = Item.buyPrice(0, 1);
                nextSlot++;
            }
        }
        if (type == NPCID.Pirate && NPC.downedPirates)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<FalseTreasureMap>());
            shop.item[nextSlot].value = Item.buyPrice(0, 4);
            nextSlot++;
        }
        if (type == NPCID.GoblinTinkerer && NPC.downedGoblins)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<GoblinRetreatOrder>());
            shop.item[nextSlot].value = Item.buyPrice(0, 4);
            nextSlot++;
        }
        if (type == NPCID.ArmsDealer && ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Ammo.MissileBolt>());
            nextSlot++;
        }
    }
    public override bool CheckDead(NPC npc)
    {
        if (npc.townNPC && npc.life <= 0)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(npc.FullName + TownDeathMsg(npc.type), new Color(178, 0, 90));
                npc.life = 0;
                npc.HitEffect();
                npc.active = false;
                npc.NPCLoot();
                SoundEngine.PlaySound(SoundID.NPCDeath1, npc.position);
            }
            else
            {
                ChatHelper.BroadcastChatMessage(
                    NetworkText.FromLiteral(npc.FullName + TownDeathMsg(npc.type)),
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
                        if (Main.rand.NextBool(8))
                        {
                            t = ItemID.DyeTradersScimitar;
                        }

                        break;
                    case NPCID.Painter:
                        if (Main.rand.NextBool(10))
                        {
                            t = ItemID.PainterPaintballGun;
                        }

                        break;
                    case NPCID.DD2Bartender:
                        if (Main.rand.NextBool(8))
                        {
                            t = ItemID.AleThrowingGlove;
                        }

                        break;
                    case NPCID.Stylist:
                        if (Main.rand.NextBool(8))
                        {
                            t = ItemID.StylistKilLaKillScissorsIWish;
                        }

                        break;
                    case NPCID.Clothier:
                        t = ItemID.RedHat;
                        break;
                    case NPCID.PartyGirl:
                        if (Main.rand.NextBool(4))
                        {
                            t = ItemID.PartyGirlGrenade;
                            s = Main.rand.Next(30, 61);
                        }

                        break;
                    case NPCID.TaxCollector:
                        if (Main.rand.NextBool(8))
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
                    int a = Item.NewItem(npc.GetSource_Loot(), npc.position, 16, 16, t, s);
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.Empty, a);
                }

                // Main.npc[npc.whoAmI].NPCLoot();
                SoundEngine.PlaySound(SoundID.NPCDeath1, npc.position);
            }

            return false;
        }

        return base.CheckDead(npc);
    }
    public override void SetDefaults(NPC npc)
    {
        #region shm mob scaling
        if (ModContent.GetInstance<AvalonWorld>().SuperHardmode)
        {
            if (!SHMMobs.Contains(npc.type))
            {
                if (Main.expertMode)
                {
                    if (npc.townNPC)
                    {
                        npc.lifeMax *= 2;
                    }
                    else if (npc.boss)
                    {
                        npc.lifeMax = (int)(npc.lifeMax * 1.2);
                        npc.damage = (int)(npc.damage * 1.3);
                    }
                    else
                    {
                        npc.lifeMax = (int)(npc.lifeMax * 1.6);
                        npc.damage = (int)(npc.damage * 1.5);
                    }
                }
                else
                {
                    if (npc.townNPC)
                    {
                        npc.lifeMax *= 2;
                    }
                    else if (npc.boss)
                    {
                        npc.lifeMax = (int)(npc.lifeMax * 1.5);
                        npc.damage *= 2;
                    }
                    else
                    {
                        npc.damage *= 2;
                        npc.lifeMax *= 2;
                    }
                }
            }
        }
        #endregion
    }
    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        if (npc.HasBuff<Plagued>())
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.VilePowder);
        }

        if (npc.HasBuff<AstralCurse>())
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit);
        }

        if (npc.HasBuff<BacteriaInfection>())
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.ScourgeOfTheCorruptor);
        }

        if (npc.HasBuff<Bleeding>())
        {
            for (int i = 0; i < npc.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks; i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
            }
        }
    }

    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress)
        {
            pool.Clear();
            pool.Add(ModContent.NPCType<Valkyrie>(), 0.6f);
            pool.Add(ModContent.NPCType<CloudBat>(), 0.9f);
        }

        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && !spawnInfo.Player.InPillarZone())
        {
            pool.Clear();
            pool.Add(ModContent.NPCType<Bactus>(), 1f);
            pool.Add(ModContent.NPCType<PyrasiteHead>(), 0.1f);
            if (Main.hardMode)
            {
                pool.Add(ModContent.NPCType<Cougher>(), 0.8f);
                pool.Add(ModContent.NPCType<Ickslime>(), 0.7f);
                if (spawnInfo.Player.ZoneRockLayerHeight)
                {
                    pool.Add(ModContent.NPCType<Viris>(), 1f);
                    pool.Add(ModContent.NPCType<GrossyFloat>(), 0.6f);
                }

                if (spawnInfo.Player.ZoneDesert)
                {
                    pool.Add(NPCID.DarkMummy, 0.3f);
                    pool.Add(ModContent.NPCType<EvilVulture>(), 0.4f);
                }
            }
        }

        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneCaesium && Main.hardMode)
        {
            pool.Clear();
            pool.Add(ModContent.NPCType<CaesiumBrute>(), 1f);
            pool.Add(ModContent.NPCType<CaesiumSeekerHead>(), 0.05f);
            pool.Add(ModContent.NPCType<CaesiumStalker>(), 0.9f);
        }

        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneDarkMatter && !spawnInfo.Player.InPillarZone())
        {
            pool.Clear();
            pool.Add(ModContent.NPCType<DarkMotherSlime>(), 0.5f);
            pool.Add(ModContent.NPCType<DarkMatterSlime>(), 0.9f);
            pool.Add(ModContent.NPCType<VampireHarpy>(), 0.9f);
            pool.Add(ModContent.NPCType<MatterMan>(), 0.9f);
            pool.Add(ModContent.NPCType<UnstableAnomaly>(), 0.9f);
        }

        if (spawnInfo.Player.GetModPlayer<ExxoBiomePlayer>().ZoneHellcastle)
        {
            pool.Clear();
            pool.Add(NPCID.Demon, 0.2f);
            pool.Add(NPCID.RedDevil, 0.2f);
            pool.Add(ModContent.NPCType<EctoHand>(), 0.3f);
            pool.Add(ModContent.NPCType<HellboundLizard>(), 1f);
            pool.Add(ModContent.NPCType<Gargoyle>(), 1f);
            if (ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode)
            {
                pool.Add(ModContent.NPCType<ArmoredHellTortoise>(), 1f);
            }
        }
    }

    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
        if (player.GetModPlayer<ExxoBuffPlayer>().AdvancedBattle)
        {
            spawnRate = (int)(spawnRate * AdvBattle.RateMultiplier);
            maxSpawns = (int)(maxSpawns * AdvBattle.SpawnMultiplier);
        }

        if (player.GetModPlayer<ExxoBuffPlayer>().AdvancedCalming)
        {
            spawnRate = (int)(spawnRate * AdvCalming.RateMultiplier);
            maxSpawns = (int)(maxSpawns * AdvCalming.SpawnMultiplier);
        }
    }
    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        var hardModeCondition = new HardmodeOnly();
        var superHardModeCondition = new Superhardmode();
        var zoneRockLayerCondition = new ZoneRockLayer();
        var contagionCondition = new ZoneContagion();
        var undergroundContagionCondition = new Combine(true, "Drops in the underground contagion", contagionCondition,
            zoneRockLayerCondition);
        var undergroundHardmodeContagionCondition = new Combine(
            true,
            undergroundContagionCondition.GetConditionDescription(), undergroundContagionCondition,
            hardModeCondition);
        var dungeonCondition = new ZoneDungeon();
        var hardmodeDungeonCondition = new Combine(true, dungeonCondition.GetConditionDescription(), hardModeCondition,
            dungeonCondition);
        var desertPostBeakCondition = new DesertPostBeakDrop();
        var snowCondition = new ZoneSnow();
        var undergroundSnow = new Combine(true, "Drops in the underground snow", snowCondition, zoneRockLayerCondition);
        var undergroundHardmodeSnow = new Combine(true, undergroundSnow.GetConditionDescription(), undergroundSnow,
            hardModeCondition);
        var bloodMoonAndNotFromStatueCondition = new Conditions.IsBloodMoonAndNotFromStatue();
        var eclipseCondition = new IsEclipse();

        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumHeadgear>(),
            150));
        globalLoot.Add(ItemDropRule.ByCondition(
            desertPostBeakCondition,
            ModContent.ItemType<AncientTitaniumPlateMail>(), 150));
        globalLoot.Add(ItemDropRule.ByCondition(desertPostBeakCondition, ModContent.ItemType<AncientTitaniumGreaves>(),
            150));
        globalLoot.Add(ItemDropRule.ByCondition(undergroundHardmodeSnow, ModContent.ItemType<SoulofIce>(), 10));
        globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<Rock>(), 600));
        globalLoot.Add(ItemDropRule.ByCondition(hardModeCondition, ModContent.ItemType<PointingLaser>(), 650)
            .HideFromBestiary());
        globalLoot.Add(ItemDropRule.ByCondition(superHardModeCondition, ModContent.ItemType<AlienDevice>(), 700)
            .HideFromBestiary());
        globalLoot.Add(ItemDropRule.ByCondition(
            undergroundHardmodeContagionCondition,
            ModContent.ItemType<RingofDisgust>(), RareChance));
        globalLoot.Add(ItemDropRule.ByCondition(hardmodeDungeonCondition, ModContent.ItemType<Trespassing>(),
            RareChance));
        globalLoot.Add(ItemDropRule.ByCondition(
            bloodMoonAndNotFromStatueCondition,
            ModContent.ItemType<BloodyWhetstone>(), 80));
        globalLoot.Add(ItemDropRule.ByCondition(hardmodeDungeonCondition, ModContent.ItemType<ACometHasStruckGround>(),
            RareChance));
        globalLoot.Add(ItemDropRule.ByCondition(eclipseCondition, ModContent.ItemType<EclipseofDoom>(), RareChance));


        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.JungleKey);
        LeadingConditionRule JungleKeyRule = new LeadingConditionRule(new CloverPotionActive());
        JungleKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.JungleKey, 1250, 1, 1, new Conditions.JungleKeyCondition()), true);
        JungleKeyRule.OnFailedRoll(new ItemDropWithConditionRule(ItemID.JungleKey, 2500, 1, 1, new Conditions.JungleKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CorruptionKey);
        LeadingConditionRule CorruptKeyRule = new LeadingConditionRule(new CloverPotionActive());
        CorruptKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.CorruptionKey, 1250, 1, 1, new Conditions.CorruptKeyCondition()), true);
        CorruptKeyRule.OnFailedRoll(new ItemDropWithConditionRule(ItemID.CorruptionKey, 2500, 1, 1, new Conditions.CorruptKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.FrozenKey);
        LeadingConditionRule FrozenKeyRule = new LeadingConditionRule(new CloverPotionActive());
        FrozenKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.FrozenKey, 1250, 1, 1, new Conditions.FrozenKeyCondition()), true);
        FrozenKeyRule.OnFailedRoll(new ItemDropWithConditionRule(ItemID.FrozenKey, 2500, 1, 1, new Conditions.FrozenKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.CrimsonKey);
        LeadingConditionRule CrimsonKeyRule = new LeadingConditionRule(new CloverPotionActive());
        CrimsonKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.CrimsonKey, 1250, 1, 1, new Conditions.CrimsonKeyCondition()), true);
        CrimsonKeyRule.OnFailedRoll(new ItemDropWithConditionRule(ItemID.CrimsonKey, 2500, 1, 1, new Conditions.CrimsonKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.HallowedKey);
        LeadingConditionRule HallowKeyRule = new LeadingConditionRule(new CloverPotionActive());
        HallowKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.HallowedKey, 1250, 1, 1, new Conditions.HallowKeyCondition()), true);
        HallowKeyRule.OnFailedRoll(new ItemDropWithConditionRule(ItemID.HallowedKey, 2500, 1, 1, new Conditions.HallowKeyCondition()), true);

        globalLoot.RemoveWhere(
            rule => rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.DungeonDesertKey);
        LeadingConditionRule DesertKeyRule = new LeadingConditionRule(new CloverPotionActive());
        DesertKeyRule.OnSuccess(new ItemDropWithConditionRule(ItemID.DungeonDesertKey, 1250, 1, 1, new Conditions.DesertKeyCondition()), true);
        DesertKeyRule.OnFailedRoll(new ItemDropWithConditionRule(ItemID.DungeonDesertKey, 2500, 1, 1, new Conditions.DesertKeyCondition()), true);

        globalLoot.Add(FrozenKeyRule);
        globalLoot.Add(JungleKeyRule);
        globalLoot.Add(CorruptKeyRule);
        globalLoot.Add(HallowKeyRule);
        globalLoot.Add(CrimsonKeyRule);
        globalLoot.Add(DesertKeyRule);

        if (Avalon.ImkSushisMod != null)
        {
            globalLoot.Add(ItemDropRule.ByCondition(
                new PostPhantasmHellcastleTokenDrop(),
                ModContent.ItemType<HellcastleToken>(), 15));
            globalLoot.Add(ItemDropRule.ByCondition(
                new SuperhardmodePreArmaTokenDrop(),
                ModContent.ItemType<SuperhardmodeToken>(), 15));
            globalLoot.Add(ItemDropRule.ByCondition(new PostArmageddonTokenDrop(), ModContent.ItemType<DarkMatterToken>(),
                15));
            globalLoot.Add(ItemDropRule.ByCondition(new PostMechastingTokenDrop(), ModContent.ItemType<MechastingToken>(),
                15));
            globalLoot.Add(ItemDropRule.ByCondition(new ZoneTropicsToken(), ModContent.ItemType<TropicsToken>(), 15));
            globalLoot.Add(ItemDropRule.ByCondition(
                new UndergroundHardmodeContagionTokenDrop(contagionCondition),
                ModContent.ItemType<ContagionToken>(), 15));
        }
    }

    internal static int[] Emblems;
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (Avalon.ImkSushisMod != null)
        {
            List<IItemDropRule> rules = npcLoot.Get(false);
            rules = rules.Where(x => x is ItemDropWithConditionRule drop &&
                (drop.itemId == Avalon.ImkSushisMod.Find<ModItem>("PostMartiansLootToken").Type ||
                drop.itemId == Avalon.ImkSushisMod.Find<ModItem>("PostPlanteraLootToken").Type ||
                drop.itemId == Avalon.ImkSushisMod.Find<ModItem>("HardmodeLootToken").Type)).ToList();
            foreach (ItemDropWithConditionRule rule in rules)
            {
                rule.condition = new Combine(true, null, rule.condition, new PostPhantasmDrop());
            }
        }
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
        var undergroundHardmodeContagionCondition = new Combine(
            true,
            undergroundContagionCondition.GetConditionDescription(), undergroundContagionCondition,
            hardModeCondition);
        var dungeonCondition = new ZoneDungeon();
        var hardmodeDungeonCondition = new Combine(true, dungeonCondition.GetConditionDescription(), hardModeCondition,
            dungeonCondition);

        #region individual

        switch (npc.type)
        {
            case NPCID.Unicorn:
            case NPCID.BloodJelly:
            case NPCID.LightMummy:
            case NPCID.DarkMummy:
            case NPCID.BloodMummy:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HiddenBlade>(), 50));
                break;
            case NPCID.Mummy:
            case NPCID.FungoFish:
            case NPCID.Clinger:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AmmoMagazine>(), 50));
                break;
            case NPCID.Golem:
                // Get main drops and duplicate
                OneFromRulesRule? oneFromRulesRule = null;
                foreach (IItemDropRule rule in npcLoot.Get(false))
                {
                    if (rule is not LeadingConditionRule rule1)
                    {
                        continue;
                    }

                    foreach (IItemDropRuleChainAttempt chain in rule1.ChainedRules)
                    {
                        if (chain is not Chains.TryIfSucceeded { RuleToChain: OneFromRulesRule ruleMain })
                        {
                            continue;
                        }

                        oneFromRulesRule = ruleMain;
                        break;
                    }

                    if (oneFromRulesRule != null)
                    {
                        break;
                    }
                }

                var condition = new Combine(true, null, new FirstTimeKillingGolem(), notExpertCondition);
                npcLoot.Add(ItemDropRule.ByCondition(condition, ItemID.Picksaw));
                if (oneFromRulesRule != null)
                {
                    npcLoot.Add(new LeadingConditionRule(notExpertCondition)).OnSuccess(oneFromRulesRule.HideFromBestiary());
                }
                else
                {
                    Mod.Logger.Error("Extra normal mode drops for Golem failed to be added, please report this to the mod author.");
                }

                break;

            case NPCID.WallofFlesh:
                List<IItemDropRule> rules = npcLoot.Get(false);
                IItemDropRule? match = null;
                foreach (IItemDropRule rule in rules)
                {
                    if (rule is not LeadingConditionRule lc)
                    {
                        continue;
                    }

                    foreach (IItemDropRuleChainAttempt chain in lc.ChainedRules)
                    {
                        if (chain is Chains.TryIfSucceeded { RuleToChain: OneFromOptionsNotScaledWithLuckDropRule ruleMain } && ruleMain.dropIds.Contains(ItemID.WarriorEmblem))
                        {
                            Emblems = ruleMain.dropIds;
                            match = rule;
                            break;
                        }
                    }

                    if (match != null)
                    {
                        break;
                    }
                }
                npcLoot.Remove(match);
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<NullEmblem>()));
                npcLoot.Add(ItemDropRule.ByCondition(notExpertCondition, ModContent.ItemType<FleshyTendril>(),
                    1,
                    13, 19));
                break;
            case NPCID.AngryBones or NPCID.AngryBonesBig or NPCID.AngryBonesBigHelmet
                or NPCID.AngryBonesBigMuscle:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlackWhetstone>(), 100));
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
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClownBomb>(), 3, 2, 6));
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
                npcLoot.Add(ItemDropRule.ByCondition(
                    preHardModeCondition,
                    ModContent.ItemType<BloodyAmulet>(),
                    10, 1, 1, 3));

                npcLoot.Add(ItemDropRule.ByCondition(
                    hardmodePreSuperHardmodeCondition,
                    ModContent.ItemType<BloodyAmulet>(),
                    100, 1, 1, 15));

                npcLoot.Add(ItemDropRule.ByCondition(
                    superHardModeCondition,
                    ModContent.ItemType<BloodyAmulet>(),
                    100, 1, 1, 7));

                break;

            case NPCID.Shark:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DivingSuit>(), 60));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DivingPants>(), 40));
                break;
        }

        #endregion individual

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
        if (npc.type is NPCID.LavaSlime)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.LavaCharm, 175));
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
                ModContent.ItemType<AuraPotion>(), ItemID.IronskinPotion, ItemID.SwiftnessPotion,
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

        //foreach (var rule in npcLoot.Get())
        //{
        //    // You must study the vanilla code to know what to objects to cast to.
        //    if (rule is ItemDropWithConditionRule drop && drop.itemId == ItemID.JungleKey && )
        //        drop.chanceDenominator = 1250;
        //}

        //if (Avalon.ImkSushisMod != null && !NPCID.Sets.CountsAsCritter[npc.type] && !npc.townNPC)
        //{
        //    npcLoot.Add(ItemDropRule.ByCondition(
        //        new PostPhantasmHellcastleTokenDrop(),
        //        ModContent.ItemType<HellcastleToken>(), 75));
        //    npcLoot.Add(ItemDropRule.ByCondition(
        //        new SuperhardmodePreArmaTokenDrop(),
        //        ModContent.ItemType<SuperhardmodeToken>(), 75));
        //    npcLoot.Add(ItemDropRule.ByCondition(new PostArmageddonTokenDrop(), ModContent.ItemType<DarkMatterToken>(),
        //        75));
        //    npcLoot.Add(ItemDropRule.ByCondition(new PostMechastingTokenDrop(), ModContent.ItemType<MechastingToken>(),
        //        75));
        //    npcLoot.Add(ItemDropRule.ByCondition(new ZoneTropicsToken(), ModContent.ItemType<TropicsToken>(), 75));
        //    npcLoot.Add(ItemDropRule.ByCondition(
        //        new UndergroundHardmodeContagionTokenDrop(undergroundContagionCondition),
        //        ModContent.ItemType<ContagionToken>(), 75));
        //}
    }

    public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
    {
        if (npc.type is NPCID.BloodJelly or NPCID.Unicorn or NPCID.DarkMummy or NPCID.LightMummy && Main.rand.NextBool(9))
        {
            target.AddBuff(ModContent.BuffType<BrokenWeaponry>(), 60 * 7);
        }
        if (npc.type == NPCID.Mummy || npc.type == NPCID.FungoFish || npc.type == NPCID.Clinger || npc.type == ModContent.NPCType<GrossyFloat>())
        {
            if (Main.rand.NextBool(9))
            {
                target.AddBuff(ModContent.BuffType<Unloaded>(), 60 * 7);
            }
        }
    }
    public override void Unload() => Emblems = null;
    public override void OnKill(NPC npc)
    {
        if (npc.type == NPCID.Golem && !NPC.downedGolemBoss)
        {
            AvalonWorld.GenerateSolarium();
        }
        if (npc.HasBuff(ModContent.BuffType<Virulent>()))
        {
            for (int i = 0; i < 3 + Main.rand.Next(3); i++)
            {
                Vector2 randomDir = new Vector2(Main.rand.NextFloat(-100f, 100f), Main.rand.NextFloat(-100f, 100f));
                Projectile.NewProjectile(npc.GetSource_FromThis(), npc.position, Vector2.Normalize(randomDir) * (1f + Main.rand.NextFloat(2f)), ModContent.ProjectileType<Projectiles.Melee.VirulentCloud>(), default, 0, Main.player[0].whoAmI);
            }
            for (int i = 0; i < 20; i++)
            {
                int greencum = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.ContagionSpray>(), 0f, 0f, default, default, 1.2f);
                Main.dust[greencum].noGravity = true;
                Main.dust[greencum].velocity *= 3f + Main.rand.NextFloat(3f);
            }
        }
        if (npc.type == NPCID.SkeletronHead && !NPC.downedBoss3)
        {
            AvalonWorld.GenerateSulphur();
        }
        if (npc.type == NPCID.DungeonSpirit && Main.rand.NextBool(15) &&
            Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon)
        {
            int proj = Projectile.NewProjectile(npc.GetSource_FromThis(), npc.position, npc.velocity,
                ModContent.ProjectileType<Projectiles.SpiritPoppy>(), 0, 0, Main.myPlayer);
            Main.projectile[proj].velocity.Y = -3.5f;
            Main.projectile[proj].velocity.X = Main.rand.Next(-45, 46) * 0.1f;
        }
        if (npc.type is NPCID.TheDestroyer or NPCID.Retinazer or NPCID.Spazmatism or NPCID.SkeletronPrime)
        {
            if (!NPC.downedMechBossAny)
            {
                if ((npc.type == NPCID.Spazmatism && NPC.AnyNPCs(NPCID.Retinazer)) || (npc.type == NPCID.Retinazer && NPC.AnyNPCs(NPCID.Spazmatism)))
                {
                    return;
                }
                AvalonWorld.GenerateHallowedOre();
            }
        }
    }
}
