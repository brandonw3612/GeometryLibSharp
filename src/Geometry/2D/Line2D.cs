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
    public Line2D(Point2D p1, Point2D p2) : base(p1, p1.VectorTo(p2),
        (double.NegativeInfinity, double.PositiveInfinity))
    {
        // Nothing else here.
    }

    #endregion

    /// <summary>
    /// Corresponding line of a line is itself.
    /// </summary>
    public override Line2D CorrespondingLine => this;

    #region relations

    /// <summary>
    /// Determine whether other <see cref="LineBase2D"/> object is equivalent to current object.
    /// </summary>
    /// <returns>
    /// True, if the objects are equivalent; <br/>
    /// False, otherwise.
    /// </returns>
    protected override bool Equals(LineBase2D other) => other is Line2D l && IsParallelTo(l) && Contains(l.FixedPoint);

    /// <summary>
    /// Calculate distance to a point.
    /// </summary>
    /// <returns>Distance to the point.</returns>
    public double DistanceTo(Point2D point)
    {
        // FixPoint -> f, point -> p, direction -> (d)
        // Foot of the perpendicular -> q
        // (fq) = +/- fq * d
        // pq = fp * sin<(fp), (fq)> = sqrt( fp^2 - ( fp * cos<(fp), (fq)> )^2 )
        //    = sqrt( (fp) * (fp) - ( fp * (fp) * (fq) / fp / fq )^2 )
        //    = sqrt( (fp) * (fp) - ( +/- (fp) * (d) )^2 )
        //    = sqrt( (fp) * (fp) - ( (fp) * (d) )^2 )
        var fp = FixedPoint.VectorTo(point);
        return Math.Sqrt(fp * fp - Math.Pow(Direction * fp, 2));
    }

    /// <summary>
    /// Calculate distance to another line.
    /// </summary>
    /// <returns>
    /// Distance, if the lines are parallel or equivalent; <br/>
    /// Null, otherwise.
    /// </returns>
    public double? DistanceTo(Line2D other) => DistanceBetween(this, other);

    /// <summary>
    /// Calculate distance between 2 lines.
    /// </summary>
    /// <param name="l1">The first line.</param>
    /// <param name="l2">The second line.</param>
    /// <returns>
    /// Distance, if the lines are parallel or equivalent; <br/>
    /// Null, otherwise.
    /// </returns>
    public static double? DistanceBetween(Line2D l1, Line2D l2) =>
        AreParallel(l1, l2) ? l1.DistanceTo(l2.FixedPoint) : null;

    #endregion
    
    /// <summary>
    /// Sample points from the line. <br/>
    /// Note: Points are infinitive.
    /// </summary>
    /// <param name="precision">Precision of the sampling, which is the distance between neighboring points.</param>
    /// <returns>All point samples.</returns>
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