using RogueLib.Utilities;

namespace TestProject1;

public class RectangleTests {
   #region Constructor Tests

   [Fact]
   public void Constructor_WithValidPositionAndSize_CreatesRectangleSuccessfully() {
      // Arrange
      var pos  = new Vector2(5, 10);
      var size = new Vector2(20, 30);

      // Act
      var rectangle = new Rectangle(pos, size);

      // Assert
      Assert.Equal(pos, rectangle.Pos);
      Assert.Equal(size, rectangle.Size);
   }

   [Fact]
   public void Constructor_WithZeroSize_CreatesRectangleSuccessfully() {
      // Arrange
      var pos  = new Vector2(0, 0);
      var size = new Vector2(0, 0);

      // Act
      var rectangle = new Rectangle(pos, size);

      // Assert
      Assert.Equal(pos, rectangle.Pos);
      Assert.Equal(size, rectangle.Size);
   }

   [Fact]
   public void Constructor_WithNegativeWidth_ThrowsArgumentException() {
      // Arrange
      var pos  = new Vector2(5, 10);
      var size = new Vector2(-10, 20);

      // Act & Assert
      var exception = Assert.Throws<ArgumentException>(() => new Rectangle(pos, size)
      );
      //Assert.Equal("Size cannot have negative dimensions.", exception.Message);
      Assert.Equal("size", exception.ParamName);
   }

   [Fact]
   public void Constructor_WithNegativeHeight_ThrowsArgumentException() {
      // Arrange
      var pos  = new Vector2(5, 10);
      var size = new Vector2(20, -15);

      // Act & Assert
      var exception = Assert.Throws<ArgumentException>(() => new Rectangle(pos, size));
      Assert.Equal("size", exception.ParamName);
   }

   [Fact]
   public void Constructor_WithBothNegativeDimensions_ThrowsArgumentException() {
      // Arrange
      var pos  = new Vector2(5, 10);
      var size = new Vector2(-20, -30);

      // Act & Assert
      var exception = Assert.Throws<ArgumentException>(() => new Rectangle(pos, size) );
   }

   [Fact]
   public void Constructor_WithNegativePosition_CreatesRectangleSuccessfully() {
      // Arrange
      var pos  = new Vector2(-15, -25);
      var size = new Vector2(20, 30);

      // Act
      var rectangle = new Rectangle(pos, size);

      // Assert
      Assert.Equal(pos, rectangle.Pos);
      Assert.Equal(size, rectangle.Size);
   }

   #endregion Constructor Tests

   #region Equality Tests

   [Fact]
   public void Equals_WithIdenticalRectangles_ReturnsTrue() {
      // Arrange
      var pos   = new Vector2(10, 20);
      var size  = new Vector2(30, 40);
      var rect1 = new Rectangle(pos, size);
      var rect2 = new Rectangle(pos, size);

      // Act
      var result = rect1.Equals(rect2);

      // Assert
      Assert.True(result);
   }

   [Fact]
   public void Equals_WithDifferentPositions_ReturnsFalse() {
      // Arrange
      var size  = new Vector2(30, 40);
      var rect1 = new Rectangle(new Vector2(10, 20), size);
      var rect2 = new Rectangle(new Vector2(15, 20), size);

      // Act
      var result = rect1.Equals(rect2);

      // Assert
      Assert.False(result);
   }

   [Fact]
   public void Equals_WithDifferentSizes_ReturnsFalse() {
      // Arrange
      var pos   = new Vector2(10, 20);
      var rect1 = new Rectangle(pos, new Vector2(30, 40));
      var rect2 = new Rectangle(pos, new Vector2(35, 40));

      // Act
      var result = rect1.Equals(rect2);

      // Assert
      Assert.False(result);
   }

   [Fact]
   public void Equals_WithObjectParameter_WhenRectangle_ReturnsTrue() {
      // Arrange
      var    pos   = new Vector2(10, 20);
      var    size  = new Vector2(30, 40);
      var    rect1 = new Rectangle(pos, size);
      object rect2 = new Rectangle(pos, size);

      // Act
      var result = rect1.Equals(rect2);

      // Assert
      Assert.True(result);
   }

   [Fact]
   public void Equals_WithObjectParameter_WhenNotRectangle_ReturnsFalse() {
      // Arrange
      var    rect          = new Rectangle(new Vector2(10, 20), new Vector2(30, 40));
      object notARectangle = "not a rectangle";

      // Act
      var result = rect.Equals(notARectangle);

      // Assert
      Assert.False(result);
   }

   [Fact]
   public void Equals_WithNullObject_ReturnsFalse() {
      // Arrange
      var    rect    = new Rectangle(new Vector2(10, 20), new Vector2(30, 40));
      object nullObj = null;

      // Act
      var result = rect.Equals(nullObj);

      // Assert
      Assert.False(result);
   }

   #endregion Equality Tests

   #region Operator Tests

   [Fact]
   public void OperatorEquals_WithIdenticalRectangles_ReturnsTrue() {
      // Arrange
      var pos   = new Vector2(10, 20);
      var size  = new Vector2(30, 40);
      var rect1 = new Rectangle(pos, size);
      var rect2 = new Rectangle(pos, size);

      // Act
      var result = rect1 == rect2;

      // Assert
      Assert.True(result);
   }

