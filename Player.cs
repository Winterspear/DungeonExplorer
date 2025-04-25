using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

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
        public int[] EquipedItem = new int[2];
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
        public void EquipItem(string playerInput)
        {
            int[] chosenItem = PlayerInventory.search(playerInput);
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
            while (displayCounter <= 7)
            {
                if (ItemIndexArray[displayCounter, 0] == 0)
                {
                    Console.Write($@"{WeaponsArray[ItemIndexArray[displayCounter, 1]].getItemName()}
                    Item Type: Weapon
                    Description: {WeaponsArray[ItemIndexArray[displayCounter, 1]].GetItemDescription()}");
                } else if (ItemIndexArray[displayCounter, 0] == 1){
                    Console.Write($@"{PotionsArray[ItemIndexArray[displayCounter, 1]].getItemName()}
                    Item Type: Potion
                    Description: {PotionsArray[ItemIndexArray[displayCounter, 1]].GetItemDescription()}");
                } else if (ItemIndexArray[displayCounter, 0] == 2){
                    Console.Write($@"{LightsourceArray[ItemIndexArray[displayCounter, 1]].getItemName()}
                    Item Type: Light Source
                    Description: {LightsourceArray[ItemIndexArray[displayCounter, 1]].GetItemDescription()}");
                } else {
                    Console.Write("Uuuuuhhhhh, How? This message shouldn't appear. How did you do this?");
                }
            }
        }
        public int[] search(string desiredItem)
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
        public int GetItemID(){return ItemID;}
        public void SetIndexID(int indexID){ItemIndex[1] = indexID;}
        public void SetCollected(bool collected){ Collected = collected;}
        public int[] GetItemIndex(){return ItemIndex;}
        public string GetItemDescription(){return ItemDescription;}
    }
    public class Weapon : Items
    {
        int Damage;
        public Weapon(int itemID, string itemName, string itemDescription, int damage, string itemType, int indexID) : base(itemID, itemName, itemDescription, true, 0, itemType, indexID)
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
        public Potion(int itemID, string itemName, string itemDescription, int healing, string itemType, int indexID) : base(itemID, itemName, itemDescription, false, 1, itemType, indexID)
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
        public LightSource(int itemID, string itemName, string itemDescription, string itemType, int indexID) : base(itemID, itemName, itemDescription, true, 2, itemType, indexID)
        {
            
        }
        
    }
    public class GameMap
    {

    }
}