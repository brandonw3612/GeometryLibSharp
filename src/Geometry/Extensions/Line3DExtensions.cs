namespace Geometry.Extensions;

/// <summary>
/// Extensions for <see cref="Line3D"/> elements.
/// </summary>
public static class Line3DExtensions
{
    /// <summary>
    /// Gets the distance between current line to the specified point.
    /// </summary>
    /// <returns>Distance to the point.</returns>
    public static double DistanceTo(this Line3D line, Point3D point) => Relations.DistanceBetween(line, point);

    /// <summary>
    /// Calculate the distance between current line to another line.
    /// </summary>
    /// <returns>
    /// Distance, if the lines are parallel or equivalent.
    /// </returns>
    /// <exception cref="Exception">Thrown if the lines are not parallel.</exception>
    public static double DistanceTo(this Line3D line, Line3D other) => Relations.DistanceBetween(line, other);
}