using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static The_Hero_Game.Form1.Character;

namespace The_Hero_Game
{   
    
    public partial class Form1 : Form
    {
        
        public abstract class Tile //Question 2.1
        {
            protected int X;

            public int getX
            {
                get { return X; }
                set { X = value; }
            }
            protected int Y;

            public int getY
            {
                get { return Y; }
                set { Y = value; }
            }

            public string Symbol;
            internal Tiletype typeofTile;

            protected Tiletype TypeofTile { get; set; }

            public enum Tiletype
            {
                Hero,
                Enemy,
                Gold,
                Weapon,
                Empty,
                barrier,
            }
            public Tile()
            {

            }
            public Tile(int x, int y, string symbol, Tiletype  typeofTile)
            {
                this.X = x;
                this.Y = y;
                Symbol = symbol;
                TypeofTile = typeofTile;
            }
        }
        public class Obstacle : Tile
        {
            public Obstacle()
            {

            }
            public Obstacle(int x, int y, string symbol, Tiletype Tiletype) : base(x, y, symbol, Tiletype.barrier)
            {

            }
        }
        public class Items : Tile // Task 2 Q.2.1
        {
            public Items()
            {

            }
            public Items(int x, int y, string symbol, Tiletype typeofTile) : base(x, y, symbol, Tiletype.Gold ) 
            {
                this.X = x;
                this.Y = y;
            }
            //public abstract string ToString();

        }
        public class Gold : Items //Task 2 Q.2.3
        {
            private int goldAmount;

            public int GoldAmount
            {
                get { return goldAmount; }
                set { goldAmount = value; }
            }
            private Random goldObj = new Random();

            public Gold(int x, int y, string Symbol = "G" ) : base(x, y, Symbol, Tiletype.Gold)
            {
                this.X = x;
                this.Y = y;
                goldAmount = goldObj.Next(1, 5);
            }
        }
        
        public class EmptyTile : Tile
        {
            public EmptyTile(int x, int y, string symbol, Tiletype Tiletype) : base(x, y,symbol, Tiletype.Empty)
            {
                this.typeofTile = Tiletype.Empty;
            }
        }
        public abstract class Character : Tile //Question 2.2
        {
            protected int Hp;
            protected int max_Hp;
            protected int Damage;
            protected int Goldpurse;

            private List<Tile> Vision = new List<Tile>();
            public enum Movement
            {
                No_movement,
                up,
                Down,
                Left,
                Right,
            }
            public Character()
            {

            }

            public Character(int x, int y , int hp, int max_hp, int damage,  Tiletype typeofTile, int goldpurse) 
            {
                Hp = hp;
                max_Hp = max_hp;
                Damage = damage;
                Goldpurse = goldpurse;
                Vision = new List<Tile>();
            }
                      
