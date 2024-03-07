using Geometry.Primitives;

namespace Geometry.Extensions;

/// <summary>
/// Extensions for <see cref="LineBase2D"/> elements.
/// </summary>
public static class LineBase2DExtensions
{
    /// <summary>
    /// Solve included angle of current object and another one.
    /// </summary>
    /// <param name="current">Current <see cref="LineBase2D"/> object.</param>
    /// <param name="other">The other <see cref="LineBase2D"/> object.</param>
    /// <returns>The included angle of the objects.</returns>
    public static double IncludedAngleWith(this LineBase2D current, LineBase2D other) =>
        Relations.IncludedAngleOf(current, other);
    
    /// <summary>
    /// Solve the intersection point of current object and another.
    /// </summary>
    /// <param name="current">Current <see cref="LineBase2D"/> object.</param>
    /// <param name="other">The other <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// A point, if the objects intersect; <br/>
    /// Null, if the objects are parallel or overlap.
    /// </returns>
    public static Point2D? IntersectionPointWith(this LineBase2D current, LineBase2D other) =>
        Relations.IntersectionPointOf(current, other);
    
    /// <summary>
    /// Determine whether current object is parallel to another <see cref="LineBase2D"/> object.
    /// </summary>
    /// <param name="current">Current <see cref="LineBase2D"/> object.</param>
    /// <param name="other">The other <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool IsParallelTo(this LineBase2D current, LineBase2D other) => Relations.AreParallel(current, other);

    /// <summary>
    /// Determine whether current object is perpendicular to another <see cref="LineBase2D"/> object.
    /// </summary>
    /// <param name="current">Current <see cref="LineBase2D"/> object.</param>
    /// <param name="other">The other <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool IsPerpendicularTo(this LineBase2D current, LineBase2D other) =>
        Relations.ArePerpendicular(current, other);
}