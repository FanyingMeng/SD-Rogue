
## Overview

`ScreenBuff` is a double-buffered console rendering system in the namespace designed for flicker-free, efficient console-based game graphics. It implements the **painter's algorithm**—content drawn last appears on top—and optimizes console updates by only redrawing changed regions. `RogueLib.Utilities`

## Key Features

- **Double Buffering**: Renders to an off-screen buffer before displaying, eliminating flicker
- **Selective Rendering**: Only updates console cells that have changed since the last frame
- **Painter's Algorithm**: Later drawing operations override earlier ones
- **Color Support**: Full ConsoleColor support for foreground colors on each character
- **Filter-Based Drawing**: Draw strings to only specific grid positions using a filter set
- **UTF-8 Support**: Handles special characters and Unicode symbols

## Creating a ScreenBuff

### Default Size (78×25)

```C# 
using RogueLib.Utilities;  
ScreenBuff screen = new ScreenBuff();
```

### Custom Size

```C#
// Create a 100×40 console buffer  
ScreenBuff screen = new ScreenBuff(width: 100, height: 40);
```

## Properties

| Property | Type  | Description                                        |
| -------- | ----- | -------------------------------------------------- |
| `Width`  | `int` | The width of the buffer in characters (read-only)  |
| `Height` | `int` | The height of the buffer in characters (read-only) |


## Drawing Operations

All `Draw()` methods write to the back buffer. The back buffer is not displayed until is called. `Display()`

### Drawing Strings

#### Draw at Origin (0, 0)

screen.Draw("Hello, World!");  

```C#
// With color  
screen.Draw("Hello, World!", ConsoleColor.Green);
```

#### Draw at Specific Position

```C#
Vector2 pos = new Vector2(10, 5);  
screen.Draw("Message", pos);  
  
// With color  
screen.Draw("Message", pos, ConsoleColor.Yellow);
```

### Drawing Characters

Draw a single character at a specific position:

```C#
Vector2 pos = new Vector2(15, 10);  
screen.Draw('@', pos);  
  
// With color  
screen.Draw('*', pos, ConsoleColor.Red);
```

### Filtered Drawing

Draw a string only to specific grid positions using a filter: `HashSet<Vector2>`

```C#

var visibleTiles = new HashSet\<Vector2>  
{  
    new Vector2(5, 5),  
    new Vector2(6, 5),  
    new Vector2(7, 5),  
    // ... more positions  
};  

  
string mapData = """  
    ######    #....#    #....#    ######    """;  
  
screen.fDraw(visibleTiles, mapData, ConsoleColor.Cyan);
```


In this example, only characters whose positions are in the `visibleTiles` set will be drawn. This is useful for:

- **Field of View (FOV)**: Only render visible map areas
- **Selective Updates**: Update specific regions without redrawing everything
- **Performance Optimization**: Skip rendering off-screen or blocked areas

## Displaying the Buffer

After completing all drawing operations, display the back buffer to the console:

screen.Display();

The method: `Display()`

