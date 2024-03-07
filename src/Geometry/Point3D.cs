using System.Numerics;
using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Point in 3-dimensional space.
/// </summary>
public class Point3D : Coordinate3D, IPoint
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
    
    /// <inheritdoc />
    public Point3D(double x, double y, double z) : base(x, y, z)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Point3D((double X, double Y, double Z) t) : base(t)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Point3D(Vector3 v) : base(v)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
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

    /// <summary>
    /// Move the point by a translation vector.
    /// </summary>
    /// <param name="translationVector">The translation done to the point.</param>
    /// <returns>The moved point.</returns>
    public Point3D MoveBy(Vector3D translationVector) =>
        (X + translationVector.X, Y + translationVector.Y, Z + translationVector.Z);
}