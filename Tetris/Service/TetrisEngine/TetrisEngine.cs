using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.TetrisEngine
{
    /// <summary>
    /// Basic tetris engine .
    /// </summary>
    public class TetrisBaseEngine : ITetrisEngine
    {       
        private bool[,] _grid { get; set; }

        /// <summary>
        /// intializing input string and intializing matrix
        /// </summary>
        /// <param name="data"></param>
        public TetrisBaseEngine()
        {
            
            // Matrix that represents a grid of bools of size 100x10 (height, width)
            _grid = new bool[100, 10];
            
            // Fill the grid with false values (grid is blank)
            MatrixInitilization(_grid);
        }

        // <summary>
        /// initializes grid, changes the grid (drops all the shapes from one input line)
        /// then checks if there is full-filled line on the grid and then removes it if so
        /// </summary>
        /// <returns></returns>
        public async Task<string> Play(string[] _inputData)
        {            
            foreach (string str in _inputData)
            {
                // Drops blocks and then removes one filled line
                // if line was removed, move all shapes above the row on whitch was the line removed by 1 
                DropBlock(_grid, str[0], str[1] - '0');
                RemoveLine(_grid);
            }
            return (await CalculateHeight(_grid)).ToString();
        }

        /// <summary>
        /// checks for filled line and remove if it exists
        /// </summary>
        /// <param name="grid"></param>
        private void RemoveLine(bool[,] grid)
        {
            int i, j, count = 0;

            for (i = 0; i < 100; i++)
            {
                for (j = 0, count = 0; j < 10; j++)
                {
                    if (grid[i, j])
                        count++;
                }

                if (count == 10)
                {
                    for (j = 0; j < 10; j++)
                        grid[i, j] = false;
                    MoveDownBlockByOne(grid, i);
                }
            }
        }

        /// <summary>
        /// moves all the shapes above the row by 1 (if row was removed)
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        private void MoveDownBlockByOne(bool[,] grid, int row)
        {
            int i, j = 0;

            for (i = 0; i < 10; i++)
            {
                for (j = row; j < 100 - row - 1; j++)
                {
                    grid[j, i] = grid[j + 1, i];
                }
                if (j == 99)
                    grid[j, i] = false;
            }
        }

        /// <summary>
        /// calculates the highest point on the grid
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private Task<int> CalculateHeight(bool[,] grid)
        {
            int max = 0;
            int cur = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (grid[j, i])
                    {
                        cur = j + 1;
                        if (cur > max)
                            max = cur;
                    }
                }
            }
            return Task.FromResult(max);
        }

        /// <summary>
        ///  initializes the grid
        /// </summary>
        /// <param name="grid"></param>
        private void MatrixInitilization(bool[,] grid)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 10; j++)
                    grid[i, j] = false;
            }
        }

        /// <summary>
        /// Dropping blocks
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        public  void DropBlock(bool[,] grid, char type, int pos)
        {
            int startY;
            int startX;

            switch (type)
            {
                // startY - the index beneath the leftmost filled square of the shape
                // it will detect if there is square underneath
                case 'Q':

                    // **
                    // **
                    // 00
                    // ^
                    // |
                    // (startX, startY) coords
                    startY = 97;
                    startX = pos;

                    // checks that there is nothing underneath the objects so it can keep "falling" down
                    // (falling by descreasing startY value)
                    while (grid[startY, startX] == false && grid[startY, startX + 1] == false && startY != 0)
                        startY--;

                    // if we are on the bottom (no other shape left), then startY is 0
                    // but startY could also be 0 in case if theres shape on the position (X, Y) = (X, 0)
                    // so check if there is no other shape
                    if (startY == 0 && grid[startY, startX] == false && grid[startY, startX + 1] == false)
                        startY--;

                    // "draw" the current object (start drawing from startX, startY position)
                    grid[startY + 1, startX] = true;
                    grid[startY + 2, startX] = true;
                    grid[startY + 1, startX + 1] = true;
                    grid[startY + 2, startX + 1] = true;

                    break;

                case 'Z':

                    startY = 98;
                    startX = pos;

                    while (startY != 0 && grid[startY, startX] == false && grid[startY - 1, startX + 1] == false
                           && grid[startY - 1, startX + 2] == false)
                        startY--;

                    grid[startY + 1, startX] = true;
                    grid[startY, startX + 1] = true;
                    grid[startY + 1, startX + 1] = true;
                    grid[startY, startX + 2] = true;

                    break;

                case 'S':

                    startY = 97;
                    startX = pos;

                    while (grid[startY, startX] == false && grid[startY, startX + 1] == false && startY != 0
                            && grid[startY + 1, startX + 2] == false)
                        startY--;

                    if (startY == 0 && grid[startY, startX] == false && grid[startY, startX + 1] == false)
                        startY--;


                    grid[startY + 1, startX] = true;
                    grid[startY + 1, startX + 1] = true;
                    grid[startY + 2, startX + 1] = true;
                    grid[startY + 2, startX + 2] = true;
                    break;

                case 'T':

                    startY = 98;
                    startX = pos;

                    while (grid[startY, startX] == false && grid[startY, startX + 2] == false && startY != 0 && grid[startY - 1, startX + 1] == false)
                        startY--;

                    grid[startY + 1, startX] = true;
                    grid[startY, startX + 1] = true;
                    grid[startY + 1, startX + 1] = true;
                    grid[startY + 1, startX + 2] = true;


                    break;

                case 'I':

                    startY = 98;
                    startX = pos;

                    // falling
                    while (grid[startY, startX] == false && grid[startY, startX + 1] == false
                           && grid[startY, startX + 2] == false && grid[startY, startX + 3] == false && startY != 0)
                        startY--;

                    // condition for the bottom 
                    if (startY == 0 && grid[startY, startX + 1] == false && grid[startY, startX + 2] == false
                    && grid[startY, startX + 3] == false && grid[startY, startX] == false)
                        startY--;

                    // drawing itself
                    grid[startY + 1, startX] = true;
                    grid[startY + 1, startX + 1] = true;
                    grid[startY + 1, startX + 2] = true;
                    grid[startY + 1, startX + 3] = true;

                    break;

                case 'L':
                    {
                        startY = 96;
                        startX = pos;

                        while (grid[startY, startX] == false && grid[startY, startX + 1] == false && startY != 0)
                            startY--;

                        if (startY == 0 && grid[startY, startX] == false && grid[startY, startX + 1] == false)
                            startY--;

                        grid[startY + 1, startX] = true;
                        grid[startY + 2, startX] = true;
                        grid[startY + 3, startX] = true;
                        grid[startY + 1, startX + 1] = true;
                        break;
                    }

                case 'J':
                    {
                        startY = 96;
                        startX = pos;

                        while (grid[startY, startX] == false && grid[startY, startX + 1] == false && startY != 0)
                            startY--;

                        if (startY == 0 && grid[startY, startX] == false && grid[startY, startX + 1] == false)
                            startY--;

                        grid[startY + 1, startX + 1] = true;
                        grid[startY + 2, startX + 1] = true;
                        grid[startY + 3, startX + 1] = true;
                        grid[startY + 1, startX] = true;
                        break;
                    }


            }

            return;
        }
    }
}
