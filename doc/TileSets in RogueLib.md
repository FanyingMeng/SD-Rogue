# TileSets in RogueLib: 

### Organizing Your Game World



## What is a TileSet?

In RogueLib, a **TIleSet** is a `HashSet<Vector2>` of grid coordinates (Vector2 positions) that represent a collection of tiles in your game world. Rather than storing a 2D array of individual tile data, TileSets let you group tiles by their purpose or state, making it easy to manage and query different aspects of your level. **TileSet**`HashSet`

Think of a TileSet as a **logical category of tiles**: 

- "all the walkable tiles," 
- "all the tiles the player has discovered,"
-  "all the walls and decorative elements," etc. 

This design pattern is both memory-efficient and conceptually clean.



## Why Use TileSets?

### Organization & Clarity

Instead of iterating through an entire 2D grid to find specific tile types, TileSets keep related tiles grouped together. Your code becomes more readable:

```
// Clear intent: check if this position is walkable
if (_walkables.Contains(newPos)) {
    _player.Pos = newPos;
}
```

versus scanning a full 2D array and checking tile properties at every position.

### Efficient Queries

HashSets provide O(1) lookup time—checking if a specific tile is in a set is instantaneous, making it ideal for gameplay queries like collision detection or visibility checks.

### Flexible Composition

TileSets can be combined using set operations (, `Intersect`, `ExceptWith`) to create complex tile groupings on the fly without duplicating data or creating new arrays. `Union`

## Common TileSet Patterns in RogueLib

### Spatial Categories

Group tiles by their map purpose:

- **`_walkables`** — Tiles the player and NPCs can move onto, the union of _floor, _tunnel, and _door
- **`_floor`** — Open floor tiles (safe, traversable)
- **`_tunnel`** — Corridor tiles connecting rooms
- **`_door`** — Doorway tiles
- **`_decor`** — Walls, pillars, and decorative non-walkable tiles

### State & Visibility

Track what the player knows and can see:

- **`_discovered`** — Tiles the player has seen before (remembered)
- **`_inFov`** — Tiles currently in the player's field of view (visible right now)

This separation allows you to draw tiles differently based on visibility: perhaps fully visible tiles are bright, while remembered-but-not-visible tiles appear dim.

## TileSets in Action: A Practical Example

When drawing the level, you want to show:

1. Decorative tiles (walls, doors, tunnels) that have been discovered
2. All tiles currently in the player's field of view (freshly visible)

Using set operations, you can construct exactly this:

```
var tilesToDraw = new TileSet(_decor);           // Start with all decor
tilesToDraw.IntersectWith(_discovered);          // Keep only discovered decor
tilesToDraw.UnionWith(_inFov);                   // Add all currently visible tiles
```

This is far cleaner than nested loops checking multiple conditions for every single tile on the map.



## Movement & Dynamic Updates

TileSets aren't static—they change as the game progresses. When the player moves:

```
_walkables.Remove(newPos);   // New position is now occupied
_walkables.Add(oldPos);      // Old position is now free
```

This keeps your walkability information synchronized with game state, and the O(1) operations mean movement checks stay fast even with complex maps.



## Design Benefits

### Decoupling

TileSets separate **tile categories** from **tile rendering**. Your logic doesn't need to know how tiles are drawn—just which positions belong to which set.

### Extensibility

Adding new tile categories (lava, ice, traps) is as simple as creating a new TileSet and populating it during level initialization.

### Debugging

Printing or visualizing a TileSet's contents is straightforward. You can quickly inspect "what tiles are walkable?" or "what's currently visible?" during development.



## Key Takeaway

TileSets embody good game architecture: they separate **concerns** (walkability vs. visibility vs. appearance), enable **efficient queries**, and keep **state synchronized**. As you expand your roguelike with features like lighting, hazards, and interactive elements, you'll likely add TileSets for each—a scalable approach that keeps complexity manageable.