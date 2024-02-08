namespace Geometry.Primitives;

/// <summary>
/// Interface providing methods for a set of points.
/// </summary>
/// <typeparam name="TPoint"></typeparam>
public interface IPointSet<TPoint> where TPoint : IPoint
{
    /// <summary>
    /// Sample points from the object.
    /// </summary>
    /// <param name="precision"> Precision of the samples. Normally this refers to the distance between 2 points.</param>
    /// <returns>All point samples.</returns>
    IEnumerable<TPoint> Sample(double precision);
    
    /// <summary>
    /// Determine whether the object contains a point.
    /// </summary>
    /// <param name="point">The point to be checked.</param>
    /// <returns>True, if the object contains the point; False, otherwise.</returns>
    bool Contains(TPoint point);
}