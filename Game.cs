using System;
using System.Collections.Generic;
using System.Data;
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
            this.player = new Player("You", 100, 5);
        }
        public void GenerateRooms()
        {
            Weapon roomWeapon;
            Potion roomPotion;
            LightSource roomLightSource;
            Enemy roomEnemy;
            /*
             * This function is used to create the room objects and add them 
             * to an array for ease of access later in the codeS
             * Inputs:
             * None
             * Outputs:
             * None
             */
            string design = @"North of you is a large pair of oak doors,
                held together with wrought iron bolts, rusted due to time.
                Beside the doors, rests small lanterns, they look to have
                been untouched for years. The doors themsleves are inset into
                a marble frame, that itself looks remarkably clean in contrast
                to its surroundings.";
            design = design.Replace("                ", "");
            Room mainEntrance = new Room(design);
            roomLightSource = new LightSource(0,"Lantern", "This lantern will provide light in dark rooms");
            mainEntrance.setLightSource(roomLightSource);
            string action = @"You reach up to where one of the lanterns
                rests. Finding it with an unused candle within,
                you decide to place it in your bag.";
            action = action.Replace("                ", " ");
            mainEntrance.SetAction(action);
            mainEntrance.SetIndex(0);
            mainEntrance.SetAdjacent(1, -1, -1, -1); /*North, East, South, West*/
            roomEnemy = new Enemy("Zombie", 20, 4);
            mainEntrance.setHostile(roomEnemy);
            this.allRooms.Add(mainEntrance);
            design = @"Making it through the large double doors, you are
                faced with the mansion's foyer. It has clearly seen
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
            roomWeapon = new Weapon (1, "Knife", "A short knife, this could come in handy when dealing with what lies a head.", 2);
            foyer.SetWeapon(roomWeapon);
            action = @"You reach the draws and gently pull one open, you 
                find a knife in oddly prestine condition, deciding it could
                be useful down the line, you place it in your bag.";
            action = action.Replace("                ", " ");
            foyer.SetAction(action);
            foyer.SetIndex(1);
            foyer.SetAdjacent(2, 3, 0, -1);
            roomEnemy = new Enemy("Skeleton", 20, 2);
            foyer.setHostile(roomEnemy);
            this.allRooms.Add(foyer);
            design = @"You enter a lavish dining room. In the centre,
                you see a large table surrounded by chairs.";
            design = design.Replace("                ", " ");
            Room diningRoom = new Room(design);
            roomPotion = new Potion(2,"Small Health Potion", "A potion that will provide a small amount of health when drank", 20);
            diningRoom.SetPotion(roomPotion);
            action = @"You lift a potion off of the table gently, careful not to damage it.";
            action = action.Replace("               ", " ");
            diningRoom.SetAction(action);
            diningRoom.SetIndex(2);
            diningRoom.SetAdjacent(4, 5, -1, 1);
            roomEnemy = new Enemy("Slime", 40, 1);
            diningRoom.setHostile(roomEnemy);
            allRooms.Add(diningRoom);
            design = @"The head of the double bed rests against the south wall. At the foot of it you see a small strongbox.";
            design = design.Replace("                ", " ");
            Room mastersBedroom = new Room(design);
            roomPotion = new Potion (3, "Medium Health Potion", "A potion that will provide a medium amount of health when drank", 35);
            mastersBedroom.SetPotion(roomPotion);
            action = @"You reach down to open the small strongbox, lifting
                the lid gently. You uncover a potion hidden within,
                but nothing else.";
            action = action.Replace("               ", " ");
            mastersBedroom.SetAction(action);
            mastersBedroom.SetIndex(3);
            mastersBedroom.SetAdjacent(5,1,6,-1);
            roomEnemy = new Enemy("Skeleton", 20,2);
            mastersBedroom.setHostile(roomEnemy);
            allRooms.Add(mastersBedroom);
            design = @"You enter the kitchen to see a wood stove against the far wall, work tops on either side.";
            design = design.Replace("                ", " ");
            Room kitchen = new Room(design);
            roomWeapon = new Weapon(4,"Skillet", "A cast Iron Skillet, it could be used as a Weapon should worst come to worst", 5);
            kitchen.SetWeapon(roomWeapon);
            action = @"You reach to the stove and pick up the Cast-Iron skillet.";
            action = action.Replace("               ", " ");
            kitchen.SetAction(action);
            kitchen.SetIndex(4);
            kitchen.SetAdjacent(-1, 7, 2, -1);
            roomEnemy = new Enemy("Zombie", 20, 4);
            kitchen.setHostile(roomEnemy);
            allRooms.Add(kitchen);
            design = @"You see a chandelier hanging down from above. There are small tables dotted around the edge of the room.";
            design = design.Replace("               ", " ");
            Room ballRoom = new Room(design);
            roomLightSource = new LightSource(5,"Candalabra", "A candalabra containing 3 candles. it should illuminate a dark room");
            ballRoom.setLightSource(roomLightSource);
            action = @"You approach one of the small tables,
                a candelabra is perched upon it. Lifting
                it up, you decide it could be usefull down
                the line and add it to your bag.";
            action = action.Replace("               ", " ");
            ballRoom.SetAction(action);
            ballRoom.SetIndex(5);
            ballRoom.SetAdjacent(-1,8,2,3);
            roomEnemy = new Enemy("Slime", 40, 1);
            ballRoom.setHostile(roomEnemy);
            allRooms.Add(ballRoom);
            design = @"A tub resides near the south wall, a mirror by the north. Nex to the mirror, on a side table, is a shaving set.";
            design = design.Replace("                ", " ");
            Room mastersBathroom = new Room(design);
            roomWeapon = new Weapon(6,"Straight Razor", "A razorblade that can be folded out of it's handel. A quick strike would be effective against any enemy.", 3);
            mastersBathroom.SetWeapon(roomWeapon);
            action = @"In the shaving set, you find a straight razor, deciding it could be useful, you add it to your bag.";
            action = action.Replace("               ", " ");
            mastersBathroom.SetAction(action);
            mastersBathroom.SetIndex(6);
            mastersBathroom.SetAdjacent(-1, 3, -1, -1);
            roomEnemy = new Enemy("Zombie", 20, 4);
            mastersBathroom.setHostile(roomEnemy);
            allRooms.Add(mastersBathroom);
            design = @"Shelves line the walls, all looking to have contained various foods, all now rotten to the point of being reduced to dust.";
            design = design.Replace("                ", " ");
            Room pantry = new Room(design);
            roomPotion = new Potion(7, "Large Healing Potion", "A potion that heals a large amount of health.", 50);
            pantry.SetPotion(roomPotion);
            action = @"On one of the shelves, you find a large potion, you add it to your bag for safe keeping.";
            action = action.Replace("               ", " ");
            pantry.SetAction(action);
            pantry.SetIndex(7);
            pantry.SetAdjacent(-1, -1, -1 , 4);
            roomEnemy = new Enemy("Demon Hound", 40, 3);
            pantry.setHostile(roomEnemy);
            allRooms.Add(pantry);
            design = @"There are several bunks around the room,
                these look to have belonged to the servants of
                the house. Under the candle light, you can see
                shadows dancing, almost as if the servants were
                still here.";
            design = design.Replace("                ", " ");
            Room servantsQuarters = new Room(design);
            action = "After searching around the room, you find nothing of use.";
            servantsQuarters.SetAction(action);
            servantsQuarters.SetDark(true);
            servantsQuarters.SetIndex(8);
            servantsQuarters.SetAdjacent(9,-1,-1,5);
            allRooms.Add(servantsQuarters);
            design = @"Not needed";
            design = design.Replace("               ", " ");
            Room finalRoom = new Room(design);
            action = @"Not needed";
            finalRoom.SetAction(action);
            finalRoom.SetIndex(9);
            finalRoom.SetAdjacent(8, -1, -1, -1);
            allRooms.Add(finalRoom);
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
            GameMap map = new GameMap();
            this.GenerateRooms();
            bool playing = true;
            bool pause = false;
            while (playing)
            {
                Console.WriteLine("1. Look around the room.");
                Console.WriteLine("2. Display status.");
                Console.WriteLine("3. Pickup item");
                Console.WriteLine("4. Move");
                Console.WriteLine("5. Attack");
                Console.WriteLine("6. Open Map");
                Console.WriteLine("0. Exit Game");
                Console.WriteLine("Please enter a listed value to continue... ");
                ConsoleKeyInfo valueTest = Console.ReadKey();
                
                if ("1" == valueTest.KeyChar.ToString())
                {
                    Console.Write($"\n{this.allRooms[player.GetRoomIndex()].GetDescription()}\n");
                }
                else if ("2" == valueTest.KeyChar.ToString())
                {
                    player.GetStatus();
                }
                else if ("3" == valueTest.KeyChar.ToString())
                {
                    Console.Write($"\n{allRooms[player.GetRoomIndex()].GetAction()}\n");
                    string artefactType = allRooms[player.GetRoomIndex()].getRoomItemType();
                    if (artefactType == "Weapon"){
                        player.PlayerInventory.AddWeapon(allRooms[player.GetRoomIndex()].getRoomWeapon()); 
                    } else if (artefactType == "Potion"){
                        player.PlayerInventory.AddPotion(allRooms[player.GetRoomIndex()].getRoomPotion());
                    } else if (artefactType == "LightSource") {
                        player.PlayerInventory.AddLightSource(allRooms[player.GetRoomIndex()].getRoomLightSource());
                    } else if (artefactType == "Void") {
                    } else { Console.Write("Uuuuuhhhhh, How? This message shouldn't appear. How did you do this?");}
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
                    pause = true;
                    if (allRooms[player.GetRoomIndex()].GetVictoryRoom())
                    {
                        Console.Write("Congratulations, you have reached the end of the current implimentation.");
                        playing = false;
                    } else if (allRooms[player.GetRoomIndex()].getDark()){
                        Console.Write("The room you entered is dark.");
                        if (player.EquipedItem[0] != 2){
                            Console.Write("You cant stay in this room, you might get attacked. You return to the previous room.");
                            player.SetRoomIndex(player.getPreviousRoomIndex());
                        }
                    }
                } else if ("5" == valueTest.KeyChar.ToString()){
                    if (!allRooms[player.GetRoomIndex()].getHostile().getDead()){
                        allRooms[player.GetRoomIndex()].getHostile().takeDamage(player.Strike());
                    } else {
                        Console.Write("The enemy is already dead");
                    }
                }else if ("6" == valueTest.KeyChar.ToString()){
                    map.OpenMap();
                    Console.Write("You were handed this map by an old friend. Nobody knew what the symbols mean. This, you will have to uncover yourself.");
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
                if (!allRooms[player.GetRoomIndex()].getHostile().getDead() || !pause)
                {
                    player.takeDamage(allRooms[player.GetRoomIndex()].getHostile().Strike());
                    if (player.getDead())
                    {
                        Console.Write("The strke from the enemy has sapped the last of the life.\nGame Over.\n");
                        playing = false;
                    }
                }
                pause = false;
            
            }
        }
    }
}