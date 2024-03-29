using Avalon.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.ResourceSets;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.UI;

public struct PlayerStaminaStatsSnapshot
{
    public int Stamina;
    public int StaminaMax;
    public int StaminaMax2;
    public float StaminaPerSegment;
    public int StaminaCount;
    public int StaminaCountMT300;

    public PlayerStaminaStatsSnapshot(Player p)
    {
        Stamina = p.GetModPlayer<ExxoStaminaPlayer>().StatStam;
        StaminaMax = p.GetModPlayer<ExxoStaminaPlayer>().StatStamMax;
        StaminaMax2 = p.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2;
        float stamPerBolt = 30;
        int stamOver30 = StaminaMax2 / 30;
        int stamBarsMoreThan5 = (StaminaMax2 - 150) / 30;
        if (stamBarsMoreThan5 < 0)
        {
            stamBarsMoreThan5 = 0;
        }
        if (stamBarsMoreThan5 > 0)
        {
            stamOver30 = (StaminaMax2 - 150) / 30;
            stamPerBolt = StaminaMax2 / 5 - 30;
        }

        int stamBarsMoreThan10 = (StaminaMax2 - 300) / 30;
        if (stamBarsMoreThan10 < 0)
        {
            stamBarsMoreThan10 = 0;
        }
        if (stamBarsMoreThan10 > 0)
        {
            stamOver30 = (StaminaMax2 - 300) / 30;
            stamPerBolt = StaminaMax2 / 5 - 30;
        }
        int num4 = StaminaMax2 - 150;
        if (StaminaMax2 < 150)
        {
            num4 = 0;
        }
        if (stamBarsMoreThan10 > 0)
        {
            num4 = StaminaMax2 - 300;
        }
        stamPerBolt += num4 / stamOver30;
        StaminaCount = stamBarsMoreThan5;
        StaminaCountMT300 = stamBarsMoreThan10;
        StaminaPerSegment = stamPerBolt;
    }
}

class StaminaBar : UIState
{
    private const int staminaPerBar = 30;
    private const int maxStaminaBars = 5;
    private const int barSpacing = 26;
    private const string labelText = "Stamina";
    private float textYOffset;
    private Vector2 labelDimensions;
    private Texture2D staminaTexture1;
    private Texture2D staminaTexture2;
    private Texture2D staminaTexture3;


    // bar style fields
    private int stamSegmentsBarsCount;
    private int stamSegmentsBarsCount2;
    private int stamSegmentsBarsCount3;
    private float stamPercent;
    private bool stamHovered;
    private int maxSegmentCount;
    private Asset<Texture2D> stamFillGreen;
    private Asset<Texture2D> stamFillPurple;
    private Asset<Texture2D> stamFillOrange;
    private Asset<Texture2D> panelLeft;
    private Asset<Texture2D> panelMiddleStam;
    private Asset<Texture2D> panelRightStam;
    // end bar style fields

    // fancy style fields
    private bool fancyStamHovered;
    private int fancyStamCount;
    private Asset<Texture2D> staminaTop;
    private Asset<Texture2D> staminaBottom;
    private Asset<Texture2D> staminaMiddle;
    private Asset<Texture2D> staminaSingle;
    private Asset<Texture2D> staminaFillGreenFancy;
    private Asset<Texture2D> staminaFillPurpleFancy;
    private Asset<Texture2D> staminaFillOrangeFancy;
    private int lastStaminaFillingIndex;
    private float currentPlayerStamina;
    private float staminaPerBolt;
    private int playerStamCountOver150;
    private int playerStamCountOver300;
    // end fancy style fields

