using System;
using System.Collections.Generic;

using System.Text;
using RogueLib.Utilities;
namespace RogueLib.Dungeon;

public abstract class Item: IDrawable 
{
    public Vector2 Pos { get; set; }

    public char Glyph {  get; init; }

    public Item(char c,Vector2 pos)
    {
        Pos = pos;
        Glyph = c;
    }
    public abstract void Draw(IRenderWindow disp);
    
}
