using Geometry.Extensions;
using Geometry.Primitives;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Geometry;

/// <summary>
/// Utilities for common geometry elements' relations.
/// </summary>
public static class Relations
{
    /// <summary>
    /// Determine whether 2 <see cref="LineBase2D"/> objects are parallel. 
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase2D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool AreParallel(LineBase2D l1, LineBase2D l2) => AreParallel(l1.Direction, l2.Direction);

    /// <summary>
    /// Determine whether 2 <see cref="LineBase3D"/> objects are parallel. 
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase3D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase3D"/> object.</param>
    /// <returns>
    /// True, if the objects are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool AreParallel(LineBase3D l1, LineBase3D l2) => AreParallel(l1.Direction, l2.Direction);
    
    /// <summary>
    /// Determine whether 2 vectors are parallel.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>
    /// True, if the vectors are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    // v1 = (x1, y1), v2 = (x2, y2)
    // v1 and v2 are parallel <=>
    //     1) v2 is not zero, v1 = k * v2, or
    //     2) v2 is zero ( v2 = (0, 0) )
    // That is to say, x1 = k * x2, y1 = k * y2, or x2 = y2 = 0.
    // Therefore, x1 * y2 = k * x2 * y2 = x2 * y1, which fits both conditions.
    public static bool AreParallel(Vector2D v1, Vector2D v2) => (v1.X * v2.Y).IsApproximatelyEqualTo(v1.Y * v2.X);
    
    /// <summary>
    /// Determine whether 2 vectors are parallel.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>
    /// True, if the vectors are parallel; <br/>
    /// False, otherwise.
    /// </returns>
    /// v1 is parallel to v2 => v1 x v2 = (0)
    public static bool AreParallel(Vector3D v1, Vector3D v2) => (v1 ^ v2) == Vector3D.Zero;
    
    /// <summary>
    /// Determine whether 2 <see cref="LineBase2D"/> objects are perpendicular. 
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase2D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase2D"/> object.</param>
    /// <returns>
    /// True, if the objects are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool ArePerpendicular(LineBase2D l1, LineBase2D l2) => ArePerpendicular(l1.Direction, l2.Direction);
    
    /// <summary>
    /// Determine whether 2 <see cref="LineBase3D"/> objects are perpendicular. 
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase3D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase3D"/> object.</param>
    /// <returns>
    /// True, if the objects are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool ArePerpendicular(LineBase3D l1, LineBase3D l2) => ArePerpendicular(l1.Direction, l2.Direction);
    
    /// <summary>
    /// Determine whether 2 vectors are perpendicular.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>
    /// True, if the vectors are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool ArePerpendicular(Vector2D v1, Vector2D v2) => (v1 * v2).IsNearZero();
    
    /// <summary>
    /// Determine whether 2 vectors are perpendicular.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>
    /// True, if the vectors are perpendicular; <br/>
    /// False, otherwise.
    /// </returns>
    public static bool ArePerpendicular(Vector3D v1, Vector3D v2) => (v1 * v2).IsNearZero();
    
    /// <summary>
    /// Gets the distance between a <see cref="Line2D"/> and a <see cref="Point2D"/> element.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <param name="point">The point.</param>
    /// <returns>Distance between the line and the point.</returns>
    public static double DistanceBetween(Line2D line, Point2D point)
    {
        // FixPoint -> F, point -> P, direction -> v_d
        // Foot of the perpendicular -> Q
        // v_FQ = lambda * v_d
        // pq = |v_FP| * sin<v_FP, v_FQ> = |v_FP| * sqrt( 1 - ( cos<v_FP, v_FQ> )^2 )
        //    = |v_FP| * sqrt( 1 - ( v_FP * v_FQ / ( |v_FP| * |v_FQ| ) )^2 )
        //    = |v_FP| * sqrt( 1 - ( lambda * v_FP * v_d / ( |v_FP| * lambda * |v_d| ) )^2 )
        //    = sqrt( (v_FP)^2 - ( v_FP * v_d / |v_d| )^2 )
        var fp = VectorBetween(line.FixedPoint, point);
        return Math.Sqrt(fp * fp - Math.Pow(line.Direction.Normalized * fp, 2));
    }

    /// <inheritdoc cref="DistanceBetween(Geometry.Line2D,Geometry.Point2D)"/>
    public static double DistanceBetween(Point2D point, Line2D line) => DistanceBetween(line, point);
    
    /// <summary>
    /// Gets the distance between a <see cref="Line3D"/> and a <see cref="Point3D"/> element.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <param name="point">The point.</param>
    /// <returns>Distance between the line and the point.</returns>
    public static double DistanceBetween(Line3D line, Point3D point)
    {
        // FixPoint -> F, point -> P, direction -> v_d
        // Foot of the perpendicular -> Q
        // v_FQ = lambda * v_d
        // pq = |v_FP| * sin<v_FP, v_FQ> = |v_FP| * sqrt( 1 - ( cos<v_FP, v_FQ> )^2 )
        //    = |v_FP| * sqrt( 1 - ( v_FP * v_FQ / ( |v_FP| * |v_FQ| ) )^2 )
        //    = |v_FP| * sqrt( 1 - ( lambda * v_FP * v_d / ( |v_FP| * lambda * |v_d| ) )^2 )
        //    = sqrt( (v_FP)^2 - ( v_FP * v_d / |v_d| )^2 )
        var fp = VectorBetween(line.FixedPoint, point);
        return Math.Sqrt(fp * fp - Math.Pow(line.Direction.Normalized * fp, 2));
    }

