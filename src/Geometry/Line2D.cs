using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Line in the 2-dimensional space.
/// </summary>
public class Line2D : LineBase2D
{
    #region constants

    private static Line2D? _axisX;
    /// <summary>
    /// The X axis in the 2-dimensional coordinate system.
    /// </summary>
    public static Line2D AxisX => _axisX ??= new(Point2D.Origin, Vector2D.I);

    private static Line2D? _axisY;
    /// <summary>
    /// The Y axis in the 2-dimensional coordinate system.
    /// </summary>
    public static Line2D AxisY => _axisY ??= new(Point2D.Origin, Vector2D.J);

    #endregion

    #region constructors

    /// <summary>
    /// Construct the line with a point on it and its direction.
    /// </summary>
    /// <param name="point">A point on the line.</param>
    /// <param name="direction">Direction of the line.</param>
    public Line2D(Point2D point, Vector2D direction) : base(point, direction,
        (double.NegativeInfinity, double.PositiveInfinity))
    {
        // Nothing else here.
    }
    
    /// <summary>
    /// Construct the line with 2 specified points.
    /// </summary>
    /// <param name="p1">The first point on the line.</param>
    /// <param name="p2">The second point on the line.</param>
    public Line2D(Point2D p1, Point2D p2) : base(p1, Relations.VectorBetween(p1, p2),
        (double.NegativeInfinity, double.PositiveInfinity))
    {
        // Nothing else here.
    }

    #endregion

    /// <summary>
    /// Corresponding line of a line is itself.
    /// </summary>
    public override Line2D CorrespondingLine => this;

    /// <inheritdoc />
    protected override bool Equals(LineBase2D other) =>
        other is Line2D l && Relations.AreParallel(this, l) && Contains(l.FixedPoint);
    
    /// <inheritdoc />
    /// <summary>
    /// Sample points from the line. <br/>
    /// Note: Points are infinitive.
    /// </summary>
    /// <returns>All point samples from the line.</returns>
    public override IEnumerable<Point2D> Sample(double precision)
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