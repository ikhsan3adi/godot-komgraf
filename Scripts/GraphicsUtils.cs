namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public static class GraphicsUtils
{
    public enum DrawStyle
    {
        DotDot,
        DotStripDot,
        StripStrip,
        CircleDot,          // Dotted circle
        CircleStrip,        // Circle with a continuous strip
        CircleDotStrip,     // Circle with a dotted strip
        EllipseDot,         // Dotted ellipse
        EllipseStrip,       // Ellipse with a continuous strip
        EllipseDotStrip,     // Ellipse with a dotted strip
        DotDash
    }

    public static void PutPixel(Node2D targetNode, float x, float y, Godot.Color? color = null)
    {
        Godot.Color actualColor = color ?? Godot.Colors.White;
        Godot.Vector2[] points = new Godot.Vector2[] { new Godot.Vector2(Mathf.Round(x), Mathf.Round(y)) };
        Godot.Vector2[] uvs = new Godot.Vector2[]
        {
            Vector2.Zero, Vector2.Down, Vector2.One, Vector2.Right
        };

        targetNode.DrawPrimitive(points, new Godot.Color[] { actualColor }, uvs);
    }

    public static void PutPixelAll(Node2D targetNode, List<Vector2> dot, DrawStyle style = DrawStyle.DotDot, Godot.Color? color = null, int stripLength = 3, int gap = 0)
    {
        for (int i = 0; i < dot.Count; i++)
        {
            float x = dot[i][0];
            float y = dot[i][1];

            switch (style)
            {
                case DrawStyle.DotDot:
                    PutPixel(targetNode, x, y, color);
                    break;

                case DrawStyle.DotStripDot:
                case DrawStyle.StripStrip:
                    if (i % (gap + 1) == 0)
                    {
                        PutPixel(targetNode, x, y, color); // Central dot

                        // Inline the strip drawing logic
                        if (style == DrawStyle.DotStripDot || style == DrawStyle.StripStrip)
                        {
                            int halfLength = stripLength / 2;
                            for (int j = -halfLength; j <= halfLength; j++)
                            {
                                PutPixel(targetNode, x + j, y, color);
                            }
                        }
                    }
                    break;

                case DrawStyle.CircleDot:
                case DrawStyle.CircleStrip:
                case DrawStyle.CircleDotStrip:
                    // Apply gap logic based on the original index (i) divided by 8 (for 8-way symmetry)
                    if ((i / 8) % (gap + 1) == 0)
                    {
                        PutPixel(targetNode, x, y, color);

                        if (style == DrawStyle.CircleStrip)
                        {
                            // No need for additional drawing for continuous strip
                        }
                        else if (style == DrawStyle.CircleDotStrip)
                        {
                            int halfLength = stripLength / 2;
                            for (int j = -halfLength; j <= halfLength; j += gap + 1)
                            {
                                PutPixel(targetNode, x + j, y, color);
                            }
                        }
                    }
                    break;

                case DrawStyle.EllipseDot:
                case DrawStyle.EllipseStrip:
                case DrawStyle.EllipseDotStrip:
                    // Apply gap logic based on the original index (i) divided by 4 (for 4-way symmetry)
                    if ((i / 4) % (gap + 1) == 0)
                    {
                        PutPixel(targetNode, x, y, color);

                        if (style == DrawStyle.EllipseStrip)
                        {
                            // No need for additional drawing for continuous strip
                        }
                        else if (style == DrawStyle.EllipseDotStrip)
                        {
                            int halfLength = stripLength / 2;
                            for (int j = -halfLength; j <= halfLength; j += gap + 1)
                            {
                                PutPixel(targetNode, x + j, y, color);
                            }
                        }
                    }
                    break;
                case DrawStyle.DotDash: // custom

                    // titik
                    PutPixel(targetNode, dot[i].X, dot[i].Y, color);

                    i += gap + 1;              // gap setelah titik
                    if (i >= dot.Count) break; // mencapai ujung garis

                    int dashEnd = Math.Min(i + stripLength, dot.Count);

                    // Gambar pixel berikutnya dari daftar dot sebagai sebuah garis.
                    for (int j = i; j < dashEnd; j++)
                        PutPixel(targetNode, dot[j].X, dot[j].Y, color);

                    // skip titik di gap
                    i += stripLength + gap;
                    break;
            }
        }
    }
}