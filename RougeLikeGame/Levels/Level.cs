using RogueLib;
using RogueLib.Dungeon;
using RogueLib.Engine;
using RogueLib.Utilities;
using SandBox01.Levels;

using TileSet = System.Collections.Generic.HashSet<RogueLib.Utilities.Vector2>;

namespace RlGameNS;

// -----------------------------------------------------------------------
// The Level is the model, all the game world objects live in the model. 
// player input updates the model, the model updates the view, and the 
// controller runs the whole thing. 
//
// Scene is the base class for all game scenes (levels). Scene is an 
// abstract class that implements IDrawable and ICommandable. 
// 
// A dungeon level is a collection or rooms and tunnels in a 78x25 grid. 
// each tile is at a point, or grid location, represented by a Vector2. 
// 
// *TileSets* are HashSets of grid points, TileSets can be used to tell 
// GameScreen what tiles to draw. TileSets can be combined with Union and 
// Intersect to create complex tile sets
// -----------------------------------------------------------------------
public class Level : Scene
{
    // ---- level config ---- 
    protected string? _map;
    protected int _senseRadius = 4;

    // --- Tile Sets -----
    // used to keep track of state of tiles on the map
    protected TileSet _walkables; // walkable tiles 
    protected TileSet _floor;
    protected TileSet _tunnel;
    protected TileSet _door;
    protected TileSet _decor; // walls and other decorations, always visible once discovered

    protected TileSet _discovered; // tiles the player has seen
    protected TileSet _inFov;      // current fov of player

    protected List<Item> _items;
    protected List<Enemy> _enemies;

    public Level(Player p, string map, Game game)
    {
        if (game == null || p == null || map == null)
            throw new ArgumentNullException("game, player, or map cannot be null");

        _player = p;
        _player.Pos = new Vector2(4, 12); // random, or at stairs
        _map = map;
        _game = game;
        

        initMapTileSets(map);
        updateDiscovered();
        registerCommandsWithScene();
        _items = new List<Item>();
        spreadGold();
        _enemies = new List<Enemy>();
        spawnEnemies();
    }

    private void spawnEnemies()
    {
        var rng = new Random();
        var count = rng.Next(3, 7); 

        for (int i = 0; i < count; i++)
        {
            
            var tile = _walkables.ElementAt(rng.Next(_walkables.Count));

            
            _enemies.Add(new Enemy('S', tile, ConsoleColor.Red));

            
            _walkables.Remove(tile);
        }
    }

    private void spreadGold()
    {
        var rng = new Random();
        var am = rng.Next(10, 20);
        for (int i = 0; i < am; i++)
        {
            var tile = _floor.ElementAt(rng.Next(_floor.Count));
            _items.Add(new Gold(tile, rng.Next(100, 200)));
        }
    }


    protected void updateDiscovered()
    {
        _inFov = fovCalc(_player!.Pos, _senseRadius);

        if (_discovered is null)
            _discovered = new TileSet();

        _discovered.UnionWith(_inFov);
    }

    protected TileSet fovCalc(Vector2 pos, int sens)
       => Vector2.getAllTiles().Where(t => (pos - t).RookLength < sens).ToHashSet();

    // -----------------------------------------------------------------------
    public override void Update()
    {
        _player!.Update();
       
        if (_enemies != null)
        {
            var rng = new Random();
            Vector2[] directions = { Vector2.N, Vector2.S, Vector2.E, Vector2.W };

            foreach (var enemy in _enemies)
            {
                

                Vector2 targetPos = enemy.Pos;

                
                int distToPlayer = (enemy.Pos - _player.Pos).RookLength;

                
                if (distToPlayer <= 8)
                {
                    int minDistance = distToPlayer;

                    
                    foreach (var dir in directions)
                    {
                        var testPos = enemy.Pos + dir;

                       
                        if (testPos == _player.Pos)
                        {
                            targetPos = testPos;
                            break;
                        }

                        
                        if (_walkables.Contains(testPos))
                        {
                            int newDist = (testPos - _player.Pos).RookLength;
                            if (newDist < minDistance)
                            {
                                minDistance = newDist;
                                targetPos = testPos; 
                            }
                        }
                    }

                    
                    if (targetPos == enemy.Pos)
                    {
                        targetPos = enemy.Pos + directions[rng.Next(directions.Length)];
                    }
                }
                

                
                if (targetPos == _player!.Pos)
                {
                    
                    int rawDamage = 5;

                    
                    int actualDamage = _player.TakeDamage(rawDamage);

                    
                    if (actualDamage > 0)
                    {
                        
                        int blocked = rawDamage - actualDamage;
                    }
                    continue;
                }

                
                if (_walkables.Contains(targetPos))
                {
                    var oldPos = enemy.Pos;
                    enemy.Pos = targetPos;

                    _walkables.Remove(targetPos);
                    _walkables.Add(oldPos);
                }
            }
        }
        
        if (_player.Hp <= 0)
        {
            
            Console.Clear();
            
            Console.WriteLine(DungeonConfig.RIP); 
            Console.ResetColor();
            Console.ReadLine();

            
            _levelActive = false;
        }
    }

