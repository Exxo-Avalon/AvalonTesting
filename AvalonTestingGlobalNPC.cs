using AvalonTesting.Buffs.AdvancedBuffs;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting;

public class AvalonTestingGlobalNPC : GlobalNPC
{
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
        /*else if (Type == ModContent.NPCType<NPCs.Iceman>())
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
        else if (Type == ModContent.NPCType<NPCs.Librarian>())
        {
            int r = Main.rand.Next(4);
            if (r == 0) result += " was nuked by a full squad.";
            if (r == 1) result += " fell victim to toxic world chat.";
            if (r == 2) result += " couldn't afford grade eighteen.";
            if (r == 3) result += " was slain by a boss cone attack.";
        }*/
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
}
