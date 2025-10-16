
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
