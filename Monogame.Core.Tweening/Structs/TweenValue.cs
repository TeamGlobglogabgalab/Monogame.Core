using System.Collections;

namespace Monogame.Core.Tweening.Structs;

public struct TweenValue : IEnumerable<decimal>
{
    public decimal[] Values;

    public TweenValue(TweenValue tweenValue)
    {
        Values = new decimal[tweenValue.Values.Length];
        for(int i=0 ; i<Values.Length ; i++)
            Values[i] = tweenValue.Values[i];
    }

    public TweenValue(decimal[] values)
    {
        Values = values;
    }

    public IEnumerator<decimal> GetEnumerator()
    {
        return Values.ToList().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #region Implicit Casts To TweenValue
    public static implicit operator TweenValue(short value) => new TweenValue(new decimal[1] { value });
    public static implicit operator TweenValue(int value) => new TweenValue(new decimal[1] { value });
    public static implicit operator TweenValue(long value) => new TweenValue(new decimal[1] { value });
    public static implicit operator TweenValue(float value) => new TweenValue(new decimal[1] { Convert.ToDecimal(value) });
    public static implicit operator TweenValue(double value) => new TweenValue(new decimal[1] { Convert.ToDecimal(value) });
    public static implicit operator TweenValue(decimal value) => new TweenValue(new decimal[1] { value });

    public static implicit operator TweenValue(System.Drawing.Point value)
        => new TweenValue(new decimal[2] { value.X, value.Y });
    public static implicit operator TweenValue(System.Drawing.PointF value) 
        => new TweenValue(new decimal[2] { Convert.ToDecimal(value.X), Convert.ToDecimal(value.Y) });
    public static implicit operator TweenValue(Microsoft.Xna.Framework.Point value) 
        => new TweenValue(new decimal[2] { value.X, value.Y });
    public static implicit operator TweenValue(Microsoft.Xna.Framework.Vector2 value) 
        => new TweenValue(new decimal[2] { Convert.ToDecimal(value.X), Convert.ToDecimal(value.Y) });
    public static implicit operator TweenValue(Microsoft.Xna.Framework.Vector3 value)
        => new TweenValue(new decimal[3] { Convert.ToDecimal(value.X), Convert.ToDecimal(value.Y), Convert.ToDecimal(value.Z) });
    public static implicit operator TweenValue(Microsoft.Xna.Framework.Rectangle value)
        => new TweenValue(new decimal[4] { value.X, value.Y, value.Width, value.Height });
    public static implicit operator TweenValue(Microsoft.Xna.Framework.Color value)
        => new TweenValue(new decimal[4] { value.R, value.G, value.B, value.A });
    public static implicit operator TweenValue(System.Drawing.Color value)
        => new TweenValue(new decimal[4] { value.R, value.G, value.B, value.A });

    public static implicit operator TweenValue(short[] values)
    {
        var result = new decimal[values.Length];
        for(int i=0; i<values.Length; i++) result[i] = values[i];
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(int[] values)
    {
        var result = new decimal[values.Length];
        for(int i=0; i<values.Length; i++) result[i] = values[i];
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(long[] values)
    {
        var result = new decimal[values.Length];
        for(int i=0; i<values.Length; i++) result[i] = values[i];
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(float[] values)
    {
        var result = new decimal[values.Length];
        for(int i=0; i<values.Length; i++) result[i] = Convert.ToDecimal(values[i]);
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(double[] values)
    {
        var result = new decimal[values.Length];
        for(int i=0; i<values.Length; i++) result[i] = Convert.ToDecimal(values[i]);
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(decimal[] values) => new TweenValue(values);

    public static implicit operator TweenValue(List<short> values)
    {
        var result = new decimal[values.Count];
        for(int i=0; i<values.Count; i++) result[i] = values[i];
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(List<int> values)
    {
        var result = new decimal[values.Count];
        for(int i=0; i<values.Count; i++) result[i] = values[i];
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(List<long> values)
    {
        var result = new decimal[values.Count];
        for(int i=0; i<values.Count; i++) result[i] = values[i];
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(List<float> values)
    {
        var result = new decimal[values.Count];
        for(int i=0; i<values.Count; i++) result[i] = Convert.ToDecimal(values[i]);
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(List<double> values)
    {
        var result = new decimal[values.Count];
        for(int i=0; i<values.Count; i++) result[i] = Convert.ToDecimal(values[i]);
        return new TweenValue(result);
    }
    public static implicit operator TweenValue(List<decimal> values) => new TweenValue(values.ToArray());
    #endregion

    #region Implicit Casts From TweenValue
    public static implicit operator short(TweenValue tweenValue) => Convert.ToInt16(tweenValue.Values[0]);
    public static implicit operator int(TweenValue tweenValue) => Convert.ToInt32(tweenValue.Values[0]);
    public static implicit operator long(TweenValue tweenValue) => Convert.ToInt64(tweenValue.Values[0]);
    public static implicit operator float(TweenValue tweenValue) => (float)tweenValue.Values[0];
    public static implicit operator double(TweenValue tweenValue) => Convert.ToDouble(tweenValue.Values[0]);
    public static implicit operator decimal(TweenValue tweenValue) => tweenValue.Values[0];
    
    public static implicit operator System.Drawing.Point(TweenValue tweenValue)
        => new System.Drawing.Point(Convert.ToInt32(tweenValue.Values[0]), Convert.ToInt32(tweenValue.Values[1]));
    public static implicit operator System.Drawing.PointF(TweenValue tweenValue)
        => new System.Drawing.PointF((float)tweenValue.Values[0], (float)tweenValue.Values[1]);
    public static implicit operator Microsoft.Xna.Framework.Point(TweenValue tweenValue)
        => new Microsoft.Xna.Framework.Point(Convert.ToInt32(tweenValue.Values[0]), Convert.ToInt32(tweenValue.Values[1]));
    public static implicit operator Microsoft.Xna.Framework.Vector2(TweenValue tweenValue)
        => new Microsoft.Xna.Framework.Vector2((float)tweenValue.Values[0], (float)tweenValue.Values[1]);
    public static implicit operator Microsoft.Xna.Framework.Vector3(TweenValue tweenValue)
        => new Microsoft.Xna.Framework.Vector3((float)tweenValue.Values[0], (float)tweenValue.Values[1], (float)tweenValue.Values[2]);
    public static implicit operator Microsoft.Xna.Framework.Rectangle(TweenValue tweenValue)
        => new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(tweenValue.Values[0]), Convert.ToInt32(tweenValue.Values[1]), 
            Convert.ToInt32(tweenValue.Values[2]), Convert.ToInt32(tweenValue.Values[3]));
    public static implicit operator Microsoft.Xna.Framework.Color(TweenValue tweenValue)
    {
        for(int i=0 ; i<tweenValue.Values.Length ; i++)
        {
            tweenValue.Values[i] = Math.Max(0, tweenValue.Values[i]);
            tweenValue.Values[i] = Math.Min(255, tweenValue.Values[i]);
        }
        return new Microsoft.Xna.Framework.Color(Convert.ToInt32(tweenValue.Values[0]), Convert.ToInt32(tweenValue.Values[1]), 
            Convert.ToInt32(tweenValue.Values[2]), Convert.ToInt32(tweenValue.Values[3]));
    }
    public static implicit operator System.Drawing.Color(TweenValue tweenValue)
    {
        for(int i=0 ; i<tweenValue.Values.Length ; i++)
        {
            tweenValue.Values[i] = Math.Max(0, tweenValue.Values[i]);
            tweenValue.Values[i] = Math.Min(255, tweenValue.Values[i]);
        }
        return System.Drawing.Color.FromArgb(Convert.ToInt32(tweenValue.Values[3]), Convert.ToInt32(tweenValue.Values[0]), 
            Convert.ToInt32(tweenValue.Values[1]), Convert.ToInt32(tweenValue.Values[2]));
    }

    public static implicit operator short[](TweenValue tweenValue)
    {
        var result = new short[tweenValue.Values.Length];
        for(int i=0; i<result.Length; i++) result[i] = Convert.ToInt16(tweenValue.Values[i]);
        return result;
    }
    public static implicit operator int[](TweenValue tweenValue)
    {
        var result = new int[tweenValue.Values.Length];
        for(int i=0; i<result.Length; i++) result[i] = Convert.ToInt32(tweenValue.Values[i]);
        return result;
    }
    public static implicit operator long[](TweenValue tweenValue)
    {
        var result = new long[tweenValue.Values.Length];
        for(int i=0; i<result.Length; i++) result[i] = Convert.ToInt64(tweenValue.Values[i]);
        return result;
    }
    public static implicit operator float[](TweenValue tweenValue)
    {
        var result = new float[tweenValue.Values.Length];
        for(int i=0; i<result.Length; i++) result[i] = (float)tweenValue.Values[i];
        return result;
    }
    public static implicit operator double[](TweenValue tweenValue)
    {
        var result = new double[tweenValue.Values.Length];
        for(int i=0; i<result.Length; i++) result[i] = Convert.ToDouble(tweenValue.Values[i]);
        return result;
    }
    public static implicit operator decimal[](TweenValue tweenValue) => tweenValue.Values;

    public static implicit operator List<short>(TweenValue tweenValue)
    {
        var result = new List<short>();
        for(int i=0; i<tweenValue.Values.Length; i++) result.Add(Convert.ToInt16(tweenValue.Values[i]));
        return result;
    }
    public static implicit operator List<int>(TweenValue tweenValue)
    {
        var result = new List<int>();
        for(int i=0; i<tweenValue.Values.Length; i++) result.Add(Convert.ToInt32(tweenValue.Values[i]));
        return result;
    }
    public static implicit operator List<long>(TweenValue tweenValue)
    {
        var result = new List<long>();
        for(int i=0; i<tweenValue.Values.Length; i++) result.Add(Convert.ToInt64(tweenValue.Values[i]));
        return result;
    }
    public static implicit operator List<float>(TweenValue tweenValue)
    {
        var result = new List<float>();
        for(int i=0; i<tweenValue.Values.Length; i++) result.Add((float)tweenValue.Values[i]);
        return result;
    }
    public static implicit operator List<double>(TweenValue tweenValue)
    {
        var result = new List<double>();
        for(int i=0; i<tweenValue.Values.Length; i++) result.Add(Convert.ToDouble(tweenValue.Values[i]));
        return result;
    }
    public static implicit operator List<decimal>(TweenValue tweenValue) => tweenValue.Values.ToList();
    #endregion
}
