using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Media;
using System.Reflection;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private List<Room> allRooms = new List<Room>();

        public Game()
        {
            /*
             * This constructor creates the player object
             * Inputs:
             * None
             * Outputs:
             * None
             */
            this.player = new Player("Username", 100);
        }
        public void GenerateRooms()
        {
            /*
             * This function is used to create the room objects and add them 
             * to an array for ease of access later in the code
             * Inputs:
             * None
             * Outputs:
             * None
             */
            string design = @"Before you is a large pair of oak doors,
                held together with wrought iron bolts, rusted due to time.
                Beside the doors, rests small lanterns, they look to have
                been untouched for years. The doors themsleves are inset into
                a marble frame, that itself looks remarkably clean in contrast
                to its surroundings.";
            design = design.Replace("                ", "");
            Room mainEntrance = new Room(design);
            mainEntrance.SetItem("Empty Lantern", "This item can be combined with a candle to create a light source, letting you traverse dark rooms.");
            string action = @"You reach up to where one of the lanterns
                rests. Finding the candle has long since been useable, you 
                extract and discard the residual wax, safely storing the 
                lantern away in your bag.";
            action = action.Replace("                ", " ");
            mainEntrance.SetAction(action);
            mainEntrance.SetIndex(0);
            mainEntrance.SetAdjacent(1, -1, -1, -1);
            this.allRooms.Add(mainEntrance);
            design = @"Making it through the large double doors, you are
                faced with a foyer a mansion's foyer. It has clearly seen
                better days, there are bookcases lining the west wall,
                however several shelves are rotten and broken, leaving
                large numbers of books - spotted with black mould - covering
                the floor, there are 2 doors leading out of the room, one to
                the north and one to the east. By the east door, a chest of
                draws rests, looking weathered and worn. 2 Paintings rest
                either side of the North door, all of them portraits,
                although the faces have all been scratched out, the marks
                looking as if they were claws, though too large for any
                housepet.";
            design = design.Replace("                ", " ");
            Room foyer = new Room(design);
            foyer.SetItem("Candle","This item can be combined with an empty lantern to create a light source, letting you traverse dark rooms.");
            action = @"You reach the draws and gently pull one open, you 
                find a candle in oddly prestine condition, deciding it could
                be useful down the line, you place it in your bag.";
            action = action.Replace("                ", " ");
            foyer.SetAction(action);
            foyer.SetIndex(1);
            foyer.SetAdjacent(2, 3, 0, -1);
            this.allRooms.Add(foyer);
        }
        public void Start()
        {
            /*
             * This is where the Game starts to play, allowing users to make choices and interact with the games features
             * Inputs:
             * None
             * Outputs:
             * None
             */
            this.GenerateRooms();
            bool playing = true;
            while (playing)
            {
                Console.WriteLine("1. Look around the room.");
                Console.WriteLine("2. Display status.");
                Console.WriteLine("3. Pickup item");
                Console.WriteLine("4. Move");
                Console.WriteLine("0. Exit Game");
                Console.WriteLine("Please enter a listed value to continue... ");
                ConsoleKeyInfo valueTest = Console.ReadKey();
                
                if ("1" == valueTest.KeyChar.ToString())
                {
                    Console.Write($"\n{this.allRooms[player.GetRoomIndex()].GetDescription()}\n");
                }
                else if ("2" == valueTest.KeyChar.ToString())
                {
                    Console.Write($"\n{this.player.GetStatus()}\n");
                }
                else if ("3" == valueTest.KeyChar.ToString())
                {
                    Console.Write($"\n{allRooms[player.GetRoomIndex()].GetAction()}\n");
                    string[] artefact = allRooms[player.GetRoomIndex()].GetCollectable();
                    string[] emptyArray = new string[1];
                    if (artefact != emptyArray)
                    {
                        player.PickUpItem(artefact);
                    }
                }
                else if ("4" == valueTest.KeyChar.ToString())
                {
                    Console.Write("\nWhich direction would you like to move?");
                    Console.Write("\n1. North");
                    Console.Write("\n2. East");
                    Console.Write("\n3. South");
                    Console.Write("\n4. West\n");
                    ConsoleKeyInfo movement = Console.ReadKey();
                    player.Travel(int.Parse(movement.KeyChar.ToString())-1, allRooms[player.GetRoomIndex()]);
                }
                else if ("0" == valueTest.KeyChar.ToString())
                {
                    Console.Write("\nThank you for playing\nExiting...\nPress any key to continue\n");
                    Console.ReadKey();
                    playing = false;
                }
                else
                {
                    Console.WriteLine("\nThat is not a valid input.\nPlease try again...\n");
                }
            
            }
        }
    }
}