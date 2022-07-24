using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles
{
    public class TropicsTreeLeaf : ModGore
    {
        public override string Texture => "Avalon/Tiles/TropicsTree_Leaf";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }
}
