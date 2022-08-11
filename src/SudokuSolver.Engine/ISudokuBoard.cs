using System.Collections.Generic;

namespace SudokuSolver.Engine
{
    public interface ISudokuBoard
    {
        ISudokuValidator Validator { get; }

        int RowCount { get; }
        int ColumnCount { get; }
        int CubeCount { get; }

        IReadOnlySet<(int row, int column)> EmptyCells { get; }
        int FilledNumbersCount { get; }
        int?[,] Grid { get; }
        
        bool ExistInRow(int row, int number);
        bool ExistInColumn(int column, int number);
        bool ExistInCube(int cube, int number);

        IEnumerable<int> GetExistingInRow(int row);
        IEnumerable<int> GetExistingInColumn(int column);
        IEnumerable<int> GetExistingInCube(int cube);
        IEnumerable<int> GetMissingInRow(int row);
        IEnumerable<int> GetMissingInColumn(int column);
        IEnumerable<int> GetMissingInCube(int cube);

        bool IsComplete();
        bool IsRowComplete(int row);
        bool IsColumnComplete(int column);
        bool IsCubeComplete(int cube);

        int? this[int row, int column] { get; set; }
    }
}