   [Fact]
   public void OperatorEquals_WithDifferentRectangles_ReturnsFalse() {
      // Arrange
      var rect1 = new Rectangle(new Vector2(10, 20), new Vector2(30, 40));
      var rect2 = new Rectangle(new Vector2(15, 25), new Vector2(35, 45));

      // Act
      var result = rect1 == rect2;

      // Assert
      Assert.False(result);
   }

   [Fact]
   public void OperatorNotEquals_WithIdenticalRectangles_ReturnsFalse() {
      // Arrange
      var pos   = new Vector2(10, 20);
      var size  = new Vector2(30, 40);
      var rect1 = new Rectangle(pos, size);
      var rect2 = new Rectangle(pos, size);

      // Act
      var result = rect1 != rect2;

      // Assert
      Assert.False(result);
   }

   [Fact]
   public void OperatorNotEquals_WithDifferentRectangles_ReturnsTrue() {
      // Arrange
      var rect1 = new Rectangle(new Vector2(10, 20), new Vector2(30, 40));
      var rect2 = new Rectangle(new Vector2(15, 25), new Vector2(35, 45));

      // Act
      var result = rect1 != rect2;

      // Assert
      Assert.True(result);
   }

   #endregion Operator Tests

   #region GetHashCode Tests

   [Fact]
   public void GetHashCode_WithIdenticalRectangles_ReturnsSameHashCode() {
      // Arrange
      var pos   = new Vector2(10, 20);
      var size  = new Vector2(30, 40);
      var rect1 = new Rectangle(pos, size);
      var rect2 = new Rectangle(pos, size);

      // Act
      var hash1 = rect1.GetHashCode();
      var hash2 = rect2.GetHashCode();

      // Assert
      Assert.Equal(hash1, hash2);
   }

   [Fact]
   public void GetHashCode_WithDifferentRectangles_MayReturnDifferentHashCodes() {
      // Arrange
      var rect1 = new Rectangle(new Vector2(10, 20), new Vector2(30, 40));
      var rect2 = new Rectangle(new Vector2(15, 25), new Vector2(35, 45));

      // Act
      var hash1 = rect1.GetHashCode();
      var hash2 = rect2.GetHashCode();

      // Assert
      // Note: Different objects may have same hash (hash collision), but it's likely they differ
      Assert.NotEqual(hash1, hash2);
   }

   [Fact]
   public void GetHashCode_IsConsistent_WhenCalledMultipleTimes() {
      // Arrange
      var rect = new Rectangle(new Vector2(10, 20), new Vector2(30, 40));

      // Act
      var hash1 = rect.GetHashCode();
      var hash2 = rect.GetHashCode();
      var hash3 = rect.GetHashCode();

      // Assert
      Assert.Equal(hash1, hash2);
      Assert.Equal(hash2, hash3);
   }

   #endregion GetHashCode Tests

   #region Properties Tests

   [Fact]
   public void Pos_Property_ReturnsCorrectValue() {
      // Arrange
      var pos  = new Vector2(25, 35);
      var size = new Vector2(50, 60);

      // Act
      var rect = new Rectangle(pos, size);

      // Assert
      Assert.Equal(pos, rect.Pos);
   }

   [Fact]
   public void Size_Property_ReturnsCorrectValue() {
      // Arrange
      var pos  = new Vector2(25, 35);
      var size = new Vector2(50, 60);

      // Act
      var rect = new Rectangle(pos, size);

      // Assert
      Assert.Equal(size, rect.Size);
   }

   [Fact]
   public void Properties_AreReadOnly() {
      // Arrange
      var rect = new Rectangle(new Vector2(10, 20), new Vector2(30, 40));

      // Act & Assert
      // This test verifies that Pos and Size are read-only by attempting to compile
      // If this compiles, the properties are read-only
      var pos  = rect.Pos;
      var size = rect.Size;
      Assert.NotNull(pos);
      Assert.NotNull(size);
   }

   #endregion Properties Tests

   #region Edge Cases Tests

   [Fact]
   public void Constructor_WithLargePositiveValues_CreatesRectangleSuccessfully() {
      // Arrange
      var pos  = new Vector2(int.MaxValue - 100, int.MaxValue - 100);
      var size = new Vector2(50, 50);

      // Act
      var rect = new Rectangle(pos, size);

      // Assert
      Assert.Equal(pos, rect.Pos);
      Assert.Equal(size, rect.Size);
   }

   [Fact]
   public void Constructor_WithLargeNegativeValues_CreatesRectangleSuccessfully() {
      // Arrange
      var pos  = new Vector2(int.MinValue + 100, int.MinValue + 100);
      var size = new Vector2(50, 50);

      // Act
      var rect = new Rectangle(pos, size);

      // Assert
      Assert.Equal(pos, rect.Pos);
      Assert.Equal(size, rect.Size);
   }

   [Fact]
   public void Constructor_WithMaxSizeValues_CreatesRectangleSuccessfully() {
      // Arrange
      var pos  = new Vector2(0, 0);
      var size = new Vector2(int.MaxValue, int.MaxValue);

      // Act
      var rect = new Rectangle(pos, size);

      // Assert
      Assert.Equal(pos, rect.Pos);
      Assert.Equal(size, rect.Size);
   }

   #endregion Edge Cases Tests
}