using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Line in the 3-dimensional space.
/// </summary>
public class Line3D : LineBase3D
{
    #region constants

    private static Line3D? _axisX;
    /// <summary>
    /// The X axis in the 3-dimensional coordinate system.
    /// </summary>
    public static Line3D AxisX => _axisX ??= new(Point3D.Origin, Vector3D.I);

    private static Line3D? _axisY;
    /// <summary>
    /// The Y axis in the 3-dimensional coordinate system.
    /// </summary>
    public static Line3D AxisY => _axisY ??= new(Point3D.Origin, Vector3D.J);

    private static Line3D? _axisZ;
    /// <summary>
    /// The Z axis in the 3-dimensional coordinate system.
    /// </summary>
    public static Line3D AxisZ => _axisZ ??= new(Point3D.Origin, Vector3D.K);

    #endregion

    #region constructors

    /// <summary>
    /// Construct the line with a point on it and its direction.
    /// </summary>
    /// <param name="point">A point on the line.</param>
    /// <param name="direction">Direction of the line.</param>
    public Line3D(Point3D point, Vector3D direction) : base(point, direction,
        (double.NegativeInfinity, double.PositiveInfinity))
    {
        // Nothing else here.
    }
    
    /// <summary>
    /// Construct the line with 2 specified points.
    /// </summary>
    /// <param name="p1">The first point on the line.</param>
    /// <param name="p2">The second point on the line.</param>
    public Line3D(Point3D p1, Point3D p2) : base(p1, Relations.VectorBetween(p1, p2),
        (double.NegativeInfinity, double.PositiveInfinity))
    {
        // Nothing else here.
    }

    #endregion

    /// <summary>
    /// Corresponding line of a line is itself.
    /// </summary>
    public override Line3D CorrespondingLine => this;

    /// <inheritdoc />
    protected override bool Equals(LineBase3D other) =>
        other is Line3D l && Relations.AreParallel(this, l) && Contains(l.FixedPoint);
    
    /// <inheritdoc />
    /// <summary>
    /// Sample points from the line. <br/>
    /// Note: Points are infinitive.
    /// </summary>
    /// <returns>All point samples from the line.</returns>
    public override IEnumerable<Point3D> Sample(double precision)
    {
        double translation = 0d;
        yield return FixedPoint;
        while (translation < double.PositiveInfinity)
        {
            translation += precision;
            yield return FixedPoint.MoveBy(Direction * translation);
            yield return FixedPoint.MoveBy(Direction * translation * -1d);
        }
    }
}