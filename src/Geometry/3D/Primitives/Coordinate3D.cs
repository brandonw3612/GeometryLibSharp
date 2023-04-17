using System.Numerics;
using Geometry.Extensions;

namespace Geometry.Primitives;

/// <summary>
/// Base class derived by <see cref="Point3D"/> and <see cref="Vector3D"/>.
/// Presenting a coordinate in 2-dimensional space.
/// </summary>
public abstract class Coordinate3D
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
    /// The Z coordinate.
    /// </summary>
    public double Z { get; }

    /// <summary>
    /// Construct from X, Y and Z coordinates.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="z">The Z coordinate.</param>
    protected Coordinate3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Construct from a double tuple containing all coordinates.
    /// </summary>
    /// <param name="t">The coordinate double tuple.</param>
    protected Coordinate3D((double X, double Y, double Z) t) : this(t.X, t.Y, t.Z)
    {
        // Nothing else here.
    }
    
    /// <summary>
    /// Construct from a <see cref="Vector3"/> object using its coordinates.
    /// </summary>
    /// <param name="v">The coordinate <see cref="Vector3"/> object.</param>
    protected Coordinate3D(Vector3 v) : this(v.X, v.Y, v.Z)
    {
        // Nothing else here.
    }

    /// <summary>
    /// Construct from an array containing coordinates.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the length of the array is not 3.</exception>
    /// <param name="array">An array with exactly 3 elements, representing all coordinates.</param>
    protected Coordinate3D(double[] array)
    {
        if (array.Length is not 3)
            throw new ArgumentException(
                "An array containing exactly 3 elements should be provided to create a coordinate.");
        X = array[0];
        Y = array[1];
        Z = array[2];
    }

    /// <summary>
    /// Validate value equality for current coordinate and another coordinate.
    /// </summary>
    /// <param name="other">Another <see cref="Coordinate3D"/> object to be compared.</param>
    /// <returns>
    /// True, if the coordinates can be considered equal; <br/>
    /// False, otherwise.
    /// </returns>
    private bool Equals(Coordinate3D other) => X.IsApproximatelyEqualTo(other.X) && Y.IsApproximatelyEqualTo(other.Y) &&
                                               Z.IsApproximatelyEqualTo(other.Z);

    /// <summary>
    /// Validate value equality for current coordinate and another object.
    /// </summary>
    /// <param name="obj">Another object to be compared.</param>
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
            Coordinate3D c => this == c && GetType() == c.GetType(),
            ValueTuple<double, double, double> t => this == t,
            Vector3 v => this == v,
            _ => false
        };
    }

    /// <summary>
    /// Compute hash code by combining the hash codes of all coordinates.
    /// </summary>
    /// <returns>Hash code of the coordinate.</returns>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    /// <summary>
    /// Convert the coordinate to a string in the form below. <br/>
    /// (X, Y, Z)
    /// </summary>
    /// <returns>String representing the coordinate.</returns>
    public override string ToString() => $"({X}, {Y}, {Z})";


    #region static comparation operators

    // Equality comparers between Coordinate3D objects.
    public static bool operator ==(Coordinate3D c1, Coordinate3D c2) => c1.Equals(c2);
    public static bool operator !=(Coordinate3D c1, Coordinate3D c2) => !c1.Equals(c2);

    // Equality comparers between Coordinate3Ds and double tuples.
    public static bool operator ==(Coordinate3D c, (double X, double Y, double Z) t) =>
        c.X.IsApproximatelyEqualTo(t.X) && c.Y.IsApproximatelyEqualTo(t.Y) && c.Z.IsApproximatelyEqualTo(t.Z);
    public static bool operator !=(Coordinate3D c, (double X, double Y, double Z) t) => !(c == t);
    public static bool operator ==((double X, double Y, double Z) t, Coordinate3D c) => c == t;
    public static bool operator !=((double X, double Y, double Z) t, Coordinate3D c) => !(c == t);
    
    // Equality comparers between Coordinate3D and Vector3 objects.
    public static bool operator ==(Coordinate3D c, Vector3 v) =>
        c.X.IsApproximatelyEqualTo(v.X) && c.Y.IsApproximatelyEqualTo(v.Y) && c.Z.IsApproximatelyEqualTo(v.Z);
    public static bool operator !=(Coordinate3D c, Vector3 v) => !(c == v);
    public static bool operator ==(Vector3 v, Coordinate3D c) => c == v;
    public static bool operator !=(Vector3 v, Coordinate3D c) => !(c == v);

    #endregion
    
    #region implicit converters

    // Implicit converters converting to semantically equivalent types.
    public static implicit operator (double, double, double)(Coordinate3D c) => (c.X, c.Y, c.Z);
    public static implicit operator double[](Coordinate3D c) => new[] {c.X, c.Y, c.Z};
    public static implicit operator Vector3(Coordinate3D c) => new((float) c.X, (float) c.Y, (float) c.Z);

    #endregion
}