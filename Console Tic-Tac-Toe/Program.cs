using System.Text;

namespace Console_Tic_Tac_Toe
{

    internal class Program
    {

        //by default I am providing 0-9 where no use of zero
        static string[] pos = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static int player = 1; //By default player 1 is set
        static int choice; //This holds the choice at which position user want to mark

        static int flag = 0;
        //The flag variable checks who has won
        //if it's value is 1 then someone has won the match
        //if -1 then Match has Draw
        //if 0 then match is still running

        static void Main(string[] args)
        {
            ChooseOption();
            static void ChooseOption()
            {
                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;


                Console.WriteLine("\"Welcome to the Console tic-tac-toe game!");
                Console.ResetColor();
                Console.WriteLine("\nUse ⬆ and ⬇ to navigate and press \u001b[32mEnter/Return\u001b[0m to select:");
                (int left, int top) = Console.GetCursorPosition();
                var option = 1;
                var decorator = "✅ \u001b[32m";
                ConsoleKeyInfo key;
                bool isSelected = false;

                while (!isSelected)
                {
                    Console.SetCursorPosition(left, top);

                    Console.WriteLine($"{(option == 1 ? decorator : "   ")}Play with player\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? decorator : "   ")}Play with computer\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? decorator : "   ")}Credits\u001b[0m");
                    Console.WriteLine($"{(option == 4 ? decorator : "   ")}Exit\u001b[0m");

                    key = Console.ReadKey(false);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            option = option == 1 ? 4 : option - 1;
                            break;

                        case ConsoleKey.DownArrow:
                            option = option == 4 ? 1 : option + 1;
                            break;

                        case ConsoleKey.Enter:
                            isSelected = true;
                            GameCore(option);
                            break;
                    }
                }
            }

            static void GameCore(int option)
            {
                switch (option)
                {
                    case 1:
                        GamePlay();
                        break;
                    case 2:
                        Console.WriteLine("\nThis option is still in development...");
                        Thread.Sleep(1500);
                        Console.WriteLine("Sorry for the inconvenience....");
                        Thread.Sleep(1500);
                        ChooseOption();
                        break;
                    case 3:
                        ShowCredits();
                        Thread.Sleep(3000);
                        ChooseOption();
                        break;
                    case 4:
                        break;
                }

                static void GamePlay()
                {
                    do
                    {
                        CreateBoard();

                        bool isValidInput = false;
                        while (!isValidInput)
                        {
                            Console.WriteLine("\n");
                            Console.Write("Enter a number: ");
                            string inputString = Console.ReadLine();

                            if (int.TryParse(inputString, out choice) && (choice <= 9 && choice >= 1))
                            {
                                // input is valid
                                isValidInput = true;
                                Marking(choice);
                            }
                            else
                            {
                                // input is invalid, restart the loop
                                Console.WriteLine("Invalid input. Please enter a valid number.");

                                Console.WriteLine("Please wait 1 second...");
                                Thread.Sleep(1000);
                                UpdateBoard();
                            }
                        }
                    }
                    while (flag != 1 && flag != -1);
                    // This loop will be run until all cell of the grid is not marked
                    //with X and O or some player is not win
                    UpdateBoard();
                    if (flag == 1)
                    // if flag value is 1 then someone has win or
                    //means who played marked last time which has win
                    {
                        Console.WriteLine("\nPlayer {0} has won", (player % 2) + 1);
                        Thread.Sleep(2000);
                        ShowCredits();
                        Thread.Sleep(2000);
                    }
                    else// if flag value is -1 the match will be draw and no one is winner
                    {
                        Console.WriteLine("Draw");
                        Thread.Sleep(2000);
                        ShowCredits();
                        Thread.Sleep(2000);
                    }
                    Console.ReadLine();
                }

                static void CreateBoard()
                {
                    Console.Clear();  // whenever loop will be again start then screen will be clear
                    Console.WriteLine("Welcome to the tic-tac-toe game!\n");
                    Console.WriteLine("Player 1: X and Player 2: O");

                    if (player % 2 == 0)  //checking the chance of the player
                    {
                        Console.WriteLine("Player \u001b[32m2\u001b[0m Chance");
                    }
                    else
                    {
                        Console.WriteLine("Player \u001B[31m1\u001b[0m Chance");
                    }
                    Console.WriteLine("\n");

                    Console.WriteLine("     |     |      ");
                    Console.WriteLine("  {0}  |  {1}  |  {2}", pos[1], pos[2], pos[3]);
                    Console.WriteLine("_____|_____|_____ ");
                    Console.WriteLine("     |     |      ");
                    Console.WriteLine("  {0}  |  {1}  |  {2}", pos[4], pos[5], pos[6]);
                    Console.WriteLine("_____|_____|_____ ");
                    Console.WriteLine("     |     |      ");
                    Console.WriteLine("  {0}  |  {1}  |  {2}", pos[7], pos[8], pos[9]);
                    Console.WriteLine("     |     |      ");
                }

                static void UpdateBoard() // clearing the console and getting filled board again
                {
                    Console.Clear();
                    CreateBoard();
                }

                static void Marking(int choice)
                {
                    //Checking the position on the mark
                    if (pos[choice] != "X" && pos[choice] != "O")
                    {
                        if (player % 2 == 0) //if choice is of player 2 then mark O else mark X
                        {
                            pos[choice] = "\u001b[32mO\u001b[0m";
                            player++;
                        }
                        else
                        {
                            pos[choice] = "\u001b[31mX\u001b[0m";
                            player++;
                        }
                    }
                    else
                    //If there is any position where user want to run
                    //and that is already marked then show message and load board again
                    {
                        Console.WriteLine("The row {0} is already marked", choice);

                        Console.WriteLine("Please wait 1 second...");
                        Thread.Sleep(1000);

                    }
                    flag = CheckWin();
                    // calling of check win
                }

                //Checking that any player has won or not
                static int CheckWin()
                {
                    //Horzontal Winning Condition
                    if (pos[1] == pos[2] && pos[2] == pos[3])
                        return 1;

                    else if (pos[4] == pos[5] && pos[5] == pos[6])
                        return 1;

                    else if (pos[7] == pos[8] && pos[8] == pos[9])
                        return 1;

                    //Vertical Winning Condition
                    else if (pos[1] == pos[4] && pos[4] == pos[7])
                        return 1;

                    else if (pos[2] == pos[5] && pos[5] == pos[8])
                        return 1;

                    else if (pos[3] == pos[6] && pos[6] == pos[9])
                        return 1;

                    //Diagonal Winning Condition
                    else if (pos[1] == pos[5] && pos[5] == pos[9])
                        return 1;

                    else if (pos[3] == pos[5] && pos[5] == pos[7])
                        return 1;

                    // Draw Condition
                    // If all the cells or values filled with X or O then any player has won the match
                    else if (pos[1] != "1" && pos[2] != "2" && pos[3] != "3"
                        && pos[4] != "4" && pos[5] != "5" && pos[6] != "6"
                        && pos[7] != "7" && pos[8] != "8" && pos[9] != "9")
                        return -1;

                    else
                        return 0;
                    //continue
                }

                static void ShowCredits()
                {
                    Console.WriteLine("\nDevelop by LarestoPresto");
                    Console.WriteLine("Beta v.1.2");
                    Console.WriteLine("25.08.2023");
                }

            }
        }

    }
}

