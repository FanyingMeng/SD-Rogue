namespace RogueLib.Engine;

public class Command {
  private string _name;
  public string Name => _name;
  public Command(string name) => _name = name;
}