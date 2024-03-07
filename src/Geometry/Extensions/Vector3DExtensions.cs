namespace Geometry.Extensions;

/// <summary>
/// Extensions for <see cref="Vector3D"/> elements.
/// </summary>
public static class Vector3DExtensions
{
    /// <summary>
    /// Included angle of 2 vectors in radians.
    /// </summary>
    /// <param name="current">Current vector.</param>
    /// <param name="other">The other vector.</param>
    /// <returns>Included angle of the vectors in radians.</returns>
    public static double IncludedAngleWith(this Vector3D current, Vector3D other) =>
        Relations.IncludedAngleOf(current, other);
    
    /// <summary>
    /// Determine whether 2 vectors are parallel.
    /// </summary>
    /// <param name="current">Current vector.</param>
    /// <param name="other">The other vector.</param>
    /// <returns>
    /// True, if the vectors are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool IsParallelTo(this Vector3D current, Vector3D other) => Relations.AreParallel(current, other);

    /// <summary>
    /// Determine whether 2 vectors are perpendicular.
    /// </summary>
    /// <param name="current">Current vector.</param>
    /// <param name="other">The other vector.</param>
    /// <returns>
    /// True, if the vectors are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool IsPerpendicularTo(this Vector3D current, Vector3D other) =>
        Relations.ArePerpendicular(current, other);
}