using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Herfleri daxil edin");
            string Letters = Console.ReadLine();

            Console.WriteLine("Sözü daxil edin");
            string Word = Console.ReadLine();
            char[] ArrWord = Word.ToCharArray();

            findWords(StringToMatrix(Letters), ArrWord, ArrWord.Length - 1);
            Console.ReadKey();
        }

        static int R;
        static int C;
        static int[] rowNum = { -1, -1, -1, 0, 0, 1, 1, 1 };
        static int[] colNum = { -1, 0, 1, -1, 1, -1, 0, 1 };


        //Given String Convert To Matrix
        public static char[,] StringToMatrix(String str)
        {
            int l = str.Length;
            int k = 0;
            R = (int)Math.Floor(Math.Sqrt(l));
            C = (int)Math.Ceiling(Math.Sqrt(l));

            if (R * C < l)
            {
                R = C;
            }

            char[,] grid = new char[R, C];

            // convert the string into grid
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    if (k < str.Length)
                        grid[i, j] = str[k];
                    k++;
                }
            }
            return grid;
        }

        static bool isvalid(int row, int col, int prevRow, int prevCol)
        {
            // return true if row number and column number
            // is in range
            return (row >= 0) && (row < R) &&
                (col >= 0) && (col < C) &&
                !(row == prevRow && col == prevCol);
        }

        //Searching word in matrix
        static void DFS(char[,] mat, int row, int col,
         int prevRow, int prevCol, char[] word,
         String path, int index, int n)
        {
            // return if current character doesn't match with
            // the next character in the word
            if (index > n || mat[row, col] != word[index])
                return;

            // append current character position to path
            path += (word[index]) + "(" + String.Join("", row)
                    + ", " + String.Join("", col) + ") ";

            // current character matches with the last character
            // in the word
            if (index == n)
            {
                Console.Write(path + "\n");
                return;
            }

            // Recur for all connected neighbours
            for (int k = 0; k < 8; ++k)
                if (isvalid(row + rowNum[k], col + colNum[k],
                            prevRow, prevCol))

                    DFS(mat, row + rowNum[k], col + colNum[k],
                        row, col, word, path, index + 1, n);
        }

        //Print word path
        static void findWords(char[,] mat, char[] word, int n)
        {
            // traverse through the all cells of given matrix
            for (int i = 0; i < R; ++i)
                for (int j = 0; j < C; ++j)

                    // occurrence of first character in matrix
                    if (mat[i, j] == word[0])

                        // check and print if path exists
                        DFS(mat, i, j, -1, -1, word, "", 0, n);
        }
    }
}