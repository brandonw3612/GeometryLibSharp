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

    #region relations

    /// <summary>
    /// Vector from current point to the end.
    /// </summary>
    /// <param name="end">The end of the vector.</param>
    /// <returns>The vector from current point to the end.</returns>
    public Vector2D VectorTo(Point2D end) => VectorBetween(this, end);

    /// <summary>
    /// Vector from the start to the current point.
    /// </summary>
    /// <param name="start">The start of the vector.</param>
    /// <returns>The vector from the start to the current point.</returns>
    public Vector2D VectorFrom(Point2D start) => VectorBetween(start, this);

    /// <summary>
    /// Vector from a specific point to another one.
    /// </summary>
    /// <param name="start">The start of the vector.</param>
    /// <param name="end">The end of the vector.</param>
    /// <returns>The vector between the points.</returns>
    public static Vector2D VectorBetween(Point2D start, Point2D end) => (end.X - start.X, end.Y - start.Y);

    /// <summary>
    /// Move the point by a translation vector.
    /// </summary>
    /// <param name="translationVector">The translation done to the point.</param>
    /// <returns>The moved point.</returns>
    public Point2D MoveBy(Vector2D translationVector) => (X + translationVector.X, Y + translationVector.Y);

    /// <summary>
    /// Calculate distance between current point and the provided point.
    /// </summary>
    /// <param name="other">The other point.</param>
    /// <returns>Distance to the provided point.</returns>
    public double DistanceTo(Point2D other) => DistanceBetween(this, other);

    /// <summary>
    /// Calculate distance between 2 points.
    /// </summary>
    /// <param name="p1">The first point.</param>
    /// <param name="p2">The second point.</param>
    /// <returns>Distance between the 2 points.</returns>
    public static double DistanceBetween(Point2D p1, Point2D p2) => VectorBetween(p1, p2).Length;

    /// <summary>
    /// Calculate distance to a line.
    /// </summary>
    /// <returns>Distance to the line.</returns>
    public double DistanceTo(Line2D line) => line.DistanceTo(this);

    #endregion
}