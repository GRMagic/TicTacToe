using System;

namespace TicTacToe
{
    static class TicTacToeIA
    {
        /// <summary>
        /// Stupid Player
        /// </summary>
        /// <returns>true when can move</returns>
        public static bool RandomMove(this TicTacToe game, Player player)
        {
            if (game.GameOver) return false;

            var random = new Random((int)DateTime.Now.Ticks);
            var position = random.Next(9);
            while (!game.Move(position / 3, position % 3, player))
            {
                position = random.Next(9);
            };
            return true;
        }

        /// <summary>
        /// Motherfucker Player
        /// </summary>
        /// <returns>true when can move</returns>
        public static bool MiniMaxMove(this TicTacToe game, Player player)
        {
            if (game.GameOver) return false;

            var bestScore = int.MinValue;
            var bestLine = 0;
            var bestColumn = 0;
            for (int l = 0; l < 3; l++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (game.CanMove(l, c))
                    {
                        var gameCopy = game.Clone() as TicTacToe;
                        gameCopy.Move(l, c, player);
                        int score = MiniMax(gameCopy, player, false);
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestLine = l;
                            bestColumn = c;
                        }
                    }
                }
            }

            return game.Move(bestLine, bestColumn, player);    
        }

        private static int MiniMax(TicTacToe game, Player player, bool isMaximizing, int depth = 0)
        {
            if (game.GameOver)
            {
                if (game.Winner == player) return  1; // Win
                if (game.Winner == null)   return  0; // Tie
                if (game.Winner != player) return -1; // Lose
            }

            if (isMaximizing)
            {
                var bestScore = int.MinValue;
                for (int l = 0; l < 3; l++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (game.CanMove(l, c))
                        {
                            var gameCopy = game.Clone() as TicTacToe;
                            gameCopy.Move(l, c, player);
                            int score = MiniMax(gameCopy, player, false, depth + 1);
                            bestScore = Math.Max(bestScore, score);
                        }
                    }
                }
                return bestScore;
            }
            else // isMinimizing
            {
                var bestScore = int.MaxValue;
                for (int l = 0; l < 3; l++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (game.CanMove(l, c))
                        {
                            var gameCopy = game.Clone() as TicTacToe;
                            gameCopy.Move(l, c, OtherPlayerThan(player));
                            int score = MiniMax(gameCopy, player, true, depth+1);
                            bestScore = Math.Min(bestScore, score);
                        }
                    }
                }
                return bestScore;
            }
        }

        private static Player OtherPlayerThan(Player me)
        {
            if (me == Player.Player1) return Player.Player2;
            return Player.Player1;
        }
    }
}
