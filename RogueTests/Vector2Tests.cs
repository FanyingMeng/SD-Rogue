using RogueLib;
using RogueLib.Utilities;

namespace TestProject1;

public class Vector2Tests {

    [Fact]
    public void CanConstructVector2() {
        // arrange
        var v = new Vector2(42, 73);
        
        // act 
        // assert 
        Assert.Equal(42, v.X);
        Assert.Equal(73, v.Y);
    }

    [Fact]
    public void DefaultConstructorInitializesToZero() {
        // arrange & act
        var v = new Vector2();

        // assert
        Assert.Equal(0, v.X);
        Assert.Equal(0, v.Y);
    }

    [Fact]
    public void ZeroConstantIsCorrect() {
        // arrange & act
        var zero = Vector2.Zero;

        // assert
        Assert.Equal(0, zero.X);
        Assert.Equal(0, zero.Y);
    } 
     [Fact]
    public void OneConstantIsCorrect() {
        // arrange & act
        var one = Vector2.One;
        
        // assert
        Assert.Equal(1, one.X);
        Assert.Equal(1, one.Y);
    }

    // Equality Tests
    [Fact]
    public void EqualsReturnsTrueForIdenticalVectors() {
        // arrange
        var v1 = new Vector2(5, 10);
        var v2 = new Vector2(5, 10);
        
        // act & assert
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void EqualsReturnsFalseForDifferentVectors() {
        // arrange
        var v1 = new Vector2(5, 10);
        var v2 = new Vector2(5, 11);
        
        // act & assert
        Assert.False(v1.Equals(v2));
    }

    [Fact]
    public void EqualityOperatorWorksCorrectly() {
        // arrange
        var v1 = new Vector2(3, 7);
        var v2 = new Vector2(3, 7);
        var v3 = new Vector2(3, 8);
        
        // act & assert
        Assert.True(v1 == v2);
        Assert.False(v1 == v3);
    }

    [Fact]
    public void InequalityOperatorWorksCorrectly() {
        // arrange
        var v1 = new Vector2(3, 7);
        var v2 = new Vector2(3, 7);
        var v3 = new Vector2(3, 8);
        
        // act & assert
        Assert.False(v1 != v2);
        Assert.True(v1 != v3);
    }

    [Fact]
    public void GetHashCodeSameForEqualVectors() {
        // arrange
        var v1 = new Vector2(5, 10);
        var v2 = new Vector2(5, 10);
        
        // act & assert
        Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
    }

    // Addition Tests
    [Fact]
    public void AdditionOperatorAddsTwoVectors() {
        // arrange
        var v1 = new Vector2(3, 4);
        var v2 = new Vector2(1, 2);
        
        // act
        var result = v1 + v2;
        
        // assert
        Assert.Equal(4, result.X);
        Assert.Equal(6, result.Y);
    }

    [Fact]
    public void AdditionWithNegativeValues() {
        // arrange
        var v1 = new Vector2(5, 10);
        var v2 = new Vector2(-3, -2);
        
        // act
        var result = v1 + v2;
        
        // assert
        Assert.Equal(2, result.X);
        Assert.Equal(8, result.Y);
    }

    // Subtraction Tests
    [Fact]
    public void SubtractionOperatorSubtractsTwoVectors() {
        // arrange
        var v1 = new Vector2(10, 15);
        var v2 = new Vector2(3, 5);
        
        // act
        var result = v1 - v2;
        
        // assert
        Assert.Equal(7, result.X);
        Assert.Equal(10, result.Y);
    }

    [Fact]
    public void SubtractionWithNegativeResult() {
        // arrange
        var v1 = new Vector2(3, 5);
        var v2 = new Vector2(10, 15);
        
        // act
        var result = v1 - v2;
        
        // assert
        Assert.Equal(-7, result.X);
        Assert.Equal(-10, result.Y);
    }

    // Multiplication Tests
    [Fact]
    public void ScalarMultiplicationLeftOperand() {
        // arrange
        var scalar = 3;
        var v = new Vector2(2, 4);
        
        // act
        var result = scalar * v;
        
        // assert
        Assert.Equal(6, result.X);
        Assert.Equal(12, result.Y);
    }

    [Fact]
    public void ScalarMultiplicationRightOperand() {
        // arrange
        var v = new Vector2(2, 4);
        var scalar = 3;
        
        // act
        var result = v * scalar;
        
        // assert
        Assert.Equal(6, result.X);
        Assert.Equal(12, result.Y);
    }

    [Fact]
    public void ScalarMultiplicationByZero() {
        // arrange
        var v = new Vector2(5, 10);
        
        // act
        var result = v * 0;
        
        // assert
        Assert.Equal(0, result.X);
        Assert.Equal(0, result.Y);
    }

    [Fact]
    public void ScalarMultiplicationByNegative() {
        // arrange
        var v = new Vector2(2, 4);
        
        // act
        var result = v * (-2);
        
        // assert
        Assert.Equal(-4, result.X);
        Assert.Equal(-8, result.Y);
    }

    // Division Tests
    [Fact]
    public void DivisionOperatorDividesByScalar() {
        // arrange
        var v = new Vector2(10, 20);
        var scalar = 2;
        
        // act
        var result = v / scalar;
        
        // assert
        Assert.Equal(5, result.X);
        Assert.Equal(10, result.Y);
    }

    [Fact]
    public void DivisionWithRemainder() {
        // arrange
        var v = new Vector2(7, 9);
        var scalar = 2;
        
        // act
        var result = v / scalar;
        
        // assert
        Assert.Equal(3, result.X);
        Assert.Equal(4, result.Y);
    }

    // Comparison Tests
    [Fact]
    public void CompareToOrdersByRowThenColumn() {
        // arrange
        var v1 = new Vector2(5, 2); // y=2, x=5
        var v2 = new Vector2(3, 2); // y=2, x=3
        
        // act
        var result = v1.CompareTo(v2);
        
        // assert
        Assert.True(result > 0); // v1 comes after v2 (same y, larger x)
    }

    [Fact]
    public void CompareToOrdersByYFirst() {
        // arrange
        var v1 = new Vector2(5, 3);  // y=3
        var v2 = new Vector2(10, 2); // y=2
        
        // act
        var result = v1.CompareTo(v2);
        
        // assert
        Assert.True(result > 0); // v1 comes after v2 (larger y)
    }

    [Fact]
    public void CompareToEqualVectors() {
        // arrange
        var v1 = new Vector2(5, 3);
        var v2 = new Vector2(5, 3);
        
        // act
        var result = v1.CompareTo(v2);
        
        // assert
        Assert.Equal(0, result);
    }

    // Length Tests
    [Fact]
    public void LengthSquaredCalculatesCorrectly() {
        // arrange
        var v = new Vector2(3, 4);
        
        // act
        var result = v.LengthSquared;
        
        // assert
        Assert.Equal(25, result); // 3² + 4² = 25
    }

    [Fact]
    public void LengthCalculatesCorrectly() {
        // arrange
        var v = new Vector2(3, 4);
        
        // act
        var result = v.Length;
        
        // assert
        Assert.Equal(5f, result); // √(3² + 4²) = 5
    }

    [Fact]
    public void LengthOfZeroVector() {
        // arrange
        var v = Vector2.Zero;
        
        // act
        var result = v.Length;
        
        // assert
        Assert.Equal(0f, result);
    }

    // Rook Length (Manhattan Distance) Tests
    [Fact]
    public void RookLengthCalculatesCorrectly() {
        // arrange
        var v = new Vector2(3, 4);
        
        // act
        var result = v.RookLength;
        
        // assert
        Assert.Equal(7, result); // |3| + |4| = 7
    }

    [Fact]
    public void RookLengthWithNegativeValues() {
        // arrange
        var v = new Vector2(-3, -4);
        
        // act
        var result = v.RookLength;
        
        // assert
        Assert.Equal(7, result);
    }

    // King Length (Chebyshev Distance) Tests
    [Fact]
    public void KingLengthCalculatesCorrectly() {
        // arrange
        var v = new Vector2(3, 5);
        
        // act
        var result = v.KingLength;
        
        // assert
        Assert.Equal(5, result); // Max(|3|, |5|) = 5
    }

    [Fact]
    public void KingLengthWithNegativeValues() {
        // arrange
        var v = new Vector2(-3, -5);
        
        // act
        var result = v.KingLength;
        
        // assert
        Assert.Equal(5, result);
    }

    // Manhattan Distance Tests
    [Fact]
    public void ManhattanDistanceBetweenTwoVectors() {
        // arrange
        var v1 = new Vector2(1, 2);
        var v2 = new Vector2(4, 6);
        
        // act
        var result = Vector2.manhattanDistance(v1, v2);
        
        // assert
        Assert.Equal(7, result); // |1-4| + |2-6| = 3 + 4 = 7
    }

    [Fact]
    public void ManhattanDistanceIdenticalVectors() {
        // arrange
        var v1 = new Vector2(5, 3);
        var v2 = new Vector2(5, 3);
        
        // act
        var result = Vector2.manhattanDistance(v1, v2);
        
        // assert
        Assert.Equal(0, result);
    }

    // IsDistanceWithin Tests
    [Fact]
    public void IsDistanceWithinReturnsTrueWhenWithinDistance() {
        // arrange
        var v1 = new Vector2(0, 0);
        var v2 = new Vector2(3, 4); // Distance = 5
        
        // act
        var result = Vector2.IsDistanceWithin(v1, v2, 5);
        
        // assert
        Assert.True(result);
    }

    [Fact]
    public void IsDistanceWithinReturnsTrueWhenExactlyAtDistance() {
        // arrange
        var v1 = new Vector2(0, 0);
        var v2 = new Vector2(3, 4); // Distance = 5
        
        // act
        var result = Vector2.IsDistanceWithin(v1, v2, 5);
        
        // assert
        Assert.True(result);
    }

    [Fact]
    public void IsDistanceWithinReturnsFalseWhenBeyondDistance() {
        // arrange
        var v1 = new Vector2(0, 0);
        var v2 = new Vector2(3, 4); // Distance = 5
        
        // act
        var result = Vector2.IsDistanceWithin(v1, v2, 4);
        
        // assert
        Assert.False(result);
    }

    // ToString Tests
    [Fact]
    public void ToStringFormatsCorrectly() {
        // arrange
        var v = new Vector2(42, 73);
        
        // act
        var result = v.ToString();
        
        // assert
        Assert.Equal("(42, 73)", result);
    }

    [Fact]
    public void ToStringWithNegativeValues() {
        // arrange
        var v = new Vector2(-5, -10);
        
        // act
        var result = v.ToString();
        
        // assert
        Assert.Equal("(-5, -10)", result);
    } 
}