using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Players;

public class ExxoSummonPlayer : ModPlayer
{
    private readonly List<bool> daggerSummons = new();
    public int DaggerSummonCount => daggerSummons.Count(val => val);

    public int HandleDaggerSummon()
    {
        int index = daggerSummons.FindIndex(val => !val);
        if (index == -1)
        {
            daggerSummons.Add(true);
            return daggerSummons.Count - 1;
        }

        daggerSummons[index] = true;
        return index;
    }

    public void RemoveDaggerSummon(int index)
    {
        if (index == daggerSummons.Count - 1)
        {
            daggerSummons.RemoveAt(index);
        }
        else
        {
            daggerSummons[index] = false;
        }
    }

    public void CheckDaggerSummon(int index)
    {
        int diff = index + 1 - daggerSummons.Count;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                daggerSummons.Add(true);
            }
        }
    }
}
