using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public string Name { get; set; }
        public float Health;
        public float BaseDamage;
        public float DamageMultiplier { get; set; }
        public GridBox currentBox;
        public int PlayerIndex;
        public Character Target { get; set; }
        public bool isDead = false;
        public Character(CharacterClass characterClass)
        {
            currentBox.xIndex = 0;
            currentBox.yIndex = 0;
            currentBox.ocupied = false;
            currentBox.Index = 0;
        }


        public void /*bool*/ TakeDamage(float amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                Die();
            }
            /*
            if((Health -= BaseDamage) <= 0)
            {
                Die();
                return true;
            }
            return false;
            */
        }

        public void Die()
        {
            isDead = true;

            if(PlayerIndex == 0)
            {
                Console.WriteLine("\nPlayer died...\n");
            }
            else
            {
                Console.WriteLine("\nEnemy died!\n");
            }
        }

        public void WalkTO(bool CanWalk)
        {

        }

        public void StartTurn(Grid battlefield, bool IsPlayerTurn)
        {

            //Verify if Player is Alive
            if (Target.Target.Health <= 0)
            {
                isDead = true;
                return;
            }

            if (PlayerIndex == 0)
            {
                Console.WriteLine("\nPlayer Action:\n");
            }
            else
            {
                Console.WriteLine("\nEnemy Action:\n");
            }

            if (CheckCloseTargets(battlefield)) // Check if there are any close attack targets (Diagonals are not considered)
            {
                Attack(Target);

                AttackTryPushAway(Target, battlefield);
                
                return;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                // Makes the current character position free because it will move
                battlefield.grids[(this.currentBox.xIndex * battlefield.xLenght) + this.currentBox.yIndex].ocupied = false;

                if (this.currentBox.xIndex > Target.currentBox.xIndex)
                {
                    //CHARACTER GOES UP
                    Console.WriteLine("\nCharacter must go Up\n");
                    battlefield.grids[((this.currentBox.xIndex - 1) * battlefield.xLenght) + currentBox.yIndex].ocupied = true;

                    if (IsPlayerTurn)
                    {
                        battlefield.PlayerCurrentLocation.xIndex--;
                        currentBox = battlefield.PlayerCurrentLocation;
                    }
                    else
                    {
                        battlefield.EnemyCurrentLocation.xIndex--;
                        currentBox = battlefield.EnemyCurrentLocation;
                    }

                    currentBox.ocupied = true;
                    Target.Target.currentBox = currentBox;

                    battlefield.drawBattlefield(5, 5);

                    return;
                }
                else if (currentBox.xIndex < Target.currentBox.xIndex)
                {
                    //CHARACTER GOES DOWN
                    Console.WriteLine("\nCharacter must go Down\n");
                    battlefield.grids[((currentBox.xIndex + 1) * battlefield.xLenght) + currentBox.yIndex].ocupied = true;

                    if (IsPlayerTurn)
                    {
                        battlefield.PlayerCurrentLocation.xIndex++;
                        currentBox = battlefield.PlayerCurrentLocation;
                    }
                    else
                    {
                        battlefield.EnemyCurrentLocation.xIndex++;
                        currentBox = battlefield.EnemyCurrentLocation;
                    }

                    currentBox.ocupied = true;
                    Target.Target.currentBox = currentBox;

                    battlefield.drawBattlefield(5, 5);

                    return;
                }

                if (currentBox.yIndex > Target.currentBox.yIndex)
                {
                    //CHARACTER GOES LEFT
                    Console.WriteLine("\nCharacter must go Left\n");

                    battlefield.grids[(currentBox.xIndex * battlefield.xLenght) + (currentBox.yIndex - 1)].ocupied = true;

                    if (IsPlayerTurn)
                    {
                        battlefield.PlayerCurrentLocation.yIndex--;
                        currentBox = battlefield.PlayerCurrentLocation;
                    }
                    else
                    {
                        battlefield.EnemyCurrentLocation.yIndex--;
                        currentBox = battlefield.EnemyCurrentLocation;
                    }

                    currentBox.ocupied = true;
                    Target.Target.currentBox = currentBox;

                    battlefield.drawBattlefield(5, 5);

                    return;
                }
                else if (currentBox.yIndex < Target.currentBox.yIndex)
                {
                    //CHARACTER GOES RIGHT
                    Console.WriteLine("\nCharacter must go Right\n");

                    battlefield.grids[(currentBox.xIndex * battlefield.xLenght) + (currentBox.yIndex + 1)].ocupied = true;

                    if (IsPlayerTurn)
                    {
                        battlefield.PlayerCurrentLocation.yIndex++;
                        currentBox = battlefield.PlayerCurrentLocation;
                    }
                    else
                    {
                        battlefield.PlayerCurrentLocation.yIndex++;
                        currentBox = battlefield.EnemyCurrentLocation;
                    }

                    currentBox.ocupied = true;
                    Target.Target.currentBox = currentBox;

                    battlefield.drawBattlefield(5, 5);

                    return;
                }
                /*
                if ((battlefield.grids.Exists(x => x.Index == currentBox.Index - 1)))
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked left\n");
                        battlefield.drawBattlefield(5, 5);

                        return;
                    }
                } else if(currentBox.xIndex < Target.currentBox.xIndex)
                {
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                    currentBox.ocupied = true;
                    return;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {PlayerIndex} walked right\n");
                    battlefield.drawBattlefield(5, 5);
                }

                if (this.currentBox.yIndex > Target.currentBox.yIndex)
                {
                    battlefield.drawBattlefield(5, 5);
                    this.currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght));
                    this.currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {PlayerIndex} walked up\n");
                    return;
                }
                else if(this.currentBox.yIndex < Target.currentBox.yIndex)
                {
                    this.currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = this.currentBox;
                    this.currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght));
                    this.currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player {PlayerIndex} walked down\n");
                    battlefield.drawBattlefield(5, 5);

                    return;
                }
                */
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            currentBox.xIndex = Target.Target.currentBox.xIndex;
            currentBox.yIndex = Target.Target.currentBox.yIndex;

            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied);
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght).ocupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght).ocupied);

            //Verify if not at the top, then if it is not, verify up
            if (battlefield.grids[((currentBox.xIndex) * battlefield.xLenght) + currentBox.yIndex].xIndex != 0) 
            {
                if (battlefield.grids[((currentBox.xIndex - 1) * battlefield.xLenght) + currentBox.yIndex].ocupied == true)
                {
                    Console.WriteLine("Found a close target at the Top \n");
                    return true;
                }
            }

            //Verify if not at the bottom, then if it is not, verify down
            if (battlefield.grids[((currentBox.xIndex) * battlefield.xLenght) + currentBox.yIndex].xIndex != (battlefield.yLength - 1))
            {
                if (battlefield.grids[((currentBox.xIndex + 1) * battlefield.xLenght) + currentBox.yIndex].ocupied == true)
                {
                    Console.WriteLine("Found a close target at the Bottom \n");
                    return true;
                }
            }

            //Verify if not at the leftmost, then if it is not, verify to the left
            if (battlefield.grids[((currentBox.xIndex) * battlefield.xLenght) + currentBox.yIndex].yIndex != 0)
            {
                if (battlefield.grids[(currentBox.xIndex * battlefield.xLenght) + (currentBox.yIndex - 1)].ocupied == true)
                {
                    Console.WriteLine("Found a close target to the Left \n");
                    return true;
                }
            }

            //Verify if not at the rightmost, then if it is not, verify to the right
            if (battlefield.grids[((currentBox.xIndex) * battlefield.xLenght) + currentBox.yIndex].yIndex != (battlefield.yLength - 1))
            {
                if (battlefield.grids[(currentBox.xIndex * battlefield.xLenght) + (currentBox.yIndex + 1)].ocupied == true)
                {
                    Console.WriteLine("Found a close target to the Right \n");
                    return true;
                }
            }

            return false; 
        }

        public void Attack (Character target)
        {
            if (PlayerIndex == 0)
            {
                Console.WriteLine("\nThe Player is Attacking the Enemy!\n");
            }
            else
            {
                Console.WriteLine("\nThe Enemy is Attacking the Player!\n");
            }

            target.TakeDamage(BaseDamage * DamageMultiplier);
            /*
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage));
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
            */
        }

        public void AttackTryPushAway(Character target, Grid battlefield)
        {
            Random rnd = new Random();
            int randomnumber = rnd.Next() % 3 + 2;

            if(randomnumber % 2 == 0) // Gives a 50% chance of pushing the character{
            {
                return;
            }

            //verify if target is not at the top
            if (battlefield.grids[((target.currentBox.xIndex) * battlefield.xLenght) + target.currentBox.yIndex].xIndex != 0)
            {
                //If the zone above the target is free, then push character Up
                if (battlefield.grids[((target.currentBox.xIndex - 1) * battlefield.xLenght) + target.currentBox.yIndex].ocupied == false)
                {
                    battlefield.grids[(target.currentBox.xIndex * battlefield.xLenght) + target.currentBox.yIndex].ocupied = false;
                    battlefield.grids[((target.currentBox.xIndex - 1) * battlefield.xLenght) + target.currentBox.yIndex].ocupied = true;

                    if (PlayerIndex == 1)
                    {
                        battlefield.PlayerCurrentLocation.xIndex--;
                        target.currentBox = battlefield.PlayerCurrentLocation;

                        target.currentBox.xIndex = battlefield.PlayerCurrentLocation.xIndex;
                        target.currentBox.yIndex = battlefield.PlayerCurrentLocation.yIndex;
                    }
                    else
                    {
                        battlefield.EnemyCurrentLocation.xIndex--;
                        target.currentBox = battlefield.EnemyCurrentLocation;

                        target.currentBox.xIndex = battlefield.EnemyCurrentLocation.xIndex;
                        target.currentBox.yIndex = battlefield.EnemyCurrentLocation.yIndex;
                    }
                    Console.WriteLine("\nCharacter was pushed Up! \n");
                }
                else //The zone above the target is not empty and the target is not at the top, so don't push it
                {
                    Console.WriteLine("\nCannot push\n");
                    return;
                }
            }
            //Target is at the top, so verify if target is not at right most
            else if (battlefield.grids[((target.currentBox.xIndex) * battlefield.xLenght) + target.currentBox.yIndex].yIndex != (battlefield.yLength - 1))
            {
                //If the zone to the right of the character is free, push the character to the right
                if (battlefield.grids[((target.currentBox.xIndex) * battlefield.xLenght) + (target.currentBox.yIndex + 1)].ocupied == false)
                {
                    battlefield.grids[(target.currentBox.xIndex * battlefield.xLenght) + target.currentBox.yIndex].ocupied = false;
                    battlefield.grids[((target.currentBox.xIndex) * battlefield.xLenght) + (target.currentBox.yIndex + 1)].ocupied = true;
                    if (PlayerIndex == 1)
                    {
                        battlefield.PlayerCurrentLocation.yIndex++;
                        target.currentBox = battlefield.PlayerCurrentLocation;

                        target.currentBox.xIndex = battlefield.PlayerCurrentLocation.xIndex;
                        target.currentBox.yIndex = battlefield.PlayerCurrentLocation.yIndex;
                    }
                    else
                    {
                        battlefield.EnemyCurrentLocation.yIndex++;
                        target.currentBox = battlefield.EnemyCurrentLocation;

                        target.currentBox.xIndex = battlefield.EnemyCurrentLocation.xIndex;
                        target.currentBox.yIndex = battlefield.EnemyCurrentLocation.yIndex;
                    }
                    Console.WriteLine("\nCharacter was pushed to the Right! \n");
                }
                else //The zone to the right of the target is not free and the target is not at the right most, so don't push it
                {
                    Console.WriteLine("\nCannot push \n");
                    return;
                }
            }
            else //Character is cornered, so don't push it
            {
                Console.WriteLine("\nCannot push \n");
                return;
            }
            battlefield.drawBattlefield(5, 5);
            /*
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage));
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
            */
        }
    }
}
