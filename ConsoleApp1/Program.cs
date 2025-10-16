
using System;

class Program
{
    static Random random = new Random();

    // Класс игрока
    class Player
    {
        public int HP { get; set; } = 100;
        public int MaxHP { get; set; } = 100;
        public Item Weapon { get; set; } = new Item("Ржавый меч", 10);
        public Item Armor { get; set; } = new Item("Старая куртка", 5);
        public bool IsFrozen { get; set; } = false;

        public void EquipWeapon(Item newWeapon)
        {
            Console.WriteLine($"\nТекущее оружие: {Weapon.Name} (атака: {Weapon.Value})");
            Console.WriteLine($"Новое оружие: {newWeapon.Name} (атака: {newWeapon.Value})");
            Console.Write("Заменить оружие? (да/нет): ");
            if (Console.ReadLine().ToLower() == "да")
            {
                Weapon = newWeapon;
                Console.WriteLine($"Экипировано: {newWeapon.Name}");
            }
        }

        public void EquipArmor(Item newArmor)
        {
            Console.WriteLine($"\nТекущие доспехи: {Armor.Name} (защита: {Armor.Value})");
            Console.WriteLine($"Новые доспехи: {newArmor.Name} (защита: {newArmor.Value})");
            Console.Write("Заменить доспехи? (да/нет): ");
            if (Console.ReadLine().ToLower() == "да")
            {
                Armor = newArmor;
                Console.WriteLine($"Экипировано: {newArmor.Name}");
            }
        }

        public void Heal()
        {
            HP = MaxHP;
            Console.WriteLine("Игрок полностью исцелён!");
        }
    }

    // Класс предмета
    class Item
    {
        public string Name { get; }
        public int Value { get; }

        public Item(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    // Класс врага
    class Enemy
    {
        public string Name { get; }
        public int HP { get; set; }
        public int Attack { get; }
        public int Defense { get; }
        public double CritChance { get; }
        public double FreezeChance { get; }
        public bool IgnoreDefense { get; }

        public Enemy(string name, int hp, int attack, int defense, double critChance = 0, double freezeChance = 0, bool ignoreDefense = false)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
            CritChance = critChance;
            FreezeChance = freezeChance;
            IgnoreDefense = ignoreDefense;
        }
    }
    // Создание врага
    static Enemy CreateEnemy()
    {
        int type = random.Next(3);
        switch (type)
        {
            case 0: // Гоблин
                return new Enemy("Гоблин", 30, 15, 5, critChance: 0.2);
            case 1: // Скелет
                return new Enemy("Скелет", 40, 12, 8, ignoreDefense: true);
            case 2: // Маг
                return new Enemy("Маг", 25, 10, 3, freezeChance: 0.15);
            default:
                return new Enemy("Гоблин", 30, 15, 5, critChance: 0.2);
        }
    }

    // Создание босса
    static Enemy CreateBoss(int turn)
    {
        int type = random.Next(4);
        switch (type)
        {
            case 0: // ВВГ (Гоблин)
                return new Enemy("ВВГ", (int)(30 * 2.0), (int)(15 * 1.5), (int)(5 * 1.2), critChance: 0.3);
            case 1: // Ковальский (Скелет)
                return new Enemy("Ковальский", (int)(40 * 2.5), (int)(12 * 1.3), (int)(8 * 1.4), ignoreDefense: true);
            case 2: // Архимаг C++
                return new Enemy("Архимаг C++", (int)(25 * 1.8), (int)(10 * 1.6), (int)(3 * 1.1), freezeChance: 0.25);
            case 3: // Пестов С--
                return new Enemy("Пестов С--", (int)(40 * 1.3), (int)(12 * 1.8), (int)(8 * 0.6), freezeChance: 0.3, ignoreDefense: true);
            default:
                return new Enemy("ВВГ", (int)(30 * 2.0), (int)(15 * 1.5), (int)(5 * 1.2), critChance: 0.3);
        }
    }

