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
            gameengine.Showmap();
            mapLabel.Text = gameengine.MapLabel.Text;
            updateForm();

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
        

        private void LeftButton(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.Left);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();
            updateForm();
        }

        private void Upbutton(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.up);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();
            updateForm();
        }

        private void RightButton(object sender, EventArgs e)
        {
            gameengine.MovePlayer(Character.Movement.Right);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();
            updateForm();

        }

        private void DownButton(object sender, EventArgs e)
        {
            
            gameengine.MovePlayer(Character.Movement.Down);
            gameengine.Showmap();
            mapLabel.Text = gameengine.MAP.ToString();
            updateForm();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        public void updateForm()
        {

            richTextBox1.Text = gameengine.MAP.PLAYERHERO.ToString();
        }

        private void Attack_Click(object sender, EventArgs e)
        {

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
        public Item(int x, int y, string symbol) : base(x, y)
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
        public Weapon(int x, int y) : base(x, y, "W")
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
        
        public MeleeWeapon(int x, int y, Types meleeWeapon) : base(x, y)
        {
            switch (meleeWeapon)
            {
                case Types.dagger:
                    this.durability = 10;
                    this.wdamage = 3;
                    this.cost = 3;
                    this.Symbol = "D";                
                    break;
                case Types.longsword:
                    this.durability = 6;
                    this.wdamage = 4;
                    this.cost = 5;
                    this.Symbol = "S";
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
       
        public RangedWeapon(int x, int y, Types rangeWeapon) : base(x, y)
        {
            switch (rangeWeapon)
            {
                case Types.Rifle:
                    this.durability = 3;
                    this.range = 3;
                    this.wdamage = 5;
                    this.cost = 7;
                    this.Symbol = "R";
                    break;
                case Types.Longbow:
                    this.durability = 4;
                    this.range = 2;
                    this.wdamage = 4;
                    this.cost = 6;
                    this.Symbol = "B";
                    break;
            }
        }
        public override int RANGE()
        {
            return this.range;
        }
    }
   

    public class Gold : Item //Task 2 Q.2.3
    {
        protected int goldAmount;

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

        public Gold(int x, int y) : base(x, y, "€")
        {
            this.X = x;
            this.Y = y;
            this.Symbol = "€";
            goldAmount = goldObj.Next(1, 5);
        }
    }

    public class EmptyTile : Tile
    {
        public EmptyTile(int x, int y) : base(x, y)
        {
            this.typeofTile = Tiletype.Empty;
            this.Symbol = ".";
        }
    }

    public abstract class Character : Tile //Question 2.2
    {
        protected int Hp;
        protected int max_Hp;
        protected int Damage;
        protected int Goldpurse = 0;
        protected Weapon currentweapon;
        public int getGoldpurse()
        {
            return Goldpurse;
        }
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
                this.Goldpurse = this.Goldpurse + number.getGold();
            }

            if (i is Weapon)
            {
                 currentweapon = (Weapon)i;
            }
            
        }
        public void Equip(Weapon w)
        {

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
                    X--;
                    break;
                case Movement.Down:
                    X++;
                    break;
                case Movement.Left:
                    Y--;
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
            this.vision.Add(gmap[X,Y - 1]);
            this.vision.Add(gmap[X,Y + 1]);
        }
        public void damage(int dmg)
        {
            this.Hp -= dmg;
        }
        public void useGold(int cost) // To spend the Gold in shop
        {
            this.Goldpurse -= cost;
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
            this.currentweapon = new MeleeWeapon(x, Y, MeleeWeapon.Types.longsword);
            this.Goldpurse = 2;
        }
        public override Movement returnMove(Movement move)
        {
            switch (move)
            {
                case Movement.up:
                    if ((this.vision[0] is EmptyTile))
                    {
                        return Movement.up;
                    }
                    break;
                case Movement.Down:
                    if ((this.vision[1] is EmptyTile))
                    {
                        return Movement.Down;
                    }
                    break;
                case Movement.Left:
                    if ((this.vision[2] is EmptyTile))
                    {
                        return Movement.Left;
                    }
                    break;
                case Movement.Right:
                    if ((this.vision[3] is EmptyTile))
                    {
                        return Movement.Right;
                    }
                    break;
                default:
                    return Movement.No_movement;
                    break;
            }
            return Movement.No_movement;

        }
    }
    public class Goblin : Enemy
    {
        public Goblin(int x, int y) : base(x, y, 1, 10, 10, "G")
        {
            
            this.currentweapon = new MeleeWeapon(x, Y, MeleeWeapon.Types.dagger);
            this.Goldpurse = 1;
        }
        public override Movement returnMove(Movement move ) // Goblin movement working from Task 1
        {
            
            switch (move)
            {
                case Movement.up:
                    if ( (this.vision[0] is EmptyTile))
                    {
                        return Movement.up;
                    }
                    break;
                case Movement.Down:
                    if ( (this.vision[1] is EmptyTile))
                    {
                        return Movement.Down;
                    }
                    break;
                case Movement.Left:
                    if ( (this.vision[2] is EmptyTile))
                    {
                        return Movement.Left;
                    }
                    break;
                case Movement.Right:
                    if ( (this.vision[3] is EmptyTile))
                    {
                        return Movement.Right;
                    }
                    break;
                default:
                    return Movement.No_movement;
                    break;
            }
            return Movement.No_movement;

        }
    }
    public class Mage : Enemy
    {
        public Mage(int x, int y) : base(x, y, 5, 5, 5, "M")
        {
            this.Goldpurse = 3;
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

        public Hero(int x, int y, int hp, int max_hp, int damage) : base(x, y, "H")
        {
            this.Hp = 50;
            this.max_Hp = 50;
            this.Damage = 2;

        }

        public override Movement returnMove(Movement move = Movement.No_movement) // Character is finally moving on runtime
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
                case Movement.Left:
                    if (this.vision[2] is EmptyTile || this.vision[2] is Item)
                    {
                        return Movement.Left;
                    }
                    else
                    {
                        return Movement.No_movement;
                    }
                case Movement.Right:
                    if (this.vision[3] is EmptyTile || this.vision[3] is Item)
                    {
                        return Movement.Right;
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
            string Weapon;
            if (currentweapon == null)
            {
                string info = "Player Stats:" + "\n";
                info += "Hp:" + Hp + "/" + max_Hp + "\n";
                info += "Current Weapon: Bare hands" + "\n";
                info += "Damage:" + Damage + "\n";
                info += "[" + X.ToString() + "," + Y.ToString() + "] \n";
                info += "Gold amount:" + getGoldpurse(); //Task 2 Question 3.2
                return info;
            }
            Weapon = currentweapon.ToString();
            return ("Player Stats:" + "\n" + "Hp:" + Hp + "/" + max_Hp + "\n" + "Current Weapon:" + Weapon + "\n" + "Weapon Range:"+this.currentweapon.RANGE() + "\nDamage:" + Damage + "\n" + "[" + X.ToString() + "," + Y.ToString() + "] \n" + "Gold amount:" + getGoldpurse()); ;
        }
        bool CheckforValidMove(Movement move)
        {
            bool isValid = false;

            switch (move)
            {
                case Movement.Right:
                    foreach (Tile T in vision)
                    {
                        if (T.getY() == Y + 1)
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
                        if (T.getY() == Y - 1)
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
        public override void Attack(Character Target)
        {
            Target.damage(this.Damage);
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
        public Map(int min_height, int max_height, int min_width, int max_width, int numenemies, int gold_amount, int weaponNum)
        {
            mapHeight = randomize(min_height, max_height);
            mapWidth = randomize(min_width, max_width);
            Gmap = new Tile[mapWidth, mapHeight];
            //fillEmpty();
            Mapcreate(numenemies);
            for (int x = 0; x <gold_amount; x++)
            {
                Create(Tile.Tiletype.Gold, 0, 0);
            }
            for (int x = 0; x < weaponNum; x++ )
            {
                Create(Tile.Tiletype.Weapon, 0, 0);
            }
            
            updateVision();
            
        }
        public void moveEnemy()
        {
            foreach (Enemy E in Enemies)
            {
                updateVision();
                Character.Movement move;
                int randomMove = randomize(0, 4);
                switch (randomMove)
                {
                    case 0:
                        move = Character.Movement.up;
                        break;
                    case 1:
                        move = Character.Movement.Down;
                        break;
                    case 2:
                        move = Character.Movement.Left;
                        break;
                    case 3:
                        move = Character.Movement.Right;
                        break;
                    default:
                        move = Character.Movement.No_movement;
                        break;

                }

                int x = E.getX();
                int y = E.getY();
                E.move(E.returnMove(move));
                
                if (!((x == E.getX()) && (y == E.getY())))
                {
                    GMAP[E.getX(), E.getY()] = E;
                    GMAP[x, y] = new EmptyTile(x, y);
                }
                
            }
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
                    
                    if (Gmap[i,j] == null)
                    {
                        Gmap[i, j] = new EmptyTile(i, j);
                    }
                }
            }
        }
        public override string ToString()
        {
            string mapstring = "";

            for (int y = 0; y < mapWidth; y++)
            {
                for (int x = 0; x < mapHeight; x++)
                {
                    mapstring += Gmap[y, x].Symbol;
                }
                mapstring += "\n";

            }
            return mapstring;
        }

        public void updateVision() // updateVisision working perfectly from Task 1
        {
            foreach (Enemy E in Enemies)
            {
                E.vision.Clear();
                E.setVision(GMAP);            
            }
            Playerhero.vision.Clear();
            Playerhero.setVision(GMAP); 
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

                    Hero NewHero = new Hero(heroX, heroY, 50, 50, 2);
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
                    int GoldX = obj.Next(0, mapWidth);
                    int GoldY = obj.Next(0, mapHeight);

                    while (Gmap[GoldX, GoldY].typeofTile != Character.Tiletype.Empty)
                    {
                        GoldX = obj.Next(0, mapWidth);
                        GoldY = obj.Next(0, mapHeight);
                    }
                    Gold newGold = new Gold(GoldX, GoldY);
                    Gmap[GoldX, GoldY] = newGold;
                    Items.Add(newGold);
                    break;
                case Character.Tiletype.Weapon:
                    int random = obj.Next(4);
                    int WeaponX = obj.Next(0, mapWidth);
                    int WeaponY = obj.Next(0, mapHeight);

                    while (Gmap[WeaponX, WeaponY].typeofTile != Character.Tiletype.Empty)
                    {
                        WeaponX = obj.Next(0, mapWidth);
                        WeaponY = obj.Next(0, mapHeight);
                    }
                    switch (random)
                    {
                        case 0:
                            Gmap[WeaponX, WeaponY] = new MeleeWeapon(WeaponX, WeaponY, MeleeWeapon.Types.dagger);
                            Items.Add((Weapon)Gmap[WeaponX, WeaponY]);
                            break;
                        case 1:
                            Gmap[WeaponX, WeaponY] = new MeleeWeapon(WeaponX, WeaponY, MeleeWeapon.Types.longsword);
                            Items.Add((Weapon)Gmap[WeaponX, WeaponY]);
                            break;
                        case 2:
                            Gmap[WeaponX, WeaponY] = new RangedWeapon(WeaponX, WeaponY, RangedWeapon.Types.Rifle);
                            Items.Add((Weapon)Gmap[WeaponX, WeaponY]);
                            break;
                        case 3:
                            Gmap[WeaponX, WeaponY] = new RangedWeapon(WeaponX, WeaponY, RangedWeapon.Types.Longbow);
                            Items.Add((Weapon)Gmap[WeaponX, WeaponY]);
                            break;
                        default:
                            Gmap[WeaponX, WeaponY] = new EmptyTile(WeaponX, WeaponY);
                            break;

                    }
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
        int Cost;
        public int getCost()
        {
            return Cost;
        }

        public Shop(Character buyer)
        {
            for (int w = 0; w < 4; w++)
            {
                weapons[w] = RandomWeapon();
            }

        }
        private Weapon RandomWeapon()
        {
            Random rweapons = new Random();
            int rdm_weapon;
            rdm_weapon = rweapons.Next(1, 5);
            switch (rdm_weapon)
            {
                case 1:
                    return new MeleeWeapon(0, 0, MeleeWeapon.Types.dagger);
                case 2:
                    return new MeleeWeapon(0, 0, MeleeWeapon.Types.longsword);
                case 3:
                    return new RangedWeapon(0, 0, RangedWeapon.Types.Rifle);
                case 4:
                    return new RangedWeapon(0, 0, RangedWeapon.Types.Rifle);
                default:
                    return new MeleeWeapon(0, 0, MeleeWeapon.Types.dagger);
            }

        }
        public bool CanBuy(int price)
        {
            if (Buyer.getGoldpurse() >= price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Buy(int num)
        {
            Buyer.useGold(weapons[num].COST);
            Buyer.Pickup(weapons[num]);
            weapons[num] = RandomWeapon();
        }
        //public string DisplayWeapon(int num)
        //{
            
        //}
    }
    [Serializable]
    public class GameEngine
    {
        public Label MapLabel = new Label();
        private Map map;

        public override string ToString()
        {
            string display = "";
            for (int x = 0; x < map.mapHeight; x++)
            {
                for (int y = 0; y < map.mapWidth; y++)
                {
                    if (map.getItemAtPosition(x,y) is Gold)
                    {
                        display = "€";
                    }

                }
            }
            return display;
        }

        public Map MAP
        {
            get { return map; }
            set { map = value; }
        }

        public GameEngine()
        {
            map = new Map(15, 30, 15, 30, 5, 5,3);

        }
        public void Showmap()
        {

            MapLabel.Text = map.ToString();

        }

        public bool MovePlayer(Character.Movement direction)
        {
            Tile H;
            int x, y;
            Item i;
            if (map.PLAYERHERO.returnMove(direction) == direction)
            {
               
                x = map.PLAYERHERO.getX();
                y = map.PLAYERHERO.getY();
                map.PLAYERHERO.move(direction);
                i = map.getItemAtPosition(map.PLAYERHERO.getX(), map.PLAYERHERO.getY());
                if (i is Item)
                {
                    map.PLAYERHERO.Pickup(i);
                }
                map.GMAP[map.PLAYERHERO.getX(), map.PLAYERHERO.getY()] = map.PLAYERHERO;
                map.GMAP[x, y] = new EmptyTile(x, y);

                map.moveEnemy();
                map.updateVision();
                

                

                return true;

            }


            return false;
        }
        //public void enemiesAttack()
        //{
        //    Enemy Enemyattacker;
        //    for (int x = 0; x < map.Enemies.Count; x++)
        //    {
        //        Enemyattacker = map.Enemies[x];
        //        for (int j = 0; j < map.getVisionSize(); j++)
        //        {
        //            if (Enemyattacker.)
        //            {

        //            }
        //        }
                
        //    }
        //}

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

