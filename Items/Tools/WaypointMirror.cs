using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Items.Tools;
public class WaypointMirror : ModItem
{
    public List<Vector2> savedLocations = new List<Vector2>();
    public List<int> WorldIDs = new List<int>();
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Right click to set a waypoint at your current location");
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.useTime = 25;
        Item.useTurn = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 25;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item6;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override void SaveData(TagCompound tag)
    {
        if (!WorldIDs.Contains(Main.worldID))
        {
            WorldIDs.Add(Main.worldID);
        }
        tag["SavedWorldIDs"] = WorldIDs;
        if (WorldIDs.IndexOf(Main.worldID) != -1)
        {
            tag["SavedLocations"] = savedLocations[WorldIDs.IndexOf(Main.worldID)];
        }
    }
    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("SavedWorldIDs"))
        {
            WorldIDs = tag.Get<List<int>>("SavedWorldIDs");
            if (tag.ContainsKey("SavedLocations"))
            {
                if (WorldIDs.Contains(Main.worldID))
                {
                    savedLocations.Add(tag.Get<Vector2>("SavedLocations"));
                }
            }
        }
    }
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        if (player.altFunctionUse == 2 && player.itemTime == Item.useTime / 2)
        {
            WorldIDs.Remove(Main.worldID);
            WorldIDs.Add(Main.worldID);
            savedLocations.Add(player.position);
            Main.NewText("Set waypoint to current location.");
        }
        else
        {
            if (player.itemTime == 0)
            {
                player.itemTime = Item.useTime;
            }
            else if (player.itemTime == Item.useTime / 2)
            {
                int index = WorldIDs.IndexOf(Main.worldID);
                Main.NewText(index);
                Vector2 loc = savedLocations[index];
                if (loc != Vector2.Zero)
                {
                    for (int num345 = 0; num345 < 70; num345++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 150, default(Color), 1.5f);
                    }
                    player.grappling[0] = -1;
                    player.grapCount = 0;
                    for (int num346 = 0; num346 < 1000; num346++)
                    {
                        if (Main.projectile[num346].active && Main.projectile[num346].owner == player.whoAmI && Main.projectile[num346].aiStyle == 7)
                        {
                            Main.projectile[num346].Kill();
                        }
                    }
                    player.Teleport(loc);
                    savedLocations.Remove(loc);
                    savedLocations.Add(loc);
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, loc.X, loc.Y, 0);
                    for (int num347 = 0; num347 < 70; num347++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
                    }
                }
                else
                {
                    Main.NewText("No waypoint found!", 250, 0, 0);
                }
            }
        }
    }
}