    /// <inheritdoc cref="DistanceBetween(Geometry.Line3D,Geometry.Point3D)"/>
    public static double DistanceBetween(Point3D point, Line3D line) => DistanceBetween(line, point);
    
    /// <summary>
    /// Calculate distance between 2 lines.
    /// </summary>
    /// <param name="line1">The first line.</param>
    /// <param name="line2">The second line.</param>
    /// <returns>
    /// Distance, if the lines are parallel or equivalent.
    /// </returns>
    /// <exception cref="Exception">Thrown if the lines are not parallel.</exception>
    public static double DistanceBetween(Line2D line1, Line2D line2)
    {
        return AreParallel(line1, line2)
            ? DistanceBetween(line1, line2.FixedPoint)
            : throw new Exception("You cannot calculate the distance between 2 non-parallel lines.");
    }
    
    /// <summary>
    /// Calculate distance between 2 lines.
    /// </summary>
    /// <param name="line1">The first line.</param>
    /// <param name="line2">The second line.</param>
    /// <returns>
    /// Distance, if the lines are parallel or equivalent.
    /// </returns>
    /// <exception cref="Exception">Thrown if the lines are not parallel.</exception>
    public static double DistanceBetween(Line3D line1, Line3D line2)
    {
        return AreParallel(line1, line2)
            ? DistanceBetween(line1, line2.FixedPoint)
            : throw new Exception("You cannot calculate the distance between 2 non-parallel lines.");
    }
    
    /// <summary>
    /// Calculate distance between 2 points.
    /// </summary>
    /// <param name="p1">The first point.</param>
    /// <param name="p2">The second point.</param>
    /// <returns>Distance between the 2 points.</returns>
    public static double DistanceBetween(Point2D p1, Point2D p2) => VectorBetween(p1, p2).Length;
    
    /// <summary>
    /// Calculate distance between 2 points.
    /// </summary>
    /// <param name="p1">The first point.</param>
    /// <param name="p2">The second point.</param>
    /// <returns>Distance between the 2 points.</returns>
    public static double DistanceBetween(Point3D p1, Point3D p2) => VectorBetween(p1, p2).Length;

    /// <summary>
    /// Solve included angle of 2 <see cref="LineBase2D"/> objects.
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase2D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase2D"/> object.</param>
    /// <returns>The included angle of the objects.</returns>
    public static double IncludedAngleOf(LineBase2D l1, LineBase2D l2) =>
        Math.Acos(Math.Abs(l1.Direction * l2.Direction));
    
    /// <summary>
    /// Solve included angle of 2 <see cref="LineBase3D"/> objects.
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase3D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase3D"/> object.</param>
    /// <returns>The included angle of the objects.</returns>
    public static double IncludedAngleOf(LineBase3D l1, LineBase3D l2) =>
        Math.Acos(Math.Abs(l1.Direction * l2.Direction));
    
    /// <summary>
    /// Included angle of 2 vectors in radians.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Included angle of the vectors in radians.</returns>
    public static double IncludedAngleOf(Vector2D v1, Vector2D v2)
    {
        if (v1 == Vector2D.Zero || v2 == Vector2D.Zero) return double.NaN;
        return Math.Acos(v1 * v2 / v1.Length / v2.Length);
    }
    
    /// <summary>
    /// Included angle of 2 vectors in radians.
    /// </summary>
    /// <param name="v1">The first vector</param>
    /// <param name="v2">The second vector.</param>
    /// <returns>Included angle of the vectors in radians.</returns>
    public static double IncludedAngleOf(Vector3D v1, Vector3D v2)
    {
        if (v1 == Vector3D.Zero || v2 == Vector3D.Zero) return double.NaN;
        return Math.Acos(v1 * v2 / v1.Length / v2.Length);
    }
    
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
        var pq = VectorBetween(l1.FixedPoint, l2.FixedPoint);
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
    /// Solve the intersection point of 2 <see cref="LineBase3D"/> objects.
    /// </summary>
    /// <param name="l1">The first <see cref="LineBase3D"/> object.</param>
    /// <param name="l2">The second <see cref="LineBase3D"/> object.</param>
    /// <returns>
    /// A point, if the objects intersect; <br/>
    /// Null, if the objects are parallel or overlap.
    /// </returns>
    public static Point3D? IntersectionPointOf(LineBase3D l1, LineBase3D l2)
    {
        // Solve intersection point of l1: { P, (d1) } and l2: { Q, (d2) }.
        // Solution: Assume the intersection point is R, then
        // equation set { (PR) = x * (d1), (QR) = y * (d2) } is solvable.
        // Therefore, (PQ) = (PR) - (QR) = x * (d1) - y * (d2).
        var pq = VectorBetween(l1.FixedPoint, l2.FixedPoint);
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
    /// Vector from a specific point to another one.
    /// </summary>
    /// <param name="start">The start of the vector.</param>
    /// <param name="end">The end of the vector.</param>
    /// <returns>The vector between the points.</returns>
    public static Vector2D VectorBetween(Point2D start, Point2D end) => (end.X - start.X, end.Y - start.Y);

    /// <summary>
    /// Vector from a specific point to another one.
    /// </summary>
    /// <param name="start">The start of the vector.</param>
    /// <param name="end">The end of the vector.</param>
    /// <returns>The vector between the points.</returns>
    public static Vector3D VectorBetween(Point3D start, Point3D end) =>
        (end.X - start.X, end.Y - start.Y, end.Z - start.Z);
}