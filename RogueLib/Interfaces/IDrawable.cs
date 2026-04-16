namespace RogueLib.Dungeon;


// IDrawable means you can draw yourself
// you support a Draw call, which will draw 
public interface IDrawable {
  void Draw(IRenderWindow disp);
}