            public virtual void Attack(Character Target )
            {
                Target.Hp -= Damage; 
            }
            public bool IsDead()
            {
                if (Hp <= 0)
                {
                    return true;
                }
                else return false;
            }
            public virtual bool CheckRange(Character Target)
            {
                int ReachableDistance = 1;

                if (toDistance(Target) <= ReachableDistance)
                {
                    return true;
                }
                else return false;
            }
            private int toDistance(Character Target)
            {
                return Math.Abs(X - Target.X) + Math.Abs(Y - Target.Y);
            }
            public abstract Movement returnMove(Movement move);
            public void move(Movement Move)
            {
                switch (Move)
                {
                    case Movement.up:
                        Y--;
                        break;
                    case Movement.Down:
                        Y++;
                        break;
                    case Movement.Left:
                        X--;
                        break;
                    case Movement.Right:
                        Y++;
                        break;
                }
            }
            public abstract override string ToString();
            //public abstract movementEnum();
            public abstract class Enemy : Character
            {
                protected Random r = new Random();
                public Enemy()
                {

                }
                public Enemy(int x, int y, int damage, int hp, int max_hp, Tiletype typeofTile) 
                {
                    this.X = x;
                    this.Y = y;
                    this.Damage = damage;
                    this.Hp = hp;
                    this.max_Hp = max_hp;
                    this.TypeofTile = typeofTile;
                }
                public override string ToString()
                {
                    string info = GetType().Name + "\n";
                    info += "at [" + X.ToString() + "," + Y.ToString() + "] \n";
                    info += Hp.ToString() + " HP \n";
                    info += "{" + Damage.ToString() + "} \n";                  
                    return info;
                }
                //public void Pickup(int item)
                //{
                //    if (item == Gold.goldAmount)
                //    {
                //        Goldpurse = goldAmount++;
                //    }
                //    else
                //    {
                //        Goldpurse = 0;
                //    }
                //}
            }
            public class Goblin : Enemy
            {                           
                public Goblin(int x, int y, Tiletype typeofTile,string symbol = "G", int damage = 1, int hp = 10, int max_hp = 10  ) : base( x, y, damage, hp, max_hp, Tiletype.Enemy )
                {
                    this.X = x;
                    this.Y = y;
                    this.Symbol = "G";

                }
                public override Movement returnMove(Movement move = Movement.No_movement)
                {
                    int RandomTileIndex = r.Next(0, Vision.Count);

                    while (Vision[RandomTileIndex].typeofTile.Equals(typeof(EmptyTile)))
                    {
                        RandomTileIndex = r.Next(0, Vision.Count);
                    }
                    if (Vision[RandomTileIndex].getX > X)
                    {
                        return Movement.Right;
                    }
                    else if (Vision[RandomTileIndex].getX < X)
                    {
                        return Movement.Left;
                    }
                    else if (Vision[RandomTileIndex].getY > Y)
                    {
                        return Movement.Down;
                    }
                    else if (Vision[RandomTileIndex].getY < Y)
                    {
                        return Movement.up;
                    }
                    return Movement.No_movement;
                }
            }
            public class Mage : Enemy
            {
                public Mage(int x, int y, Tiletype typeofTile, string symbol = "M", int max_hp = 5, int hp = 5, int damage = 5) :base(x, y, damage, hp, max_hp, Tiletype.Enemy)
                {
                    this.Symbol = "M";
                }
                public Mage(int x, int y)
                {
                    this.X = x;
                    this.Y = y;
                    this.Hp = 5;
                    this.Damage = 5;
                    this.Symbol = "M";
                }
                public override Movement returnMove(Movement move)
                {
                    return Movement.No_movement;
                }
                public override bool CheckRange(Character Target)
                {
                    int ReachableDistance = 1;

                    if (toDistance(Target) <= ReachableDistance)
                    {
                        return true;
                    }
                    else return false;
                }

            }
            public class Hero : Character
            { 
                public Hero()
                {

                }
                public Hero(int x, int y, int hp, Tiletype typeofTile, int max_hp, int damage = 2, string symbol = "H") : base(x, y, damage, hp, max_hp, Tiletype.Hero, 0)
                {
                    this.Symbol = "H";
                }

                public Hero(int heroX, int heroY, Tiletype typeofTile, string v1, int v2, int v3, int v4)
                {
                    this.typeofTile = typeofTile;
                    this.Symbol = "H";
                }

