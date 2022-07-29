using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Audio;

namespace Avalon.Tiles;

public class DragonAltar : ModTile
{
    public override void SetStaticDefaults()
    {
        var name = CreateMapEntryName();
        name.SetDefault("Dragon Altar");
        AddMapEntry(new Color(35, 94, 174), name);
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
        MinPick = 250;
    }
    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        return true;
        //if (!ModContent.GetInstance<AvalonWorld>().SuperHardmode && !Main.hardMode && !AvalonWorld.downedDragonLord) blockDamaged = false;
        //return ModContent.GetInstance<AvalonWorld>().SuperHardmode && Main.hardMode && AvalonWorld.downedDragonLord;
    }
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Placeable.Crafting.DragonAltar>());
    }
    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Items.Consumables.DragonSpine>();
    }
    public override bool RightClick(int i, int j)
    {
        Player p = Main.LocalPlayer;
        for (int v = 0; v < p.inventory.Length; v++)
        {
            if (p.inventory[v].type == ModContent.ItemType<Items.Consumables.DragonSpine>() && !NPC.AnyNPCs(ModContent.NPCType<NPCs.DragonLordHead>()))
            {
                p.inventory[v].stack--;
                SoundEngine.PlaySound(SoundID.Roar, p.position);
                NPC.SpawnOnPlayer(p.whoAmI, ModContent.NPCType<NPCs.DragonLordHead>());
                return true;
            }
        }
        return false;
    }
}
