using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.UI;

namespace AvalonTesting.UI;

public class ExxoUIPieChart : ExxoUIElement
{
    private const int MaxData = 12;

    private readonly PieCacheData[] cachedPieData = new PieCacheData[MaxData];

    private readonly Asset<Effect> pieChartEffect =
        AvalonTesting.Mod.Assets.Request<Effect>("Effects/PieChart", AssetRequestMode.ImmediateLoad);

    private readonly List<PieData> pieDataList = new();
    public override bool IsDynamicallySized => false;
    public PieData CurrentHoverPie { get; set; }

    private Vector4[] PieShaderData
    {
        get
        {
            var data = new Vector4[MaxData];
            for (int i = 0; i < MaxData; i++)
            {
                data[i] = cachedPieData[i].ShaderData;
            }

            return data;
        }
    }

    public override bool ContainsPoint(Vector2 point) =>
        Vector2.Distance(GetDimensions().Center(), point) <= GetDimensions().Width / 2;

    public void RegisterData(PieData pieData)
    {
        pieDataList.Add(pieData);
        BuildCache();
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        spriteBatch.End();
        pieChartEffect.Value.CurrentTechnique = pieChartEffect.Value.Techniques["Default"];

        using var whiteRectangle = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        whiteRectangle.SetData(new[] { Color.White });

        pieChartEffect.Value.Parameters["Thresholds"].SetValue(PieShaderData);

        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
            DepthStencilState.None,
            RasterizerState.CullNone, pieChartEffect.Value, Main.UIScaleMatrix);

        spriteBatch.Draw(
            whiteRectangle,
            GetDimensions().ToRectangle(),
            Color.White);
        spriteBatch.End();
        BeginDefaultSpriteBatch(spriteBatch);
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        if (IsMouseHovering)
        {
            Vector2 point =
                (UserInterface.ActiveInstance.MousePosition - GetDimensions().Center()).SafeNormalize(Vector2.Zero);
            double rotation = Math.Atan2(point.Y, point.X);

            CurrentHoverPie = cachedPieData[0].PieData;

            for (int i = 0; i < MaxData - 1; ++i)
            {
                if (rotation > cachedPieData[i].Threshold)
                {
                    CurrentHoverPie = cachedPieData[i + 1].PieData;
                }
            }

            Tooltip = CurrentHoverPie.Label;
        }
        else
        {
            Tooltip = string.Empty;
        }
    }

    private void BuildCache()
    {
        int count = pieDataList.Count < MaxData ? pieDataList.Count : MaxData;
        float percentCount = 0;

        for (int i = 0; i < count; i++)
        {
            cachedPieData[i] = new PieCacheData(pieDataList[i].GetThresholdData(ref percentCount), pieDataList[i]);
        }

        float count1 = percentCount;
        var otherData = new PieData("Other", Color.Gray, () => 1 - count1);
        var otherCacheData = new PieCacheData(otherData.GetThresholdData(ref percentCount), otherData);

        for (int i = count; i < MaxData; i++)
        {
            cachedPieData[i] = otherCacheData;
        }
    }

    private readonly struct PieCacheData
    {
        public PieCacheData(Vector4 shaderData, PieData pieData)
        {
            ShaderData = shaderData;
            PieData = pieData;
        }

        public readonly Vector4 ShaderData;
        public readonly PieData PieData;
        public float Threshold => ShaderData.W;
    }

    public class PieData
    {
        public readonly string Label;
        private readonly Func<float> percentageProvider;

        public PieData(string label, Color color, Func<float> percentageProvider)
        {
            Label = label;
            Color = color;
            this.percentageProvider = percentageProvider;
        }

        public Color Color { get; set; }

        public Vector4 GetThresholdData(ref float count)
        {
            count += percentageProvider.Invoke();
            return new Vector4(Color.ToVector3(), (count * MathHelper.TwoPi) - MathHelper.Pi);
        }
    }
}
