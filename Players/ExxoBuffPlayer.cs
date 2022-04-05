using System.IO;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

public class ExxoBuffPlayer : ModPlayer
{
    private bool daggerBuffLock;
    public float DaggerStaffRotation { get; private set; }

    public void UpdateDaggerStaff()
    {
        if (daggerBuffLock)
        {
            return;
        }

        DaggerStaffRotation = (DaggerStaffRotation % MathHelper.TwoPi) + 0.01f;
        daggerBuffLock = true;
    }

    public override void PreUpdateBuffs()
    {
        daggerBuffLock = false;
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
}
