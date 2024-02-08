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

    #region relations

    /// <summary>
    /// Determine whether 2 vectors are parallel.
    /// </summary>
    /// <param name="other">Another vector.</param>
    /// <returns>
    /// True, if the vectors are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    public bool IsParallelTo(Vector2D other) => AreParallel(this, other);

    /// <summary>
    /// Determine whether 2 vectors are parallel.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>
    /// True, if the vectors are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    // v1 = (x1, y1), v2 = (x2, y2)
    // v1 and v2 are parallel <=>
    //     1) v2 is not zero, v1 = k * v2, or
    //     2) v2 is zero ( v2 = (0, 0) )
    // That is to say, x1 = k * x2, y1 = k * y2, or x2 = y2 = 0.
    // Therefore, x1 * y2 = k * x2 * y2 = x2 * y1, which fits both conditions.
    public static bool AreParallel(Vector2D v1, Vector2D v2) => (v1.X * v2.Y).IsApproximatelyEqualTo(v1.Y * v2.X);

    /// <summary>
    /// Determine whether 2 vectors are perpendicular.
    /// </summary>
    /// <param name="other">Another vector.</param>
    /// <returns>
    /// True, if the vectors are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public bool IsPerpendicularTo(Vector2D other) => ArePerpendicular(this, other);

    /// <summary>
    /// Determine whether 2 vectors are perpendicular.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>
    /// True, if the vectors are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool ArePerpendicular(Vector2D v1, Vector2D v2) => (v1 * v2).IsNearZero();

    /// <summary>
    /// Included angle of 2 vectors in radians.
    /// </summary>
    /// <param name="other">Another vector.</param>
    /// <returns>Included angle of the vectors in radians.</returns>
    public double IncludedAngleWith(Vector2D other) => IncludedAngleOf(this, other);

    /// <summary>
    /// Included angle of 2 vectors in radians.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Included angle of the vectors in radians.</returns>
    public static double IncludedAngleOf(Vector2D v1, Vector2D v2)
    {
        if (v1 == Zero || v2 == Zero) return double.NaN;
        return Math.Acos(v1 * v2 / v1.Length / v2.Length);
    }

    /// <summary>
    /// Included angle of 2 vectors in degrees.
    /// </summary>
    /// <param name="other">Another vector.</param>
    /// <returns>Included angle of the vectors in degrees.</returns>
    public double IncludedAngleDegreeWith(Vector2D other) => IncludedAngleDegreeOf(this, other);

    /// <summary>
    /// Included angle of 2 vectors in degrees.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Included angle of the vectors in degrees.</returns>
    public static double IncludedAngleDegreeOf(Vector2D v1, Vector2D v2)
    {
        if (v1 == Zero || v2 == Zero) return double.NaN;
        return Math.Acos(v1 * v2 / v1.Length / v2.Length) / Math.PI * 180d;
    }

    /// <summary>
    /// Sample a vector perpendicular to current vector.
    /// </summary>
    /// <returns>The perpendicular vector sample.</returns>
    public Vector2D SamplePerpendicularVector() => (Y, -X);
    
    #endregion

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