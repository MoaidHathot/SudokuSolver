using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver.Engine.Extensions;

namespace SudokuSolver.Engine.Validators
{
    public class ClassicSudokuValidator : ISudokuValidator
    {
        private ClassicSudoku _sudoku;

        public ClassicSudokuValidator(ClassicSudoku classicSudoku)
        {
            _sudoku = classicSudoku;
        }

        public bool IsSudokuComplete()
        {
            for (var row = 0; row < _sudoku.RowCount; ++row)
            {
                if (!_sudoku.IsRowComplete(row))
                {
                    return false;
                }
            }

            for (var column = 0; column < _sudoku.ColumnCount; ++column)
            {
                if (!_sudoku.IsColumnComplete(column))
                {
                    return false;
                }
            }

            for (var cube = 0; cube < _sudoku.CubeCount; ++cube)
            {
                if (!_sudoku.IsCubeComplete(cube))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSudokuValid()
        {
            if (_sudoku.RowCount != _sudoku.ColumnCount)
            {
                throw new NotSupportedException();
            }

            for (var index = 0; index < _sudoku.RowCount; ++index)
            {
                if (!this.IsCrossValid(_sudoku, index, index))
                {
                    return false;
                }
            }

            for (var cube = 0; cube < _sudoku.CubeCount; ++cube)
            {
                if (!IsCubeValid(cube))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsValidPlacement(int row, int column, int number)
            => !_sudoku.ExistInRow(row, number) && !_sudoku.ExistInColumn(column, number) && !_sudoku.ExistInCube(SudokuCalculator.GetCubeFromCell(row, column), number);

        public bool IsRowValid(int row)
        {
            var set = new NumberSet(SudokuCalculator.SudokuSize);

            for (var column = 0; column < SudokuCalculator.SudokuSize; ++column)
            {
                if (_sudoku[row, column] is int number)
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

        public bool IsColumnValid(int column)
        {
            var set = new NumberSet(SudokuCalculator.SudokuSize);

            for (var row = 0; row < SudokuCalculator.SudokuSize; ++row)
            {
                if (_sudoku[row, column] is int number)
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

        public bool IsCubeValid(int cube)
        {
            var set = new NumberSet(SudokuCalculator.SudokuSize);

            var (startingRow, startingColumn) = SudokuCalculator.GetStartingCubeCellFromCubeIndex(cube);

            var rowLimit = startingRow + SudokuCalculator.SudokuCubeSize;
            var columnLimit = startingColumn + SudokuCalculator.SudokuCubeSize;

            for (var row = startingRow; row < rowLimit; ++row)
            {
                for (var column = startingColumn; column < columnLimit; ++column)
                {
                    if (_sudoku[row, column] is int number)
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
    }
}
