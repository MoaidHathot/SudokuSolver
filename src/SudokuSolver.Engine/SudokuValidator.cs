using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver.Engine.Extensions;

namespace SudokuSolver.Engine
{
    internal class SudokuValidator
    {
        public (bool valid, bool complete) GetSudokuState(Sudoku sudoku)
        {
            //todo - optimize - calculate both at the same time;
            var valid = IsSudokuValid(sudoku);

            if (!valid)
            {
                return (false, false);
            }

            return (valid, IsSudokuComplete(sudoku));
        }


        public bool IsSudokuComplete(Sudoku sudoku)
        {
            for (var row = 0; row < sudoku.RowCount; ++row)
            {
                if (!sudoku.IsRowComplete(row))
                {
                    return false;
                }
            }

            for (var column = 0; column < sudoku.ColumnCount; ++column)
            {
                if (!sudoku.IsColumnComplete(column))
                {
                    return false;
                }
            }

            for (var cube = 0; cube < sudoku.CubeCount; ++cube)
            {
                if (!sudoku.IsCubeComplete(cube))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSudokuValid(Sudoku sudoku)
        {
            if (sudoku.RowCount != sudoku.ColumnCount)
            {
                throw new NotSupportedException();
            }

            for (var index = 0; index < sudoku.RowCount; ++index)
            {
                if (!IsCrossValid(sudoku, index, index))
                {
                    return false;
                }
            }

            for (var cube = 0; cube < sudoku.CubeCount; ++cube)
            {
                if (!IsCubeValid(sudoku, cube))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsValidPlacement(Sudoku sudoku, int row, int column, int number)
            => !sudoku.ExistInRow(row, number) && !sudoku.ExistInColumn(column, number) && !sudoku.ExistInCube(SudokuCalculator.GetCubeFromCell(row, column), number);

        public bool IsRowValid(Sudoku sudoku, int row)
        {
            var set = new NumberSet(SudokuCalculator.SudokuSize);

            for (var column = 0; column < SudokuCalculator.SudokuSize; ++column)
            {
                if (sudoku[row, column] is int number)
                {
                    if (set.IsExist(number))
                    {
                        return false;
                    }
                    
                    set.Add(number);
                }
            }

            return true;
        }

        public bool IsColumnValid(Sudoku sudoku, int column)
        {
            var set = new NumberSet(SudokuCalculator.SudokuSize);

            for (var row = 0; row < SudokuCalculator.SudokuSize; ++row)
            {
                if (sudoku[row, column] is int number)
                {
                    if (set.IsExist(number))
                    {
                        return false;
                    }

                    set.Add(number);
                }
            }

            return true;
        }

        public bool IsCubeValid(Sudoku sudoku, int cube)
        {
            var set = new NumberSet(SudokuCalculator.SudokuSize);

            var (startingRow, startingColumn) = SudokuCalculator.GetStartingCubeCellFromCubeIndex(cube);

            var rowLimit = startingRow + SudokuCalculator.SudokuCubeSize;
            var columnLimit = startingColumn + SudokuCalculator.SudokuCubeSize;

            for (var row = startingRow; row < rowLimit; ++row)
            {
                for (var column = startingColumn; column < columnLimit; ++column)
                {
                    if (sudoku[row, column] is int number)
                    {
                        if (set.IsExist(number))
                        {
                            return false;
                        }

                        set.Add(number);
                    }
                }
            }

            return true;
        }

        public bool IsCrossValid(Sudoku sudoku, int row, int column)
            => IsRowValid(sudoku, row) && IsColumnValid(sudoku, column);
    }
}
