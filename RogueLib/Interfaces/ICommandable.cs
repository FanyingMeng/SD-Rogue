using RogueLib.Engine;

namespace RogueLib.Dungeon;

public interface ICommandable {
  bool HasCommand(ConsoleKey key);
  void DoCommand(Command command);
}