# RogueLib.Vector2 

## Overview

Is a lightweight, immutable, value-type structure in the namespace designed for 2D grid-based game development. It provides integer-based coordinate representation with comprehensive support for arithmetic operations, distance calculations, and directional vectors.

Vector2 is IEquatable and IComparable. 
## Basic Usage

### Creating Vectors

using RogueLib.Utilities;  

```C#
// Create a vector with specific coordinates  
Vector2 position = new Vector2(10, 5);  
  
// Create a vector at origin (0, 0)  
Vector2 origin = new Vector2();  
  
// Use predefined zero vector  
Vector2 zero = Vector2.Zero;  
Vector2 one = Vector2.One;
```

### Properties

| Property | Type  | Description                        |
| -------- | ----- | ---------------------------------- |
| `X`      | `int` | The horizontal coordinate (column) |
| `Y`      | `int` | The vertical coordinate (row)      |

### Common Vectors

provides predefined static vectors for common positions and directions: `Vector2`

```C#
// Cardinal directions  
Vector2 north = Vector2.N;   // Move up  
Vector2 south = Vector2.S;    // Move down  
Vector2 east = Vector2.E;       // Move right  
Vector2 west = Vector2.W;     // Move left  
  
// Diagonal directions  
Vector2 northeast = Vector2.NE;  
Vector2 northwest = Vector2.NW;  
Vector2 southeast = Vector2.SE;  
Vector2 southwest = Vector2.SW;  
  
// Special vectors  
Vector2 zero = Vector2.Zero;  // (0, 0)  
Vector2 one = Vector2.One;    // (1, 1)
```

## Arithmetic Operations

### Addition and Subtraction

Combine vectors to calculate new positions:

```C#
Vector2 pos = new Vector2(5, 5);  
Vector2 offset = new Vector2(2, -1);  
  
Vector2 newPos = pos + offset;  // (7, 4)  
Vector2 diff = pos - offset;    // (3, 6)
```

### Scalar Multiplication and Division

Scale vectors by integer values:

```C#
Vector2 direction = Vector2.E;  // (1, 0)  
Vector2 distance5 = 5 * direction;  // (5, 0)  
Vector2 distance10 = direction * 10; // (10, 0)  
  
Vector2 halfDistance = new Vector2(10, 20) / 2;  // (5, 10)
```

## Comparison and Equality

### Equality Operators

```C#
Vector2 a = new Vector2(5, 5);  
Vector2 b = new Vector2(5, 5);  
Vector2 c = new Vector2(6, 5);  
  
if (a == b) { /* true */ }  
if (a != c) { /* true */ }
```

### Sorting and Comparison

Vectors are comparable and sorted by row (Y) first, then column (X):

```C#
List\<Vector2> positions = new()   
{   
    new Vector2(0, 2),  
    new Vector2(5, 1),  
    new Vector2(3, 1)  
};  
  
positions.Sort();  // Sorted: (5,1), (3,1), (0,2)
```

## Distance Calculations


### Rook Length 

Also known as **Manhattan distance** or **taxicab distance**—distance without diagonal movement (useful for grid-based games):

Distance for movement on a chessboard without diagonals:

```C#
Vector2 offset = new Vector2(3, 4);  
int distance = offset.RookLength;  // 7
```


### King Length (Chebyshev Distance)

Distance for movement on a chessboard with diagonal movement allowed:

```C#
Vector2 offset = new Vector2(3, 4);  
int distance = offset.KingLength;  // 4 (max of absolute values)
```

### Euclidean Distance

Vector2 had two measures of Euclidean distance: the normal float given by sqrt(x^2 + y^2), and length squared, which is just normal distance without taking the square root. The squared distance avoids the need for floats and is much faster. If you are just comparing distances, use the distance squared. 

## Parsing Text to Coordinates

Convert text strings (including multi-line maps) into character-coordinate pairs:

```C#
string map = """  
    ..#..    .....    #...#    """;  
  
foreach (var (character, position) in Vector2.Parse(map)) {  
    if (character == '#') {  
        Console.WriteLine($"Wall at {position}");  
    }  
}
```

The `Parse()` method:

- Returns an `IEnumerable<(char, Vector2)>` of character-position tuples
- Automatically handles and `\r\n` line endings `\n`
- Resets X coordinate to 0 at each newline
- Increments Y coordinate for each new line
- Skips whitespace characters but includes their coordinates

## Generating Grid Positions

Get all tile positions within a grid:

```C#

// Get all tiles in an 80×25 grid (default console size)  
foreach (var tile in Vector2.getAllTiles()) {  
    // Process each position  
}  
  
// Get all tiles in a custom-sized grid  
foreach (var tile in Vector2.getAllTiles(40, 30)) {  
    // Process each 40×30 grid position  
}
```



## Best Practices

1. **Use predefined directions** for common movements:


```C#
// Good  
playerPos += Vector2.N;  
  
// Less readable  
playerPos += new Vector2(0, -1);
```

2. **Leverage Parse() for map data**:

```C#
var walls = Vector2.Parse(mapString)  
    .Where(entry => entry.ch == '#')  
    .Select(entry => entry.Item2)  
    .ToList();
```

3. **Choose the right distance metric**:
    - Use for grid-based movement (no diagonals) **RookLength**
    - Use for chess-like movement (with diagonals) **KingLength**
    - Use for accurate Euclidean distances **Length**
4. **Treat Vector2 as immutable** 
	```C#
		Vector2 pos = new Vector2(5, 5);  
		Vector2 newPos = pos + Vector2.N;  // Don't modify pos directly
	```


## Interface Compliance

implements: `Vector2`

- **`IEquatable<Vector2>`** for efficient equality comparisons
- **`IComparable<Vector2>`** for sorting collections of vectors

This makes it suitable for use in collections, dictionaries, and LINQ queries.



