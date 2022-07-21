using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Players;
public class ExxoAccEffectPlayer : ModPlayer
{
    public bool ShadowCharm;
    public bool PulseCharm;
    public bool BagOfFire;
    public bool BagOfBlood;
    public bool BagOfFrost;
    public bool BagOfHallows;
    public bool BagOfIck;
    public bool BagOfShadows;
    public override void ResetEffects()
    {
        ShadowCharm = false;
        PulseCharm = false;
        BagOfBlood = false;
        BagOfFire = false;
        BagOfFrost = false;
        BagOfHallows = false;
        BagOfIck = false;
        BagOfShadows = false;
    }
}
