using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUINumberInputWithButtons : ExxoUIList
{
    public readonly ExxoUINumberInput NumberInput;
    private readonly ExxoUIList buttonColumn;
    private readonly ExxoUIImageButton decrementButton;
    private readonly ExxoUIImageButton incrementButton;

    public ExxoUINumberInputWithButtons(int amountNumbers = 3)
    {
        Direction = Direction.Horizontal;
        FitWidthToContent = true;
        FitHeightToContent = true;
        ContentVAlign = UIAlign.Center;

        NumberInput = new ExxoUINumberInput(amountNumbers);
        Append(NumberInput);

        buttonColumn = new ExxoUIList
        {
            FitWidthToContent = true, FitHeightToContent = true, Justification = Justification.Center,
        };

        incrementButton =
            new ExxoUIImageButton(
                Terraria.Main.Assets.Request<Texture2D>("Images/UI/Minimap/Default/MinimapButton_ZoomIn"));
        incrementButton.OnClick += delegate
        {
            NumberInput.Number++;
        };
        buttonColumn.Append(incrementButton);
        decrementButton =
            new ExxoUIImageButton(
                Terraria.Main.Assets.Request<Texture2D>("Images/UI/Minimap/Default/MinimapButton_ZoomOut"));
        decrementButton.OnClick += delegate
        {
            NumberInput.Number--;
        };
        buttonColumn.Append(decrementButton);
        Append(buttonColumn);
    }
}
