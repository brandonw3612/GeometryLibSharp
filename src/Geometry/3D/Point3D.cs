using System.Numerics;
using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Point in 3-dimensional space.
/// </summary>
public class Point3D : Coordinate3D
{
    #region constants

    private static Point3D? _origin;
    /// <summary>
    /// Origin in 3-dimensional space. <br/>
    /// Coordinate: (0, 0, 0).
    /// </summary>
    public static Point3D Origin => _origin ??= (0d, 0d, 0d);

    #endregion

    #region constructors
    
    /// <summary>
    /// Construct from X, Y and Z coordinates.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="z">The Z coordinate.</param>
    public Point3D(double x, double y, double z) : base(x, y, z)
    {
        // Nothing else here.
    }
    
    /// <summary>
    /// Construct from a double tuple containing all coordinates.
    /// </summary>
    /// <param name="t">The coordinate double tuple.</param>
    public Point3D((double X, double Y, double Z) t) : base(t)
    {
        // Nothing else here.
    }
    
    /// <summary>
    /// Construct from a <see cref="Vector3"/> object using its coordinates.
    /// </summary>
    /// <param name="v">The coordinate <see cref="Vector3"/> object.</param>
    public Point3D(Vector3 v) : base(v)
    {
        // Nothing else here.
    }
    
    /// <summary>
    /// Construct from an array containing coordinates.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the length of the array is not 3.</exception>
    /// <param name="array">An array with exactly 3 elements, representing both coordinates.</param>
    public Point3D(double[] array) : base(array)
    {
        // Nothing else here.
    }
    
    #endregion

    #region implicit converters

    // Implicit converters converting from semantically equivalent types.
    public static implicit operator Point3D((double, double, double) t) => new(t);
    public static implicit operator Point3D(double[] array) => new(array);
    public static implicit operator Point3D(Vector3 v) => new(v);

    #endregion

    #region relations

    /// <summary>
    /// Vector from current point to the end.
    /// </summary>
    /// <param name="end">The end of the vector.</param>
    /// <returns>The vector from current point to the end.</returns>
    public Vector3D VectorTo(Point3D end) => VectorBetween(this, end);

    /// <summary>
    /// Vector from the start to the current point.
    /// </summary>
    /// <param name="start">The start of the vector.</param>
    /// <returns>The vector from the start to the current point.</returns>
    public Vector3D VectorFrom(Point3D start) => VectorBetween(start, this);

    /// <summary>
    /// Vector from a specific point to another one.
    /// </summary>
    /// <param name="start">The start of the vector.</param>
    /// <param name="end">The end of the vector.</param>
    /// <returns>The vector between the points.</returns>
    public static Vector3D VectorBetween(Point3D start, Point3D end) =>
        (end.X - start.X, end.Y - start.Y, end.Z - start.Z);

    /// <summary>
    /// Move the point by a translation vector.
    /// </summary>
    /// <param name="translationVector">The translation done to the point.</param>
    /// <returns>The moved point.</returns>
    public Point3D MoveBy(Vector3D translationVector) =>
        (X + translationVector.X, Y + translationVector.Y, Z + translationVector.Z);

    /// <summary>
    /// Calculate distance between current point and the provided point.
    /// </summary>
    /// <param name="other">The other point.</param>
    /// <returns>Distance to the provided point.</returns>
    public double DistanceTo(Point3D other) => DistanceBetween(this, other);

    /// <summary>
    /// Calculate distance between 2 points.
    /// </summary>
    /// <param name="p1">The first point.</param>
    /// <param name="p2">The second point.</param>
    /// <returns>Distance between the 2 points.</returns>
    public static double DistanceBetween(Point3D p1, Point3D p2) => VectorBetween(p1, p2).Length;

    /// <summary>
    /// Calculate distance to a line.
    /// </summary>
    /// <returns>Distance to the line.</returns>
    public double DistanceTo(Line3D line) => line.DistanceTo(this);

    #endregion
}