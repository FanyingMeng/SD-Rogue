namespace RogueLib.Utilities;

public struct Rectangle {
   public Vector2 Pos  { get; }
   public Vector2 Size { get; }

   public Rectangle(Vector2 pos, Vector2 size) {
      if (size.X < 0 || size.Y < 0)
         throw new ArgumentException("Size cannot have negative dimensions.", nameof(size));

      Pos  = pos;
      Size = size;
   }

   public          bool Equals(Rectangle other)                      => Pos == other.Pos && Size == other.Size;
   public override bool Equals(object?   obj)                        => obj is Rectangle other && Equals(other);
   public override int  GetHashCode()                                => HashCode.Combine(Pos, Size);
   public static   bool operator ==(Rectangle left, Rectangle right) => left.Equals(right);
   public static   bool operator !=(Rectangle left, Rectangle right) => !left.Equals(right);
}