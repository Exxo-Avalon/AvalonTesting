using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

public class ExxoSummonPlayer : ModPlayer
{
    public LinkedList<int> DaggerSummons { get; private set; }
    public override bool CloneNewInstances => false;

    public override void Initialize()
    {
        DaggerSummons = new LinkedList<int>();
    }

    public LinkedListNode<int> HandleDaggerSummon()
    {
        return DaggerSummons.AddLast(DaggerSummons.Count);
    }

    public void RemoveDaggerSummon(LinkedListNode<int> linkedListNode)
    {
        LinkedListNode<int> nextNode = linkedListNode.Next;
        while (nextNode != null)
        {
            nextNode.Value--;
            nextNode = nextNode.Next;
        }

        DaggerSummons.Remove(linkedListNode);
    }

    public LinkedListNode<int> ObtainExistingDaggerSummon(int index)
    {
        int diff = index + 1 - DaggerSummons.Count;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                DaggerSummons.AddLast(DaggerSummons.Count);
            }

            return DaggerSummons.Last;
        }
        else
        {
            return DaggerSummons.Find(index);
        }
    }
}
