﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLenght;
        public int yLength;

        public Types.GridBox PlayerCurrentLocation;
        public Types.GridBox EnemyCurrentLocation;
        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLength = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    grids.Add(newBox);
                    Console.Write($"{newBox.Index}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void drawBattlefield(int Lines, int Columns)
        {
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox currentgrid = new GridBox();
                    currentgrid.xIndex = i;
                    currentgrid.yIndex = j;

                    bool bOccupiedByPlayer = false;

                    if (currentgrid.xIndex == PlayerCurrentLocation.xIndex && currentgrid.yIndex == PlayerCurrentLocation.yIndex)
                    {
                        currentgrid.ocupied = true;
                        bOccupiedByPlayer = true;
                    }
                    else if (currentgrid.xIndex == EnemyCurrentLocation.xIndex && currentgrid.yIndex == EnemyCurrentLocation.yIndex)
                    {
                        currentgrid.ocupied = false;
                    }

                    if (currentgrid.ocupied)
                    {
                        if (bOccupiedByPlayer)
                        {
                            Console.Write("[P]\t");
                        }
                        else
                        {
                            Console.Write("[E]\t");
                        }
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}
