namespace ExxoAvalonOrigins.Buffs;

public class TroxiniumDagger : BaseDagger<Projectiles.Summon.TroxiniumDagger>
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Troxinium Dagger");
        Description.SetDefault("The dagger will fight for you");
        base.SetStaticDefaults();
    }
}
