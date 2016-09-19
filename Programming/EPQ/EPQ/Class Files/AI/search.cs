using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EPQ.Class_Files
{
    public class search
    {
        public position minimax(board gb, competitor player)
        {
            //variables
            position? bestPos = null;
            List<position> openPositions = gb.openPositions;
            board newBoard;

            //Loop all open spaces searching for the best move
            for (int i = 0; i < openPositions.Count; i++)
            {
                newBoard = gb.clone();
                position newPos = openPositions[i];

                newBoard[newPos.X, newPos.Y] = player;

                if (newBoard.winner == competitor.freeSpace && newBoard.openPositions.Count > 0)
                {
                    position tempMove = minimax(newBoard, ((competitor)(-(int)player)));
                    newPos.score = tempMove.score;
                }
                else
                {
                    //score moves
                    if (newBoard.winner == competitor.freeSpace)
                        newPos.score = 0;
                    else if (newBoard.winner == competitor.player)
                        newPos.score = -1;
                    else if (newBoard.winner == competitor.AI)
                        newPos.score = 1;
                }

                //check it is the best move
                if (bestPos == null ||
                   (player == competitor.player && newPos.score < ((position)bestPos).score) ||
                   (player == competitor.AI && newPos.score > ((position)bestPos).score))
                {
                    bestPos = newPos;
                }
            }

            //return the best move
            return (position)bestPos;
        }
    }
}