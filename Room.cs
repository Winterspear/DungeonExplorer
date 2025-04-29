using System;
using System.Data;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace DungeonExplorer
{
    public class Room
    {
        private string description;
        private int[] directions = {-1, -1, -1, -1};
        private string[] collectable = new string[2];
        public Weapon RoomWeapon;
        public Potion RoomPotion;
        public LightSource RoomLightSource;
        public string RoomItemType = "Void";
        private string action = "Error";
        private bool itemPickedUp = false;
        private int index;
        public bool Dark;
        public bool VictoryRoom = false;
        public Enemy Hostile;
        public Room(string description){this.description = description;}
        /*
         * Shown above is the constructor for the Room class
         * Inputs:
         * String: description
         * Outputs:
         * None
         */

        public string GetDescription(){return this.description;}
        /*
         * This function (Shown above) allows the room's description to be shown.
         * Inputs:
         * None
         * Outputs:
         * String
         */
        
        public void SetAdjacent(int north, int east, int south, int west)
        {
            /*
             * This function is used to tell what rooms are agacent to this one, allowing for traversal
             * Inputs:
             * int: north, east, south, west
             * Outputs:
             * None
             */
            this.directions[0] = north;
            this.directions[1] = east;
            this.directions[2] = south;
            this.directions[3] = west;
        }
        public int[] GetDirections(){return this.directions;}
        /*
         * Shown above is a function that returns the directions
         * Inputs:
         * None
         * Outputs:
         * Int Array: (4 Items)
         */
        public void SetWeapon(Weapon roomWeapon)
        {
            RoomWeapon = roomWeapon;
            RoomItemType = "Weapon";
        }
        public void SetPotion(Potion roomPotion)
        {
            RoomPotion = roomPotion;
            RoomItemType = "Potion";
        }
        public void setLightSource(LightSource roomLightSource)
        {
            RoomLightSource = roomLightSource;
            RoomItemType = "LightSource";
        }
        public void SetAction(string itemAction){this.action = itemAction;}
        /*
         * Seen above is the setting of a story telling element on how the character picks up the item
         * Inputs:
         * string: itemAction
         * Outputs:
         * None
         */
         public Weapon getRoomWeapon(){return RoomWeapon;}
         public Potion getRoomPotion(){return RoomPotion;}
         public LightSource getRoomLightSource(){return RoomLightSource;}
         public string getRoomItemType(){return RoomItemType;}
        public string GetAction(){return this.action;}
        /*
         * Shown above is the function that returns how the character picks up the item
         * Inputs:
         * None
         * Outputs:
         * String: action
         */
        public void UpdateAction()
        {
            /*
             * This will update weather the item has been collected or not:
             * Inputs:
             * None
             * Outputs:
             * None
             */
            this.action = "You have already gotten the item from this room";
            this.itemPickedUp = true;
        }
        public bool GetItemPickedUp(){return this.itemPickedUp;}
        /*
         * Shown above is a test for if the item has been picked up
         * Inputs:
         * None
         * Outputs:
         * Bool: itemPickedUp
         */
        public void SetIndex(int submittedindex){this.index=submittedindex;}
        /*
         * Shown above is the setting of the rooms' index
         * Inputs:
         * int: Submittedindex
         * Outputs:
         * None
         */
        public int GetIndex(){return this.index;}
        /*
         * Shown above is the returning of the rooms' index
         * Inputs:
         * None
         * Outputs:
         * Int: Index
         */
        public void SetDark(bool dark){Dark = dark;}
        public void SetVicoryRoom(bool win){VictoryRoom = win;}
        public bool GetVictoryRoom(){return VictoryRoom;}
        public bool getDark(){return Dark;}
        public void setHostile(Enemy hostile){Hostile = hostile;}
        public Enemy getHostile(){return Hostile;}
    }
}