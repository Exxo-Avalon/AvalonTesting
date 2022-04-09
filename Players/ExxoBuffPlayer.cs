using System.IO;
using AvalonTesting.Buffs.AdvancedBuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

public class ExxoBuffPlayer : ModPlayer
{
    public bool AdvancedBattle;
    private bool daggerBuffLock;
    public bool EarthInsignia;
    public int OldFallStart;
    public float DaggerStaffRotation { get; private set; }
    public int FrameCount { get; private set; }
    public int ShadowCooldown { get; private set; } = 300;

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
        EarthInsignia = false;
    }

    public override void PreUpdateBuffs()
    {
        FrameCount++;
        ShadowCooldown++;
        daggerBuffLock = false;
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
}
