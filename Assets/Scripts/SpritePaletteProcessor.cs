using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SpritePaletteProcessor
{
    public static Color[] GetPalette(Texture2D texture)
    {
        HashSet<Color> unique = new HashSet<Color>();
        foreach (Color color in texture.GetPixels())
        {
            if (color.a > 0.1f) // Ignore transparent pixels
            {
                unique.Add(color);
            }
        }

        if (unique.Count != 3)
        {
            Debug.LogWarning($"Expected 3 colors, found {unique.Count}");
            return null;
        }

        Debug.Log($"Palette colors: {string.Join(", ", unique.Select(c => $"#{ColorUtility.ToHtmlStringRGB(c)}"))}");        
        return unique
            .OrderBy(c => c.r * 0.2126f + c.g * 0.7152f + c.b * 0.0722f)
            .ToArray();
            
    }
}