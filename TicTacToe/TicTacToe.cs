using System;

namespace TicTacToe
{
    class TicTacToe : ICloneable
    {
        private Player?[,] board;
        public bool GameOver { get; private set; }
        public Player? Winner { get; private set; }
        public Player? LastPlayer { get; private set; }

        public TicTacToe()
        {
            board = new Player?[3, 3];
        }

        public bool Move(int lin, int col, Player player)
        {
            if (GameOver || player == LastPlayer || !CanMove(lin,col)) return false;
            
            board[lin, col] = player;
            LastPlayer = player;
            GameOver = CheckGameOverSituations();

            return true;
        }

        public bool CanMove(int lin, int col) => !board[lin, col].HasValue;

        public void Print()
        {
            for(var l =0; l<3; l++)
            {
                for (var c = 0; c < 3; c++)
                {
                    Console.Write(board[l, c].HasValue ? (char)board[l, c] : ' ');
                    if (c < 2) Console.Write(" | ");
                }
                Console.WriteLine();
                if (l < 2) Console.WriteLine("- - - - -");
            }
        }

        private bool CheckGameOverSituations()
        {
            var empty = 9;
            for (var a = 0; a < 3; a++)
            {
                var inLine1 = 0;
                var inColumn1 = 0;
                var inLine2 = 0;
                var inColumn2 = 0;
                for (var b = 0; b < 3; b++)
                {
                    if (board[b, a].HasValue)
                    {
                        empty--;
                    }
                    else
                    {
                        continue;
                    }
                        
                    if (board[a, b] == Player.Player1) inLine1++;
                    else if (board[a, b] == Player.Player2) inLine2++;
                    if (board[b, a] == Player.Player1) inColumn1++;
                    else if (board[b, a] == Player.Player2) inColumn2++;                    
                }
                if (inLine1 == 3 || inColumn1 == 3)
                {
                    Winner = Player.Player1;
                    return true;
                }
                if (inLine2 == 3 || inColumn2 == 3)
                {
                    Winner = Player.Player2;
                    return true;
                }
            }

            if((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) || (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2]))
            {
                Winner = board[1, 1];
                return Winner != null;
            }

            return empty == 0;
        }

        public object Clone()
        {
            return new TicTacToe
            {
                GameOver = GameOver,
                board = board.Clone() as Player?[,]
            };
        }

    }
}
