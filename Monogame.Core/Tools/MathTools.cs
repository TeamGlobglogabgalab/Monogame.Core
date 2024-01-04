using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Core.Tools;

public static class MathTools
{
    public static short Min(params short[] values)
    {
        var minValue = values[0];
        for(int i=1; i< values.Length; i++)
            if (values[i] < minValue) 
                minValue = values[i];
        return minValue;
    }

    public static int Min(params int[] values)
    {
        var minValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] < minValue)
                minValue = values[i];
        return minValue;
    }

    public static long Min(params long[] values)
    {
        var minValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] < minValue)
                minValue = values[i];
        return minValue;
    }

    public static float Min(params float[] values)
    {
        var minValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] < minValue)
                minValue = values[i];
        return minValue;
    }

    public static double Min(params double[] values)
    {
        var minValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] < minValue)
                minValue = values[i];
        return minValue;
    }

    public static decimal Min(params decimal[] values)
    {
        var minValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] < minValue)
                minValue = values[i];
        return minValue;
    }

    public static short Max(params short[] values)
    {
        var maxValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] > maxValue)
                maxValue = values[i];
        return maxValue;
    }

    public static int Max(params int[] values)
    {
        var maxValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] > maxValue)
                maxValue = values[i];
        return maxValue;
    }

    public static long Max(params long[] values)
    {
        var maxValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] > maxValue)
                maxValue = values[i];
        return maxValue;
    }

    public static float Max(params float[] values)
    {
        var maxValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] > maxValue)
                maxValue = values[i];
        return maxValue;
    }

    public static double Max(params double[] values)
    {
        var maxValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] > maxValue)
                maxValue = values[i];
        return maxValue;
    }

    public static decimal Max(params decimal[] values)
    {
        var maxValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (values[i] > maxValue)
                maxValue = values[i];
        return maxValue;
    }
}
