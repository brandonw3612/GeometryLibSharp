namespace Geometry;

/// <summary>
/// Constants involved in this library.
/// </summary>
public static class Constants
{
    /// <summary>
    /// The maximum allowed error for two real numbers to be considered equal in the library.
    /// <example>
    /// When Math.Abs(<b>x</b> - <b>y</b>) is less than <see cref="ApproximateEqualError"/>, we consider <b>x</b> and <b>y</b> as equal.
    /// </example>
    /// </summary>
    public const double ApproximateEqualError = 1e-5d;
}