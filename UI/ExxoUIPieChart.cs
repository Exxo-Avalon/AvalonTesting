using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace AvalonTesting.UI;

public class ExxoUIPieChart : ExxoUIElement
{
    private const int MaxData = 12;

    private readonly Asset<Effect> pieChartEffect =
        AvalonTesting.Mod.Assets.Request<Effect>("Effects/PieChart", AssetRequestMode.ImmediateLoad);

    private readonly List<PieData> pieDataList = new();
    public override bool IsDynamicallySized => false;

    private Vector4[] PieShaderData
    {
        get
        {
            var data = new Vector4[MaxData];
            int count = pieDataList.Count < MaxData ? pieDataList.Count : MaxData;
            float percentCount = 0;

            for (int i = 0; i < count; i++)
            {
                data[i] = pieDataList[i].GetThresholdData(ref percentCount);
            }

            for (int i = count; i < MaxData; i++)
            {
                data[i] = new Vector4(1, 1, 1, 10);
            }

            return data;
        }
    }

    public override bool ContainsPoint(Vector2 point) =>
        Vector2.Distance(GetDimensions().Center(), point) <= GetDimensions().Width / 2;

    public void RegisterData(PieData pieData) => pieDataList.Add(pieData);

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

    public class PieData
    {
        private readonly Func<float> percentageProvider;

        public PieData(Color color, Func<float> percentageProvider)
        {
            Color = color;
            this.percentageProvider = percentageProvider;
        }

        public Color Color { get; set; }

        public Vector4 GetThresholdData(ref float count)
        {
            count += percentageProvider.Invoke();
            return new Vector4(Color.ToVector3(), count * MathHelper.TwoPi);
        }
    }
}
