using System.Numerics;
using Geometry.Extensions;
using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Vector in the 2-dimensional space.
/// </summary>
public class Vector2D : Coordinate2D
{
    /// <summary>
    /// Length (or norm) of the vector.
    /// </summary>
    public double Length => Math.Sqrt(this * this);

    /// <summary>
    /// Normalized vector, whose direction is the same to the original vector
    /// and <see cref="Length"/> is 1.
    /// </summary>
    /// <exception cref="ArithmeticException">If the <see cref="Length"/> of the vector is 0.</exception>
    public Vector2D Normalized => Length.IsNearZero()
        ? throw new ArithmeticException("You cannot normalize a Zero vector.")
        : this / Length;

    /// <summary>
    /// Sample a vector perpendicular to current vector.
    /// </summary>
    /// <returns>The perpendicular vector sample.</returns>
    public Vector2D SamplePerpendicularVector() => (Y, -X);

    #region constants

    private static Vector2D? _zero;
    /// <summary>
    /// Zero vector in 2-dimensional space. <br/>
    /// Coordinate: (0, 0).
    /// </summary>
    public static Vector2D Zero => _zero ??= (0d, 0d);

    private static Vector2D? _i;
    /// <summary>
    /// Unit vector in the positive direction on X-axis in 2-dimensional space. <br/>
    /// Coordinate: (1, 0).
    /// </summary>
    public static Vector2D I => _i ??= (1d, 0d);
    
    private static Vector2D? _j;
    /// <summary>
    /// Unit vector in the positive direction on Y-axis in 2-dimensional space. <br/>
    /// Coordinate: (0, 1).
    /// </summary>
    public static Vector2D J => _j ??= (0d, 1d);

    #endregion
    
    #region constructors
    
    /// <inheritdoc />
    public Vector2D(double x, double y) : base(x, y)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Vector2D((double X, double Y) t) : base(t)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Vector2D(Vector2 v) : base(v)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Vector2D(double[] array) : base(array)
    {
        // Nothing else here.
    }
    
    #endregion
    
    #region implicit converters

    // Implicit converters converting from semantically equivalent types.
    public static implicit operator Vector2D((double, double) t) => new(t);
    public static implicit operator Vector2D(double[] array) => new(array);
    public static implicit operator Vector2D(Vector2 v) => new(v);
    
    #endregion
    
    #region arithmetic operators

    /// <summary>
    /// Sum of 2 vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Sum of the vectors.</returns>
    public static Vector2D operator +(Vector2D v1, Vector2D v2) => (v1.X + v2.X, v1.Y + v2.Y);

    /// <summary>
    /// Difference of 2 vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Difference of the vectors.</returns>
    public static Vector2D operator -(Vector2D v1, Vector2D v2) => (v1.X - v2.X, v1.Y - v2.Y);

    /// <summary>
    /// Scalar product of 2 vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Scalar product of the vectors.</returns>
    public static double operator *(Vector2D v1, Vector2D v2) => v1.X * v2.X + v1.Y * v2.Y;

    /// <summary>
    /// Scalar product of a vector and a number.
    /// </summary>
    /// <param name="v">The vector.</param>
    /// <param name="d">The number.</param>
    /// <returns>Scalar product of the vector and the number.</returns>
    public static Vector2D operator *(Vector2D v, double d) => (v.X * d, v.Y * d);

    /// <summary>
    /// Scalar product of a vector and a number.
    /// </summary>
    /// <param name="d">The number.</param>
    /// <param name="v">The vector.</param>
    /// <returns>Scalar product of the vector and the number.</returns>
    public static Vector2D operator *(double d, Vector2D v) => v * d;

    /// <summary>
    /// Quotient of a vector and a number. <br/>
    /// Note: A vector can only act as the dividend.
    /// </summary>
    /// <param name="v">The vector.</param>
    /// <param name="d">The number.</param>
    /// <returns>Quotient of the vector and the number.</returns>
    public static Vector2D operator /(Vector2D v, double d) => (v.X / d, v.Y / d);

    #endregion
}