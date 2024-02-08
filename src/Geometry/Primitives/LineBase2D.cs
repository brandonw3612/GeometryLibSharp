using Geometry.Extensions;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Geometry.Primitives;

// Note:
// In this library, a line / half line / segment is identified by a fixed point on it and its direction.
// We create a number axis to help realize the concept, using the fixed point as the Origin and
// the direction as the positive direction of the axis.
// We apply this method everywhere in this class and classes derived from it.

/// <summary>
/// Base class derived by <see cref="Line2D"/>, <see cref="HalfLine2D"/> and <see cref="Segment2D"/>.
/// Presenting a line of part of a line in 2-dimensional space.
/// </summary>
public abstract class LineBase2D : IPointSet<Point2D>
{
    /// <summary>
    /// Fixed point on the <see cref="LineBase2D"/> object.
    /// </summary>
    protected readonly Point2D FixedPoint;
    
    /// <summary>
    /// Direction vector of the <see cref="LineBase2D"/> object.
    /// Seek for detailed description in the classes derived from this.
    /// </summary>
    public Vector2D Direction { get; }

    /// <summary>
    /// Boundaries for both end of the <see cref="LineBase2D"/> object.
    /// A positive or negative infinity value for either end means that
    /// the <see cref="LineBase2D"/> object does not have this end;
    /// Otherwise, the value of the ends means its coordinate on the number axis.
    /// whose origin is <see cref="FixedPoint"/> and positive direction is <see cref="Direction"/>.
    /// </summary>
    protected readonly (double Start, double End) Boundaries;
    
    /// <summary>
    /// Corresponding line of the <see cref="LineBase2D"/> object.
    /// Seek for detailed description in the classes derived from this.
    /// </summary>
    public abstract Line2D CorrespondingLine { get; }

    /// <summary>
    /// Only constructor method for a <see cref="LineBase2D"/> object.
    /// </summary>
    /// <param name="fixedPoint">Fixed point on the <see cref="LineBase2D"/> object.</param>
    /// <param name="direction">Direction vector of the <see cref="LineBase2D"/> object.</param>
    /// <param name="boundaries">Boundaries for both end of the <see cref="LineBase2D"/> object.</param>
    /// <exception cref="ArithmeticException">Provided direction vector is zero.</exception>
    internal LineBase2D(Point2D fixedPoint, Vector2D direction, (double, double) boundaries)
    {
        if (direction.Length.IsNearZero())
            throw new ArithmeticException("Lines must be constructed with a given direction vector.");
        Direction = direction.Normalized;
        FixedPoint = fixedPoint;
        Boundaries = boundaries;
    }

    /// <inheritdoc />
    public bool Contains(Point2D point)
    {
        var relativeVector = FixedPoint.VectorTo(point);
        // The point is not even on the corresponding line.
        if (!Direction.IsParallelTo(relativeVector)) return false;
        // Compute the coordinate od the point on the number axis.
        var x = relativeVector * Direction;
        return Boundaries.Start <= x && x <= Boundaries.End;
    }

    /// <summary>
    /// Determine whether current object is parallel to another <see cref="LineBase2D"/> object.
    /// </summary>
    /// <param name="other">Another <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    public bool IsParallelTo(LineBase2D other) => AreParallel(this, other);

    /// <summary>
    /// Determine whether 2 <see cref="LineBase2D"/> objects are parallel. 
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase2D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool AreParallel(LineBase2D l1, LineBase2D l2) => l1.Direction.IsParallelTo(l2.Direction);
    
    /// <summary>
    /// Determine whether current object is perpendicular to another <see cref="LineBase2D"/> object.
    /// </summary>
    /// <param name="other">Another <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public bool IsPerpendicularTo(LineBase2D other) => ArePerpendicular(this, other);

    /// <summary>
    /// Determine whether 2 <see cref="LineBase2D"/> objects are perpendicular. 
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase2D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool ArePerpendicular(LineBase2D l1, LineBase2D l2) => l1.Direction.IsPerpendicularTo(l2.Direction);

    /// <summary>
    /// Solve the intersection point of current object and another.
    /// </summary>
    /// <param name="other">Another <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// A point, if the objects intersect; <br/>
    /// Null, if the objects are parallel or overlap.
    /// </returns>
    public Point2D? IntersectionPointWith(LineBase2D other) => IntersectionPointOf(this, other);
    
    /// <summary>
    /// Solve the intersection point of 2 <see cref="LineBase2D"/> objects.
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase2D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// A point, if the objects intersect; <br/>
    /// Null, if the objects are parallel or overlap.
    /// </returns>
    public static Point2D? IntersectionPointOf(LineBase2D l1, LineBase2D l2)
    {
        // Solve intersection point of l1: { P, (d1) } and l2: { Q, (d2) }.
        // Solution: Assume the intersection point is R, then
        // equation set { (PR) = x * (d1), (QR) = y * (d2) } is solvable.
        // Therefore, (PQ) = (PR) - (QR) = x * (d1) - y * (d2).
        var pq = l1.FixedPoint.VectorTo(l2.FixedPoint);
        var result = Matrix.Build.DenseOfColumnArrays(l1.Direction, -1 * l2.Direction)
            .Solve(Vector.Build.DenseOfArray(pq));
        // Error solving the equation.
        if (result is not {Count: 2}) return null;
        // NaN -> Multiple solutions; +/-Infinity -> No solution
        if (result.Any(static ri => ri is double.NaN or double.NegativeInfinity or double.PositiveInfinity))
            return null;
        var r = l1.FixedPoint.MoveBy(result[0] * l1.Direction);
        // If R is not both on l1 and l2 then the objects do not intersect.
        return l1.Contains(r) && l2.Contains(r) ? r : null;
    }

    /// <summary>
    /// Solve included angle of current object and another one.
    /// </summary>
    /// <param name="other">Another <see cref="LineBase2D"/> object.</param>
    /// <returns>The included angle of the objects.</returns>
    public double IncludedAngleWith(LineBase2D other) => IncludedAngleOf(this, other);

    /// <summary>
    /// Solve included angle of 2 <see cref="LineBase2D"/> objects.
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase2D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase2D"/> object.</param>
    /// <returns>The included angle of the objects.</returns>
    public static double IncludedAngleOf(LineBase2D l1, LineBase2D l2) =>
        Math.Acos(Math.Abs(l1.Direction * l2.Direction));

    /// <summary>
    /// Determine whether current object is equivalent to another.
    /// </summary>
    /// <param name="other">Another <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are equivalent (of the same type and semantically the same); <br/>
    /// False, otherwise.
    /// </returns>
    protected abstract bool Equals(LineBase2D other);
    
    public static bool operator ==(LineBase2D line1, LineBase2D line2) => line1.Equals(line2);
    public static bool operator !=(LineBase2D line1, LineBase2D line2) => !line1.Equals(line2);

    /// <inheritdoc />
    public abstract IEnumerable<Point2D> Sample(double precision);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj is LineBase2D other && Equals(other);
    }

    /// <inheritdoc />
    /// <summary>
    /// Compute hash code by combining the hash codes of the fixed point, direction vector and type.
    /// </summary>
    public override int GetHashCode() => HashCode.Combine(FixedPoint, Direction, GetType());
}