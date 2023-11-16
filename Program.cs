/// <summary>
/// tahacs
/// </summary>
class SudokuSolver
{
    private static int SIZE = 9;

    // تابع اصلی برای حل جدول سودوکو
    public bool SolveSudoku(int[,] board)
    {
        int row, col;

        // بررسی اگر تمام خانه ها پر شده باشند، جدول تکمیل شده است
        if (!FindEmptyLocation(board, out row, out col))
            return true;

        // امتحان کردن اعداد از 1 تا 9 در هر خانه خالی
        for (int num = 1; num <= SIZE; num++)
        {
            if (IsSafe(board, row, col, num))
            {
                // درج عدد در خانه خالی
                board[row, col] = num;

                // بازگشت به طور بازگشتی برای حل بقیه خانه ها
                if (SolveSudoku(board))
                    return true;

                // اگر قرار دادن عدد در خانه فعلی منجر به حل نشد، عدد را برگردانید
                board[row, col] = 0;
            }
        }
        return false;
    }

    // بررسی می کند آیا می توان عدد را در خانه خاص قرار داد
    private bool IsSafe(int[,] board, int row, int col, int num)
    {
        // بررسی ردیف ها
        for (int d = 0; d < SIZE; d++)
        {
            if (board[row, d] == num)
                return false;
        }

        // بررسی ستون ها
        for (int r = 0; r < SIZE; r++)
        {
            if (board[r, col] == num)
                return false;
        }

        // بررسی بلوک 3x3
        int sqrt = (int)Math.Sqrt(SIZE);
        int boxRowStart = row - row %sqrt;
        int boxColStart = col - col %sqrt;

        for (int r = boxRowStart; r < boxRowStart + sqrt; r++)
        {
            for (int d = boxColStart; d < boxColStart + sqrt; d++)
            {
                if (board[r, d] == num)
                    return false;
            }
        }

        return true;
    }

    // پیدا کردن خانه خالی در جدول
    private bool FindEmptyLocation(int[,] board, out int row, out int col)
    {
        for (row = 0; row < SIZE; row++)
        {
            for (col = 0; col < SIZE; col++)
            {
                if (board[row, col] == 0)
                    return true;
            }
        }

        col = 0;
        return false;
    }

    // تابع برای چاپ جدول سودوکو
    public void PrintSudoku(int[,] board)
    {
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        int[,] board = {
            { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
            { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
            { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
            { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
            { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
            { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
            { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
            { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
            { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
        };

        SudokuSolver solver = new SudokuSolver();

        if (solver.SolveSudoku(board))
        {
            Console.WriteLine("do it");
            solver.PrintSudoku(board);
        }
        else
        {
            Console.WriteLine("i cant do it!");
        }
    }
}