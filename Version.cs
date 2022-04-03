namespace AvalonTesting;

public class Version
{
    private readonly int rewrite;
    private readonly int major;
    private readonly int minor;
    private readonly int fix;
    private readonly bool isDev;

    public Version(int rewrite, int major, int minor, int fix, bool isDev = false)
    {
        this.rewrite = rewrite;
        this.major = major;
        this.minor = minor;
        this.fix = fix;
        this.isDev = isDev;
    }
    
    public Version(string version)
    {
        string[] components = version.Split('.');
        rewrite = int.Parse(components[0]);
        major = int.Parse(components[1]);
        minor = int.Parse(components[2]);
        fix = int.Parse(components[3]);
        if (components.Length > 4)
        {
            isDev = components[4] == "dev";
        }
    }

    public override string ToString()
    {
        return $"{rewrite}.{major}.{minor}.{fix}{(isDev ? ".dev" : "")}";
    }

    public static bool operator <(Version v1, Version v2)
    {
        if (v2 == null)
        {
            return false;
        }

        if (v1 == null)
        {
            return true;
        }

        if (v1.rewrite != v2.rewrite)
        {
            return v1.rewrite < v2.rewrite;
        }

        if (v1.major != v2.major)
        {
            return v1.major < v2.major;
        }

        if (v1.minor != v2.minor)
        {
            return v1.minor < v2.minor;
        }

        if (v1.fix != v2.fix)
        {
            return v1.fix < v2.fix;
        }

        return v1.isDev;
    }

    public static bool operator >(Version v2, Version v1)
    {
        return v1 < v2;
    }
}
