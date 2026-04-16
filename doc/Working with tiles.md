# Vector2's Static Methods: 

# `Parse()` `getAllTiles()`



Vector2's static utility methods are essential tools for game development in RogueLib. They abstract away tedious coordinate manipulation, letting you work at a higher level of abstraction. Let's explore how each one serves game developers.



## Access Every Position on Your Map `getAllTiles()`

### What It Does

Returns every valid coordinate in your game grid. For a console game, this means every position from (0,0) to the maximum width and height of your game world. `getAllTiles()`

### Why It's Powerful

Instead of manually writing nested loops to iterate through every grid position:

```
for (int x = 0; x < width; x++) {
    for (int y = 0; y < height; y++) {
        // Do something with position (x, y)
    }
}
```

You simply call and work with the collection directly. This is especially useful when combined with LINQ queries. `getAllTiles()`



### Game Development Applications

**Field of View Calculations** One of the most common uses is computing what a character can see. Rather than manually iterating rows and columns, you filter all tiles based on distance or line-of-sight:

```
var visibleTiles = Vector2.getAllTiles()
    .Where(tile => (playerPos - tile).RookLength < senseRadius)
    .ToHashSet();
```

This reads like natural English: "Get all tiles where the distance from the player is less than the sense radius." The implementation details (which algorithm, which distance metric) are abstracted away.



**Spawning Enemies or Items** You might need to find a random walkable tile for spawning:

```
var randomSpawnTile = Vector2.getAllTiles()
    .Where(t => walkableTiles.Contains(t))
    .OrderBy(_ => new Random().Next())
    .First();
```



**Level Queries** Checking "what tiles meet this condition?" becomes straightforward:

```
var dangerousTiles = Vector2.getAllTiles()
    .Where(t => lavaTiles.Contains(t) || trapTiles.Contains(t))
    .ToHashSet();
```



### Educational Value

Using teaches students the power of **functional programming** and **LINQ** in C#. Instead of imperative loops with side effects, you declare what you want, not how to get it. This is a fundamental shift in programming thinking. `getAllTiles()`

------

------

## `Parse()` — Convert Strings into Game Maps

### What It Does

`Parse()` takes a **map string** (typically ASCII art representing your dungeon layout) and yields each character along with its grid coordinate. This bridges the gap between human-readable map design and machine-readable coordinate data.

### Why It's Powerful

Map design becomes visual and intuitive. Instead of manually creating 2D arrays or hardcoding tile positions, designers write maps as strings:

```
########
#......#
#.....+#
########
```

And your code effortlessly converts this into usable game data. No counting indices manually—the parser handles it.

### Game Development Applications

**Level Initialization** When loading a level, you iterate through the map string and categorize each tile:

```
foreach (var (character, position) in Vector2.Parse(mapString)) {
    if (character == '.') floor.Add(position);
    else if (character == '+') doors.Add(position);
    else if (character == '#') walls.Add(position);
}
```

Each character becomes a rule about what tile type lives at that coordinate. Adding a new tile type? Just add another condition.

**Procedural Variations** You could generate multiple map variations from a template:

```
var baseMap = "######\n#....#\n######";
var variant1 = Vector2.Parse(baseMap)
    .Where(t => t.character != '#') // Remove walls
    .Select(t => (character: '.', position: t.position));
```

**Map Validation** During development, you might check that your maps are valid:

```
var hasSpawnPoint = Vector2.Parse(mapString)
    .Any(t => t.character == '@');  // @ marks player spawn
```

### Readability & Maintenance

The biggest win is **clarity**. A game designer can look at the map file and understand the layout instantly. No array indexing confusion, no off-by-one errors from manual coordinate entry. Changes to the map are visible and straightforward.

------

------

## How They Work Together

These methods shine when combined. A typical level initialization flow:

1. **`Parse()`** converts your ASCII map into (character, position) pairs
2. Categorize each character into appropriate TileSets
3. **`getAllTiles()`** feeds into visibility calculations
4. Use those calculations to determine which tiles to render and which to hide

This pipeline keeps your code clean and focused on game logic rather than coordinate arithmetic.

## Teaching C# Concepts

Both methods are excellent teaching tools:

- **`getAllTiles()`** demonstrates generator methods, LINQ, and functional composition
- **`Parse()`** shows string parsing, tuples, and iterators
- Together, they show how abstractions let you think in terms of your **domain** (maps, visibility) rather than implementation details (loops, indices)

As a student, understanding these methods reveals how professional code avoids repetitive, error-prone manual work through well-designed utilities.