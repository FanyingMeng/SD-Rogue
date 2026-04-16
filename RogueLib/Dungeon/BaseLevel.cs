using RogueLib.Engine;
using RogueLib.Utilities;

namespace RogueLib.Dungeon;

public class BaseLevel : Scene
{
    protected string? _map;    // dungeon background to be drawn first

   
    public override void DoCommand(Command command)
    {
        if (command.Name == "quit")
        {
            _levelActive = false;
        }
    }


    public override void Draw(IRenderWindow disp)
    {
        
        if (!string.IsNullOrEmpty(_map))
        {
            disp.Draw(_map, ConsoleColor.Gray);
        }

        
        _player?.Draw(disp);
    }

    
    public override void Update()
    {
        
        _player?.Update();
    }
}