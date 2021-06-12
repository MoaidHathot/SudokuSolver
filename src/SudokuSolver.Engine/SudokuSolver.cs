using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using SudokuSolver.Engine.Extensions;

namespace SudokuSolver.Engine
{
    public class SudokuSolver
    {
        private readonly SudokuValidator _validator = new();

        public bool Solve(int?[,] grid, Action<CellChangedEventArgs>? callback = null)
            => Solve(new Sudoku(grid), callback);

        private bool Solve(Sudoku sudoku, Action<CellChangedEventArgs>? callback = null)
        {
            if (sudoku.IsComplete())
            {
                return true;
            }

            var (row, column) = sudoku.EmptyCells.FirstOrDefault();

            foreach (var number in sudoku.GetMissingInRow(row))
            {
                if (_validator.IsValidPlacement(sudoku, row, column, number))
                {
                    sudoku[row, column] = number;
                    HandleCellChanged(callback, row, column, number);

                    if (Solve(sudoku, callback))
                    {
                        return true;
                    }

                    sudoku[row, column] = null;
                    HandleCellChanged(callback, row, column, number);
                }
            }

            return false;
        }

        public bool Validate(int?[,] grid)
            => Validate(new Sudoku(grid));

        private bool Validate(Sudoku sudoku)
            => _validator.GetSudokuState(sudoku) is (true, true);

        private void HandleCellChanged(Action<CellChangedEventArgs>? callback, int row, int column, int? value)
            => callback?.Invoke(new CellChangedEventArgs(row, column, value));
    }
}