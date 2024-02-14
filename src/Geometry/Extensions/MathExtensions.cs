namespace Geometry.Extensions;

/// <summary>
/// Extension methods that simplify math calculations globally.
/// </summary>
public static class MathExtensions
{
    /// <summary>
    /// Determine whether two real numbers can be approximately considered equal. <br/>
    /// Maximum allowed error is defined in <see cref="Constants.ApproximateEqualError" />.
    /// </summary>
    /// <returns>
    /// True, if the numbers can be considered as equal; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool IsApproximatelyEqualTo(this double someDouble, double another) =>
        Math.Abs(someDouble - another) < Constants.ApproximateEqualError;

    /// <summary>
    /// Determine whether a real number can be approximately considered as 0. <br/>
    /// Maximum allowed error is defined in <see cref="Constants.ApproximateEqualError" />.
    /// </summary>
    /// <returns>
    /// True, if the number can be considered as 0; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool IsNearZero(this double someDouble) => someDouble.IsApproximatelyEqualTo(0d);
}