using RogueLib.Dungeon;
using RogueLib.Engine;
using RogueLib.Utilities;
using TileSet = System.Collections.Generic.HashSet<RogueLib.Utilities.Vector2>;

namespace RlGameNS;


class Program {

   static void Main(string[] args) {
      Console.Clear();
      Game game = new MyGame();
      game.run();
      
   }
}