using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Segment in the 2-dimensional space.
/// </summary>
public class Segment2D : LineBase2D
{
    /// <summary>
    /// Construct the segment with both ends.
    /// </summary>
    /// <param name="start">Start of the segment.</param>
    /// <param name="end">End of the segment.</param>
    public Segment2D(Point2D start, Point2D end) : base(start, Relations.VectorBetween(start, end),
        (0, Relations.DistanceBetween(start, end)))
    {
        // Nothing else here.
    }

    /// <summary>
    /// Start of the segment.
    /// </summary>
    public Point2D Start => FixedPoint;

    private Point2D? _end;

    /// <summary>
    /// End of the segment.
    /// </summary>
    public Point2D End => _end ??= FixedPoint.MoveBy(Direction * Boundaries.End);

    /// <summary>
    /// Length of the segment.
    /// </summary>
    public double Length => Boundaries.End;
    
    private Line2D? _correspondingLine;

    /// <summary>
    /// Corresponding line of the segment.
    /// </summary>
    public override Line2D CorrespondingLine => _correspondingLine ??= new(FixedPoint, Direction);

    /// <inheritdoc />
    protected override bool Equals(LineBase2D other)
    {
        if (other is not Segment2D segment) return false;
        if (Start == segment.Start && End == segment.End) return true;
        if (Start == segment.End && End == segment.Start) return true;
        return false;
    }
    
    /// <inheritdoc />
    /// <summary>
    /// Sample points from the segment.
    /// </summary>
    /// <returns>All point samples from the segment.</returns>
    public override IEnumerable<Point2D> Sample(double precision)
    {
        for (double translation = Boundaries.Start; translation <= Boundaries.End; translation += precision)
        {
            yield return FixedPoint.MoveBy(Direction * translation);
        }
    }
}