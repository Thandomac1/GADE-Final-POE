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

        public int getX()
        {
            return X;

        }
        protected int Y;

        public int getY()
        {
            return Y;

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

        public Tile(int x, int y)
        {
            this.X = x;
            this.Y = y;

        }
    }

    public class Obstacle : Tile
    {

        public Obstacle(int x, int y) : base(x, y)
        {
            this.Symbol = "X";
        }
    }

    public class Item : Tile // Task 2 Q.2.1
    {

        public Item(int x, int y) : base(x, y)
        {
            this.X = x;
            this.Y = y;
        }
        //public abstract string ToString();

    }

    public class Gold : Item //Task 2 Q.2.3
    {
        private int goldAmount;

        public int getGold()
        {
            return goldAmount;
        }

        public int GoldAmount
        {
            get { return goldAmount; }
            set { goldAmount = value; }
        }
        private Random goldObj = new Random();

        public Gold(int x, int y) : base(x, y)
        {
            this.X = x;
            this.Y = y;
            goldAmount = goldObj.Next(1, 5);
        }
    }

    public class EmptyTile : Tile
    {
        public EmptyTile(int x, int y) : base(x, y)
        {
            this.typeofTile = Tiletype.Empty;
            this.Symbol = "=";
        }
    }

    public abstract class Character : Tile //Question 2.2
    {
        protected int Hp;
        protected int max_Hp;
        protected int Damage;
        protected int Goldpurse;

        public Tile[] vision;
        public enum Movement
        {
            No_movement,
            up,
            Down,
            Left,
            Right,
        }


        public Character(int x, int y, string symbol) : base(x, y)
        {
            this.Symbol = symbol;
        }
        public void Pickup(Item i)
        {
            Gold number;
            if (i is Gold)
            {
                number = (Gold)i;
                Goldpurse = Goldpurse + number.getGold();
            }
        }
        public string getSymbol()
        {
            return Symbol;
        }

        public virtual void Attack(Character Target)
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
    }
    public abstract class Enemy : Character
    {
        protected Random r = new Random();

        public Enemy(int x, int y, int damage, int hp, int max_hp, string symbol) : base(x, y, symbol)
        {
            this.Damage = damage;
            this.Hp = hp;
            this.max_Hp = max_hp;

        }
        public override string ToString()
        {
            string info = GetType().Name + "\n";
            info += "at [" + X.ToString() + "," + Y.ToString() + "] \n";
            info += Hp.ToString() + " HP \n";
            info += "{" + Damage.ToString() + "} \n";
            return info;
        }

    }
    public class Goblin : Enemy
    {
        public Goblin(int x, int y) : base(x, y, 1, 10, 10, "G")
        {


        }
        public override Movement returnMove(Movement move = Movement.No_movement)
        {
            int RandomTileIndex = r.Next(0, vision.Length + 1);

            while (vision[RandomTileIndex].typeofTile.Equals(typeof(EmptyTile)))
            {
                RandomTileIndex = r.Next(0, vision.Length);
            }
            if (vision[RandomTileIndex].getX() > X)
            {
                return Movement.Right;
            }
            else if (vision[RandomTileIndex].getX() < X)
            {
                return Movement.Left;
            }
            else if (vision[RandomTileIndex].getY() > Y)
            {
                return Movement.Down;
            }
            else if (vision[RandomTileIndex].getY() < Y)
            {
                return Movement.up;
            }
            return Movement.No_movement;
        }
    }
    public class Mage : Enemy
    {
        public Mage(int x, int y) : base(x, y, 5, 5, 5, "M")
        {

        }

        public override Movement returnMove(Movement move)
        {
            return Movement.No_movement;
        }
        public override bool CheckRange(Character Target)
        {
            int x, y;
            x = Math.Abs(Target.getX() - this.getX());
            y = Math.Abs(Target.getY() - this.getY());

            if ((x == 0 || x == 1) && (y == 0 || y == 1))
            {
                if (x == 0 && y == 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

    }
    public class Hero : Character
    {

        public Hero(int x, int y, int hp) : base(x, y, "H")
        {

        }

        public override Movement returnMove(Movement move = Movement.No_movement)
        {
            switch (move)
            {
                case Movement.up:
                    if (this.vision[(int)Character.Movement.up] is EmptyTile || this.vision[(int)Character.Movement.up] is Item)
                    {
                        return Movement.up;
                    }
                    else
                    {
                        return Movement.No_movement;
                    }
                case Movement.Down:
                    if (this.vision[(int)Character.Movement.Down] is EmptyTile || this.vision[(int)Character.Movement.Down] is Item)
                    {
                        return Movement.Down;
                    }
                    else
                    {
                        return Movement.No_movement;
                    }
                case Movement.Right:
                    if (this.vision[(int)Character.Movement.Right] is EmptyTile || this.vision[(int)Character.Movement.Right] is Item)
                    {
                        return Movement.Right;
                    }
                    else
                    {
                        return Movement.No_movement;
                    }
                case Movement.Left:
                    if (this.vision[(int)Character.Movement.Left] is EmptyTile || this.vision[(int)Character.Movement.Left] is Item)
                    {
                        return Movement.Left;
                    }
                    else
                    {
                        return Movement.No_movement;
                    }
                default:
                    return Movement.No_movement;
            }
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
                    foreach (Tile T in vision)
                    {
                        if (T.getX() == X + 1)
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
                    foreach (Tile T in vision)
                    {
                        if (T.getX() == X - 1)
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
                    foreach (Tile T in vision)
                    {
                        if (T.getX() == X - 1)
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
                    foreach (Tile T in vision)
                    {
                        if (T.getX() == X + 1)
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

        public int mapHeight;

        public int MAPHEIGHT
        {
            get { return mapHeight; }
            set { mapHeight = value; }
        }
        public int mapWidth;

        public int MAPWIDTH
        {
            get { return mapWidth; }
            set { mapWidth = value; }
        }
        Random obj = new Random();
        public List<Enemy> Enemies = new List<Enemy>();

        public List<Item> Items { get; set; }// Task 2 Q.3.1 Making of the item array 
        Label mapLabel = new Label();

        public Map()
        {

        }
        public Map(int min_height, int max_height, int min_width, int max_width, int numenemies, int gold_amount)
        {
            mapHeight = randomize(min_height, max_height);
            mapWidth = randomize(min_width, max_width);
            Gmap = new Tile[mapWidth, mapHeight];
            fillEmpty();
            Mapcreate(numenemies);
            //updateVision();
        }
        public int randomize(int min, int max)
        {
            int heightmin;
            heightmin = obj.Next(min, max);
            return heightmin;

        }

        public void fillEmpty()
        {
            for (int i = mapWidth; i < mapWidth; i++)
            {
                for (int j = mapHeight; j < mapHeight; j++)
                {
                    Gmap[i, j] = new EmptyTile(i, j);
                }
            }
        }
        public override string ToString()
        {
            string mapstring = "";

            for (int y = 0; y < mapWidth; y++)
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
            //foreach (Enemy E in Enemies)
            //{
            //    E.Vision.Clear();

            //    if (E.X > 0)
            //    {
            //        E.Vision.Add(Gmap[E.X - 1, E.Y]);
            //    }
            //    if (E.X < mapWidth)
            //    {
            //        E.Vision.Add(Gmap[E.X + 1, E.Y]);
            //    }
            //    if (E.X > 0)
            //    {
            //        E.Vision.Add(Gmap[E.X - 1, E.Y]);
            //    }
            //    if (E.X < 0)
            //    {
            //        E.Vision.Add(Gmap[E.X + 1, E.Y]);
            //    }
            //}
            //Playerhero.Vision.Clear();

            //if (Playerhero.X > 0)
            //{
            //    Playerhero.Vision.Add(Gmap[Playerhero.X - 1, Playerhero.Y]);
            //}
            //if (Playerhero.X < mapWidth)
            //{
            //    Playerhero.Vision.Add(Gmap[Playerhero.X + 1, Playerhero.Y]);
            //}
            //if (Playerhero.X > 0)
            //{
            //    Playerhero.Vision.Add(Gmap[Playerhero.X, Playerhero.Y - 1]);
            //}
            //if (Playerhero.X < mapHeight)
            //{
            //    Playerhero.Vision.Add(Gmap[Playerhero.X, Playerhero.Y + 1]);
            //}
        }

        public void Mapcreate(int numenemies)
        {

            for (int y = 0; y < mapWidth; y++)
            {
                for (int x = 0; x < mapHeight; x++)
                {
                    if (x == 0 || x == mapHeight- 1 || y == 0 || y == mapWidth - 1)
                    {
                        Create(Character.Tiletype.barrier, y, x);
                    }
                    else
                    {
                        Create(Character.Tiletype.Empty, y, x);
                    }
                }
            }
            Create(Character.Tiletype.Hero, 0, 0);

            for (int e = 0; e < numenemies; e++)
            {
                Create(Character.Tiletype.Enemy, 0, 0);
            }
        }

        public void Create(Character.Tiletype typeofTile, int X, int Y)
        {
            switch (typeofTile)
            {
                case Character.Tiletype.barrier:
                    Obstacle Newbarrier = new Obstacle(X, Y);
                    Gmap[X, Y] = Newbarrier;
                    break;
                case Character.Tiletype.Empty:
                    EmptyTile NewEmptyTile = new EmptyTile(X, Y);
                    Gmap[X, Y] = NewEmptyTile;
                    break;
                case Character.Tiletype.Hero:
                    int heroX = obj.Next(0, mapWidth);
                    int heroY = obj.Next(0, mapHeight);

                    while (Gmap[heroX, heroY].typeofTile != Character.Tiletype.Empty)
                    {
                        heroX = obj.Next(0, mapWidth);
                        heroY = obj.Next(0, mapHeight);
                    }

                    Hero NewHero = new Hero(heroX, heroY, 10);
                    Playerhero = NewHero;
                    Gmap[heroX, heroY] = NewHero;
                    break;
                case Character.Tiletype.Enemy:
                    int EnemyX = obj.Next(0, mapWidth);
                    int EnemyY = obj.Next(0, mapHeight);

                    while (Gmap[EnemyX, EnemyY].typeofTile != Character.Tiletype.Empty)
                    {
                        EnemyX = obj.Next(0, mapWidth);
                        EnemyY = obj.Next(0, mapHeight);
                    }
                    int randomEnemies = obj.Next(2);
                    if (randomEnemies == 1)
                    {
                        Goblin NewEnemy = new Goblin(EnemyX, EnemyY);
                        Gmap[EnemyX, EnemyY] = NewEnemy;
                        Enemies.Add(NewEnemy);
                    }
                    else
                    {
                        Mage New_enemy = new Mage(EnemyX, EnemyY); //   Task 2 Q.3.1
                        Gmap[EnemyX, EnemyY] = New_enemy;
                        Enemies.Add(New_enemy);
                    }
                    break;
                case Character.Tiletype.Gold:
                    Gold newGold = new Gold(X, Y);
                    Gmap[X, Y] = newGold;
                    break;
            }

        }
        

        public static implicit operator int(Map v)
        {
            throw new NotImplementedException();
        }

        public Item getItemAtPosition(int x, int y)
        {
            for (int jx = 0; jx < Items.Count; jx++)
            {
                if (Items[jx] != null)
                {
                    if (Items[jx].getX() == x && Items[jx].getY() == y)
                    {
                        Item tempItem;

                        tempItem = Items[jx];
                        Items[jx] = null;

                        return tempItem;
                    }
                }
            }
            return null;
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

            for (int y = 0; y < map.MAPWIDTH; y++)
            {
                for (int x = 0; x < map.MAPHEIGHT; x++)
                {
                    MapLabel.Text = MapLabel.Text + map.GMAP[y, x].Symbol;
                }
                MapLabel.Text += "\n";
            }
            
        }
        
        public bool MovePlayer(Character.Movement direction)
        {
            Tile H;
            int x, y;
            Item i;
            if (map.PLAYERHERO.returnMove(direction) == direction)
            {
                H = map.PLAYERHERO;
                x = map.PLAYERHERO.getX();
                y = map.PLAYERHERO.getY();
                map.PLAYERHERO.move(direction);
                i = map.getItemAtPosition(map.PLAYERHERO.getX(), map.PLAYERHERO.getX());
                if (i is Gold)
                {
                    map.PLAYERHERO.Pickup(i);
                }
                map.GMAP[map.PLAYERHERO.getY(), map.PLAYERHERO.getX()] = H;
                map.GMAP[x, y] = new EmptyTile(x, y);

                map.updateVision();



                return true;
            }

            return false;
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            GameEngine gameengine = new GameEngine();
            mapLabel.Text = "Hello";
            gameengine.Showmap();
            mapLabel.Text = gameengine.MapLabel.Text;

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}

