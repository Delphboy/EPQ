using System.Collections.Generic;

namespace EPQ.Class_Files
{
    //enum to hold player
    public enum competitor
    {
        player = 1,
        AI = -1,
        freeSpace = 0
    }

    public class board
    {
        // 2D array to hold the spaces on the board and what player is in that space
        public competitor[,] spaces;

        // Initalise a new game board's spaces
        public board()
        {
            spaces = new competitor[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        }

        // Used to get or set a value on the board (spaces)
        public competitor this[int x, int y]
        {
            get
            {
                return spaces[x, y];
            }
            set
            {
                spaces[x, y] = value;
            }
        }

        // returns whether or not all the spaces on the board are full
        public bool isFull
        {
            get
            {
                foreach (competitor comp in spaces)
                {
                    if (comp == competitor.freeSpace) return false;
                }
                return true;
            }
        }

        // gets maximum size of board
        public int size
        {
            get
            {
                return 9;
            }
        }

        // Returns a list of all the open positions on the game board
        public List<position> openPositions
        {
            get
            {
                List<position> openPositions = new List<position>();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (spaces[i, j] == competitor.freeSpace)
                        {
                            openPositions.Add(new position(i, j));
                        }
                    }
                }

                return openPositions;
            }
        }

        // Clones the game board
        public board clone()
        {
            board b = new board();
            b.spaces = (competitor[,])this.spaces.Clone();
            return b;
        }

        // determines if the current board has a winner
        public competitor winner
        {
            get
            {
                int count = 0;

                //columns
                for (int x = 0; x < 3; x++)
                {
                    count = 0;

                    for (int y = 0; y < 3; y++)
                        count += (int)spaces[x, y];

                    if (count == 3)
                        return competitor.player;
                    if (count == -3)
                        return competitor.AI;
                }

                //rows
                for (int x = 0; x < 3; x++)
                {
                    count = 0;

                    for (int y = 0; y < 3; y++)
                        count += (int)spaces[y, x];

                    if (count == 3)
                        return competitor.player;
                    if (count == -3)
                        return competitor.AI;
                }

                //diagnols right to left
                count = 0;
                count += (int)spaces[0, 0];
                count += (int)spaces[1, 1];
                count += (int)spaces[2, 2];
                if (count == 3)
                    return competitor.player;
                if (count == -3)
                    return competitor.AI;

                //diagnols left to right
                count = 0;
                count += (int)spaces[0, 2];
                count += (int)spaces[1, 1];
                count += (int)spaces[2, 0];
                if (count == 3)
                    return competitor.player;
                if (count == -3)
                    return competitor.AI;

                return competitor.freeSpace;
            }
        }
    }

    // Describes a position on the board
    public struct position
    {
        public int X;
        public int Y;
        public int score;

        public position(int x, int y)
        {
            X = x;
            Y = y;
            score = 0;
        }
    }
}