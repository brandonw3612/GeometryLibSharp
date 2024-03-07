namespace Geometry.Extensions;

/// <summary>
/// Extensions for <see cref="Point2D"/> elements.
/// </summary>
public static class Point2DExtensions
{
    /// <summary>
    /// Gets the distance between current point to the specified line.
    /// </summary>
    /// <param name="point">Current point.</param>
    /// <param name="line">The specified line.</param>
    /// <returns>Distance to the line.</returns>
    public static double DistanceTo(this Point2D point, Line2D line) => Relations.DistanceBetween(line, point);

    /// <summary>
    /// Calculate distance between current point and the provided point.
    /// </summary>
    /// <param name="current">Current point, start of the vector.</param>
    /// <param name="other">The other point.</param>
    /// <returns>Distance to the provided point.</returns>
    public static double DistanceTo(this Point2D current, Point2D other) => Relations.DistanceBetween(current, other);
    
    /// <summary>
    /// Vector from current point to the end.
    /// </summary>
    /// <param name="current">Current point, start of the vector.</param>
    /// <param name="end">The end of the vector.</param>
    /// <returns>The vector from current point to the end.</returns>
    public static Vector2D VectorTo(this Point2D current, Point2D end) => Relations.VectorBetween(current, end);

    /// <summary>
    /// Vector from the start to the current point.
    /// </summary>
    /// <param name="current">Current point, end of the vector.</param>
    /// <param name="start">The start of the vector.</param>
    /// <returns>The vector from the start to the current point.</returns>
    public static Vector2D VectorFrom(this Point2D current, Point2D start) => Relations.VectorBetween(start, current);
}