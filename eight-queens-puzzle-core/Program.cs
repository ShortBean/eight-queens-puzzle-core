using System;

namespace eight_queens_puzzle_core
{
    class Program
    {
        static readonly int quanQueen = 8; // 皇后數
        static int solutionNo = 1; // 第幾個解

        /// <summary>
        /// 主程式
        /// </summary>
        static void Main()
        {
            char[,] board = new char[quanQueen, quanQueen];

            // 遞迴擺放皇后位置，並印出結果
            if (SolveNQUtil(board, 0) == false)
            {
                // 一個解都沒有
                Console.Write("{0}-queens puzzle has no solution.", quanQueen);
            }

            Console.WriteLine();
            Console.Write("Press any key to close the 8-queens puzzle app...");
            Console.ReadKey();
        }

        /// <summary>
        /// 遞迴擺放皇后位置，並印出結果
        /// </summary>
        /// <param name="board">棋盤</param>
        /// <param name="idxCol">第幾欄</param>
        /// <returns>true: 有解，false: 無解</returns>
        private static bool SolveNQUtil(char[,] board, int idxCol)
        {
            // N 個皇后都放置完
            if (idxCol == quanQueen)
            {
                PrintSolution(board);
                return true;
            }

            // 固定欄，並嘗試將皇后放置於各列
            bool res = false;
            for (int idxRow = 0; idxRow < quanQueen; idxRow++)
            {
                // 檢查此位置是否安全
                if (IsSafe(board, idxRow, idxCol))
                {
                    // 將皇后放置在安全位置
                    board[idxRow, idxCol] = 'Ｑ';

                    // 遞迴走訪下一欄
                    res = SolveNQUtil(board, idxCol + 1) || res;

                    // 走訪完後沒放滿 N 個，移除上一個放好的皇后
                    board[idxRow, idxCol] = '．';
                }
            }

            return res;
        }

        /// <summary>
        /// 檢查 board[row, col] 是否安全可放置
        /// 因為放置的順序是由左至右，所以只需檢查此位置以左(0至col-1)已放置的皇后是否對此位置造成威脅
        /// </summary>
        /// <param name="board">棋盤</param>
        /// <param name="row">要檢查的位置在哪一列</param>
        /// <param name="col">要檢查的位置在哪一欄</param>
        /// <returns>true: 可放置，false: 不可放置</returns>
        private static bool IsSafe(char[,] board, int row, int col)
        {
            int i, j;

            // 向左檢查此列是否已有皇后
            for (i = 0; i < col; i++)
            {
                if (board[row, i] == 'Ｑ')
                    return false;
            }
            // 檢查左上對角線是否已有皇后
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j] == 'Ｑ')
                    return false;
            }
            // 檢查左下對角線是否已有皇后
            for (i = row, j = col; j >= 0 && i < quanQueen; i++, j--)
            {
                if (board[i, j] == 'Ｑ')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 印出棋盤
        /// </summary>
        /// <param name="board">棋盤</param>
        private static void PrintSolution(char[,] board)
        {
            Console.Write("Solution {0}\n", solutionNo++);
            for (int i = 0; i < quanQueen; i++)
            {
                for (int j = 0; j < quanQueen; j++)
                {
                    Console.Write("{0}", board[i, j] == '\0' ? '．' : board[i, j]);
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }
}
