using System;
using System.Linq;

namespace NickScotney.Games.TicTacToe.ConsoleApp
{
    class Program
    {
        //  Flag to see debug messages on or off
        static bool debug = false;
        //  Define the Game board here
        static int[] gameBoard;

        static void Main(string[] args)
        {
            //  Initialize the board
            InitializeBoard();

            //  Player Turn variable
            int cpuTurn;
            int playerTurn;
            int winningPlayer = -1;
            Random rand = new Random();

            while (winningPlayer == -1) {
                //  Print the board to the screen
                PrintBoard();

                //  Get the player's turn
                Console.WriteLine("Enter a position (Between 1 - 9): "); 
                playerTurn = int.Parse(Console.ReadLine());

                //  Make sure the player's turn is valid
                while (!IsValidMove(playerTurn - 1)) {
                    Console.WriteLine("Invalid Board Position | Enter a position (Between 1 - 9): ");
                    playerTurn = int.Parse(Console.ReadLine());
                }

                //  if debug is enabled, output a message to the console telling the player what they just picked
                if (debug)
                    Console.WriteLine($"Player Picked: {playerTurn}");

                //  Update the gameboard to check if the player's position is already taken
                gameBoard[playerTurn - 1] = 1;

                //  Check to see if one of the cpu has won the game
                if ((winningPlayer = IsWinCondition()) > -1) {
                    //  Break the while loop
                    break;
                }

                //  Generate the CPU's Turn
                cpuTurn = rand.Next(0, 8);

                //  Make sure the cpu's turn is valid
                while (!IsValidMove(cpuTurn)) {
                    cpuTurn = rand.Next(0, 8);
                }

                //  if debug is enabled, output a message to the console telling the player what the cpu picked
                if (debug)
                    Console.WriteLine($"CPU Picked: {cpuTurn + 1}");
                

                //  Update the gameboard to check if the CPU's position is already taken
                gameBoard[cpuTurn] = 2;

                //  Update the winning player field here.  I don't need to do an if here as the while loop will handle that
                winningPlayer = IsWinCondition();
            }

            Console.Clear();

            //  Output Game Over Message.  
            //  If the game was tied
            if (winningPlayer == 0)
                Console.WriteLine("The game was tied . . . ");
            //  If the game was won by player or CPU
            else
                Console.WriteLine($"The {(winningPlayer == 1 ? "PLAYER" : "CPU")} won the game");

            //  Print the final board here
            PrintBoard();
        }

        static void InitializeBoard()
        {
            gameBoard = new int[9];

            for (int i = 0; i < 9; i++)
                gameBoard[i] = 0;
        }

        static bool IsValidMove(int boardIndex)
            => ((boardIndex >= 0)
                && boardIndex < 9)
                && gameBoard[boardIndex] == 0;

        static int IsWinCondition()
        {
            //  Check win conditions here
            //  Row 1
            if (((gameBoard[0] == gameBoard[1])
                && gameBoard[1] == gameBoard[2])
                && gameBoard[0] != 0)
                return gameBoard[0];
            //  Row 2
            if (((gameBoard[3] == gameBoard[4])
                && gameBoard[4] == gameBoard[5])
                && gameBoard[3] != 0)
                return gameBoard[3];
            //  Row 3
            if (((gameBoard[6] == gameBoard[7])
                && gameBoard[7] == gameBoard[8])
                && gameBoard[6] != 0)
                return gameBoard[6];
            //  Column 1
            if (((gameBoard[0] == gameBoard[3])
                && gameBoard[3] == gameBoard[6])
                && gameBoard[0] != 0)
                return gameBoard[0];
            //  Column 2
            if (((gameBoard[1] == gameBoard[4])
                && gameBoard[4] == gameBoard[7])
                && gameBoard[1] != 0)
                return gameBoard[1];
            //  Column 3
            if (((gameBoard[2] == gameBoard[5])
                && gameBoard[5] == gameBoard[8])
                && gameBoard[2] != 0)
                return gameBoard[2];
            //  Diagonal 1
            if (((gameBoard[0] == gameBoard[4])
                && gameBoard[4] == gameBoard[8])
                && gameBoard[0] != 0)
                return gameBoard[0];
            //  Diagonal 2
            if (((gameBoard[2] == gameBoard[4])
                && gameBoard[4] == gameBoard[6])
                && gameBoard[2] != 0)
                return gameBoard[2];
            //  Check if all spaces are occupied
            if (gameBoard.Count(val => val == 0) == 0)
                return 0;

            return -1;
        }

        static void PrintBoard()
        {
            //  Clear the current console and write a header
            Console.Clear();
            Console.Write("Tic-Tac-Toe");

            //  Loop each cell on the game board, and print the correct value
            for (int i = 0; i < 9; i++)
            {
                //  Check if 'i' is divisible by 3 and if so we need to write a new line
                if ((i % 3) == 0)
                    Console.WriteLine();

                //  Print the board
                //  X, O or . for the counter value (. = 0, X = 1, O = 2)
                switch (gameBoard[i])
                {
                    case 0:
                        Console.Write(".");
                        break;
                    case 1:
                        Console.Write("X");
                        break;
                    case 2:
                        Console.Write("O");
                        break;
                }
            }
            //  Adds a new line after printing the board
            Console.WriteLine();
        }
    }
}