    // Создание предмета из сундука
    static void OpenChest(Player player)
    {
        int type = random.Next(3);
        switch (type)
        {
            case 0: // Зелье
                Console.WriteLine("Найдено лечебное зелье!");
                player.Heal();
                break;
            case 1: // Оружие
                Item newWeapon = new Item($"Меч +{random.Next(5, 16)}", random.Next(5, 16));
                player.EquipWeapon(newWeapon);
                break;
            case 2: // Доспехи
                Item newArmor = new Item($"Доспехи +{random.Next(3, 11)}", random.Next(3, 11));
                player.EquipArmor(newArmor);
                break;
        }
    }
    // Бой
    static bool Fight(Player player, Enemy enemy)
    {
        Console.WriteLine($"\nБой с {enemy.Name}! (HP: {enemy.HP}, Атака: {enemy.Attack}, Защита: {enemy.Defense})");

        while (enemy.HP > 0 && player.HP > 0)
        {
            if (player.IsFrozen)
            {
                Console.WriteLine("Игрок заморожен и пропускает ход!");
                player.IsFrozen = false;
            }
            else
            {
                Console.Write("\nВаш ход (атака/защита): ");
                string action = Console.ReadLine().ToLower();
                bool isDefending = action == "защита";

                // Ход игрока
                if (action == "атака")
                {
                    int damage = Math.Max(0, player.Weapon.Value - enemy.Defense);
                    enemy.HP -= damage;
                    Console.WriteLine($"Вы нанесли {damage} урона. HP врага: {enemy.HP}");
                }

                // Проверка на уклонение (40% шанс при защите)
                bool dodged = isDefending && random.NextDouble() < 0.4;
                if (dodged)
                {
                    Console.WriteLine("Вы уклонились от атаки врага!");
                    continue;
                }
            }

            // Ход врага
            if (enemy.HP > 0)
            {
                int damage = enemy.Attack;
                if (enemy.CritChance > 0 && random.NextDouble() < enemy.CritChance)
                {
                    damage *= 2;
                    Console.WriteLine($"{enemy.Name} нанёс критический урон!");
                }

                if (!enemy.IgnoreDefense && !player.IsFrozen)
                {
                    int block = random.Next((int)(player.Armor.Value * 0.7), player.Armor.Value + 1);
                    damage = Math.Max(0, damage - block);
                }

                player.HP -= damage;
                Console.WriteLine($"{enemy.Name} нанёс {damage} урона. Ваше HP: {player.HP}");

                if (enemy.FreezeChance > 0 && random.NextDouble() < enemy.FreezeChance)
                {
                    player.IsFrozen = true;
                    Console.WriteLine($"{enemy.Name} заморозил вас! Пропустите следующий ход.");
                }
            }
        }

        if (player.HP <= 0)
        {
            Console.WriteLine("Вы погибли! Игра окончена.");
            return false;
        }

        Console.WriteLine($"{enemy.Name} повержен!");
        return true;
    }

    // Основной игровой цикл
    static void Main()
    {
        Player player = new Player();
        int turn = 0;

        Console.WriteLine("Добро пожаловать в рогалик!");

        while (player.HP > 0)
        {
            turn++;
            Console.WriteLine($"\nХод {turn}. Ваше HP: {player.HP}");
            Console.WriteLine($"Оружие: {player.Weapon.Name} (атака: {player.Weapon.Value})");
            Console.WriteLine($"Доспехи: {player.Armor.Name} (защита: {player.Armor.Value})");

            if (turn % 10 == 0)
            {
                // Босс каждые 10 ходов
                Enemy boss = CreateBoss(turn);
                if (!Fight(player, boss))
                    break;
            }
            else
            {
                // Случайное событие: враг или сундук
                if (random.NextDouble() < 0.5)
                {
                    Enemy enemy = CreateEnemy();
                    if (!Fight(player, enemy))
                        break;
                }
                else
                {
                    Console.WriteLine("\nВы нашли сундук!");
                    OpenChest(player);
                }
            }
        }
    }
}