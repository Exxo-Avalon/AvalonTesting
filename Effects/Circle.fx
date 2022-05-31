float4 Color;

float4 PS(in float4 uv: TEXCOORD0) : COLOR0
{
    return Color * (distance(float2(0.5, 0.5), uv.xy) <= 0.5);
}

technique Default
{
    pass Pass0
    {
        PixelShader = compile ps_2_0 PS();
    }
}
