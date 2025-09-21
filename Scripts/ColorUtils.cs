namespace Godot;

using Godot;

public static class ColorUtils
{
    public static Color ColorStorage(int colorIndex)
    {
        switch (colorIndex)
        {
            case 1:
                return Colors.Red; // Use Colors.Red
            case 2:
                return Colors.Green; // Use Colors.Green
            case 3:
                return Colors.Blue; // Use Colors.Blue
            case 4:
                return Colors.Yellow; // Use Colors.Yellow
            case 5:
                return Colors.Magenta; // Use Colors.Magenta
            case 6:
                return Colors.Cyan; // Use Colors.Cyan
            case 7:
                return new Color("#ff7f00"); // Orange
            case 8:
                return new Color("#ff007f"); // Rose / Bright Pink
            case 9:
                return new Color("#7f00ff"); // Violet
            case 10:
                return new Color("#007fff"); // Azure
            case 11:
                return new Color("#00ff7f"); // Spring Green
            case 12:
                return new Color("#7fff00"); // Chartreuse
            case 13:
                return new Color("#a15f10"); // Brown
            case 14:
                return new Color("#eaa8a"); // Vanilla  (Corrected hex)
            default:
                return Colors.White; // Use Colors.White
        }
    }

        public static Color LerpColor(Color a, Color b, float t)
    {
        return a.Lerp(b, t);
    }

    public static Color BrightenColor(Color color, float amount)
    {
        // Clamp amount to be between 0 and 1
        amount = Mathf.Clamp(amount, 0f, 1f);

        // Increase the values of R, G, and B components by the amount
        return new Color(color.R + (1f - color.R) * amount,
                         color.G + (1f - color.G) * amount,
                         color.B + (1f - color.B) * amount,
                         color.A);
    }

    public static Color DarkenColor(Color color, float amount)
    {
        // Clamp amount to be between 0 and 1
        amount = Mathf.Clamp(amount, 0f, 1f);

        // Decrease the values of R, G, and B components by the amount
        return new Color(color.R * (1f - amount),
                         color.G * (1f - amount),
                         color.B * (1f - amount),
                         color.A);
    }

    public static Color InvertColor(Color color)
    {
        return new Color(1f - color.R, 1f - color.G, 1f - color.B, color.A);
    }

    public static Color GetComplementaryColor(Color color)
    {
        // Convert RGB to HSV
        float hue, saturation, value;
        color.ToHsv(out hue, out saturation, out value);

        // Shift hue by 180 degrees (or 0.5 in the normalized HSV representation)
        hue = (hue + 0.5f) % 1f;

        // Convert back to RGB
        return Color.FromHsv(hue, saturation, value, color.A);
    }
}