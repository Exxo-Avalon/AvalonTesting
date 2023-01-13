
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Avalon.Systems;
using System.Linq;
using System.IO;

namespace Avalon.Items.Tools{

public class WaypointSystem : ModSystem{
	
       // public static Vector2 savedLocation;
        //public static string playerName;
        public static IDictionary<string, Vector2> wpDict = new Dictionary<string, Vector2>();
        public override void OnWorldLoad(){
	wpDict.Clear();
        } 
	
        public override void SaveWorldData(TagCompound tag)
        {
      	tag.Add("names", wpDict.Keys.ToList());
	tag.Add("locations", wpDict.Values.ToList());
        }
        public override void LoadWorldData(TagCompound tag)
        {
	IList<string> names = tag.GetList<string>("names");
	IList<Vector2> locations = tag.GetList<Vector2>("locations");
	wpDict.Clear();
	for(int i = 0;i<names.Count;i++){
	wpDict.Add(names[i], locations[i]);
	}
        }
	
        
    
    }
    public override void SaveWorldData(TagCompound tag)
    {
        tag["savedLocation"] = savedLocation;
    }
    public override void LoadWorldData(TagCompound tag)
    {
        savedLocation = tag.Get<Vector2>("savedLocation");
    }
}
public class WaypointMirror : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Portable Pylon");
        Tooltip.SetDefault("Teleports you to the last saved location\nRight click to set a waypoint at your current location\nMaximum of 1 saved location");
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
        Item.UseSound = SoundID.Item;
   }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {
        if (player.altFunctionUse == 2 && player.itemTime == Item.useTime / 2)
        {
		WaypointSystem.wpDict[player.name]= player.Center + new Vector2(0, -15);
            
            Main.NewText("Set waypoint to current location.");
        }
        else{
            if (player.itemTime == 0)
            {
                player.itemTime = Item.useTime;
            }
            else if (player.itemTime == Item.useTime / 2)
            {
                
                if (WaypointSystem.wpDict[player.name] != Vector2.Zero)
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
                    player.Teleport(WaypointSystem.wpDict[player.name]);
                    //NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, WaypointSystem.savedLocation.X, WaypointSystem.savedLocation.Y, 0);
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
    public override void NetSend(BinaryWriter writer){
	writer.Write(WaypointSystem.wpDict.Count);
	foreach (KeyValuePair<string,Vector2> pair in WaypointSystem.wpDict)
	{
	writer.Write(pair.Key);
 	 writer.WriteVector2(pair.Value);
	}
	}
	public override void NetReceive(BinaryReader reader){
	int count = reader.ReadInt32();
	for(int i = 0; i<count;i++){
		WaypointSystem.wpDict.Add(reader.ReadString(), reader.ReadPackedVector2()); 
	}		
	}	

}
}
