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
        private string action = "Error";
        private bool itemPickedUp = false;
        private int index;
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
        public void SetItem(string itemName, string itemDescription)
        {
            /*
             * This is the creation of an item
             * Inputs:
             * string: itemName, itemDescription
             * Outputs:
             * None
             */
            this.collectable[0] = itemName;
            this.collectable[1] = itemDescription;
        }
        public void SetAction(string itemAction){this.action = itemAction;}
        /*
         * Seen above is the setting of a story telling element on how the character picks up the item
         * Inputs:
         * string: itemAction
         * Outputs:
         * None
         */
        public string[] GetCollectable()
        {
            /*
             * This allows the Character to pick up an item, and tell if an item has already been collected
             * Inputs:
             * None
             * Outputs:
             * string: (Either collectable or empty)
             */
            string[] empty = new string[1];
            if (this.itemPickedUp == false)
            {
                this.UpdateAction();
                return this.collectable;
            }
            else{return empty;}
        }
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

    }
}