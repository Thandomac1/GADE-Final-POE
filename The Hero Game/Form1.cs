using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Hero_Game
{   
    public partial class Form1 : Form
    {
        public abstract class Tile //Question 2.1
        {
            protected int X;
            protected int Y;

            public enum Tiletype
            {
                Hero,
                Enemy,
                Gold,
                Weapon,
            }
            public Tile()
            {

            }
            public Tile(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        public abstract class Obstacle : Tile
        {
            public Obstacle()
            {

            }
            public Obstacle(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        public abstract class EmptyTile : Tile
        {

        }
        public abstract class Character : Tile //Question 2.2
        {
            protected int Hp;
            protected int max_Hp;
            protected int Damage;
            char Tile_Border = 'X';
            string[] characterVision = new string[] { "north", "south", "west", "East" };

            public Character()
            {

            }

            public Character(int hp, int max_hp, int damage)
            {
                Hp = hp;
                max_Hp = max_hp;
                Damage = damage;
            }
            public Character(int x, int y, char tile_border)
            {
                this.X = x;
                this.Y = y;
                Tile_Border = tile_border;
            }
            public enum Movement
            {
                No_movement,
                up,
                Down,
                Left,
                Right,
            }
            public virtual void Attack()
            {

            }
            public bool IsDead()
            {
                return false;
            }
            public virtual bool CheckRange()
            {
                return false;
            }
            private int toDistance()
            {
                return 0;
            }
            public abstract Movement returnMove(Movement move);
            public void move()
            {

            }
            public abstract override string ToString();
            //public abstract movementEnum();
            public abstract class Enemy : Character
            {
                protected Random r = new Random();
                public Enemy()
                {

                }
                public Enemy(int x, int y, int damage, int hp, int max_hp)
                {
                    this.X = x;
                    this.Y = y;
                    this.Damage = damage;
                    this.Hp = hp;
                    this.max_Hp = max_hp;
                }
                public override string ToString()
                {
                    return ("Enemy goblin at" + X + Y + Damage);
                }
            }
            public class Goblin : Enemy
            {
                
                public Goblin()
                {

                }
                public Goblin(int x, int y)
                {
                    this.X = x;
                    this.Y = y;
                    this.max_Hp = 10;
                    this.Damage = 1;
                    
                }
                public override Movement returnMove(Movement move)
                {
                   move = (Movement)r.Next(1, 5);
                    return move;
                }
            }
            public class Hero : Character
            {
                
                public Hero()
                {

                }

                public Hero(int x, int y)
                {
                    X = x;
                    Y = y;
                }

                public Hero(int x, int y, int hp, int max_hp)
                {
                    this.X = x;
                    this.Y = y;
                    this.Hp = hp;
                    this.max_Hp = max_hp;
                    this.Damage = 2;
                    
                }
                public override Movement returnMove(Movement move)
                {

                    return move;
                }
                public override string ToString()
                {
                    return ("Player stats:" + "\n" + "Hp:" + Hp + max_Hp + "\n" + "Damage:" + Damage + "\n" + "[" + X + "," + Y + "]");
                }
            }
            public class Map
            {
                private Tile[,] map = new Tile[0,0];
                private Hero hero;
                private Enemy[] enemies;
                private  int mapHeight;
                private  int mapWidth;
                Random obj = new Random();

                public Map()
                {

                }
                public Map(int min_height, int max_height, int min_width, int max_width, int numenemies  )
                {
                    mapHeight = randomize(min_height, max_height);                   
                    mapWidth = randomize(min_width, max_width);
                    map = new Tile[mapWidth, mapHeight];
                    
                    Create();
                }
                public int randomize(int min, int max)
                {
                    int heightmin;
                    heightmin = obj.Next(min, max);
                    return heightmin;

                }
                public override string ToString()
                {
                    string map = " ";
                    for (int row = 0; row< mapWidth; row++)
                    {
                        for (int column = 0; column< mapHeight; column++)
                        {
                            map[row, column] = " ";
                        }
                    }
                    for (int r = 0; r < mapWidth; r++)
                    {
                        map[r, 0] = "X";
                        map[r, mapHeight - 1] = "X";
                    }
                    for (int r = 0; r < mapHeight; r++)
                    {
                        map[0, r] = "X";
                        map[mapWidth - 1, r] = "X";
                    }
                    return map;
                }
                public void Create()
                {
                    Hero hero = new Hero();
                    hero = (Hero)Create(Tile.Tiletype.Hero);
                    
                    
                }
                public void updateVision()
                {

                }
                private Tile Create(Tiletype type)
                {
                    int x = obj.Next(mapWidth);
                    int y = obj.Next(mapHeight);
                    if (map[x, y] == null)
                    {
                        switch (type)
                        {
                            case Tile.Tiletype.Hero: return new Hero(x, y);
                                break;
                            case Tile.Tiletype.Enemy: return new Enemy(x, y);
                                break;
                        }                                                           
                    }
                }
                
               
            }
             public class GameEngine
            {
                public char hero = 'H';
                public char goblin = 'G';
                public char boundries = 'X';
                public char ground = ',';
                private int map { get; }

                public GameEngine()
                {

                }
                public bool MovePlayer(Movement direction )
                {
                    return true;
                }
            }
            
        }
        public Form1()
        {
            InitializeComponent();
            //GameEngine gamemap = new GameEngine(10,20,10,20,7);
            
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
