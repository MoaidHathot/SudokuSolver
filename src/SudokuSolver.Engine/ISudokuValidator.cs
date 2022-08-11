namespace SudokuSolver.Engine
{
    public interface ISudokuValidator
    {
        bool IsSudokuComplete();
        bool IsValidPlacement(int row, int column, int number);
        bool IsSudokuValid();

        bool IsRowValid(int row);
        bool IsColumnValid(int column);
        bool IsCubeValid(int cube);
    }
}