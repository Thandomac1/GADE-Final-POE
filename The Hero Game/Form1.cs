using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace The_Hero_Game
{

    public partial class Form1 : Form
    {
        GameEngine gameengine = new GameEngine();
        public Form1()
        {

            InitializeComponent();
            GameEngine gameengine = new GameEngine();
            gameengine.Showmap();
            mapLabel.Text = gameengine.MapLabel.Text;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.Down);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.Left);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.up);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.Right);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            gameengine.MovePlayer(Character.Movement.Down);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();
        }


        //private void button4_Click(object sender, EventArgs e)
        //{

        //}
    }
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
        public Tile()
        {

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
        public Item()
        {

        }
        public Item(int x, int y) : base(x, y)
        {
            this.X = x;
            this.Y = y;
        }
        //public abstract string ToString();

    }
    public abstract class Weapon : Item
    {
        protected int wDamage;

        public int WDAMAGE
        {
            get { return wDamage; }
            set { wDamage = value; }
        }
        protected int Range;
        public virtual int RANGE()
        {
            return Range;
        }
        protected int Durability;
        public int DURABILITY
        {
            get { return Durability; }
            set { Durability = value; }
        }
        protected int Cost;
        public int COST
        {
            get { return Cost; }
            set { Cost = value; }
        }
        protected string weaponType;
        public string WEAPONTYPE
        {
            get { return weaponType; }
            set { weaponType = value; }
        }
        public Weapon(int x, int y) : base(x, y)
        {
            this.Symbol = "W";
        }
        public Weapon()
        {

        }
    }
    public class MeleeWeapon : Weapon
    {
        string weapon;
        int wdamage;
        int durability;
        int cost;

        public Types melee_Weapons;
        public enum Types
        {
            dagger,
            longsword,
        }
        
        public MeleeWeapon(int x, int y, string weapon) : base(x, y)
        {
            switch (weapon)
            {
                case "dagger":
                    this.durability = 10;
                    this.wdamage = 3;
                    this.cost = 3;
                    break;
                case "Longsword":
                    this.durability = 6;
                    this.wdamage = 4;
                    this.cost = 5;
                    break;                   
            }
        }
        public override int RANGE()
        {
            return 1;
        }
    }
    
    public class RangedWeapon : Weapon
    {
        string weapon;
        int wdamage;
        int durability;
        int cost;
        int range;

        public Types range_Weapons;
        public enum Types
        {
            Rifle,
            Longbow,
        }
       
        public RangedWeapon(int x, int y, string weapon) : base(x, y)
        {
            switch (weapon)
            {
                case "Rifle":
                    this.durability = 3;
                    this.range = 3;
                    this.wdamage = 5;
                    this.cost = 7;
                    break;
                case "Longbow":
                    this.durability = 4;
                    this.range = 2;
                    this.wdamage = 4;
                    this.cost = 6;
                    break;
            }
        }
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
        protected Weapon weapons;
        public Weapon WEAPONS
        {
            get { return weapons; }
            set { weapons = value; }
        }

        public List<Tile> vision = new List<Tile>();
        
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
        public void setVision(Tile[,] gmap)
        {
            this.vision.Add(gmap[X - 1, Y]);
            this.vision.Add(gmap[X + 1, Y]);
            this.vision.Add(gmap[X,Y + 1]);
            this.vision.Add(gmap[X,Y - 1]);
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
    public class Leader : Enemy
    {
        public int[] leaderpos_x = new int[2];
        public int[] leaderpos_y = new int[2];
        private Map map;
        public Map MAP
        {
            get { return map; }
            set { map = value; }
        }

        private int Tile;
        public int TILE
        {
            get { return Tile; }
            set { Tile = value; }
        }
        public Leader(int x, int y) : base(x, y, 2, 20, 20, "L")
        {

        }
        public override Movement returnMove(Movement move)
        {
            int RandomTileIndex = r.Next(0, vision.Count + 1);

            while (vision[RandomTileIndex].typeofTile.Equals(typeof(EmptyTile)))
            {
                RandomTileIndex = r.Next(0, vision.Count);
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
    public class Goblin : Enemy
    {
        public Goblin(int x, int y) : base(x, y, 1, 10, 10, "G")
        {


        }
        public override Movement returnMove(Movement move = Movement.No_movement)
        {
            int RandomTileIndex = r.Next(0, vision.Count + 1);

            while (vision[RandomTileIndex].typeofTile.Equals(typeof(EmptyTile)))
            {
                RandomTileIndex = r.Next(0, vision.Count);
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
                    if (this.vision[0] is EmptyTile || this.vision[0] is Item)
                    {
                        return Movement.up;
                    }
                    else
                    {
                        return Movement.No_movement;
                    }
                case Movement.Down:
                    if (this.vision[1] is EmptyTile || this.vision[1] is Item)
                    {
                        return Movement.Down;
                    }
                    else
                    {
                        return Movement.No_movement;
                    }
                case Movement.Right:
                    if (this.vision[2] is EmptyTile || this.vision[2] is Item)
                    {
                        return Movement.Right;
                    }
                    else
                    {
                        return Movement.No_movement;
                    }
                case Movement.Left:
                    if (this.vision[3] is EmptyTile || this.vision[3] is Item)
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
        public int getVisionSize()
        {
            return getVisionSize();
        }
        public int getFriendlyattack()
        {
            return getFriendlyattack();
        }

        public List<Item> Items = new List<Item>(); // Task 2 Q.3.1 Making of the item array 
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

            for (int y = 0; y < mapHeight; y++)
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
                E.vision.Clear();

                if (E.getX() > 0)
                {
                    E.vision.Add(Gmap[E.getX() - 1, E.getY()]);
                }
                if (E.getX() < mapHeight)
                {
                    E.vision.Add(Gmap[E.getX() + 1, E.getY()]);
                }
                if (E.getX() > 0)
                {
                    E.vision.Add(Gmap[E.getX() - 1, E.getY()]);
                }
                if (E.getX() < mapWidth)
                {
                    E.vision.Add(Gmap[E.getX() + 1, E.getY()]);
                }
            }
            Playerhero.vision.Clear();

            if (Playerhero.getX() > 0)
            {
                Playerhero.vision.Add(Gmap[Playerhero.getX() - 1, Playerhero.getY()]);
            }
            if (Playerhero.getX() < mapHeight)
            {
                Playerhero.vision.Add(Gmap[Playerhero.getX() + 1, Playerhero.getY()]);
            }
            if (Playerhero.getX() > 0)
            {
                Playerhero.vision.Add(Gmap[Playerhero.getX(), Playerhero.getY() - 1]);
            }
            if (Playerhero.getX() < mapWidth)
            {
                Playerhero.vision.Add(Gmap[Playerhero.getX(), Playerhero.getY() + 1]);
            }
        }

        public void Mapcreate(int numenemies)
        {

            for (int y = 0; y < mapWidth; y++)
            {
                for (int x = 0; x < mapHeight; x++)
                {
                    if (x == 0 || x == mapHeight - 1 || y == 0 || y == mapWidth - 1)
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
                    int randomEnemies = obj.Next(3);
                    if (randomEnemies == 1)
                    {
                        Goblin NewEnemy = new Goblin(EnemyX, EnemyY);
                        Gmap[EnemyX, EnemyY] = NewEnemy;
                        Enemies.Add(NewEnemy);
                    }
                    else if (randomEnemies == 2)
                    {
                        Mage New_enemy = new Mage(EnemyX, EnemyY); //   Task 2 Q.3.1
                        Gmap[EnemyX, EnemyY] = New_enemy;
                        Enemies.Add(New_enemy);
                    }
                    else
                    {
                        Leader new_enemy = new Leader(EnemyX, EnemyY); // Final POE Q.3.1
                        Gmap[EnemyX, EnemyY] = new_enemy;
                        Enemies.Add(new_enemy);
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
    public class Shop
    {
        private Weapon[] weapons = new Weapon[3];

        private Hero Buyer;

        public Shop(Character buyer)
        {
            int rdm_weapon;
           
            Random rweapons = new Random();
            for (int w = 0; w < 4; w++)
            {
                
            }
        }
        //private Weapon RandomWeapon()
        //{

        //}
        //public bool CanBuy(Hero buyer)
        //{
            
        //}
    }
    [Serializable]
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

        public class SaveandLoad
        {
            public string filename = "data.thandomac";
            private SaveandLoad SandL;
            private BinaryFormatter Bformatter;
            public void Save()
            {
                if (SandL == null)
                    SandL = new SaveandLoad();
                var gamefile = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                Bformatter.Serialize(gamefile, SandL);
                gamefile.Close();
            }
            public void Load()
            {
                var gamefile = new FileStream(filename, FileMode.Open, FileAccess.Read);
                if (gamefile != null)
                {
                    SandL = (SaveandLoad)Bformatter.Deserialize(gamefile);
                }
            }
        }




    }
}

