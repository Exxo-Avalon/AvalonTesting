using System.Collections.Generic;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

public class ExxoSummonPlayer : ModPlayer
{
    public LinkedList<int> DaggerSummons { get; } = new();
    public LinkedList<int> StingerProbes { get; } = new();
    public override bool CloneNewInstances => false;

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

        return DaggerSummons.Find(index);
    }

    public LinkedListNode<int> HandleStingerProbe()
    {
        return StingerProbes.AddLast(StingerProbes.Count);
    }

    public void RemoveStingerProbe(LinkedListNode<int> linkedListNode)
    {
        LinkedListNode<int> nextNode = linkedListNode.Next;
        while (nextNode != null)
        {
            nextNode.Value--;
            nextNode = nextNode.Next;
        }

        StingerProbes.Remove(linkedListNode);
    }

    public LinkedListNode<int> ObtainExistingStingerProbe(int index)
    {
        int diff = index + 1 - StingerProbes.Count;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                StingerProbes.AddLast(StingerProbes.Count);
            }

            return StingerProbes.Last;
        }

        return StingerProbes.Find(index);
    }
}
