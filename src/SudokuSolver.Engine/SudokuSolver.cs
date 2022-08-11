using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using SudokuSolver.Engine.Extensions;
using SudokuSolver.Engine.Validators;

namespace SudokuSolver.Engine
{
    public class SudokuSolver
    {
        public bool Solve(ISudokuBoard sudoku, Action<CellChangedEventArgs>? callback = null)
        {
            if (sudoku.IsComplete())
            {
                return true;
            }

            var validator = sudoku.Validator;

            var (row, column) = sudoku.EmptyCells.FirstOrDefault();

            foreach (var number in sudoku.GetMissingInRow(row))
            {
                if (validator.IsValidPlacement(row, column, number))
                {
                    sudoku[row, column] = number;
                    HandleCellChanged(callback, row, column, number, sudoku);

                    if (Solve(sudoku, callback))
                    {
                        return true;
                    }

                    sudoku[row, column] = null;
                    HandleCellChanged(callback, row, column, number, sudoku);
                }
            }

            return false;
        }

        public bool Validate(ClassicSudoku sudoku)
            => sudoku.Validator.IsSudokuComplete() && sudoku.Validator.IsSudokuValid();

        private void HandleCellChanged(Action<CellChangedEventArgs>? callback, int row, int column, int? value, ISudokuBoard board)
            => callback?.Invoke(new CellChangedEventArgs(row, column, value, board));
    }
}