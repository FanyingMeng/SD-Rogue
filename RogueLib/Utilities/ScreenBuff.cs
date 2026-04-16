using RogueLib.Dungeon;
using FilterSet = System.Collections.Generic.HashSet<RogueLib.Utilities.Vector2>;

namespace RogueLib.Utilities;

// ==============================================================
// Screen class
//
// Creates a fixed size buffer for drawing to the console. With a
// defalt size of 78x25, but can be changed in the constructor.
//
// Draw() calls write to the back buffer
// Display() writes the back buffer to the screen.
//
// A double-buffered screen for writing to the console. This
// allows you to update the console in a flicker-free manner
// without using Console.Clear(). It all so supports the painter
// algorithm, whatever was drawn last will be on top. Which makes it
// perfect for games.
// ==============================================================
public class ScreenBuff : IRenderWindow {
   // statics and constants
   protected const ConsoleColor _notAColor = (ConsoleColor)(-1); // -1 is sentinal value

   // fields
   private readonly int             _width;
   private readonly int             _height;
   private readonly char[,]         _back;       // the back buffer
   private readonly char[,]         _front;      // the front buffer
   private readonly ConsoleColor[,] _backColor;  // back buffer color
   private readonly ConsoleColor[,] _frontColor; // front buffer color

   public int Width  => _width;
   public int Height => _height;

   // ----------- ctor and public methods ----------------
   public ScreenBuff(int width = 78, int height = 25) {
      _width      = width;
      _height     = height;
      _back       = new char[_width, _height];
      _front      = new char[_width, _height];
      _backColor  = new ConsoleColor[_width, _height];
      _frontColor = new ConsoleColor[_width, _height];
      ResetFront();
   }


   // ==============================================================
   //            Publicly exposed methods
   // ==============================================================
   // Draw() methods all draw to the back buffer.
   // Draw methods follow the painter algorighm, whatever was
   // drawn last will be on top.

   public virtual void fDraw(FilterSet fs, string s, ConsoleColor color = _notAColor)
      => BuildOffsetFrame(s, Vector2.Zero, color, fs);

   public virtual void Draw(string s, ConsoleColor color = _notAColor)
      => BuildOffsetFrame(s, Vector2.Zero, color);

   public virtual void Draw(string s, Vector2 pos, ConsoleColor color = _notAColor)
      => BuildOffsetFrame(s, pos, color);

   public virtual void Draw(char c, Vector2 pos, ConsoleColor foregroundColor = _notAColor) {
      if (!IsValidPos(pos)) return;

      _back[pos.X, pos.Y]      = c;
      _backColor[pos.X, pos.Y] = (foregroundColor != _notAColor) ? foregroundColor : Console.ForegroundColor;
   }

   // Call Display() to write the back buffer to the screen.
   public void Display() => FlushToScreen();

   // ==============================================================
   //            private implementation methods
   // ==============================================================
   protected bool IsValidPos(Vector2 pos)
      => pos.X >= 0 && pos.X < _width && pos.Y >= 0 && pos.Y < _height;

   protected void ResetFront() {
      // fill front with sentinal values to indicate that nothing is on screen
      var fgColor = Console.ForegroundColor;

      for (int y = 0; y < _height; y++) {
         for (int x = 0; x < _width; x++) {
            _front[x, y]      = '\0';               // sentinal value 
            _frontColor[x, y] = (ConsoleColor)(-1); // sentinal value
         }
      }
   }


   protected void BuildOffsetFrame(string     s, Vector2 offset, ConsoleColor color = _notAColor,
                                   FilterSet? filterSet = null) {
      
      var fgColor = (color == _notAColor) ? Console.ForegroundColor : color;
      foreach (var (c, p) in Vector2.Parse(s)) {
         
         if (filterSet is not null && !filterSet.Contains(p)) 
            continue;
         var op = p + offset;
         if (!IsValidPos(op)) 
            continue;
         _back[op.X, op.Y] = c;
         _backColor[op.X, op.Y] = fgColor;
      }
   }


   protected void FlushToScreen() {
      Console.CursorVisible  = false;
      Console.OutputEncoding = System.Text.Encoding.UTF8;

      for (int y = 0; y < _height; y++) {
         for (int x = 0; x < _width; x++) {
            // if it hasn't changed, skip it
            if (_back[x, y] == _front[x, y] && _backColor[x, y] == _frontColor[x, y]) continue;

            // Something changed — pay the cost of positioning + writing
            Console.SetCursorPosition(x, y);
            var saveColor = Console.ForegroundColor;
            Console.ForegroundColor = _backColor[x, y];
            Console.Write(_back[x, y]);
            Console.ForegroundColor = saveColor;

            // Record what is now on screen
            _front[x, y]      = _back[x, y];
            _frontColor[x, y] = _backColor[x, y];
         }
      }

      Console.ResetColor();
      Console.SetCursorPosition(0, _height);
   }
}