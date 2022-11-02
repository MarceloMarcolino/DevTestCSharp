using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            CharacterClass playerCharacterClass;
            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            Character PlayerCharacter;
            Character EnemyCharacter;
            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;

            /*Makes the Player and Enemy appear at different random spots each playthrough */
            Random rnd = new Random();

            Setup(); 


            void Setup()
            {

                GetPlayerChoice();
                CreateEnemyCharacter();
                StartGame();
            }

            void GetPlayerChoice()
            {
                string PlayerChoiceInput;
                while (true) //Loop for the Player to choose a valid Class
                {
                    //asks for the player to choose between for possible classes via console.
                    Console.WriteLine("Choose Between One of this Classes:\n");
                    Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");

                    //store the player choice in a variable
                    string choice = Console.ReadLine();

                    /*Verifies if the Input is valid*/
                    if (PlayerChoiceInput.Length() == 1
                    {
                        if (PlayerChoiceInput[0] == '1')
                            || PlayerChoiceInput[0] == '2'
                            || PlayerChoiceInput[0] == '3'
                            || PlayerChoiceInput[0] == '4'
                        {
                            break;
                        }
                    }
                }
                int choiceValue = Int16.Parse(PlayerChoiceInput);

                switch (choiceValue)
                {
                    case 1:
                        CreatePlayerCharacter(choiceValue, 150, 30);
                        break;
                    case 2:
                        CreatePlayerCharacter(choiceValue, 100, 40);
                        break;
                    case 3:
                        CreatePlayerCharacter(choiceValue, 200, 20);
                        break;
                    case 4:
                        CreatePlayerCharacter(choiceValue, 50, 80);
                        break;
                    default:
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex, int classEnergy, int classPower)
            {
               
                CharacterClass characterClass = (CharacterClass)classIndex;

                switch (classIndex)
                {
                    case 1:
                        Console.WriteLine("Player Class Choice: Paladin\n");
                        break;
                    case 2:
                        Console.WriteLine("Player Class Choice: Warrior\n");
                        break;
                    case 3:
                        Console.WriteLine("Player Class Choice: Cleric\n");
                        break;
                    case 4:
                        Console.WriteLine("Player Class Choice: Archer\n");
                        break;
                }
                PlayerCharacter = new Character(characterClass);

                PlayerCharacter.Health = classEnergy;
                PlayerCharacter.BaseDamage = classPower;
                PlayerCharacter.DamageMultiplier = 1;
                PlayerCharacter.PlayerIndex = 0;
                /*
                Console.WriteLine($"Player Class Choice: {characterClass}");
                PlayerCharacter = new Character(characterClass);
                PlayerCharacter.Health = 100;
                PlayerCharacter.BaseDamage = 20;
                PlayerCharacter.PlayerIndex = 0;
                CreateEnemyCharacter();
                */

            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                int classEnergy;
                int classPower;

                CharacterClass enemyClass = (CharacterClass)randomInteger;
                
                switch (randomInteger)
                {
                    case 1:
                        Console.WriteLine("Enemy Class Choice: Paladin\n");
                        classEnergy = 150;
                        classPower = 30;
                        break;
                    case 2:
                        Console.WriteLine("Enemy Class Choice: Warrior\n");
                        classEnergy = 100;
                        classPower = 40;
                        break;
                    case 3:
                        Console.WriteLine("Enemy Class Choice: Cleric\n");
                        classEnergy = 200;
                        classPower = 20;
                        break;
                    case 4:
                        Console.WriteLine("Enemy Class Choice: Archer\n");
                        classEnergy = 50;
                        classPower = 80;
                        break;
                }
                EnemyCharacter = new Character(enemyClass);

                EnemyCharacter.Health = classEnergy;
                EnemyCharacter.BaseDamage = classPower;
                EnemyCharacter.DamageMultiplier = 1;
                EnemyCharacter.PlayerIndex = 1;

                /*
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                EnemyCharacter = new Character(enemyClass);
                EnemyCharacter.Health = 100;
                PlayerCharacter.BaseDamage = 20;
                PlayerCharacter.PlayerIndex = 1;
                StartGame();
                */

            }

            void StartGame()
            {
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;

                //Place Players on Battlefield
                AlocatePlayers();

                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);

                grid.drawBattlefield(5, 5);

                StartTurn();

            }

            void StartTurn(){

                currentTurn++;

                bool bIsPlayerTurn = true;

                Console.Write("\n----------------------------------------------------------------------------------------\n");
                Console.WriteLine($"Start of the turn {currentTurn}\n\n");
                Console.WriteLine($"Player Health: {PlayerCharacter.Health} \n");
                Console.WriteLine($"Player Character Current Box: Line: {PlayerCharacter.currentBox.xIndex} Column: {PlayerCharacter.currentBox.yIndex} \n\n");

                Console.WriteLine($"Enemy Health: {EnemyCharacter.Health} \n");
                Console.WriteLine($"Enemy Character Current Box: Line: {EnemyCharacter.currentBox.xIndex} Column: {EnemyCharacter.currentBox.yIndex} \n\n");

                /*
                if (currentTurn == 0)
                {
                    //AllPlayers.Sort();  
                }
                */

                foreach(Character character in AllPlayers)
                {
                    character.StartTurn(grid, bIsPlayerTurn);
                    bIsPlayerTurn = false;
                }


                Console.WriteLine($"Player Health: {PlayerCharacter.Health} \n");
                Console.WriteLine($"Player Character Current Box: Line: {PlayerCharacter.currentBox.xIndex} Column: {PlayerCharacter.currentBox.yIndex} \n\n");

                Console.WriteLine($"Enemy Health: {EnemyCharacter.Health} \n");
                Console.WriteLine($"Enemy Character Current Box: Line: {EnemyCharacter.currentBox.xIndex} Column: {EnemyCharacter.currentBox.yIndex} \n\n");

                Console.WriteLine($"End of the turn {currentTurn}\n\n");
                Console.Write("\n----------------------------------------------------------------------------------------\n");

                //currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if(PlayerCharacter.Health == 0)
                {
                    Console.WriteLine("\n--------------------------------GAME OVER----------------------------------\n");
                    Console.WriteLine("-----------------------------The player LOSES...-----------------------------\n");
                } 
                else if (EnemyCharacter.Health == 0)
                {
                    Console.WriteLine("\n--------------------------------GAME OVER----------------------------------\n");
                    Console.WriteLine("-----------------------------The player WINS...-----------------------------\n");
                    
                    /*
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    // endgame?

                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                    */
                } 
                else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Press Enter to start the next turn\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                //int index = rand.Next(min, max);
                int index = rand.Next() % max + min;
                return index;
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();
                AlocateEnemyCharacter();
            }

            void AlocatePlayerCharacter()
            {
                int random = GetRandomInt(0, ((grid.xLenght * grid.yLength) - 1));
                //int random = 0;
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.ocupied)
                {
                    GridBox PlayerCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.grids[random] = RandomLocation;
                    PlayerCharacter.currentBox = grid.grids[random];
                    Console.WriteLine($"Player Character Current Box: Line: {PlayerCharacter.currentBox.xIndex} Column: {PlayerCharacter.currentBox.yIndex} \n");
                    grid.PlayerCurrentLocation = PlayerCurrentLocation;
                }
                else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                int random = GetRandomInt(0, ((grid.xLenght * grid.yLength) - 1));
                //int random = 24;
                GridBox RandomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!RandomLocation.ocupied)
                {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    grid.grids[random] = RandomLocation;
                    EnemyCharacter.currentBox = grid.grids[random];
                    Console.WriteLine($"Enemy Character Current Box: Line: {EnemyCharacter.currentBox.xIndex} Column: {EnemyCharacter.currentBox.yIndex} \n");
                    //grid.drawBattlefield(5 , 5);
                    grid.EnemyCurrentLocation = EnemyCurrentLocation;
                }
                else
                {
                    AlocateEnemyCharacter();
                }

                
            }

        }
    }
}
