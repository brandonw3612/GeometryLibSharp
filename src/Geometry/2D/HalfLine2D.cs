using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Half-line in the 2-dimensional space.
/// </summary>
public class HalfLine2D : LineBase2D
{
    /// <summary>
    /// Construct the half-line with its end and direction vector.
    /// </summary>
    /// <param name="end">The end of the half-line.</param>
    /// <param name="direction">The direction of the half-line, pointing from the end to infinity.</param>
    public HalfLine2D(Point2D end, Vector2D direction) : base(end, direction, (0, double.PositiveInfinity))
    {
        // Nothing else here.
    }

    /// <summary>
    /// The end of the half-line.
    /// </summary>
    public Point2D End => FixedPoint;

    private Line2D? _correspondingLine;

    /// <summary>
    /// Corresponding line of the half-line.
    /// </summary>
    public override Line2D CorrespondingLine => _correspondingLine ??= new(FixedPoint, Direction);

    /// <summary>
    /// Determine whether other <see cref="LineBase2D"/> object is equivalent to current object.
    /// </summary>
    /// <returns>
    /// True, if the objects are equivalent; <br/>
    /// False, otherwise.
    /// </returns>
    protected override bool Equals(LineBase2D other) =>
        other is HalfLine2D hl && Direction == hl.Direction && End == hl.End;

    /// <summary>
    /// Sample points from the half-line. <br/>
    /// Note: Points are infinitive.
    /// </summary>
    /// <param name="precision">Precision of the sampling, which is the distance between neighboring points.</param>
    /// <returns>All point samples.</returns>
    public override IEnumerable<Point2D> Sample(double precision)
    {
        double translation = 0d;
        while (translation < double.PositiveInfinity)
        {
            yield return FixedPoint.MoveBy(Direction * translation);
            translation += precision;
        }
    }
}