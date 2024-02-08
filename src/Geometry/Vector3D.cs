using System.Numerics;
using Geometry.Extensions;
using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Vector in the 3-dimensional space.
/// </summary>
public class Vector3D : Coordinate3D
{
    /// <summary>
    /// Length / Norm of the vector.
    /// </summary>
    public double Length => Math.Sqrt(this * this);

    /// <summary>
    /// Normalized vector, whose direction is the same to the original vector
    /// and <see cref="Length"/> is 1.
    /// </summary>
    /// <exception cref="ArithmeticException">If the <see cref="Length"/> of the vector is 0.</exception>
    public Vector3D Normalized => Length.IsNearZero()
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
    public bool IsParallelTo(Vector3D other) => AreParallel(this, other);

    /// <summary>
    /// Determine whether 2 vectors are parallel.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>
    /// True, if the vectors are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    /// v1 is parallel to v2 => v1 x v2 = (0)
    public static bool AreParallel(Vector3D v1, Vector3D v2) => (v1 ^ v2) == Zero;

    /// <summary>
    /// Determine whether 2 vectors are perpendicular.
    /// </summary>
    /// <param name="other">Another vector.</param>
    /// <returns>
    /// True, if the vectors are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public bool IsPerpendicularTo(Vector3D other) => ArePerpendicular(this, other);

    /// <summary>
    /// Determine whether 2 vectors are perpendicular.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>
    /// True, if the vectors are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool ArePerpendicular(Vector3D v1, Vector3D v2) => (v1 * v2).IsNearZero();

    /// <summary>
    /// Included angle of 2 vectors in radians.
    /// </summary>
    /// <param name="other">Another vector.</param>
    /// <returns>Included angle of the vectors in radians.</returns>
    public double IncludedAngleWith(Vector3D other) => IncludedAngleOf(this, other);

    /// <summary>
    /// Included angle of 2 vectors in radians.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Included angle of the vectors in radians.</returns>
    public static double IncludedAngleOf(Vector3D v1, Vector3D v2)
    {
        if (v1 == Zero || v2 == Zero) return double.NaN;
        return Math.Acos(v1 * v2 / v1.Length / v2.Length);
    }

    /// <summary>
    /// Included angle of 2 vectors in degrees.
    /// </summary>
    /// <param name="other">Another vector.</param>
    /// <returns>Included angle of the vectors in degrees.</returns>
    public double IncludedAngleDegreeWith(Vector3D other) => IncludedAngleDegreeOf(this, other);

    /// <summary>
    /// Included angle of 2 vectors in degrees.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Included angle of the vectors in degrees.</returns>
    public static double IncludedAngleDegreeOf(Vector3D v1, Vector3D v2)
    {
        if (v1 == Zero || v2 == Zero) return double.NaN;
        return Math.Acos(v1 * v2 / v1.Length / v2.Length) / Math.PI * 180d;
    }

    /// <summary>
    /// Sample a vector perpendicular to current vector.
    /// </summary>
    /// <returns>The perpendicular vector sample.</returns>
    public Vector3D SamplePerpendicularVector() => Z.IsNearZero() ? (0d, 0d, 1d) : (Z, Z, -X - Y);

    #endregion

    #region constants

    private static Vector3D? _zero;
    /// <summary>
    /// Zero vector in 3-dimensional space. <br/>
    /// Coordinate: (0, 0, 0).
    /// </summary>
    public static Vector3D Zero => _zero ??= (0d, 0d, 0d);

    private static Vector3D? _i;
    /// <summary>
    /// Unit vector in the positive direction on X-axis in 3-dimensional space. <br/>
    /// Coordinate: (1, 0, 0).
    /// </summary>
    public static Vector3D I => _i ??= (1d, 0d, 0d);
    
    private static Vector3D? _j;
    /// <summary>
    /// Unit vector in the positive direction on Y-axis in 2-dimensional space. <br/>
    /// Coordinate: (0, 1, 0).
    /// </summary>
    public static Vector3D J => _j ??= (0d, 1d, 0d);

    private static Vector3D? _k;
    /// <summary>
    /// Unit vector in the positive direction on Z-axis in 3-dimensional space. <br/>
    /// Coordinate: (0, 0, 1).
    /// </summary>
    public static Vector3D K => _k ??= (0d, 0d, 1d);
    
    #endregion
    
    #region constructors
    
    /// <inheritdoc />
    public Vector3D(double x, double y, double z) : base(x, y, z)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Vector3D((double X, double Y, double Z) t) : base(t)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Vector3D(Vector3 v) : base(v)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Vector3D(double[] array) : base(array)
    {
        // Nothing else here.
    }
    
    #endregion
    
    #region implicit converters

    // Implicit converters converting from semantically equivalent types.
    public static implicit operator Vector3D((double, double, double) t) => new(t);
    public static implicit operator Vector3D(double[] array) => new(array);
    public static implicit operator Vector3D(Vector3 v) => new(v);
    
    #endregion
    
    #region arithmetic operators

    /// <summary>
    /// Sum of 2 vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Sum of the vectors.</returns>
    public static Vector3D operator +(Vector3D v1, Vector3D v2) => (v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

    /// <summary>
    /// Difference of 2 vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Difference of the vectors.</returns>
    public static Vector3D operator -(Vector3D v1, Vector3D v2) => (v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

    /// <summary>
    /// Scalar product of 2 vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Scalar product of the vectors.</returns>
    public static double operator *(Vector3D v1, Vector3D v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    /// <summary>
    /// Scalar product of a vector and a number.
    /// </summary>
    /// <param name="v">The vector.</param>
    /// <param name="d">The number.</param>
    /// <returns>Scalar product of the vector and the number.</returns>
    public static Vector3D operator *(Vector3D v, double d) => (v.X * d, v.Y * d, v.Z * d);

    /// <summary>
    /// Scalar product of a vector and a number.
    /// </summary>
    /// <param name="d">The number.</param>
    /// <param name="v">The vector.</param>
    /// <returns>Scalar product of the vector and the number.</returns>
    public static Vector3D operator *(double d, Vector3D v) => v * d;

    /// <summary>
    /// Quotient of a vector and a number. <br/>
    /// Note: A vector can only act as the dividend.
    /// </summary>
    /// <param name="v">The vector.</param>
    /// <param name="d">The number.</param>
    /// <returns>Quotient of the vector and the number.</returns>
    public static Vector3D operator /(Vector3D v, double d) => (v.X / d, v.Y / d, v.Z / d);

    /// <summary>
    /// Cross product of two vectors.
    /// </summary>
    /// <param name="v1">The first vector.</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>The cross product of the vectors.</returns>
    public static Vector3D operator ^(Vector3D v1, Vector3D v2) => (v1.Y * v2.Z - v1.Z * v2.Y,
        v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);

    #endregion
}