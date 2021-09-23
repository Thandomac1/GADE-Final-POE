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
                public Enemy(int x, int y, int damage, int hp, int max_Hp)
                {

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
                    char goblin = 'G';
                }
                public override Movement returnMove(Movement move)
                {
                   move = (Movement)r.Next(1, 5);
                    return move;
                }
            }
            public class Hero : Character
            {
                
                public Hero(int x, int y, int hp, int max_hp)
                {
                    this.X = x;
                    this.Y = y;
                    this.Hp = hp;
                    this.max_Hp = max_hp;
                    this.Damage = 2;
                    char hero = 'H';
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
                private int[,] map = new int[0,0];
                private string hero;
                private int[] enemies;
                private  int mapHeight;
                private  int mapWidth;
                Random obj = new Random();

                public Map(int min_height, int max_height, int min_width, int max_width, int[] numEnemies)
                {
                    mapHeight = randomize(min_height, max_height);                   
                    mapWidth = randomize(min_width, max_width);
                    enemies = numEnemies;
                    
                }
                public int randomize(int min, int max)
                {
                    int heightmin;
                    heightmin = obj.Next(min, max);
                    return heightmin;

                }
                public void Create()
                {

                }
                public void updateVision()
                {

                }
               
            }
            public class GameEngine
            {
                private int map { get; }

                public GameEngine()
                {

                }
                public bool MovePlayer(Movement direction )
                {
                    return false;
                }
            }
        }
        public Form1()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
