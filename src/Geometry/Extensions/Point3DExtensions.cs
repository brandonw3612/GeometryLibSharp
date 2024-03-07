namespace Geometry.Extensions;

/// <summary>
/// Extensions for <see cref="Point3D"/> elements.
/// </summary>
public static class Point3DExtensions
{
    /// <summary>
    /// Gets the distance between current point to the specified line.
    /// </summary>
    /// <param name="point">Current point.</param>
    /// <param name="line">The specified line.</param>
    /// <returns>Distance to the line.</returns>
    public static double DistanceTo(this Point3D point, Line3D line) => Relations.DistanceBetween(line, point);

    /// <summary>
    /// Calculate distance between current point and the provided point.
    /// </summary>
    /// <param name="current">Current point, start of the vector.</param>
    /// <param name="other">The other point.</param>
    /// <returns>Distance to the provided point.</returns>
    public static double DistanceTo(this Point3D current, Point3D other) => Relations.DistanceBetween(current, other);
    
    /// <summary>
    /// Vector from current point to the end.
    /// </summary>
    /// <param name="current">Current point, start of the vector.</param>
    /// <param name="end">The end of the vector.</param>
    /// <returns>The vector from current point to the end.</returns>
    public static Vector3D VectorTo(this Point3D current, Point3D end) => Relations.VectorBetween(current, end);

    /// <summary>
    /// Vector from the start to the current point.
    /// </summary>
    /// <param name="current">Current point, end of the vector.</param>
    /// <param name="start">The start of the vector.</param>
    /// <returns>The vector from the start to the current point.</returns>
    public static Vector3D VectorFrom(this Point3D current, Point3D start) => Relations.VectorBetween(start, current);
}