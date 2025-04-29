using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{

    public abstract class Creature
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }
        private int roomIndex = 0;
        public bool Dead = false;

        public Creature(string name, int health, int damage) 
        {
            /*
             * This is the constructor for the Player class
             * Inputs
             * String: name
             * Int: health
             * Outputs:
             * None
             */
            Name = name;
            Health = health;
            Damage = damage;
        }      
        public int GetRoomIndex()
        {
            /*
             * This tells the character which room they are in
             * Inputs:
             * None
             * Outputs:
             * int
             */
            return roomIndex;
        }
        public void SetRoomIndex(int roomIndexUpdate)
        {
            /*
             * This updates the room index when a player changes room
             * Inputs:
             * int: roomIndexUpdate
             * Outputs:
             * None
             */
            roomIndex = roomIndexUpdate;
        }
        public void Heal(int effect)
        {
            Health = Health + effect;
        }
        public int Strike()
        {
            if (!Dead){
            return Damage;
            }else {return 0;}
        }
        public void takeDamage(int damage){
            Health = Health - damage;
            if (damage != 0){
            Console.Write($"\n{Name} took {damage} damage\n");
            if (Health <= 0)
            {
                Console.Write($"{Name} has been slain.\n");
                }else{Console.Write($"{Name} has {Health} Health Points left.\n");}
            }
            if (Health <= 0 ){Dead = true;}
        }
        public bool getDead(){return Dead;}
        
    }
    public class Player : Creature
    {
        public Inventory PlayerInventory = new Inventory();
        public int[] EquipedItem = {-1,-1};
        public int PreviousRoomIndex;
        public Player(string name, int health, int damage) : base(name, health, damage){}
        public void Travel(int direction, Room location)
        {
            /*
             * This allows the character to move between rooms
             * Inputs:
             * Room: Location
             * Int: direction
             * Outputs:
             * None
             */
            int[] availableDirections = location.GetDirections();
            if (direction <=4)
            {
                if (availableDirections[direction] > -1)
                {
                    PreviousRoomIndex = GetRoomIndex();
                    this.SetRoomIndex(availableDirections[direction]);
                    Console.Write("\nYou Enter a new room\n");
                }
                else
                {
                    Console.Write("\nThere is no door there\n");
                }
            } else {Console.Write("\nThat is not a valid direction.\n");}
        }
        public void EquipItem(string playerInput)
        {
            int[] chosenItem = PlayerInventory.searchIndexArrayForEquip(playerInput);
            EquipedItem[0] = chosenItem[0];
            EquipedItem[1] = chosenItem[1];
        }
        public new int Strike()
        {
            if (EquipedItem[0] == 0)
            {
                return Damage + PlayerInventory.getWeapon(EquipedItem[1]).getDamage();
            }
            return Damage;
        }
        public void imbibe(string playerInput){
            int[] chosenItem = PlayerInventory.searchIndexArrayForImbibe(playerInput);
            if (chosenItem[0] == 1)
            {
                if(!PlayerInventory.PotionsArray[chosenItem[1]].getUsed()){
                    Heal(PlayerInventory.PotionsArray[chosenItem[1]].getHealing());
                    Console.WriteLine($"You Were Healed {PlayerInventory.PotionsArray[chosenItem[1]].getHealing()} health points");
                    PlayerInventory.PotionsArray[chosenItem[1]].setUsed(true);
                }
            }
        }
        public void GetStatus()
        {
            Console.Write($"\nHealth: {Health}\nDamage: {Strike()}");
            PlayerInventory.DisplayInventoryContents();
            while (true)
            {
                Console.Write($"\nWhat would you like to do?\n1. Equip an item\n2. Drink a Potion\n3. Close inventory\n");
                ConsoleKeyInfo option = Console.ReadKey();
                if (option.KeyChar.ToString() == "3"){Console.WriteLine(""); break;}
                else if (option.KeyChar.ToString() == "1")
                {
                    Console.Write("\nPlease choose an item (by name)\n");
                    string itemToEquip = Console.ReadLine();
                    EquipItem(itemToEquip);
                    if (EquipedItem[0] == -1){
                        Console.WriteLine("Items unequiped)");
                    } else {Console.Write($"You have Eqiped a {itemToEquip}");}
                } else if (option.KeyChar.ToString() == "2"){
                    Console.Write("\nPlease choose an potion (by full name)\n");
                    string PotionToDrink = Console.ReadLine();
                    PotionToDrink = PotionToDrink.Replace("\n", "");
                    imbibe(PotionToDrink);

                }else{
                    Console.Write("\nThat is not a valid input. Please try again.\n");
                }
            }
        }
        public int getPreviousRoomIndex(){return PreviousRoomIndex;}
    }
    public class Enemy : Creature
    {
        string EnemyType = "";
        public Enemy(string name, int health, int damage) : base(name, health, damage)
        {
        }
    }
    public class Inventory
    {
        public Weapon[] WeaponsArray = new Weapon[3];
        public int WeaponArrayEmptySpacePointer = 0;
        public Potion[] PotionsArray = new Potion[3];
        public int PotionArrayEmptySpacePointer = 0;
        public LightSource[] LightsourceArray = new LightSource[2];
        public int LightSourceArrayEmptySpacePointer = 0;
        public Items[] InventoryContents = new Items[3];
        public int[,] ItemIndexArray = new int[8,2];
        public int[] ItemIDArray = new int[8];
        public int ItemIDIndexArrayEmptySpacePointer = 0;
        public Inventory()
        {
            Weapon emptyWeapon = new Weapon(-1, "", "", -1);
            Potion emptyPotion = new Potion(-1, "", "", -1);
            LightSource emptyLightSource = new LightSource(-1, "", "");
            int valueSetter = 0;
            WeaponsArray[0] = emptyWeapon;
            WeaponsArray[1] = emptyWeapon;
            WeaponsArray[2] = emptyWeapon;
            PotionsArray[0] = emptyPotion;
            PotionsArray[1] = emptyPotion;
            PotionsArray[2] = emptyPotion;
            LightsourceArray[0] = emptyLightSource;
            LightsourceArray[1] = emptyLightSource;
            while (valueSetter < 8)
            {
                ItemIndexArray[valueSetter++,0] = -1;
            }
        }
        public void AddWeapon(Weapon artefact)
        {
            WeaponsArray[WeaponArrayEmptySpacePointer] = artefact;
            artefact.SetIndexID(WeaponArrayEmptySpacePointer);
            WeaponArrayEmptySpacePointer++;
            ItemIDArray[ItemIDIndexArrayEmptySpacePointer] = artefact.GetItemID();
            ItemIndexArray[ItemIDIndexArrayEmptySpacePointer,0] = artefact.getTypeID();
            ItemIndexArray[ItemIDIndexArrayEmptySpacePointer,1] = artefact.getIndexID();
            ItemIDIndexArrayEmptySpacePointer++;
            artefact.SetCollected(true);
        }
        public void AddPotion(Potion artefact)
        {
            PotionsArray[PotionArrayEmptySpacePointer] = artefact;
            artefact.SetIndexID(PotionArrayEmptySpacePointer);
            PotionArrayEmptySpacePointer++;
            ItemIDArray[ItemIDIndexArrayEmptySpacePointer] = artefact.GetItemID();
            ItemIndexArray[ItemIDIndexArrayEmptySpacePointer,0] = artefact.getTypeID();
            ItemIndexArray[ItemIDIndexArrayEmptySpacePointer,1] = artefact.getIndexID();
            ItemIDIndexArrayEmptySpacePointer++;
            artefact.SetCollected(true);
        }
        public void AddLightSource(LightSource artefact)
        {
            LightsourceArray[LightSourceArrayEmptySpacePointer] = artefact;
            artefact.SetIndexID(LightSourceArrayEmptySpacePointer);
            LightSourceArrayEmptySpacePointer++;
            ItemIDArray[ItemIDIndexArrayEmptySpacePointer] = artefact.GetItemID();
            ItemIndexArray[ItemIDIndexArrayEmptySpacePointer,0] = artefact.getTypeID();
            ItemIndexArray[ItemIDIndexArrayEmptySpacePointer,1] = artefact.getIndexID();
            ItemIDIndexArrayEmptySpacePointer++;
            artefact.SetCollected(true);
        }
        public Weapon getWeapon(int indexID){return WeaponsArray[indexID];}
        public void SortByItemID()
        {
            int currentPointer = 0;
            int tempValueStorage;
            bool swap = true;
            while (swap)
            {
                swap = false;
                while (currentPointer < 9)
                {
                    if (ItemIDArray[currentPointer]> ItemIDArray[currentPointer+1])
                    {
                        tempValueStorage = ItemIDArray[currentPointer];
                        ItemIDArray[currentPointer] = ItemIDArray[currentPointer+1];
                        ItemIDArray[currentPointer+1] = tempValueStorage;
                        swap=true;
                    }
                    currentPointer++;
                }
                if (swap == false)
                {
                    break;
                }
                
            }
        }
        public Items[] GetInventoryContents(){return InventoryContents;}
        public void DisplayInventoryContents()
        {
            int displayCounter = 0;
            string displayText;
            while (displayCounter <= 7)
            {
                if (ItemIndexArray[displayCounter, 0] == 0)
                {
                    if (WeaponsArray[ItemIndexArray[displayCounter, 1]].GetItemID() != -1)
                    {
                        displayText = $@"
                        {WeaponsArray[ItemIndexArray[displayCounter, 1]].getItemName()}
                        Item Type: Weapon
                        Description: {WeaponsArray[ItemIndexArray[displayCounter, 1]].GetItemDescription()}";
                        displayText = displayText.Replace("                        ","");
                        Console.Write(displayText);
                    }
                } else if (ItemIndexArray[displayCounter, 0] == 1){
                    if (PotionsArray[ItemIndexArray[displayCounter, 1]].GetItemID() != -1)
                    {
                        displayText = $@"
                        {PotionsArray[ItemIndexArray[displayCounter, 1]].getItemName()}
                        Item Type: Potion
                        Description: {PotionsArray[ItemIndexArray[displayCounter, 1]].GetItemDescription()}
                        Used: {PotionsArray[ItemIndexArray[displayCounter,1]].getUsed()}";
                        displayText = displayText.Replace("                        ","");
                        Console.Write(displayText);
                    }
                } else if (ItemIndexArray[displayCounter, 0] == 2){
                    if (LightsourceArray[ItemIndexArray[displayCounter, 1]].GetItemID() != -1)
                    {
                        displayText = $@"
                        {LightsourceArray[ItemIndexArray[displayCounter, 1]].getItemName()}
                        Item Type: Light Source
                        Description: {LightsourceArray[ItemIndexArray[displayCounter, 1]].GetItemDescription()}";
                        displayText = displayText.Replace("                        ", "");
                        Console.Write(displayText);
                    }
                }else if (ItemIndexArray[displayCounter, 0]== -1) {
                }else {
                    Console.Write("\nUuuuuhhhhh, How? This message shouldn't appear. How did you do this?");
                }
                displayCounter++;
            }
        }
        public int[] searchIndexArrayForEquip(string desiredItem)
        {
            int searchIndex = 0;
            int arrayIndex = 0;
            int[] outputArray = new int[2];
            while (true)
            {
                while (searchIndex < 3){
                    if (arrayIndex == 0)
                    {
                        if (WeaponsArray[searchIndex].getItemName().ToLower() == desiredItem.ToLower())
                        {
                            outputArray[0] = arrayIndex;
                            outputArray[1] = searchIndex;
                            return outputArray;
                        }
                    } else if (arrayIndex == 1){
                        if (PotionsArray[searchIndex].getItemName().ToLower() == desiredItem.ToLower())
                        {
                            Console.Write("A potion is not an equipable item");
                            outputArray[0] = -1;
                            outputArray[1] = -1;
                        }
                    } else if (arrayIndex == 2){
                        if (searchIndex == 2)
                        {
                            Console.Write("You do not possess that item.");
                            outputArray[0] = -1;
                            outputArray[1] = -1;
                            return outputArray;
                        }else if (LightsourceArray[searchIndex].getItemName().ToLower() == desiredItem.ToLower()){
                            outputArray[0] = arrayIndex;
                            outputArray[1] = searchIndex;
                            return outputArray;
                        }
                    }
                    searchIndex++;
                }
                searchIndex = 0;
                arrayIndex++;
                if (arrayIndex == 4){break;}
            }
            outputArray[1] = -1;
            outputArray[0] = -1;
            return outputArray;
        }
        public int[] searchIndexArrayForImbibe(string desiredItem)
        {
            int searchIndex = 0;
            int arrayIndex = 0;
            int[] outputArray = new int[2];
            while (true)
            {
                while (searchIndex < 3){
                    if (arrayIndex == 0)
                    {
                        if (WeaponsArray[searchIndex].getItemName().ToLower() == desiredItem.ToLower()){
                            Console.Write("\nYou cannot drink a weapon.");
                        }
                    } else if (arrayIndex == 1){
                        if (PotionsArray[searchIndex].getItemName().ToLower() == desiredItem.ToLower())
                        {
                            outputArray[0] = arrayIndex;
                            outputArray[1] = searchIndex;
                            return outputArray;
                        }
                    } else if (arrayIndex == 2){
                        if (searchIndex == 2)
                        {
                            Console.Write("You do not possess that item.");
                            return outputArray;
                        }else if (LightsourceArray[searchIndex].getItemName().ToLower() == desiredItem.ToLower()){
                            Console.Write("\nDrinking wax is a bad idea.\n");
                        }
                    } else
                    {Console.Write("You do not own that item");}
                    searchIndex++;
                }
                searchIndex = 0;
                arrayIndex++;
                if (arrayIndex == 4){break;}
            }
            outputArray[1] = -1;
            outputArray[0] = -1;
            return outputArray;
        }
        public int[,] getItemIndexArray(){return ItemIndexArray;}
    }
    public abstract class Items
    {
        public int[] ItemIndex = new int[2];
        public int ItemID;
        public string ItemName;
        public string ItemDescription;
        public bool Equipable;
        public string ItemType;
        public bool Collected = false;
        public Items(int itemID, string itemName, string itemDescription, bool equipable, int typeID, string itemType)
        {
            ItemIndex[0] = typeID;
            ItemName = itemName;
            ItemDescription = itemDescription;
            Equipable = equipable;
            ItemType = itemType;
            ItemID = itemID;
        }
        public int getTypeID(){return ItemIndex[0];}
        public int getIndexID(){return ItemIndex[1];}
        public string getItemName(){return ItemName;}
        public string GetItemType(){return ItemType;}
        public int GetItemID(){return ItemID;}
        public void SetIndexID(int indexID){ItemIndex[1] = indexID;}
        public void SetCollected(bool collected){ Collected = collected;}
        public bool getCollected(){return Collected;}
        public int[] GetItemIndex(){return ItemIndex;}
        public string GetItemDescription(){return ItemDescription;}
    }
    public class Weapon : Items
    {
        int Damage;
        public Weapon(int itemID, string itemName, string itemDescription, int damage) : base(itemID, itemName, itemDescription, true, 0, "Weapon")
        {
            Damage = damage;
        }
        public int getDamage()
        {
            return Damage;
        }
    }
    public class Potion : Items
    {
        public bool Used = false;
        public int Healing;
        public Potion(int itemID, string itemName, string itemDescription, int healing) : base(itemID, itemName, itemDescription, false, 1, "Potion")
        {
            Healing = healing;
        }
        public int getHealing(){return Healing;}
        public void setUsed(bool used){Used = used;}
        public bool getUsed(){return Used;}
    }
    public class LightSource : Items
    {
        public LightSource(int itemID, string itemName, string itemDescription) : base(itemID, itemName, itemDescription, true, 2, "LightSource"){}
        
    }
    public class VoidItem : Items
    {
        public VoidItem() : base(-1, "", "", false, -1, "Void"){}
    }
    public class GameMap
    {
        public void OpenMap(){
            string filePath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            filePath = filePath.Replace("\\bin\\Debug","\\map.jpg");
            Process.Start(filePath);
        }
    }
}