                public override Movement returnMove(Movement move = Movement.No_movement)
                {
                    if (CheckforValidMove(move))
                    {
                        return move;
                    }
                    else return Movement.No_movement;
                }
                public override string ToString()
                {
                    string info = "Player Stats:" + "\n";
                    info += "Hp:" + Hp.ToString() + "/" + max_Hp.ToString() + "\n";
                    info += "Damage:" + Damage.ToString() + "\n";
                    info += "[" + X.ToString() + "," + Y.ToString() + "] \n";
                    info += "Gold amount:" + Goldpurse.ToString(); //Task 2 Question 3.2
                    return info;
                }
                bool CheckforValidMove(Movement move)
                {
                    bool isValid = false;

                    switch (move)
                    {
                        case Movement.Right:
                            foreach (Tile T in Vision)
                            {
                                if (T.getX == X + 1)
                                {
                                    if (T.typeofTile == Tiletype.Empty)
                                    {
                                        isValid = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        case Movement.Left:
                            foreach (Tile T in Vision)
                            {
                                if (T.getX == X - 1)
                                {
                                    if (T.typeofTile == Tiletype.Empty)
                                    {
                                        isValid = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        case Movement.up:
                            foreach (Tile T in Vision)
                            {
                                if (T.getX == X - 1)
                                {
                                    if (T.typeofTile == Tiletype.Empty)
                                    {
                                        isValid = true;
                                        break;
                                    }
                                }
                            }
                            break;
                        case Movement.Down:
                            foreach (Tile T in Vision)
                            {
                                if (T.getX == X + 1)
                                {
                                    if (T.typeofTile == Tiletype.Empty)
                                    {
                                        isValid = true;
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                    return isValid;
                }
            }
            public class Map
            {
                private Tile[,] Gmap;

                public Tile[,] GMAP
                {
                    get { return Gmap; }
                    set { Gmap = value; }
                }
                private Hero Playerhero;

                public Hero PLAYERHERO
                {
                    get { return Playerhero; }
                    set { Playerhero = value; }
                }

                public  int mapHeight;

                public int MAPHEIGHT
                {
                    get { return mapHeight; }
                    set { mapHeight = value; }
                }
                public  int mapWidth;

                public int MAPWIDTH
                {
                    get { return mapWidth; }
                    set { mapWidth = value; }
                }
                Random obj = new Random();
                public List<Enemy> Enemies = new List<Enemy>();

                public List<Items> Items { get; set; }// Task 2 Q.3.1 Making of the item array 
                Label mapLabel = new Label();

                public Map()
                {

                }
                public Map(int min_height, int max_height, int min_width, int max_width, int numenemies, int gold_amount  )
                {
                    mapHeight = randomize(min_height, max_height);                   
                    mapWidth = randomize(min_width, max_width);
                    Gmap = new Tile[mapWidth, mapHeight];
                    fillEmpty();
                    Mapcreate(numenemies);
                    updateVision();
                }
                public int randomize(int min, int max)
                {
                    int heightmin;
                    heightmin = obj.Next(min, max);
                    return heightmin;

                }

                public void fillEmpty()
                {
                    for(int i = mapWidth; i < mapWidth; i++)
                    {
                        for(int j = mapHeight; j < mapHeight; j++)
                        {
                            Gmap[i, j] = new EmptyTile(i, j, "X", Tiletype.Empty);
                        }
                    }
                }
                public override string ToString()
                {
                    string mapstring = "";

                    for (int y = 0; y < mapHeight; y++ )
                    {
                        for (int x = 0; x < mapWidth; x++)
                        {
                            mapstring += Gmap[x, y].Symbol;
                        }
                        mapstring += "\n";
                       
                    }
                    return mapstring;
                }

                public void updateVision()
                {
                    foreach (Enemy E in Enemies)
                    {
                        E.Vision.Clear();

                        if (E.X > 0)
                        {
                            E.Vision.Add(Gmap[E.X - 1, E.Y]);
                        }
                        if (E.X < mapWidth)
                        {
                            E.Vision.Add(Gmap[E.X + 1, E.Y]);
                        }
                        if (E.X > 0)
                        {
                            E.Vision.Add(Gmap[E.X - 1, E.Y]);
                        }
                        if (E.X < 0)
                        {
                            E.Vision.Add(Gmap[E.X + 1, E.Y]);
                        }
                    }
                    Playerhero.Vision.Clear();

                    if (Playerhero.X > 0)
                    {
                        Playerhero.Vision.Add(Gmap[Playerhero.X - 1, Playerhero.Y]);
                    }
                    if (Playerhero.X < mapWidth)
                    {
                        Playerhero.Vision.Add(Gmap[Playerhero.X + 1, Playerhero.Y]);
                    }
                    if (Playerhero.X > 0)
                    {
                        Playerhero.Vision.Add(Gmap[Playerhero.X, Playerhero.Y - 1]);
                    }
                    if (Playerhero.X < mapHeight)
                    {
                        Playerhero.Vision.Add(Gmap[Playerhero.X, Playerhero.Y + 1]);
                    }
                }
                public void Mapcreate(int numenemies)
                {

                    for (int y= 0; y < mapWidth; y++ )
                    {
                        for (int x= 0; x < mapHeight; x++)
                        {
                            if (x == 0 || x == mapHeight - 1 || y  == 0 || y == mapWidth - 1 )
                            {
                                Create(Tiletype.barrier, y, x);
                            }
                            else
                            {
                                Create(Tiletype.Empty, y, x);
                            }
                        }
                    }
                    Create(Tiletype.Hero, 0, 0);

                    for (int e = 0; e < numenemies; e++)
                    {
                        Create(Tiletype.Enemy, 0, 0);
                    }
                } 
               
                public void Create(Tiletype typeofTile, int X, int Y)
                {
                    switch (typeofTile)
                    {
                        case Tiletype.barrier:
                            Obstacle Newbarrier = new Obstacle(X, Y, "X", Tiletype.barrier);
                            Gmap[X, Y] = Newbarrier;
                            break;
                        case Tiletype.Empty:
                            EmptyTile NewEmptyTile = new EmptyTile(X, Y, "-", Tiletype.Empty);
                            Gmap[X, Y] = NewEmptyTile;
                            break;
                        case Tiletype.Hero:
                            int heroX = obj.Next(0, mapWidth);
                            int heroY = obj.Next(0, mapHeight);

                                while (Gmap[heroX, heroY].typeofTile != Tiletype.Empty)
                                {
                                    heroX = obj.Next(0, mapWidth);
                                    heroY = obj.Next(0, mapHeight);
                                }
                                
                                Hero NewHero = new Hero(heroX, heroY, Tiletype.Hero, "H", 100, 100, 10 );
                            Playerhero = NewHero;
                            Gmap[heroX, heroY] = NewHero;
                            break;
                        case Tiletype.Enemy:
                            int EnemyX = obj.Next(0, mapWidth);
                            int EnemyY = obj.Next(0, mapHeight);

                            while (Gmap[EnemyX, EnemyY].typeofTile != Tiletype.Empty)
                            {
                                EnemyX = obj.Next(0, mapWidth);
                                EnemyY = obj.Next(0, mapHeight);
                            }

                            Goblin NewEnemy = new Goblin(EnemyX, EnemyY, Tiletype.Enemy, "G", 100, 100, 10);
                            Enemies.Add(NewEnemy);
                            Gmap[EnemyX, EnemyY] = NewEnemy;
                            //Mage New_enemy = new Mage(EnemyX, EnemyY, typeofTile, "M", 5,5,5); //   Task 2 Q.3.1
                            //Enemies.Add(New_enemy);
                            break;
                        case Tiletype.Gold:                          
                            Gold newGold = new Gold(X, Y, "G");
                            Gmap[X, Y] = newGold;
                            break;
                    }
                   
                }
                //public int getItemAtPosition(int x, int y)
                //{
                //    for (int x = 0; x < Items; x++)
                //    {
                //        for (int y = 0; y < items; y++ )
                //        {
                //            if ()
                //            {

                //            }
                //        }
                //    }
                //}

                public static implicit operator int(Map v)
                {
                    throw new NotImplementedException();
                }
            }
             public class GameEngine
            {
                public Label MapLabel = new Label();
                private Map map;

                public Map MAP
                {
                    get { return map; }
                    set { map = value; }
                }

                public GameEngine()
                {
                    map = new Map(10, 20, 10, 20, 5, 5);
                    
                }
                public void Showmap()
                {
                    MapLabel.Text = "";
                    for (int y = 0; y < map.MAPWIDTH; y++)
                    {
                        for (int x = 0; x < map.MAPHEIGHT; x++)
                        {
                            MapLabel.Text = MapLabel.Text + map.GMAP[y, x].Symbol;
                        }
                        MapLabel.Text += "\n";
                    }
                }
                public bool MovePlayer(Movement direction)
                {
                    if (map.PLAYERHERO.returnMove(direction) == direction)
                    {
                        map.Create(Tiletype.Empty, map.PLAYERHERO.X, map.PLAYERHERO.Y);

                        map.PLAYERHERO.move(direction);
                        map.GMAP[map.PLAYERHERO.X, map.PLAYERHERO.Y] = map.PLAYERHERO;

                        return true;
                    }

                    return false;
                }
                public string PlayerAttack(int EnemyIndex)
                {
                    bool EnemyinRange = false;

                    foreach (Tile T in map.PLAYERHERO.Vision)
                    {
                        if (T.getX == map.Enemies[EnemyIndex].X && (T.getY == map.Enemies[EnemyIndex].Y))
                        {
                            EnemyinRange = true;
                            break;
                        }
                    }

                    if (EnemyinRange)
                    {
                        map.PLAYERHERO.Attack(map.Enemies[EnemyIndex]);
                        return "You did " + map.PLAYERHERO.Damage.ToString() + "damage to a " + map.Enemies[EnemyIndex].typeofTile.ToString() + " leaving them on " + map.Enemies[EnemyIndex].Hp.ToString() + "HP";
                    }
                    else
                    {
                        return "target was not in range...";
                    }
                }
            }
            
            
        }
        GameEngine gameengine = new GameEngine();
        public Form1()
        {
            
            InitializeComponent();

            
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString(); 
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mapLabel.Text = "Hello";
            gameengine.MovePlayer(Character.Movement.Down);
            gameengine.Showmap();
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.Left);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.up);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.Right);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            gameengine.MovePlayer(Character.Movement.Down);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();
        }
    }
}
