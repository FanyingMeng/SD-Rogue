using RogueLib.Engine;
using RogueLib.Utilities;
using CommandMap = System.Collections.Generic.Dictionary<System.ConsoleKey, string>;

namespace RogueLib.Dungeon;

public abstract class Scene : ICommandable, IDrawable {
  // scenes must implement these services -------------------------
  public abstract void DoCommand(Command command);
  public abstract void Draw(IRenderWindow disp); // render the scene
  public abstract void Update();                // update the scene


   // fields -------------------------------------------------------
   protected Player? _player;             // reference back to the player
   public    Game?   _game;               // reference back to the game
   protected bool    _levelActive = true; // currently active level

   // command system ------------------------------------------------
   public    bool       IsActive => _levelActive;
   protected CommandMap _commandMap;

   public bool HasCommand(ConsoleKey inputKey)
      => _commandMap.ContainsKey(inputKey);

   public string GetCommand(ConsoleKey inputKey)
      => _commandMap[inputKey];

   protected void RegisterCommand(ConsoleKey inputKey, string command)
      => _commandMap[inputKey] = command;

   // Constructor -----------------------------------------------------
   public Scene()
   {
      _commandMap = new();
   }
}