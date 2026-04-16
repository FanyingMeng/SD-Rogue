using RogueLib.Dungeon;
using System;
using System.Collections.Generic;
using RogueLib.Utilities;
using System.Text;
using RlGameNS;

namespace SandBox01.Levels;

public class Gold : Item
{


    public int Amount { get; init; }

    public Gold(Vector2 pos, int amount) : base('*', pos)
    {
        Amount = amount;

    }

    public override void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos,ConsoleColor.Yellow);
    }
}

