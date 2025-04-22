using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
        
    }
    public class Player : Creature
    {
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
        public Weapon[] StoredWeapons = new Weapon[3];
        public Potion[] StoredPotions = new Potion[3];
        public LightSource[] StoredLightSources = new LightSource[2];
        public List<Items[]> InventoryContents = new List<Items[]>();
        public Inventory()
        {
            InventoryContents.Add(StoredWeapons);
            InventoryContents.Add(StoredPotions);
            InventoryContents.Add(StoredLightSources);
        }
        public void addWeapon(Weapon artefact)
        {
            InventoryContents[0][artefact.getItemID()] = (artefact);
        }
    }
    public abstract class Items
    {
        public int[] ItemID = new int[2];
        public string ItemName;
        public string ItemDescription;
        public bool Equipable;
        public Items(int itemID, string itemName, string itemDescription, bool equipable, int typeID)
        {
            ItemID[0] = typeID;
            ItemID[1] = itemID;
            ItemName = itemName;
            ItemDescription = itemDescription;
            Equipable = equipable;
        }
        public int getTypeID()
        {
            return ItemID[0];
        }
        public int getItemID()
        {
            return ItemID[1];
        }
    }
    public class Weapon : Items
    {
        int Damage;
        bool equiped = false;
        public Weapon(int itemID, string itemName, string itemDescription, int damage) : base(itemID, itemName, itemDescription, true, 0)
        {
            Damage = damage;
        }
    }
    public class Potion : Items
    {
        public int Healing;
        public Potion(int itemID, string itemName, string itemDescription, int healing) : base(itemID, itemName, itemDescription, false, 1)
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
        bool equiped = false;
        public LightSource(int itemID, string itemName, string itemDescription) : base(itemID, itemName, itemDescription, true, 2)
        {
            
        }
        
    }
    public class GameMap
    {

    }
}