using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DungeonExplorer
{
    public abstract class Creature
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }
        private int roomIndex = 0;

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
            return Damage;
        }
        
    }
    public class Player : Creature
    {
        public Inventory PlayerInventory = new Inventory();
        public int EquipedItem;
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
            if (availableDirections[direction] > -1 & availableDirections[direction] < 2)
            {
                this.SetRoomIndex(availableDirections[direction]);
                Console.Write("\nYou Enter a new room\n");
            }
            else if (availableDirections[direction]>1){
                Console.Write("\nThe door appears to be locked\nRoom not yet implemented.\n"); 
                // This will be replace if I update the game further
            }
            else
            {
                Console.Write("\nThere is no door there\n");
            }
        }
        public void EquipItem()
        {

        }
        public new int Strike()
        {
            if (EquipedItem != -1)
            {
                if (PlayerInventory.GetInventoryContents()[EquipedItem].GetItemType() == "Weapon")
                {
                    return Damage + PlayerInventory.GetInventoryContents()[EquipedItem].getDamage();
                }
            }
            return Damage;
        }
    }
    public class Enemy : Creature
    {
        string EnemyType = "";
        public Enemy(string name, int health, string monsterType, int damage) : base(name, health, damage)
        {
            EnemyType = monsterType;
        }
    }
    public class Inventory
    {
        public Weapon[] WeaponsArray = new Weapon[3];
        public Potion[] PotionsArray = new Potion[3];
        public LightSource[] LightsourceArray = new LightSource[2];
        public Items[] InventoryContents = new Items[3];
        public int[] IndexArray = new int[8];
        public int[] ItemIDArray = new int[8];
        public int EmptySpacePointer = 0;
        public Inventory()
        {

        }
        public void AddItem(Items artefact)
        {
            if (artefact.GetItemType() == "Weapon")
            {
            InventoryContents[EmptySpacePointer] = artefact;
            ItemIDArray[EmptySpacePointer] = artefact.getIndexID();
            }
            EmptySpacePointer++;
        }
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
    }
    public abstract class Items
    {
        public int[] ItemIndex = new int[2];
        public int ItemID;
        public string ItemName;
        public string ItemDescription;
        public bool Equipable;
        public string ItemType;
        public Items(int itemID, string itemName, string itemDescription, bool equipable, int typeID, string itemType, int indexID)
        {
            ItemIndex[0] = typeID;
            ItemIndex[1] = indexID;
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
    }
    public class Weapon : Items
    {
        int Damage;
        public Weapon(int itemID, string itemName, string itemDescription, int damage, string itemType) : base(itemID, itemName, itemDescription, true, 0, itemType)
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
        public Potion(int itemID, string itemName, string itemDescription, int healing, string itemType) : base(itemID, itemName, itemDescription, false, 1, itemType)
        {
            Healing = healing;
        }
        public void imbibe(Player user)
        {
            user.Heal(Healing);
        }
    }
    public class LightSource : Items
    {
        public LightSource(int itemID, string itemName, string itemDescription, string itemType) : base(itemID, itemName, itemDescription, true, 2, itemType)
        {
            
        }
        
    }
    public class GameMap
    {

    }
}