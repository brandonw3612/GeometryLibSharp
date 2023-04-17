namespace Geometry.Primitives;

/// <summary>
/// Interface providing a method for sampling points from a geometry.
/// </summary>
/// <typeparam name="T">Point type.</typeparam>
public interface ISamplable<T>
{
    /// <summary>
    /// Sample points from the object.
    /// </summary>
    /// <param name="precision"> Precision of the samples. Normally this refers to the distance between 2 points.</param>
    /// <returns>All point samples.</returns>
    IEnumerable<T> Sample(double precision);
}