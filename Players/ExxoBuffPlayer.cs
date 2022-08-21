using System;
using Avalon.Buffs;
using Avalon.Buffs.AdvancedBuffs;
using Avalon.Network;
using Avalon.Network.Handlers;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Players;
// TODO: Add duratanium shield DoT debuff logic
public class ExxoBuffPlayer : ModPlayer
{
    public const string LavaMermanName = "LavaMerman";
    public bool AccLavaMerman;
    public bool AdvancedBattle;
    public bool AdvancedCalming;

    public bool Unloaded;
    public bool BrokenWeaponry;
    public bool Electrified;

    public int DeleriumCount;
    public int FracturingArmorLastRecord;
    public int FracturingArmorLevel;
    public int InfectDamage;
    public bool Lucky;
    public bool Malaria;
    public bool Melting;
    public bool CaesiumPoison;
    public bool DarkInferno;
    public bool NoSticky;
    public int OldFallStart;
    public bool Ward;
    public int WardCurseDOT;

    public int TimeSlowCounter;

    public bool SkyBlessing;
    public int SkyStacks = 1;

    public int StingerProbeTimer;
    private bool lavaMerman;
    public float DaggerStaffRotation { get; set; }
    public float StingerProbeRotation { get; set; }
    public float ReflectorStaffRotation { get; set; }
    public int FrameCount { get; private set; }
    public int ShadowCooldown { get; private set; }

