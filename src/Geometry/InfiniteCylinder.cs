using Geometry.Extensions;
using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Infinite curvilinear surface.
/// </summary>
public class InfiniteCylinder : IPointSet<Point3D>
{
    /// <summary>
    /// Axis of the cylinder.
    /// </summary>
    public Line3D Axis { get; }
    
    /// <summary>
    /// Radius of the cylinder.
    /// </summary>
    public double Radius { get; }

    /// <summary>
    /// Construct an infinite cylinder with its axis and radius.
    /// </summary>
    /// <param name="axis">Axis of the cylinder.</param>
    /// <param name="radius">Radius of the cylinder.</param>
    public InfiniteCylinder(Line3D axis, double radius)
    {
        Axis = axis;
        Radius = radius;
    }

    /// <inheritdoc />
    /// <summary>
    /// Sample points from the infinite cylinder.
    /// </summary>
    /// <returns>Sampled points from the infinite cylinder.</returns>
    public IEnumerable<Point3D> Sample(double precision)
    {
        var vectorZ = Axis.Direction;
        var vectorX = vectorZ.SamplePerpendicularVector().Normalized;
        var vectorY = (vectorZ ^ vectorX).Normalized;
        var step = precision / Radius;
        foreach (var axisPoint in Axis.Sample(precision))
        {
            for (var rad = 0d; rad < 2 * Math.PI; rad += step)
            {
                yield return axisPoint.MoveBy(vectorX * Math.Cos(rad) * Radius + vectorY * Math.Sin(rad) * Radius);
            }
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Determine whether the infinite cylinder contains a point.
    /// </summary>
    /// <returns>True, if the infinite cylinder contains the point; False, otherwise.</returns>
    public bool Contains(Point3D point) => point.DistanceTo(Axis).IsApproximatelyEqualTo(Radius);
}