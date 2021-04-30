using System;
using System.Linq;
using System.Threading;
using SudokuSolver.Engine.Extensions;

namespace SudokuSolver.Engine
{
    public class SudokuSolver
    {
        private readonly SudokuValidator _validator = new();
        private readonly Sudoku _sudoku;


        public event EventHandler<(int row, int column, int? value)>? OnCellChanged;

        public SudokuSolver(int?[,] grid)
            => _sudoku = new Sudoku(grid);

        public bool Solve()
        {
            if (_sudoku.IsComplete())
            {
                return true;
            }

            var (row, column) = _sudoku.EmptyCells.FirstOrDefault();

            foreach (var number in _sudoku.GetMissingInRow(row))
            {
                if (_validator.IsValidPlacement(_sudoku, row, column, number))
                {
                    _sudoku[row, column] = number;
                    HandleCellChanged(row, column, number);

                    if (Solve())
                    {
                        return true;
                    }

                    _sudoku[row, column] = null;
                    HandleCellChanged(row, column, number);
                }
            }

            return false;
        }

        public bool Validate()
            => _validator.GetSudokuState(_sudoku) is (true, true);

        private void HandleCellChanged(int row, int column, int? value)
            => OnCellChanged?.Invoke(this, (row, column, value));
    }
}