    public StaminaBar()
    {
        // FOR SOME REASON THIS DOES NOT LOAD THE TEXTURES
        staminaTexture1 = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Stamina", AssetRequestMode.ImmediateLoad).Value;
        staminaTexture2 = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Stamina2", AssetRequestMode.ImmediateLoad).Value;
        staminaTexture3 = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Stamina3", AssetRequestMode.ImmediateLoad).Value;

        int manaStarSpacing = 28;
        textYOffset = manaStarSpacing * 11 + 30;

        labelDimensions = FontAssets.MouseText.Value.MeasureString(labelText);

        Top.Set(textYOffset + labelDimensions.Y, 0);
        Width.Set(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Stamina").Value.Width, 0);
    }

    #region bar style
    private void PrepareFieldsBars()
    {
        PlayerStaminaStatsSnapshot snap = new PlayerStaminaStatsSnapshot(Main.LocalPlayer);
        ExxoStaminaPlayer p = Main.LocalPlayer.GetModPlayer<ExxoStaminaPlayer>();
        staminaPerBolt = snap.StaminaPerSegment;
        stamSegmentsBarsCount = (int)(snap.StaminaMax2 / staminaPerBolt);
        if (stamSegmentsBarsCount > maxStaminaBars)
        {
            stamSegmentsBarsCount = maxStaminaBars;
        }
        maxSegmentCount = 5;
        stamSegmentsBarsCount2 = snap.StaminaCount;
        stamSegmentsBarsCount3 = snap.StaminaCountMT300;

        stamPercent = ((float)p.StatStam / p.StatStamMax2);
    }
    private void StaminaPanelDrawerBars(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sourceRect = null;
        offset = Vector2.Zero;
        sprite = panelLeft;
        drawScale = 1f;
        if (elementIndex == lastElementIndex)
        {
            sprite = panelRightStam;
            offset = new Vector2(-19f, -6f);
        }
        else if (elementIndex != firstElementIndex)
        {
            sprite = panelMiddleStam;
        }
    }
    private void StaminaFillingDrawerBars(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sprite = stamFillGreen;
        if (elementIndex >= stamSegmentsBarsCount - stamSegmentsBarsCount2)
        {
            sprite = stamFillPurple;
        }
        if (elementIndex >= stamSegmentsBarsCount - stamSegmentsBarsCount3)
        {
            sprite = stamFillOrange;
        }
        FillBarByValues(elementIndex, sprite, stamSegmentsBarsCount, stamPercent, out offset, out drawScale, out sourceRect);
    }
    private static void FillBarByValues(int elementIndex, Asset<Texture2D> sprite, int segmentsCount, float fillPercent, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sourceRect = null;
        offset = Vector2.Zero;
        float num = 1f;
        float num2 = 1f / segmentsCount;
        float t = 1f - fillPercent;
        float lerpValue = Utils.GetLerpValue(num2 * elementIndex, num2 * (elementIndex + 1), t, clamped: true);
        num = 1f - lerpValue;
        drawScale = 1f;
        Rectangle value = sprite.Frame();
        int num3 = (int)(value.Width * (1f - num));
        offset.X += num3;
        value.X += num3;
        value.Width -= num3;
        sourceRect = value;
    }
    #endregion bar style

    #region fancy style
    private void PrepareFieldsFancy()
    {
        PlayerStaminaStatsSnapshot snapshot = new PlayerStaminaStatsSnapshot(Main.LocalPlayer);
        playerStamCountOver150 = snapshot.StaminaCount;
        playerStamCountOver300 = snapshot.StaminaCountMT300;
        currentPlayerStamina = snapshot.Stamina;
        staminaPerBolt = snapshot.StaminaPerSegment;
        fancyStamCount = (int)(snapshot.StaminaMax2 / staminaPerBolt);
        if (fancyStamCount > maxStaminaBars)
        {
            fancyStamCount = maxStaminaBars;
        }
        lastStaminaFillingIndex = (int)(currentPlayerStamina / staminaPerBolt);
    }
    private void DrawStaminaBarFancy(SpriteBatch spriteBatch)
    {
        Vector2 vector = new Vector2(Main.screenWidth - 40, 28 * 9 + 10);
        _ = fancyStamCount;
        bool isHovered = false;
        ResourceDrawSettings resourceDrawSettings = default;
        resourceDrawSettings.ElementCount = fancyStamCount;
        resourceDrawSettings.ElementIndexOffset = 0;
        resourceDrawSettings.TopLeftAnchor = vector;
        resourceDrawSettings.GetTextureMethod = StaminaPanelDrawerFancy;
        resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
        resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
        resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
        resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
        resourceDrawSettings.Draw(spriteBatch, ref isHovered);
        resourceDrawSettings = default;
        resourceDrawSettings.ElementCount = fancyStamCount;
        resourceDrawSettings.ElementIndexOffset = 0;
        resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 16f);
        resourceDrawSettings.GetTextureMethod = StaminaFillingDrawerFancy;
        resourceDrawSettings.OffsetPerDraw = Vector2.UnitY * 2f;
        resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
        resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
        resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
        resourceDrawSettings.Draw(spriteBatch, ref isHovered);
        fancyStamHovered = isHovered;
        if (fancyStamHovered)
        {
            string mouseText = string.Format("{0}/{1}", Main.LocalPlayer.GetModPlayer<ExxoStaminaPlayer>().StatStam, Main.LocalPlayer.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2);
            Main.instance.MouseText(mouseText);
        }
    }
    private void StaminaFillingDrawerFancy(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sourceRect = null;
        offset = new Vector2(-2, -2);
        if (elementIndex == firstElementIndex)
        {
            offset = new(-2f, 0f);
        }
        //sprite = staminaFillGreenFancy;
        //sprite = staminaBottom;
        sprite = staminaFillGreenFancy;
        if (elementIndex < playerStamCountOver300)
        {
            sprite = staminaFillOrangeFancy;
        }
        else if (elementIndex < playerStamCountOver150)
        {
            sprite = staminaFillPurpleFancy;
        }
        float num = (drawScale = Utils.GetLerpValue(staminaPerBolt * elementIndex, staminaPerBolt * (elementIndex + 1), currentPlayerStamina, clamped: true));
        if (elementIndex == lastStaminaFillingIndex && num > 0f)
        {
            drawScale += Main.cursorScale - 1f;
        }
    }
    private void StaminaPanelDrawerFancy(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
    {
        sourceRect = null;
        offset = Vector2.Zero;
        sprite = staminaTop;
        drawScale = 1f;
        if (elementIndex == lastElementIndex && elementIndex == firstElementIndex)
        {
            sprite = staminaSingle;
        }
        else if (elementIndex == lastElementIndex)
        {
            sprite = staminaBottom;
            offset = new Vector2(0f, 0f);
        }
        else if (elementIndex != firstElementIndex)
        {
            sprite = staminaMiddle;
        }
    }
    #endregion fancy style
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (Main.player[Main.myPlayer].ghost)
        {
            return;
        }
        if (Main.ResourceSetsManager.ActiveSetKeyName == "HorizontalBars")
        {
            panelLeft = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Panel_Left");
            stamFillGreen = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFill_Green");
            stamFillPurple = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFill_Purple");
            stamFillOrange = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFill_Orange");
            panelMiddleStam = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaPanel_Middle");
            panelRightStam = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaPanel_Right");

            PrepareFieldsBars();
            SpriteBatch sb = Main.spriteBatch;
            int xpos = 16;
            int ypos = 66;
            int finalXPos = Main.screenWidth - 135 - 22 + xpos;
            Vector2 vector = new Vector2(finalXPos, ypos);
            vector.X += (maxSegmentCount - stamSegmentsBarsCount) * panelMiddleStam.Value.Width;
            bool isHovered = false;

            ResourceDrawSettings resourceDrawSettings = default;
            resourceDrawSettings.ElementCount = stamSegmentsBarsCount + 2;
            resourceDrawSettings.ElementIndexOffset = 0;
            resourceDrawSettings.TopLeftAnchor = vector;
            resourceDrawSettings.GetTextureMethod = StaminaPanelDrawerBars;
            resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
            resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
            resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
            resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
            resourceDrawSettings.Draw(spriteBatch, ref isHovered);
            resourceDrawSettings = default;
            resourceDrawSettings.ElementCount = stamSegmentsBarsCount;
            resourceDrawSettings.ElementIndexOffset = 0;
            resourceDrawSettings.TopLeftAnchor = vector + new Vector2(6f, 6f);
            resourceDrawSettings.GetTextureMethod = StaminaFillingDrawerBars;
            resourceDrawSettings.OffsetPerDraw = new Vector2(stamFillGreen.Width(), 0f);
            resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.Zero;
            resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
            resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
            resourceDrawSettings.Draw(spriteBatch, ref isHovered);
            stamHovered = isHovered;

            if (stamHovered)
            {
                string mouseText = string.Format("{0}/{1}", Main.LocalPlayer.GetModPlayer<ExxoStaminaPlayer>().StatStam, Main.LocalPlayer.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2);
                Main.instance.MouseText(mouseText);
            }

            Left.Set(vector.X, 0);
            Height.Set(panelMiddleStam.Value.Height, 0);
            Width.Set(26 + stamFillGreen.Value.Width * stamSegmentsBarsCount + 6, 0);
        }
        else if (Main.ResourceSetsManager.ActiveSetKeyName == "New")
        {
            staminaTop = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFancy_Top");
            staminaMiddle = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFancy_Middle");
            staminaBottom = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFancy_Bottom");
            staminaSingle = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFancy_Single");
            staminaFillGreenFancy = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFancy_FillGreen");
            staminaFillPurpleFancy = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFancy_FillPurple");
            staminaFillOrangeFancy = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/StaminaFancy_FillOrange");
            PrepareFieldsFancy();
            DrawStaminaBarFancy(spriteBatch);

            Left.Set(Main.screenWidth - 25 - (ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Stamina").Value.Width / 2f), 0);
            Height.Set(fancyStamCount * staminaTop.Value.Height, 0);
            Width.Set(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Stamina").Value.Width, 0);
        }
        else if (Main.ResourceSetsManager.ActiveSetKeyName == "Default")
        {
            CalculatedStyle dimensions = GetDimensions();
            // Draw labelText above stamina bar
            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, labelText, new Vector2((Main.screenWidth - labelDimensions.X + 15), textYOffset), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default(Vector2), 0.7f, SpriteEffects.None, 0f);

            var player = Main.LocalPlayer.GetModPlayer<ExxoStaminaPlayer>();

            int stamBars = player.StatStamMax2 / staminaPerBar;
            if (stamBars > maxStaminaBars)
            {
                stamBars = maxStaminaBars;
            }

            int staminaThreshold = staminaPerBar * maxStaminaBars;
            int amountBars = player.StatStamMax2 / staminaPerBar;
            int amountHighestTierBars = ((amountBars - 1) % maxStaminaBars) + 1;
            int highestStatLevel = ((player.StatStamMax2 - 1) / staminaThreshold) + 1;

            int staminaCounter = 0;
            bool activeFound = false;

            for (var i = 1; i < stamBars + 1; i++)
            {
                int intensity;
                float scale = 1f;
                bool activeBar = false;

                int statLevel = highestStatLevel;
                if (i > amountHighestTierBars)
                {
                    statLevel--;
                }

                if (!activeFound && staminaCounter + statLevel * staminaPerBar >= player.StatStam)
                {
                    float barProgress = (player.StatStam - staminaCounter) / (float)(statLevel * staminaPerBar);
                    intensity = (int)(30 + 225f * barProgress);
                    if (intensity < 30)
                    {
                        intensity = 30;
                    }
                    scale = barProgress / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }

                    activeBar = true;
                    activeFound = true;
                }
                else if (!activeFound)
                {
                    intensity = 255;
                }
                else
                {
                    intensity = 30;
                    scale = 0.75f;
                }

                staminaCounter += statLevel * staminaPerBar;

                // Bobs the scale of the active bar with the cursor bobbing
                if (activeBar)
                {
                    scale += Main.cursorScale - 1f;
                }

                Texture2D texture;
                switch (statLevel)
                {
                    case 1:
                        texture = staminaTexture1;
                        break;

                    case 2:
                        texture = staminaTexture2;
                        break;

                    case 3:
                        texture = staminaTexture3;
                        break;

                    default:
                        texture = staminaTexture1;
                        break;
                }

                int alpha = (int)(intensity * 0.9f);
                int scaleOffsetX = (int)((texture.Width - texture.Width * scale) / 2f);
                var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
                var position = new Vector2(dimensions.X + scaleOffsetX + (texture.Width / 2f), dimensions.Y + (barSpacing * (i - 1)) + (texture.Height / 2f));

                spriteBatch.Draw(texture, position, null, new Color(intensity, intensity, intensity, alpha), 0f, origin, scale, SpriteEffects.None, 0f);
            }

            if (IsMouseHovering)
            {
                string mouseText = string.Format("{0}/{1}", player.StatStam, player.StatStamMax2);
                Main.instance.MouseText(mouseText);
            }

            Left.Set(Main.screenWidth - 25 - (ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Stamina").Value.Width / 2f), 0);
            Height.Set(barSpacing * stamBars, 0);
            Width.Set(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/Stamina").Value.Width, 0);
        }
        base.DrawSelf(spriteBatch);
    }
}
