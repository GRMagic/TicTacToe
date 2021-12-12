using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {


            //MarkIA(game, Player.Player1);
            //Mark(game, Player.Player2, 1, 1);
            //MarkIA(game, Player.Player1);
            //Mark(game, Player.Player2, 0, 2);
            //MarkIA(game, Player.Player1);
            //Mark(game, Player.Player2, 1, 2);
            //MarkIA(game, Player.Player1);
            //MarkIA(game, Player.Player1);
            //Mark(game, Player.Player2, 0, 1);
            //MarkIA(game, Player.Player1);
            //Mark(game, Player.Player2, 2, 0);
            //MarkIA(game, Player.Player1);
            //Mark(game, Player.Player2, 2, 2);
            //MarkIA(game, Player.Player1);


            Console.WriteLine(@"Keys to move:
7 | 8 | 9
- - - - -
4 | 5 | 6
- - - - -
1 | 2 | 3
");
            Console.WriteLine("Who starts?");
            Console.WriteLine("1 - IA");
            Console.WriteLine("2 - Human");
            var nextPlayer = Console.ReadKey().KeyChar == '1' ? Player.Player1 : Player.Player2;

            var game = new TicTacToe();
            Console.WriteLine();
            game.Print();

            while (!game.GameOver)
            {
                if (nextPlayer == Player.Player1)
                {
                    MarkIA(game, Player.Player1);
                    nextPlayer = Player.Player2;
                }
                else
                {
                    var invalidMove = false;
                    do
                    {
                        var key = Console.ReadKey(true).KeyChar;
                        switch (key)
                        {
                            case '7':
                                Mark(game, Player.Player2, 0, 0);
                                break;
                            case '8':
                                Mark(game, Player.Player2, 0, 1);
                                break;
                            case '9':
                                Mark(game, Player.Player2, 0, 2);
                                break;
                            case '4':
                                Mark(game, Player.Player2, 1, 0);
                                break;
                            case '5':
                                Mark(game, Player.Player2, 1, 1);
                                break;
                            case '6':
                                Mark(game, Player.Player2, 1, 2);
                                break;
                            case '1':
                                Mark(game, Player.Player2, 2, 0);
                                break;
                            case '2':
                                Mark(game, Player.Player2, 2, 1);
                                break;
                            case '3':
                                Mark(game, Player.Player2, 2, 2);
                                break;
                            default:
                                invalidMove = true;
                                break;
                        }
                        
                    } while (invalidMove);
                    nextPlayer = Player.Player1;
                }
                if (game.GameOver) continue;

            }
            if(game.Winner == null)
            {
                Console.WriteLine("Tie!");
            }
            else
            {
                Console.WriteLine($"The {game.Winner} wins!");
            }
        }

        static void Mark(TicTacToe game, Player player, int l, int c)
        {
            Console.WriteLine();
            game.Move(l, c, player);
            game.Print();
            Console.WriteLine();
        }

        static void MarkIA(TicTacToe game, Player player)
        {
            Console.WriteLine();
            game.MiniMaxMove(player);
            game.Print();
            Console.WriteLine();
        }
    }
}
