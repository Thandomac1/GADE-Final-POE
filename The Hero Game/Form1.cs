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
            }
        }
        public class Hero : Character
        {
   
            public Hero(int x, int y, int hp) 
            {
               
                this.Hp = hp;

            }
        }
        public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
                
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