    public override void Load()
    {
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{Avalon.TextureAssetsPath}/Costumes/LavaMerman_Head", EquipType.Head,
            null, LavaMermanName);
        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{Avalon.TextureAssetsPath}/Costumes/LavaMerman_Body", EquipType.Body,
            null, LavaMermanName);
        EquipLoader.AddEquipTexture(
            Mod, $"{nameof(Avalon)}/{Avalon.TextureAssetsPath}/Costumes/LavaMerman_Legs", EquipType.Legs,
            null, LavaMermanName);
    }

    public override void SetStaticDefaults()
    {
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        int lavaMermanHead = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Head);
        int lavaMermanBody = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Body);
        int lavaMermanLegs = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Legs);

        ArmorIDs.Head.Sets.DrawHead[lavaMermanHead] = false;
        ArmorIDs.Body.Sets.HidesTopSkin[lavaMermanBody] = true;
        ArmorIDs.Body.Sets.HidesArms[lavaMermanBody] = true;
        ArmorIDs.Legs.Sets.HidesBottomSkin[lavaMermanLegs] = true;
    }

    public override void ResetEffects()
    {
        AdvancedBattle = false;
        AdvancedCalming = false;
        Lucky = false;
        Malaria = false;
        Melting = false;
        NoSticky = false;
        AccLavaMerman = false;
        lavaMerman = false;
        SkyBlessing = false;
        Unloaded = false;
        BrokenWeaponry = false;
        DarkInferno = false;
        CaesiumPoison = false;
        Electrified = false;
        Ward = false;
    }

    public override void PreUpdateBuffs()
    {
        StingerProbeRotation = (StingerProbeRotation % MathHelper.TwoPi) + 0.01f;
        DaggerStaffRotation = (DaggerStaffRotation % MathHelper.TwoPi) + 0.01f;
        ReflectorStaffRotation = (ReflectorStaffRotation % MathHelper.TwoPi) + 0.01f;
        if (Player.active)
        {
            FrameCount++;
            ShadowCooldown++;
            if (Player.GetModPlayer<ExxoEquipEffectPlayer>().AstralCooldown > 0)
            {
                Player.GetModPlayer<ExxoEquipEffectPlayer>().AstralCooldown--;
            }
        }
    }
    public override void PostUpdateEquips()
    {
        if (AccLavaMerman && !Player.GetModPlayer<ExxoEquipEffectPlayer>().HideVarefolk && Collision.LavaCollision(Player.position, Player.width, Player.height))
        {
            lavaMerman = true;
            Player.merman = true;
        }
    }
    public override void PreUpdate()
    {
        if (Player.tongued)
        {
            bool flag14 = false;
            if (AvalonWorld.WallOfSteel >= 0)
            {
                float num75 = Main.npc[AvalonWorld.WallOfSteel].position.X + Main.npc[AvalonWorld.WallOfSteel].width / 2;
                num75 += Main.npc[AvalonWorld.WallOfSteel].direction * 200;
                float num104 = Main.npc[AvalonWorld.WallOfSteel].position.Y + Main.npc[AvalonWorld.WallOfSteel].height / 2;
                Vector2 center = Player.Center;
                float num76 = num75 - center.X;
                float num77 = num104 - center.Y;
                float num78 = (float)Math.Sqrt(num76 * num76 + num77 * num77);
                float num79 = 11f;
                float num80 = num78;
                if (num78 > num79)
                {
                    num80 = num79 / num78;
                }
                else
                {
                    num80 = 1f;
                    flag14 = true;
                }
                num76 *= num80;
                num77 *= num80;
                Player.velocity.X = num76;
                Player.velocity.Y = num77;
            }
            else
            {
                flag14 = true;
            }
            if (flag14 && Main.myPlayer == Player.whoAmI)
            {
                for (int num81 = 0; num81 < Player.MaxBuffs; num81++)
                {
                    if (Player.buffType[num81] == 38)
                    {
                        Player.DelBuff(num81);
                    }
                }
            }
        }
    }
    public override void PostUpdateBuffs()
    {
        OldFallStart = Player.fallStart;
    }
    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) =>
        ModContent.GetInstance<ExxoBuffPlayerSyncHandler>()
            .Send(new BasicPlayerNetworkArgs(Player), toWho, fromWho);

    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        if (Player.HasBuff<AdvAmmoReservation>() && Main.rand.NextFloat() < AdvAmmoReservation.Chance)
        {
            return false;
        }

        return base.CanConsumeAmmo(weapon, ammo);
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (Player.whoAmI != Main.myPlayer)
        {
            return;
        }
    }
    public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
    {
        if (Player.HasItem(ModContent.ItemType<Items.Potions.ImmortalityPotion>()) && !Player.HasBuff(ModContent.BuffType<ImmortalityCooldown>()))
        {
            Player.statLife = Player.statLifeMax2 / 3;
            Player.AddBuff(ModContent.BuffType<ImmortalityCooldown>(), 60 * 60 * 3);
            int i = Player.FindItem(ModContent.ItemType<Items.Potions.ImmortalityPotion>());
            Player.inventory[i].stack--;
            SoundEngine.PlaySound(SoundID.Item3, Player.position);
            if (Player.inventory[i].stack <= 0)
            {
                Player.inventory[i].SetDefaults();
            }
            return false;
        }
        if (Malaria)
        {
            damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was bitten by a mosquito.");
        }
        if (Melting)
        {
            damageSource = PlayerDeathReason.ByCustomReason(Player.name + " melted away.");
        }
        if (DarkInferno)
        {
            damageSource = PlayerDeathReason.ByCustomReason(Player.name + " withered away in the dark flames.");
        }
        if (Electrified)
        {
            damageSource = PlayerDeathReason.ByCustomReason(Player.name + " had an electrifying personality.");
        }
        return true;
    }
    public override void UpdateBadLifeRegen()
    {
        if (Electrified)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }
            //Player.lifeRegenTime = 0;
            //int minus = Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield && Player.HasItemInArmor(ModContent.ItemType<Items.Accessories.DurataniumShield>()) ? 4 :
            //    Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield ? 6 : 8;
            //int minus2 = Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield && Player.HasItemInArmor(ModContent.ItemType<Items.Accessories.DurataniumOmegaShield>())
            //    ? 16 : Player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield ? 24 : 32;
            //Player.lifeRegen -= minus;
            //if (Player.velocity.X != 0)
            //{
            //    Player.lifeRegen -= minus2;
            //}
        }
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.drawPlayer.HasBuff<SpectrumBlur>())
        {
            drawInfo.drawPlayer.eocDash = 1;
        }
    }

    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
                                 ref bool customDamage,
                                 ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        if (Player.HasBuff<SpectrumBlur>() && Player.whoAmI == Main.myPlayer && Main.rand.NextBool(10))
        {
            SpectrumDodge();
            return false;
        }

        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit,
            ref customDamage,
            ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    }

    public override void PostUpdateRunSpeeds() => FloorVisualsAvalon();

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback,
                                              ref bool crit,
                                              ref int hitDirection)
    {
        if (target.HasBuff(ModContent.BuffType<AstralCurse>()))
        {
            damage *= 3;
        }

        if (Player.HasBuff(ModContent.BuffType<BacteriaEndurance>()))
        {
            damage += 8;
        }
    }
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        if (target.HasBuff(ModContent.BuffType<AstralCurse>()))
        {
            damage *= 3;
        }

        if (Player.HasBuff(ModContent.BuffType<BacteriaEndurance>()))
        {
            damage += 8;
        }
    }

    public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
    {
        if (Player.HasItem(ModContent.ItemType<Items.Weapons.Blah.BlahsEnergyBlade>()) && Main.rand.NextBool(100) &&
            !Player.HasBuff(ModContent.BuffType<BenevolentWard>()) && !Player.HasBuff(ModContent.BuffType<WardCurse>()))
        {
            Player.AddBuff(ModContent.BuffType<BenevolentWard>(), 8 * 60);
        }
        if (Ward)
        {
            WardCurseDOT += damage;
            damage = 1;
        }
        if (Player.HasBuff(ModContent.BuffType<ShadowCurse>()))
        {
            damage *= 2;
        }
    }
    public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
    {
        if (Player.HasItem(ModContent.ItemType<Items.Weapons.Blah.BlahsEnergyBlade>()) && Main.rand.NextBool(100) &&
            !Player.HasBuff(ModContent.BuffType<BenevolentWard>()) && !Player.HasBuff(ModContent.BuffType<WardCurse>()))
        {
            Player.AddBuff(ModContent.BuffType<BenevolentWard>(), 8 * 60);
        }
        if (Ward)
        {
            WardCurseDOT += damage;
            Main.NewText(WardCurseDOT);
            damage = 1;
        }
        if (Player.HasBuff(ModContent.BuffType<ShadowCurse>()))
        {
            damage *= 2;
        }

        if (CaesiumPoison)
        {
            damage = (int)(damage * 1.15f);
        }
    }
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        if (Player.whoAmI != Main.myPlayer)
        {
            return;
        }
    }

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a,
                                     ref bool fullBright)
    {
        if (lavaMerman)
        {
            Player.head = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Head);
            Player.body = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, LavaMermanName, EquipType.Legs);
        }
    }

    public void ResetShadowCooldown() => ShadowCooldown = 0;

    public void FloorVisualsAvalon()
    {
        int num = (int)((Player.position.X + (Player.width / 2)) / 16f);
        int num2 = (int)((Player.position.Y + Player.height) / 16f);
        int num3 = -1;
        if (Main.tile[num, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num, num2].TileType])
        {
            num3 = Main.tile[num, num2].TileType;
        }
        else if (Main.tile[num - 1, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num - 1, num2].TileType])
        {
            num3 = Main.tile[num - 1, num2].TileType;
        }
        else if (Main.tile[num + 1, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num + 1, num2].TileType])
        {
            num3 = Main.tile[num + 1, num2].TileType;
        }

        if (num3 > -1)
        {
            if (num3 == 229 && !NoSticky)
            {
                Player.sticky = true;
            }
            else
            {
                Player.sticky = false;
            }
        }
    }

    private void SpectrumDodge()
    {
        Player.immune = true;
        if (Player.longInvince)
        {
            Player.immuneTime = 60;
        }
        else
        {
            Player.immuneTime = 30;
        }

        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SpectrumDodge"), Player.position);
        for (int i = 0; i < Player.hurtCooldowns.Length; i++)
        {
            Player.hurtCooldowns[i] = Player.immuneTime;
        }

        if (Player.whoAmI == Main.myPlayer)
        {
            NetMessage.SendData(Terraria.ID.MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f);
        }
    }
}
