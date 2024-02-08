using System.Numerics;
using Geometry.Extensions;

namespace Geometry.Primitives;

/// <summary>
/// Base class derived by <see cref="Point2D"/> and <see cref="Vector2D"/>.
/// Presenting a coordinate in 2-dimensional space.
/// </summary>
public abstract class Coordinate2D
{
    /// <summary>
    /// The X coordinate.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// The Y coordinate.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Construct from X and Y coordinates.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    protected Coordinate2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Construct from a double tuple containing both coordinates.
    /// </summary>
    /// <param name="t">The coordinate double tuple.</param>
    protected Coordinate2D((double X, double Y) t) : this(t.X, t.Y)
    {
        // Nothing else here.
    }
    
    /// <summary>
    /// Construct from a <see cref="Vector2"/> object using its coordinates.
    /// </summary>
    /// <param name="v">The coordinate <see cref="Vector2"/> object.</param>
    protected Coordinate2D(Vector2 v) : this(v.X, v.Y)
    {
        // Nothing else here.
    }

    /// <summary>
    /// Construct from an array containing coordinates.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the length of the array is not 2.</exception>
    /// <param name="array">An array with exactly 2 elements, representing both coordinates.</param>
    protected Coordinate2D(double[] array)
    {
        if (array.Length is not 2)
            throw new ArgumentException(
                "An array containing exactly 2 elements should be provided to create a coordinate.");
        X = array[0];
        Y = array[1];
    }

    /// <summary>
    /// Validate value equality for current coordinate and another coordinate.
    /// </summary>
    /// <param name="other">Another <see cref="Coordinate2D"/> object to be compared.</param>
    /// <returns>
    /// True, if the coordinates can be considered equal;<br/>
    /// False, otherwise.
    /// </returns>
    private bool Equals(Coordinate2D other) => X.IsApproximatelyEqualTo(other.X) && Y.IsApproximatelyEqualTo(other.Y);

    /// <inheritdoc />
    /// <returns>
    /// True, if the other object is <br/> (1) of semantically equivalent type, or <br/>
    /// (2) is the same type as the current object and shares the same coordinates and is equal to this coordinate; <br/>
    /// False, otherwise.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, null)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj switch
        {
            Coordinate2D c => this == c && GetType() == c.GetType(),
            ValueTuple<double, double> t => this == t,
            Vector2 v => this == v,
            _ => false
        };
    }

    /// <inheritdoc />
    /// <summary>
    /// Compute hash code by combining the hash codes of both coordinates.
    /// </summary>
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    /// <summary>
    /// Convert the coordinate to a string in the form below. <br/>
    /// (X, Y)
    /// </summary>
    public override string ToString() => $"({X}, {Y})";


    #region static comparation operators

    // Equality comparers between Coordinate2D objects.
    public static bool operator ==(Coordinate2D c1, Coordinate2D c2) => c1.Equals(c2);
    public static bool operator !=(Coordinate2D c1, Coordinate2D c2) => !c1.Equals(c2);

    // Equality comparers between Coordinate2Ds and double tuples.
    public static bool operator ==(Coordinate2D c, (double X, double Y) t) =>
        c.X.IsApproximatelyEqualTo(t.X) && c.Y.IsApproximatelyEqualTo(t.Y);
    public static bool operator !=(Coordinate2D c, (double X, double Y) t) => !(c == t);
    public static bool operator ==((double X, double Y) t, Coordinate2D c) => c == t;
    public static bool operator !=((double X, double Y) t, Coordinate2D c) => !(c == t);
    
    // Equality comparers between Coordinate2D and Vector2 objects.
    public static bool operator ==(Coordinate2D c, Vector2 v) =>
        c.X.IsApproximatelyEqualTo(v.X) && c.Y.IsApproximatelyEqualTo(v.Y);
    public static bool operator !=(Coordinate2D c, Vector2 v) => !(c == v);
    public static bool operator ==(Vector2 v, Coordinate2D c) => c == v;
    public static bool operator !=(Vector2 v, Coordinate2D c) => !(c == v);

    #endregion
    
    #region implicit converters

    // Implicit converters converting to semantically equivalent types.
    public static implicit operator (double, double)(Coordinate2D c) => (c.X, c.Y);
    public static implicit operator double[](Coordinate2D c) => new[] {c.X, c.Y};
    public static implicit operator Vector2(Coordinate2D c) => new((float) c.X, (float) c.Y);

    #endregion
}