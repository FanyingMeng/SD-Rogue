namespace RogueLib.Utilities;

// Immutable 2D vector class with operators for vector math and comparison 
// Equatable and Comparable, with predefined vectors for common directions
// and a utility functions for calculating distances, generate a sequence of 
// all addresses in a grid. 
// 
public struct Vector2 : IEquatable<Vector2>, IComparable<Vector2> {
   public int X { get; }
   public int Y { get; }

   public Vector2(int x, int y) => (X, Y) = (x, y);
   public Vector2() : this(0, 0) { }

   // predefined vectors
   public static readonly Vector2 Zero = new Vector2(0, 0);
   public static readonly Vector2 One  = new Vector2(1, 1);

   // 4 Cardinal directions
   public static readonly Vector2 S = new Vector2(0, 1);
   public static readonly Vector2 E = new Vector2(1, 0);
   public static readonly Vector2 N = -1 * S;
   public static readonly Vector2 W = -1 * E;

   // 4 diagonal directions
   public static readonly Vector2 NE = N + E;
   public static readonly Vector2 NW = N + W;
   public static readonly Vector2 SE = S + E;
   public static readonly Vector2 SW = S + W;


   public override string ToString() => $"({X}, {Y})";

   // IEquatable<Vector2>
   public          bool Equals(Vector2 other) => (X == other.X) && (Y == other.Y);
   public override bool Equals(object? obj)   => obj is Vector2 other && Equals(other);
   public override int  GetHashCode()         => HashCode.Combine(X, Y);

   // IComparable<Vector2> (order by row, then column, y is the row index, x is the column index)
   public int CompareTo(Vector2 other)
      => Y.CompareTo(other.Y) != 0 ? Y.CompareTo(other.Y) : X.CompareTo(other.X);

   // Operator overloads +, -, scalar multiplication, division
   public static Vector2 operator +(Vector2 left, Vector2 right) => new(left.X + right.X, left.Y + right.Y);
   public static Vector2 operator -(Vector2 left, Vector2 right) => new(left.X - right.X, left.Y - right.Y);
   public static Vector2 operator *(int     left, Vector2 right) => new(right.X * left, right.Y * left);
   public static Vector2 operator *(Vector2 left, int     right) => new(left.X * right, left.Y * right);
   public static Vector2 operator /(Vector2 left, int     right) => new(left.X / right, left.Y / right);


   // Equality operators
   public static bool operator ==(Vector2 a, Vector2 b) => a.Equals(b);
   public static bool operator !=(Vector2 a, Vector2 b) => !a.Equals(b);


   // Utilities
   public static int manhattanDistance(Vector2 a, Vector2 b)
      => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

   /// <summary>
   /// Gets whether the distance between the two given <see cref="Vector2">Vector2</see> is within
   /// the given distance.
   /// </summary>
   /// <param name="a">First Vector2.</param>
   /// <param name="b">Second Vector2.</param>
   /// <param name="distance">Maximum distance between them.</param>
   /// <returns><c>true</c> if the distance between <c>a</c> and <c>b</c> is less than or equal to <c>distance</c>.</returns>
   public static bool IsDistanceWithin(Vector2 a, Vector2 b, int distance)
      => (a - b).LengthSquared <= (distance * distance);

   /// <summary>
   /// Gets the absolute magnitude of the Vec squared.
   /// </summary>
   public int LengthSquared => (X * X) + (Y * Y);

   /// <summary>
   /// Gets the absolute magnitude of the Vec.
   /// </summary>
   public float Length => (float)Math.Sqrt(LengthSquared);

   /// <summary>
   /// Distance between two points on a chess board without diagonal movement.
   /// Also known as Manhattan or taxicab distance.
   /// </summary>
   public int RookLength => Math.Abs(X) + Math.Abs(Y);

   /// <summary>
   /// Distance between two points on a chess board, with diagonal movement.
   /// Also known as Chebyshev distance.
   /// </summary>
   public int KingLength => Math.Max(Math.Abs(X), Math.Abs(Y));


   // Enumerable of all grid points in a range
   public static IEnumerable<Vector2> getAllTiles(int w = 78, int h = 25) {
      var rows = Enumerable.Range(0, h);
      var cols = Enumerable.Range(0, w);
      foreach (var row in rows)
      foreach (var col in cols) {
         yield return new Vector2(col, row);
      }
   }


   // ----------------------------------------------------------------------------
   //  Parse a string into a grid of characters with coordinates
   //  avoid extra memory allocations by walking the string character by character
   // ----------------------------------------------------------------------------
   public static IEnumerable<(char ch, Vector2 )> Parse(string text) {
      if (text is null)
         throw new ArgumentNullException(nameof(text));

      int x = 0;
      int y = 0;

      for (int i = 0; i < text.Length; i++) {
         char ch = text[i];

         if (ch == '\r') {
            if (i + 1 < text.Length && text[i + 1] == '\n') {
               i++;
            }

            x = 0;
            y++;
            continue;
         }

         if (ch == '\n') {
            x = 0;
            y++;
            continue;
         }

         yield return (ch, new Vector2(x, y));
         x++;
      }
   }
}