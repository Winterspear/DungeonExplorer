using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();
        private int roomIndex = 0;

        public Player(string name, int health) 
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
        }
        public void PickUpItem(string[] collectable)
        {
            /*
             * Adds an item to the inventory array
             * Inputs:
             * String Array: collecatbles (2 items)
             * Outputs:
             * None
             */
            this.inventory.AddRange(collectable);
        }
        public string InventoryContents()
        {
            /*
             * Turns the inventory array into a string to dsplay it
             * Input:
             * None
             * Output:
             * String
             */
            return string.Join("\n", inventory);
        }
        public string GetStatus()
        {
            /*
             * This displays the character's health and inventory to the player
             * Inputs:
             * None
             * Outputs:
             * String
             */
            string healthString = Health.ToString();
            string archive = InventoryContents();
            int archiveCount = archive.Length;
            if (archiveCount <=2)
            {
                archive = "Empty";
            }
            return $"Health: {healthString}\nInventory:\n{archive}";
            
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
}