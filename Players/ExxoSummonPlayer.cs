using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Avalon.Players;

public class ExxoSummonPlayer : ModPlayer
{
    public LinkedList<int> DaggerSummons { get; } = new();
    public LinkedList<int> StingerProbes { get; } = new();
    public LinkedList<int> Reflectors { get; } = new();
    public bool PrimeMinion;
    public bool HungryMinion;
    protected override bool CloneNewInstances => false;

    public LinkedListNode<int> HandleDaggerSummon() => DaggerSummons.AddLast(DaggerSummons.Count);


    public override void ResetEffects()
    {
        PrimeMinion = false;
        HungryMinion = false;
    }
    public void UpdatePrimeMinionStatus(EntitySource_ItemUse_WithAmmo source)
    {
        int firstMinion = ModContent.ProjectileType<Projectiles.Summon.PriminiCannon>();
        if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.PrimeArmsCounter>()] < 1)
        {
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.owner == Player.whoAmI && projectile.type != firstMinion)
                {
                    projectile.Kill();
                }
            }
        }
        if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.PriminiCannon>()] < 1)
        {
            Projectile.NewProjectile(source, Player.Center.X - 40f, Player.Center.Y - 40f, 0f, 0f, ModContent.ProjectileType<Projectiles.Summon.PriminiCannon>(), 50, 6.5f, Player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(source, Player.Center.X + 40f, Player.Center.Y - 40f, 0f, 0f, ModContent.ProjectileType<Projectiles.Summon.PriminiLaser>(), 50, 6.5f, Player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(source, Player.Center.X - 40f, Player.Center.Y + 40f, 0f, 0f, ModContent.ProjectileType<Projectiles.Summon.PriminiSaw>(), 50, 6.5f, Player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(source, Player.Center.X + 40f, Player.Center.Y + 40f, 0f, 0f, ModContent.ProjectileType<Projectiles.Summon.PriminiVice>(), 50, 6.5f, Player.whoAmI, 0f, 0f);
        }
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


    public LinkedListNode<int> HandleReflectorSummon() => Reflectors.AddLast(Reflectors.Count);

    public void RemoveReflectorSummon(LinkedListNode<int> linkedListNode)
    {
        LinkedListNode<int> nextNode = linkedListNode.Next;
        while (nextNode != null)
        {
            nextNode.Value--;
            nextNode = nextNode.Next;
        }

        Reflectors.Remove(linkedListNode);
    }

    public LinkedListNode<int> ObtainExistingReflectorSummon(int index)
    {
        int diff = index + 1 - Reflectors.Count;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                Reflectors.AddLast(Reflectors.Count);
            }

            return Reflectors.Last;
        }

        return Reflectors.Find(index);
    }

    public LinkedListNode<int> HandleStingerProbe() => StingerProbes.AddLast(StingerProbes.Count);

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
