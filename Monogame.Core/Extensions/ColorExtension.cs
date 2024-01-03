using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Core.Extensions;

public static class ColorExtension
{
    public static Microsoft.Xna.Framework.Color FromHex(this Microsoft.Xna.Framework.Color color, string hexcode)
    {
        string pattern = @"#([0-9a-fA-F]{3}|[0-9a-fA-F]{6})\b";
        if (!System.Text.RegularExpressions.Regex.IsMatch(hexcode, pattern))
            throw new ArgumentException("Invalid hexcode");

        hexcode = hexcode.TrimStart('#');
        int hexValue = int.Parse(hexcode, System.Globalization.NumberStyles.HexNumber);
        return new Microsoft.Xna.Framework.Color(
            (hexValue >> 16) & 0xFF, 
            (hexValue >> 8) & 0xFF, 
            hexValue & 0xFF);
    }
}