- Compares the back buffer to the front buffer (what's currently on screen)
- Only updates console cells that have changed
- Minimizes console I/O operations
- Preserves cursor position at the bottom of the buffer
- Hides the cursor during rendering

## Practical Example

### Game Loop Pattern

using RogueLib.Utilities;  
  
ScreenBuff screen = new ScreenBuff();  

```C#
while (gameRunning) {  
    // Clear back buffer implicitly by drawing over old content  
        // Draw background  
    screen.Draw(mapString, ConsoleColor.Gray);  
      
    // Draw game objects (painter's algorithm—last drawn is on top)  
    screen.Draw('@', playerPosition, ConsoleColor.Yellow);  
    screen.Draw('E', enemyPosition, ConsoleColor.Red);  
      
    // Draw UI text  
    screen.Draw("HP: 100/100", new Vector2(0, 24), ConsoleColor.White);  
      
    // Update the screen once per frame  
    screen.Display();  
      
    // Handle input and update game state  
}
```


### Visibility Rendering

// Calculate field of view from player position  
```C#
var visibleArea = CalculateFieldOfView(playerPos, sightRadius);  
```

  
// Draw full map, but only show visible areas  
```C#
screen.fDraw(visibleArea, fullMapString, ConsoleColor.White);  
```

  
// Draw unexplored areas dimly 
```C# 
var exploredButNotVisible = exploredTiles - visibleArea;  
screen.fDraw(exploredButNotVisible, fullMapString, ConsoleColor.DarkGray);
```

## Color Handling

### Default Colors

If no color is specified, the current console foreground color is used:

Console.ForegroundColor = ConsoleColor.Green;  
screen.Draw("This will be green");

### Setting Colors

screen.Draw("Red text", ConsoleColor.Red);  
screen.Draw("Blue text", new Vector2(5, 5), ConsoleColor.Blue);  
screen.Draw('X', new Vector2(10, 10), ConsoleColor.Magenta);

### Available Colors

- `Black`, `DarkBlue`, `DarkGreen`, `DarkCyan`
- `DarkRed`, `DarkMagenta`, `DarkYellow`, `Gray`
- `DarkGray`, `Blue`, `Green`, `Cyan`
- `Red`, `Magenta`, `Yellow`, `White`

## Performance Considerations

### Optimization Features

1. **Selective Updates**: Only changed cells are redrawn—not the entire console
2. **Double Buffering**: Prevents flickering by building a complete frame before display
3. **Efficient Comparison**: Back buffer is compared against front buffer to skip unchanged cells

### Best Practices

1. **Draw in Layers**: Use painter's algorithm—draw background, then objects, then UI

```C#
// Good order:  
screen.Draw(backgroundMap);      // Bottom layer  
screen.Draw('E', enemyPos);      // Entities  
screen.Draw('@', playerPos);     // Player (on top)  
screen.Draw(uiText, uiPos);      // UI layer
```

2. **Call Display() Once Per Frame**: Don't call multiple times in a single update `Display()`

```C# 
// Good  
DrawAllGameElements();  
screen.Display();  
  
// Bad - wasteful  
screen.Draw(background);  
screen.Display();  
screen.Draw(player);  
screen.Display();
```


3. **Use Filtered Drawing for Performance**: When rendering only a portion of a large map

```
// Only draw visible areas instead of entire map  
screen.fDraw(visibleTiles, largeMap, color);
```

4. **Batch Drawings**: Group related drawing operations together

```C#
// Draw all entities before calling Display()  
foreach (var entity in entities) {  
    screen.Draw(entity.Glyph, entity.Position, entity.Color);  
}  
screen.Display();
```
	
## Interface Implementation

`ScreenBuff` implements the interface, making it compatible with game systems that expect a standard rendering surface. 

## Common Patterns

### Clearing the Screen

The screen automatically clears when you draw new content over old content (painter's algorithm). For a complete refresh:

```C#
// Draw a background character across the entire screen  
screen.Draw(new string(' ', screen.Width * screen.Heigth), ConsoleColor.Black);
```
	
### Drawing Strings at Offset Positions

Strings are parsed character-by-character with embedded newlines:

```C#
string box = """  
    ┌─────┐    │...  │    └─────┘    """;  
  
screen.Draw(box, new Vector2(5, 5));  // Position the box's top-left at (5, 5)
```

### Building UI Layouts

```C# 
// Title at top  
screen.Draw("=== GAME MENU ===", new Vector2(30, 2), ConsoleColor.Cyan);  
  
// Menu items in the middle  
screen.Draw("1. New Game", new Vector2(25, 10), ConsoleColor.White);  
screen.Draw("2. Load Game", new Vector2(25, 11), ConsoleColor.White);  
screen.Draw("3. Quit", new Vector2(25, 12), ConsoleColor.White);  
  
// Status bar at bottom  
screen.Draw("Use arrow keys to navigate", new Vector2(0, 24));
```


