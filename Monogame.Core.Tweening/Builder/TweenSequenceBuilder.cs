using Microsoft.Xna.Framework;
using MonoGame.Core.Graphics.Components;

namespace Monogame.Core.Tweening.Builder;

public static class TweenSequenceBuilder
{
    public static ISequenceToOn<short, ITweenSequence> From(short startValue) => new Tweens.TweenSequence<short>().From(startValue);
    public static ISequenceToOn<int, ITweenSequence> From(int startValue) => new Tweens.TweenSequence<int>().From(startValue);
    public static ISequenceToOn<long, ITweenSequence> From(long startValue) => new Tweens.TweenSequence<long>().From(startValue);
    public static ISequenceToOn<float, ITweenSequence> From(float startValue) => new Tweens.TweenSequence<float>().From(startValue);
    public static ISequenceToOn<double, ITweenSequence> From(double startValue) => new Tweens.TweenSequence<double>().From(startValue);
    public static ISequenceToOn<decimal, ITweenSequence> From(decimal startValue) => new Tweens.TweenSequence<decimal>().From(startValue);
    public static ISequenceTo<float, ITweenSequence> OnRotation(Drawable target) => new Tweens.TweenSequence<float>().OnRotation(target);
    public static ISequenceTo<float, ITweenSequence> OnOpacity(Drawable target) => new Tweens.TweenSequence<float>().OnOpacity(target);

    public static ISequenceToOn<System.Drawing.Point, ITweenSequence> From(System.Drawing.Point startValue) => new Tweens.TweenSequence<System.Drawing.Point>().From(startValue);
    public static ISequenceToOn<System.Drawing.PointF, ITweenSequence> From(System.Drawing.PointF startValue) => new Tweens.TweenSequence<System.Drawing.PointF>().From(startValue);
    public static ISequenceToOn<Point, ITweenSequence> From(Point startValue) => new Tweens.TweenSequence<Point>().From(startValue);
    public static ISequenceToOn<Vector2, ITweenSequence> From(Vector2 startValue) => new Tweens.TweenSequence<Vector2>().From(startValue);
    public static ISequenceTo<Point, ITweenSequence> OnPosition(Drawable target) => new Tweens.TweenSequence<Point>().OnPosition(target);
    public static ISequenceTo<Vector2, ITweenSequence> OnScale(Drawable target) => new Tweens.TweenSequence<Vector2>().OnScale(target);

    public static ISequenceToOn<Vector3, ITweenSequence> From(Vector3 startValue) => new Tweens.TweenSequence<Vector3>().From(startValue);

    public static ISequenceToOn<short[], ITweenSequence> From(params short[] startValues) => new Tweens.TweenSequence<short[]>().From(startValues);
    public static ISequenceToOn<int[], ITweenSequence> From(params int[] startValues) => new Tweens.TweenSequence<int[]>().From(startValues);
    public static ISequenceToOn<long[], ITweenSequence> From(params long[] startValues) => new Tweens.TweenSequence<long[]>().From(startValues);
    public static ISequenceToOn<float[], ITweenSequence> From(params float[] startValues) => new Tweens.TweenSequence<float[]>().From(startValues);
    public static ISequenceToOn<double[], ITweenSequence> From(params double[] startValues) => new Tweens.TweenSequence<double[]>().From(startValues);
    public static ISequenceToOn<decimal[], ITweenSequence> From(params decimal[] startValues) => new Tweens.TweenSequence<decimal[]>().From(startValues);

    public static ISequenceToOn<List<short>, ITweenSequence> From(List<short> startValues) => new Tweens.TweenSequence<List<short>>().From(startValues);
    public static ISequenceToOn<List<int>, ITweenSequence> From(List<int> startValues) => new Tweens.TweenSequence<List<int>>().From(startValues);
    public static ISequenceToOn<List<long>, ITweenSequence> From(List<long> startValues) => new Tweens.TweenSequence<List<long>>().From(startValues);
    public static ISequenceToOn<List<float>, ITweenSequence> From(List<float> startValues) => new Tweens.TweenSequence<List<float>>().From(startValues);
    public static ISequenceToOn<List<double>, ITweenSequence> From(List<double> startValues) => new Tweens.TweenSequence<List<double>>().From(startValues);
    public static ISequenceToOn<List<decimal>, ITweenSequence> From(List<decimal> startValues) => new Tweens.TweenSequence<List<decimal>>().From(startValues);

    public static ISequenceToOn<System.Drawing.Color, ITweenSequence> From(System.Drawing.Color startColor) => new Tweens.TweenSequence<System.Drawing.Color>().From(startColor);
    public static ISequenceToOn<Color, ITweenSequence> From(Color startColor) => new Tweens.TweenSequence<Color>().From(startColor);

    /// <summary>
    /// Class/Struct T must implement implicit cast for Monogame.Core.Tweening.Structs.TweenValue
    /// </summary>
    public static ISequenceToOn<T, ITweenSequence> From<T>(T startValue) => new Tweens.TweenSequence<T>().From(startValue);
}
