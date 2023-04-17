using Geometry.Primitives;

namespace Geometry;

/// <summary>
/// Segment in the 3-dimensional space.
/// </summary>
public class Segment3D : LineBase3D
{
    /// <summary>
    /// Construct the segment with both ends.
    /// </summary>
    /// <param name="start">Start of the segment.</param>
    /// <param name="end">End of the segment.</param>
    public Segment3D(Point3D start, Point3D end) : base(start, start.VectorTo(end), (0, start.DistanceTo(end)))
    {
        // Nothing else here.
    }

    /// <summary>
    /// Start of the segment.
    /// </summary>
    public Point3D Start => FixedPoint;

    private Point3D? _end;

    /// <summary>
    /// End of the segment.
    /// </summary>
    public Point3D End => _end ??= FixedPoint.MoveBy(Direction * Boundaries.End);

    /// <summary>
    /// Length of the segment.
    /// </summary>
    public double Length => Boundaries.End;
    
    private Line3D? _correspondingLine;

    /// <summary>
    /// Corresponding line of the segment.
    /// </summary>
    public override Line3D CorrespondingLine => _correspondingLine ??= new(FixedPoint, Direction);

    /// <summary>
    /// Determine whether other <see cref="LineBase3D"/> object is equivalent to current object.
    /// </summary>
    /// <returns>
    /// True, if the objects are equivalent; <br/>
    /// False, otherwise.
    /// </returns>
    protected override bool Equals(LineBase3D other)
    {
        if (other is not Segment3D segment) return false;
        if (Start == segment.Start && End == segment.End) return true;
        if (Start == segment.End && End == segment.Start) return true;
        return false;
    }
    
    /// <summary>
    /// Sample points from the line.
    /// </summary>
    /// <param name="precision">Precision of the sampling, which is the distance between neighboring points.Ï</param>
    /// <returns>All point samples.</returns>
    public override IEnumerable<Point3D> Sample(double precision)
    {
        for (double translation = Boundaries.Start; translation <= Boundaries.End; translation += precision)
        {
            yield return FixedPoint.MoveBy(Direction * translation);
        }
    }
}