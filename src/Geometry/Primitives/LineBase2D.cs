using Geometry.Extensions;

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
    internal readonly Point2D FixedPoint;
    
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
        var relativeVector = Relations.VectorBetween(FixedPoint, point);
        // The point is not even on the corresponding line.
        if (!Relations.AreParallel(Direction, relativeVector)) return false;
        // Compute the coordinate od the point on the number axis.
        var x = relativeVector * Direction;
        return Boundaries.Start <= x && x <= Boundaries.End;
    }

    /// <summary>
    /// Determine whether current object is equivalent to another.
    /// </summary>
    /// <param name="other">Another <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are equivalent (of the same type and semantically the same); <br/>
    /// False, otherwise.
    /// </returns>
    protected abstract bool Equals(LineBase2D other);
    
    /// <inheritdoc cref="LineBase2D.Equals(LineBase2D)"/>
    /// <summary>
    /// Determine whether two <see cref="LineBase2D"/> object are equivalent.
    /// </summary>
    /// <param name="line1">One <see cref="LineBase2D"/> object.</param>
    /// <param name="line2">The other <see cref="LineBase2D"/> object.</param>
    public static bool operator ==(LineBase2D line1, LineBase2D line2) => line1.Equals(line2);
    
    /// <summary>
    /// Determine whether two <see cref="LineBase2D"/> object are not equivalent.
    /// </summary>
    /// <param name="line1">One <see cref="LineBase2D"/> object.</param>
    /// <param name="line2">The other <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// False, if the objects are equivalent (of the same type and semantically the same); <br/>
    /// True, otherwise.
    /// </returns>
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