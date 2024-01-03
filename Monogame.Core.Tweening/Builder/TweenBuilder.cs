using Microsoft.Xna.Framework;
using MonoGame.Core.Graphics.Components;

namespace Monogame.Core.Tweening.Builder;

public static class TweenBuilder
{
    public static IToOn<short, ITween> From(short startValue) => new Tweens.Tween<short>().From(startValue);
    public static IToOn<int, ITween> From(int startValue) => new Tweens.Tween<int>().From(startValue);
    public static IToOn<long, ITween> From(long startValue) => new Tweens.Tween<long>().From(startValue);
    public static IToOn<float, ITween> From(float startValue) => new Tweens.Tween<float>().From(startValue);
    public static IToOn<double, ITween> From(double startValue) => new Tweens.Tween<double>().From(startValue);
    public static IToOn<decimal, ITween> From(decimal startValue) => new Tweens.Tween<decimal>().From(startValue);
    public static ITo<float, ITween> OnRotation(Drawable target) => new Tweens.Tween<float>().OnRotation(target);
    public static ITo<float, ITween> OnOpacity(Drawable target) => new Tweens.Tween<float>().OnOpacity(target);

    public static IToOn<System.Drawing.Point, ITween> From(System.Drawing.Point startValue) => new Tweens.Tween<System.Drawing.Point>().From(startValue);
    public static IToOn<System.Drawing.PointF, ITween> From(System.Drawing.PointF startValue) => new Tweens.Tween<System.Drawing.PointF>().From(startValue);
    public static IToOn<Point, ITween> From(Point startValue) => new Tweens.Tween<Point>().From(startValue);
    public static IToOn<Vector2, ITween> From(Vector2 startValue) => new Tweens.Tween<Vector2>().From(startValue);
    public static ITo<Point, ITween> OnPosition(Drawable target) => new Tweens.Tween<Point>().OnPosition(target);
    public static ITo<Vector2, ITween> OnScale(Drawable target) => new Tweens.Tween<Vector2>().OnScale(target);

    public static IToOn<Vector3, ITween> From(Vector3 startValue) => new Tweens.Tween<Vector3>().From(startValue);

    public static IToOn<short[], ITween> From(params short[] startValues) => new Tweens.Tween<short[]>().From(startValues);
    public static IToOn<int[], ITween> From(params int[] startValues) => new Tweens.Tween<int[]>().From(startValues);
    public static IToOn<long[], ITween> From(params long[] startValues) => new Tweens.Tween<long[]>().From(startValues);
    public static IToOn<float[], ITween> From(params float[] startValues) => new Tweens.Tween<float[]>().From(startValues);
    public static IToOn<double[], ITween> From(params double[] startValues) => new Tweens.Tween<double[]>().From(startValues);
    public static IToOn<decimal[], ITween> From(params decimal[] startValues) => new Tweens.Tween<decimal[]>().From(startValues);

    public static IToOn<List<short>, ITween> From(List<short> startValues) => new Tweens.Tween<List<short>>().From(startValues);
    public static IToOn<List<int>, ITween> From(List<int> startValues) => new Tweens.Tween<List<int>>().From(startValues);
    public static IToOn<List<long>, ITween> From(List<long> startValues) => new Tweens.Tween<List<long>>().From(startValues);
    public static IToOn<List<float>, ITween> From(List<float> startValues) => new Tweens.Tween<List<float>>().From(startValues);
    public static IToOn<List<double>, ITween> From(List<double> startValues) => new Tweens.Tween<List<double>>().From(startValues);
    public static IToOn<List<decimal>, ITween> From(List<decimal> startValues) => new Tweens.Tween<List<decimal>>().From(startValues);

    public static IToOn<System.Drawing.Color, ITween> From(System.Drawing.Color startColor) => new Tweens.Tween<System.Drawing.Color>().From(startColor);
    public static IToOn<Color, ITween> From(Color startColor) => new Tweens.Tween<Color>().From(startColor);

    /// <summary>
    /// Class/Struct T must implement implicit cast for Monogame.Core.Tweening.Structs.TweenValue
    /// </summary>
    public static IToOn<T, ITween> From<T>(T startValue) => new Tweens.Tween<T>().From(startValue);
}
