using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        int index1;
        int choice = 1;
        char[] board = { ' ',' ',' ',' ',' ',' ',' ',' ',' ' };// Single array represents the board '*' means empty box in board
        public int isFull()// Tabla e plina
        {
	        for (int i = 0; i<9; i++)
	        {
		        if (board[i] != 'X')
		        {
			        if (board[i] != 'O')
			        {
				        return 0;
			        }
		        }
	        }
	        return 1;
        }
        public int user_won()// Verifica daca utilizatorul a castigat
        {
            for (int i = 0; i < 9; i += 3) //se verfica pe linii
            {
                if ((board[i] == board[i + 1]) && (board[i + 1] == board[i + 2]) && (board[i] == 'O'))
                    return 1;
            }
            for (int i = 0; i < 3; i++) //pe coloane
            {
                if ((board[i] == board[i + 3]) && (board[i + 3] == board[i + 6]) && (board[i] == 'O'))
                    return 1;
            }
            if ((board[0] == board[4]) && (board[4] == board[8]) && (board[0] == 'O'))//diagonala sus-jos stanga dreapta
            {
                return 1;
            }
            if ((board[2] == board[4]) && (board[4] == board[6]) && (board[2] == 'O'))//diagonala sus-jos dreapta stanga
            {
                return 1;
            }
            return 0;
        }
        public int cpu_won()// Verifica daca a castigat CPU
        {
            for (int i = 0; i < 9; i += 3)
            {
                if ((board[i] == board[i + 1]) && (board[i + 1] == board[i + 2]) && (board[i] == 'X'))
                    return 1;
            }
            for (int i = 0; i < 3; i++)
            {
                if ((board[i] == board[i + 3]) && (board[i + 3] == board[i + 6]) && (board[i] == 'X'))
                    return 1;
            }
            if ((board[0] == board[4]) && (board[4] == board[8]) && (board[0] == 'X'))
            {
                return 1;
            }
            if ((board[2] == board[4]) && (board[4] == board[6]) && (board[2] == 'X'))
            {
                return 1;
            }
            return 0;
        }
        public int minimax(bool flag)// Algoritmul MINIMAX cu retezarea Alfa-Beta
        {
	        if (cpu_won() == 1)
	        {
		        return 10;
	        }
	        else if (user_won() == 1)
	        {
		        return -10;
	        }
	        else if (isFull() == 1)
	        {
		        return 0;
	        }


	        int[] score = { 1,1,1, 1,1,1, 1,1,1 };//daca score[i]=1 -> e gol

	        int max_val = -1000,//alfa
		        min_val = 1000;//beta

	        int i, j, value = 1;
	
	        for (i = 0; i<9; i++)
	        {
		        if (board[i] == ' ') //daca nu e pus nimic peste
		        {
			        if (min_val>max_val) //beta>alfa :conditia inversa retezare-daca ar fi alfa beta nu ar mai genera solutii 
			        {
				        if (flag == true) //randul lui X
				        {
					        board[i] = 'X';
					        value = minimax(false); //intra si muta 0-return val min
				        }
				        else //randul lui 0
				        {
					        board[i] = 'O';
					        value = minimax(true); //muta X-return val max
				        }
				        board[i] = ' '; 
				        score[i] = value; //ceea ce returneaza minimax : min, max
			        }
		        }
	        }

	        if (flag == true) //daca este randul lui X-maximizare
	        {
		        max_val = -1000;
		        for (j = 0; j<9; j++) //parcurg tabla
		        {
			        if (score[j] > max_val && score[j] != 1) //daca ceea ce a returnat minmax este mai mare decat valoarea maxima 
													        //si pe daca scorul a fost modificats
			        {
				        max_val = score[j];  //valoarea maxima devine scorul
				        index1 = j; //se pastreaza pozitia pentru val maxima(best move)
			        }
		        }
		        
		        return max_val; //se returneaza valoarea maxima

	        }
	        else // (flag == false) //daca este randul lui 0
	        {
		        min_val = 1000;
		        for (j = 0; j<9; j++)
		        {
			        if (score[j] < min_val && score[j] != 1)
			        {
				        min_val = score[j];
				        index1 = j;
			        }
		        }

		        
		        return min_val;
	        }
        }
        public void draw_board() //afisare tabela
        {
            button1.Text = board[0].ToString();
            button2.Text = board[1].ToString();
            button3.Text = board[2].ToString();
            button4.Text = board[3].ToString();
            button5.Text = board[4].ToString();
            button6.Text = board[5].ToString();
            button7.Text = board[6].ToString();
            button8.Text = board[7].ToString();
            button9.Text = board[8].ToString();
        }
        public void DisableButtons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }
        public void EnableButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
        }
        public void CPUMove()
        {
            // Miscare CPU
            minimax(true);
            board[index1] = 'X';
            draw_board();
            if (cpu_won() == 1)
            {
                Console.WriteLine("CPU WON.....");
                MessageBox.Show("Ai pierdut!", " ",
                                 MessageBoxButtons.OK
                                 );
                DisableButtons();
            }
            else {
                if (isFull() == 1)
                {
                    Console.WriteLine("Draw....");
                    MessageBox.Show("Remiza!", " ",
                                     MessageBoxButtons.OK
                                     );
                    DisableButtons();
                }
            }
            
        }
        public void OnMove(int move)
        {
            // Miscare Jucator
                if (board[move - 1] == ' ')
                {
                    board[move - 1] = 'O';
                    draw_board();
                }

                if (user_won() == 1)
                {
                    MessageBox.Show("Ai castigat!", " ",
                                 MessageBoxButtons.OK
                                 );
                    DisableButtons();
                }
                else
                {
                    if (isFull() == 1)
                    {
                        MessageBox.Show("Remiza!", " ",
                                     MessageBoxButtons.OK
                                     );
                        DisableButtons();
                    }
                }
            }
        public Form1()
        {
            InitializeComponent();
            DisableButtons(); // Initial, butoanele sunt disabled
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Start Game
            EnableButtons();
            for (int i = 0; i < 9; i++) // Golire tabela
            {
                board[i] = Convert.ToChar(" ");
            }
            if (choice == 1)
            {
                draw_board();
            }
            else
            {
                Random r = new Random();
                int rInt = r.Next(0, 8);
                board[rInt] = 'X'; // sa inceapa random prima miscare
                draw_board();
                // CPUMove;
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked != true) { // false
                choice = 0;
                checkBox2.Checked = true;
            }
            else
            {
                choice = 1;
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked != true) // false
            {
                checkBox1.Checked = true;
                choice = 1;
            }
            else
            {
                checkBox1.Checked = false;
                choice = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == " ")
            {
                OnMove(1);
                CPUMove();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == " ")
            {
                OnMove(2);
                CPUMove();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == " ")
            {
                OnMove(3);
                CPUMove();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == " ")
            {
                OnMove(4);
                CPUMove();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == " ")
            {
                OnMove(5);
                CPUMove();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == " ")
            {
                OnMove(6);
                CPUMove();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == " ")
            {
                OnMove(7);
                CPUMove();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == " ")
            {
                OnMove(8);
                CPUMove();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Text == " ")
            {
                OnMove(9);
                CPUMove();
            }
        }
    }
}
