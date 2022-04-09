using System.IO;
using AvalonTesting.Buffs;
using AvalonTesting.Buffs.AdvancedBuffs;
using AvalonTesting.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

public class ExxoBuffPlayer : ModPlayer
{
    public bool AdvancedBattle;
    public bool AstralProject;
    private bool daggerBuffLock;
    public int DeleriumCount;
    public bool EarthInsignia;
    public int FracturingArmorLastRecord;
    public int FracturingArmorLevel;
    public int InfectDamage;
    public bool Lucky;
    public bool Malaria;
    public bool Melting;
    public int OldFallStart;
    public float DaggerStaffRotation { get; private set; }
    public int FrameCount { get; private set; }
    public int ShadowCooldown { get; private set; }
    public int AstralCooldown { get; private set; }

    public override void OnEnterWorld(Player player)
    {
        ShadowCooldown = 300;
        AstralCooldown = 3600;
    }

    public void UpdateDaggerStaff()
    {
        if (daggerBuffLock)
        {
            return;
        }

        DaggerStaffRotation = (DaggerStaffRotation % MathHelper.TwoPi) + 0.01f;
        daggerBuffLock = true;
    }

    public override void ResetEffects()
    {
        AdvancedBattle = false;
        AstralProject = false;
        EarthInsignia = false;
        Lucky = false;
        Malaria = false;
        Melting = false;
    }

    public override void PreUpdateBuffs()
    {
        FrameCount++;
        ShadowCooldown++;
        AstralCooldown = (int)MathHelper.Min(AstralCooldown++, 3600);
        daggerBuffLock = false;
        if (!AstralProject && Player.HasBuff<AstralProjecting>())
        {
            Player.ClearBuff(ModContent.BuffType<AstralProjecting>());
        }
    }

    public override void PostUpdateBuffs()
    {
        OldFallStart = Player.fallStart;
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)AvalonTesting.MessageType.BuffPlayerSyncPlayer);
        packet.Write((byte)Player.whoAmI);
        packet.Write(DaggerStaffRotation);
        packet.Send(toWho, fromWho);
    }

    public void HandleSyncPlayer(BinaryReader reader)
    {
        DaggerStaffRotation = reader.ReadSingle();
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo)
    {
        if (Player.HasBuff<AdvAmmoReservation>() && Main.rand.NextFloat() < AdvAmmoReservation.Chance)
        {
            return false;
        }

        return base.CanConsumeAmmo(weapon, ammo);
    }

    public void ResetShadowCooldown()
    {
        ShadowCooldown = 0;
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (AstralProject && ModContent.GetInstance<KeybindSystem>().AstralHotkey.JustPressed)
        {
            if (Player.HasBuff<AstralProjecting>())
            {
                Player.ClearBuff(ModContent.BuffType<AstralProjecting>());
                AstralCooldown = 3600;
            }
            else if (AstralCooldown >= 3600)
            {
                Player.AddBuff(ModContent.BuffType<AstralProjecting>(), 15 * 60);
            }
        }
    }

    public override void UpdateBadLifeRegen()
    {
        if (Melting)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }

            Player.lifeRegenTime = 0;
            Player.lifeRegen -= 32;
        }

        if (Malaria)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }

            Player.lifeRegenTime = 0;
            Player.lifeRegen -= 30;
        }
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        if (Player.HasBuff<SpectrumBlur>())
        {
            Player.eocDash = 1;
        }
    }

    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
                                 ref bool customDamage,
                                 ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
    {
        if (Player.HasBuff<SpectrumBlur>() && Player.whoAmI == Main.myPlayer && Main.rand.Next(10) == 0)
        {
            SpectrumDodge();
            return false;
        }

        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound,
            ref genGore, ref damageSource);
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

        SoundEngine.PlaySound(SoundID.Item, Player.position,
            SoundLoader.GetSoundSlot(Mod, "Sounds/Item/SpectrumDodge"));
        for (int i = 0; i < Player.hurtCooldowns.Length; i++)
        {
            Player.hurtCooldowns[i] = Player.immuneTime;
        }

        if (Player.whoAmI == Main.myPlayer)
        {
            NetMessage.SendData(MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f);
        }
    }
}
