const float PI = 3.14159265359;

float4 Thresholds[16];

float4 PS(in float4 uv: TEXCOORD0) : COLOR0
{
    float2 origPoint = uv.xy - float2(0.5, 0.5);

    float rotation = atan2(origPoint.y, origPoint.x);
    float4 color = float4(Thresholds[0].xyz, 1);

    for (int i = 0; i < 15; ++i) {
        color = (rotation > Thresholds[i].w) ? float4(Thresholds[i + 1].xyz, 1) : color;
    }

    return color * (distance(float2(0.5, 0.5), uv.xy) <= 0.5);
}

technique Default
{
    pass Pass0
    {
        PixelShader = compile ps_2_0 PS();
    }
}