    public override void Draw(IRenderWindow? disp)
    {
        
        var tilesToDraw = new TileSet(_decor);
        tilesToDraw.IntersectWith(_discovered);
        tilesToDraw.UnionWith(_inFov);

        disp.fDraw(tilesToDraw, _map, ConsoleColor.Gray);

        var rng = new Random();
        if (_player.Turn % 5 == 0)
            _player._color = (ConsoleColor)rng.Next(10, 16);
        _player!.Draw(disp);
         

        drawItems(disp);
        drawEnemies(disp);
        disp.Draw(_player.HUD, new Vector2(0, 24), ConsoleColor.Green);
    }

    public override void DoCommand(Command command)
    {
        
        if (command.Name == "up")
        {
            MovePlayer(Vector2.N);
        }
        else if (command.Name == "down")
        {
            MovePlayer(Vector2.S);
        }
        else if (command.Name == "left")
        {
            MovePlayer(Vector2.W);
        }
        else if (command.Name == "right")
        {
            MovePlayer(Vector2.E);
        }       
        else if (command.Name == "quit")
        {
            _levelActive = false;
        }
    }

    // -------------------------------------------------------------------------

    private void drawItems(IRenderWindow disp)
    {
        foreach (var item in _items)
        {
            
            if (_discovered.Contains(item.Pos))
            {
                item.Draw(disp);
            }
        }
    }

    private void drawEnemies(IRenderWindow disp)
    {
        foreach (var enemy in _enemies)
        {
            
            if (_inFov.Contains(enemy.Pos))
            {
                enemy.Draw(disp);
            }
        }
    }

    private void initMapTileSets(string map)
    {
        var lines = map.Split('\n');
        
        _floor = new TileSet();
        _tunnel = new TileSet();
        _door = new TileSet();
        _decor = new TileSet();

        foreach (var (c, p) in Vector2.Parse(map))
        {
            if (c == '.') _floor.Add(p);
            if (c == '+') _door.Add(p);
            if (c == '#') _tunnel.Add(p);
            if (c != ' ') _decor.Add(p);
        }

        _walkables = _floor.Union(_tunnel).Union(_door).ToHashSet();
        
    }

   

    private void registerCommandsWithScene()
    {
        RegisterCommand(ConsoleKey.UpArrow, "up");
        RegisterCommand(ConsoleKey.W, "up");
        RegisterCommand(ConsoleKey.K, "up");

        RegisterCommand(ConsoleKey.DownArrow, "down");
        RegisterCommand(ConsoleKey.S, "down");
        RegisterCommand(ConsoleKey.J, "down");

        RegisterCommand(ConsoleKey.LeftArrow, "left");
        RegisterCommand(ConsoleKey.A, "left");
        RegisterCommand(ConsoleKey.H, "left");

        RegisterCommand(ConsoleKey.RightArrow, "right");
        RegisterCommand(ConsoleKey.D, "right");
        RegisterCommand(ConsoleKey.L, "right");

        RegisterCommand(ConsoleKey.Q, "quit");
    }


    public void MovePlayer(Vector2 delta)
    {
        var newPos = _player!.Pos + delta;
        var targetEnemy = _enemies.Find(e => e.Pos == newPos);

        if (targetEnemy != null)
        {
            
            targetEnemy.Hp -= _player!.Str;

           
            if (targetEnemy.Hp <= 0)
            {
                _enemies.Remove(targetEnemy); 
                _walkables.Add(newPos);       
            }

            
            updateDiscovered(); 
            return;
        }

        if (_walkables.Contains(newPos))
        {
            
            var itemToPickUp = _items.Find(i => i.Pos == newPos);
            if (itemToPickUp != null)
            {
                
                if (itemToPickUp is Gold gold)
                {
                    _player.AddGold(gold.Amount);
                }

                
                _items.Remove(itemToPickUp);
            }
            // ---------------------------

            var oldPos = _player!.Pos;
            _player!.Pos = newPos;
            _walkables.Remove(newPos); 
            _walkables.Add(oldPos);   
            updateDiscovered();
        }
    }

    public void QuitLevel()
    {
        _levelActive = false;
    }
}