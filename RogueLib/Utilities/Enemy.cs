using RogueLib.Dungeon;
using RogueLib.Utilities;
using System;

namespace RlGameNS;

public class Enemy : IActor, IDrawable
{
    public Vector2 Pos { get; set; }
    public char Glyph { get; init; }
    public ConsoleColor Color { get; init; }
    public int Hp { get; set; }

    public Enemy(char glyph, Vector2 pos, ConsoleColor color, int hp = 10)
    {
        Glyph = glyph;
        Pos = pos;
        Color = color;
        Hp = hp;
    }

    public void Draw(IRenderWindow disp)
    {
        disp.Draw(Glyph, Pos, Color);
    }
}