using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AvalonTesting;
public class AvalonTestingWorld : ModSystem
{
    public override void PostUpdateEverything()
    {
        Items.Armor.SpectrumHelmet.StaticUpdate();
    }
}
