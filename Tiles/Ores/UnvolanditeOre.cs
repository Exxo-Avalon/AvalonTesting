using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class UnvolanditeOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 8f;
        AddMapEntry(new Color(78, 79, 41), LanguageManager.Instance.GetText("Unvolandite"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 840;
        Main.tileBlockLight[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 2100;
        ItemDrop = ModContent.ItemType<Items.Ore.UnvolanditeOre>();
        HitSound = SoundID.Tink;
        MinPick = 245;
        TileID.Sets.Ore[Type] = true;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (j > Main.rockLayer)
        {
            if (Main.rand.Next(5500) == 0 && Vector2.Distance(Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)].position, new Vector2(i * 16, j * 16)) < 12 * 16)
            {
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), i * 16, j * 16, ModContent.NPCType<NPCs.UnvolanditeMiteDigger>());
            }
        }
    }
}
