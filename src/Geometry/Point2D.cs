using System.Numerics;
using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Point in 2-dimensional space.
/// </summary>
public class Point2D : Coordinate2D, IPoint
{
    #region constants

    private static Point2D? _origin;
    /// <summary>
    /// Origin in 2-dimensional space. <br/>
    /// Coordinate: (0, 0).
    /// </summary>
    public static Point2D Origin => _origin ??= (0d, 0d);

    #endregion

    #region constructors
    
    /// <inheritdoc />
    public Point2D(double x, double y) : base(x, y)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Point2D((double X, double Y) t) : base(t)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Point2D(Vector2 v) : base(v)
    {
        // Nothing else here.
    }
    
    /// <inheritdoc />
    public Point2D(double[] array) : base(array)
    {
        // Nothing else here.
    }
    
    #endregion

    #region implicit converters

    // Implicit converters converting from semantically equivalent types.
    public static implicit operator Point2D((double, double) t) => new(t);
    public static implicit operator Point2D(double[] array) => new(array);
    public static implicit operator Point2D(Vector2 v) => new(v);

    #endregion

    /// <summary>
    /// Move the point by a translation vector.
    /// </summary>
    /// <param name="translationVector">The translation done to the point.</param>
    /// <returns>The moved point.</returns>
    public Point2D MoveBy(Vector2D translationVector) => (X + translationVector.X, Y + translationVector.Y);
}