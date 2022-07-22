using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles
{
    public class DarkMatterTreeLeaf : ModGore
    {
        public override string Texture => "AvalonTesting/Tiles/DarkMatterTree_Leaf";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }
}
