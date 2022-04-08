using AvalonTesting.Buffs;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Armor;
using AvalonTesting.Prefixes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Players;
public class ExxoPlayer : ModPlayer
{
    public static ReLogic.Content.Asset<Texture2D>[] spectrumArmorTextures;
    public bool darkInferno = false;
    public bool curseOfIcarus = false;
    public int screenShakeTimer;
    public bool spikeImmune = false;
    public bool trapImmune = false;
    public bool caesiumPoison = false;
    public bool hadesCross = false;

    public override void ResetEffects()
    {
        darkInferno = false;
        curseOfIcarus = false;
        trapImmune = false;
        spikeImmune = false;
        caesiumPoison = false;
        hadesCross = false;
    }
    public override void FrameEffects()
    {
        // TODO: Need new hook, FrameEffects doesn't run while paused.
        if (hadesCross)
        {
            var exampleCostume = ModContent.GetInstance<HadesCross>();
            Player.head = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Head);
            Player.body = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Body);
            Player.legs = Mod.GetEquipSlot(exampleCostume.Name, EquipType.Legs);
        }
    }

    public override void PostUpdate()
    {
        if (screenShakeTimer == 1)
        {
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/Stomp"), (int)Player.position.X, (int)Player.position.Y).Volume *= 0.8f;
        }
        if (screenShakeTimer > 0)
        {
            screenShakeTimer--;
        }
    }
    public override void PostUpdateEquips()
    {
        for (int i = 0; i < 3; i++)
        {
            ArmorPrefix prefix;
            if ((prefix = PrefixLoader.GetPrefix(Player.armor[i].prefix) as ArmorPrefix) != null)
            {
                prefix.UpdateEquip(Player);
            }
        }
        if (curseOfIcarus)
        {
            Player.rocketTimeMax = 42;
            Player.wingsLogic = 0;
            if (Player.mount.CanFly() || Player.mount.CanHover()) // Setting player.mount._flyTime does not work for all mounts. Bye-bye mounts!
            {
                Player.mount.Dismount(Player);
            }
        }
    }
    public override void ModifyScreenPosition()
    {
        if (screenShakeTimer > 0)
        {
            Main.screenPosition += Main.rand.NextVector2Circular(20, 20);
        }
    }
    public override void UpdateEquips()
    {
        if (hadesCross)
        {
            Player.AddBuff(ModContent.BuffType<Varefolk>(), 60);
        }
    }
    public override void UpdateBadLifeRegen()
    {
        if (darkInferno)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }
            Player.lifeRegenTime = 0;
            Player.lifeRegen -= 16;
        }
        if (caesiumPoison)
        {
            if (Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
            }
            Player.lifeRegenTime = 0;
            Player.lifeRegen -= 20;
        }
    }
}
