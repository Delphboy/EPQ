using EPQ.Class_Files;
using System;
using System.Windows.Forms;

namespace EPQ
{
    public partial class Form1 : Form
    {
        private board b = new board();
        public search s = new search();

        private bool player = true;  //true = player    |    false = AI

        private int scoreX = 0;
        private int scoreO = 0;
        private int movesPlayed = 0;

        public Form1()
        {
            InitializeComponent();
        }

        //Load Form event
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //update board
        private void update(int x, int y, Button btn)
        {
            //update player, competitor and button text if that position is free

            if (b.spaces[x, y] == competitor.freeSpace)
            {
                if (player == true)
                {
                    btn.Text = "X";
                    b.spaces[x, y] = competitor.player;
                    player = false;
                }
                else
                {
                    btn.Text = "O";
                    b.spaces[x, y] = competitor.AI;
                    player = true;
                }
                //incriment moves
                movesPlayed++;

                //check for 3 in a row
                checkForThree();
            }
            DataOut();
        }

        //Call AI move
        private void AIMove()
        {
            position bestMove = s.minimax(b, competitor.AI);
            update(bestMove.X, bestMove.Y, getBtn(bestMove.X, bestMove.Y));
        }

        private Button getBtn(int X, int Y)
        {
            switch (Y)
            {
                case 0:
                    if (X == 0) return btn1;
                    if (X == 1) return btn2;
                    if (X == 2) return btn3;
                    break;

                case 1:
                    if (X == 0) return btn4;
                    if (X == 1) return btn5;
                    if (X == 2) return btn6;
                    break;

                case 2:
                    if (X == 0) return btn7;
                    if (X == 1) return btn8;
                    if (X == 2) return btn9;
                    break;

                //error handling
                default:
                    MessageBox.Show("An error occured calculating the button pressed");
                    break;
            }
            return btn9;
        }

        //check 3 in a row
        private void checkForThree()
        {
            //check row
            checkWinner(btn1, btn2, btn3);
            checkWinner(btn4, btn5, btn6);
            checkWinner(btn7, btn8, btn9);

            //check column
            checkWinner(btn1, btn4, btn7);
            checkWinner(btn2, btn5, btn8);
            checkWinner(btn3, btn6, btn9);

            //check diagonally
            checkWinner(btn1, btn5, btn9);
            checkWinner(btn3, btn5, btn7);

            //check if all filled an no winner
            if (movesPlayed == 9)
                checkAllFilled();
        }

        //Check for winner
        private void checkWinner(Button btnOne, Button btnTwo, Button btnThree)
        {
            //if all three buttons have the same text and aren't blank
            if (btnOne.Text.Length + btnTwo.Text.Length + btnThree.Text.Length > 0)
            {
                if ((btnOne.Text == btnTwo.Text) && (btnTwo.Text == btnThree.Text))
                {
                    //set the colours the to indentify win
                    btnOne.BackColor = System.Drawing.Color.Yellow;
                    btnTwo.BackColor = System.Drawing.Color.Yellow;
                    btnThree.BackColor = System.Drawing.Color.Yellow;

                    //announce win
                    displayWinner(btnOne);

                    //reset board
                    resetBoard();
                }
            }
        }

        //check for all tiles filled but no winner
        private void checkAllFilled()
        {
            MessageBox.Show("All tiles filled, no winner", "No Winner");
            resetBoard();
            updateScore();
        }

        //display winner and update the score
        private void displayWinner(Button btnWinner)
        {
            if (btnWinner.Text == "X")
                scoreX++;
            else
                scoreO++;

            if (btnWinner.Text == "O")
                MessageBox.Show("AI won the game!", "Game Over");
            else
                MessageBox.Show("Player won the game!", "Game Over");

            updateScore();
        }

        //update score board
        private void updateScore()
        {
            //update AI
            LblAI.Text = "AI Score: " + scoreO.ToString();

            //update Player
            LblPlayer.Text = "Player Score: " + scoreX.ToString();
        }

        //reset board
        private void resetBoard()
        {
            //reset buttons
            btn1.Text = "";
            btn1.BackColor = DefaultBackColor;
            btn2.Text = "";
            btn2.BackColor = DefaultBackColor;
            btn3.Text = "";
            btn3.BackColor = DefaultBackColor;
            btn4.Text = "";
            btn4.BackColor = DefaultBackColor;
            btn5.Text = "";
            btn5.BackColor = DefaultBackColor;
            btn6.Text = "";
            btn6.BackColor = DefaultBackColor;
            btn7.Text = "";
            btn7.BackColor = DefaultBackColor;
            btn8.Text = "";
            btn8.BackColor = DefaultBackColor;
            btn9.Text = "";
            btn9.BackColor = DefaultBackColor;

            //reset board
            b = new board();

            //reset moves
            movesPlayed = 0;
        }

        //handle input//
        private void btn1_Click(object sender, EventArgs e)
        {
            if (b.spaces[0, 0] == competitor.freeSpace)
            {
                update(0, 0, btn1);
                AIMove();
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (b.spaces[1, 0] == competitor.freeSpace)
            {
                update(1, 0, btn2);
                AIMove();
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (b.spaces[2, 0] == competitor.freeSpace)
            {
                update(2, 0, btn3);
                AIMove();
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (b.spaces[0, 1] == competitor.freeSpace)
            {
                update(0, 1, btn4);
                AIMove();
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (b.spaces[1, 1] == competitor.freeSpace)
            {
                update(1, 1, btn5);
                AIMove();
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (b.spaces[2, 1] == competitor.freeSpace)
            {
                update(2, 1, btn6);
                AIMove();
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (b.spaces[0, 2] == competitor.freeSpace)
            {
                update(0, 2, btn7);
                AIMove();
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (b.spaces[1, 2] == competitor.freeSpace)
            {
                update(1, 2, btn8);
                AIMove();
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (b.spaces[2, 2] == competitor.freeSpace)
            {
                update(2, 2, btn9);
                AIMove();
            }
        }

        //OUTPUT Data Structures to console for debugging
        private void DataOut()
        {
            Console.Write(b.spaces[0, 0] + " | ");
            Console.Write(b.spaces[1, 0] + " | ");
            Console.Write(b.spaces[2, 0] + Environment.NewLine);

            Console.Write(b.spaces[0, 1] + " | ");
            Console.Write(b.spaces[1, 1] + " | ");
            Console.Write(b.spaces[2, 1] + Environment.NewLine);

            Console.Write(b.spaces[0, 2] + " | ");
            Console.Write(b.spaces[1, 2] + " | ");
            Console.Write(b.spaces[2, 2] + Environment.NewLine);

            Console.WriteLine("=================");
        }
    }
}