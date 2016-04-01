using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities
{
    public static class ArrayUtility
    {
        public static T[,] CreateRectangularArray<T>(IList<List<T>> lists)
        {
            int max_inner_length = 0;
            for (int i = 0; i < lists.Count; i++)
            {
                if (max_inner_length < lists[i].Count) max_inner_length = lists[i].Count;
            }

            T[,] result = new T[max_inner_length, lists.Count];

            for (int i = 0; i < lists.Count; i++)
            {
                for (int j = 0; j < lists[i].Count; j++)
                {
                    result[j, i] = lists[i][j];
                }
            }

            return result;
        }

        public static T[,] TransposeRowsAndColumns<T>(T[,] arr)
        {
            int rowCount = arr.GetLength(0);
            int columnCount = arr.GetLength(1);
            T[,] transposed = new T[columnCount, rowCount];
            if (rowCount == columnCount)
            {
                transposed = (T[,])arr.Clone();
                for (int i = 1; i < rowCount; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        T temp = transposed[i, j];
                        transposed[i, j] = transposed[j, i];
                        transposed[j, i] = temp;
                    }
                }
            }
            else
            {
                for (int column = 0; column < columnCount; column++)
                {
                    for (int row = 0; row < rowCount; row++)
                    {
                        transposed[column, row] = arr[row, column];
                    }
                }
            }
            return transposed;
        }
    }
}
