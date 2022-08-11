using SudokuSolver.Engine.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SudokuSolver.Engine.SandwichSudoku;

namespace SudokuSolver.Engine
{
    public class SandwichSudokuValidator : ISudokuValidator
    {
        private readonly ISudokuValidator _classicValidator;
        public readonly SandwichSudoku _sudoku;

        public SandwichSudokuValidator(SandwichSudoku sudoku)
        {
            _classicValidator = new ClassicSudokuValidator(sudoku);
            _sudoku = sudoku;
        }

        public bool IsSudokuComplete()
            => _classicValidator.IsSudokuComplete();

        public bool IsValidPlacement(int row, int column, int number)
        {
            if(_classicValidator.IsValidPlacement(row, column, number) is false)
            {
                return false;
            }

            return CheckRowSum(row, column, number) && CheckColumnSum(row, column, number);
        }

        private bool CheckFutureRowSum(int row, int number, int column, int firstIndex, int secondIndex)
        {
            var start = Math.Min(firstIndex, secondIndex);
            var end = Math.Max(firstIndex, secondIndex);

            if(column < start || column > end)
            {
                return true;
            }
            
            var sum = 0; 
            for(var index = start + 1; index < end; index++)
            {
                var value = _sudoku.Grid[row, index];
                
                if (value is null)
                {
                    return true;
                }

                sum += value ?? 0;
            }

            return sum == _sudoku.RowSum[row];
        }

        private bool CheckFutureColumnSum(int column, int number, int row, int firstIndex, int secondIndex)
        {
            var start = Math.Min(firstIndex, secondIndex);
            var end = Math.Max(firstIndex, secondIndex);

            if(row < start || row > end)
            {
                return true;
            }

            var sum = 0; 
            for(var index = start + 1; index < end; index++)
            {
                var value = _sudoku.Grid[index, column];
                
                if (value is null)
                {
                    return true;
                }

                sum += value ?? 0;
            }

            return sum == _sudoku.ColumSum[column];

        }

        private bool CheckRowSum(int row, int column, int number)
        {
            var (indexOfOne, indexOfNine) = _sudoku.RowSandwichPairs[row];
            (indexOfOne, indexOfNine) = GetRowPair(row);

            return (indexOfOne, indexOfNine) switch
            {
                (null, null) => true,
                (int oneIndex, null) when number == 9 => CheckFutureRowSum(row, number, column, oneIndex, column),
                (null, int nineIndex) when number == 1 => CheckFutureRowSum(row, number, column, nineIndex, column),
                (int one, int second) => CheckFutureRowSum(row, number, column, one, second),
                _ => true,
            };
        }

        private bool CheckRowSum(int row)
        {
            var (indexOfOne, indexOfNine) = _sudoku.RowSandwichPairs[row];
            
            if(indexOfOne is null || indexOfNine is null)
            {
                return true;
            }

            var (start, end) = indexOfOne < indexOfNine ? (indexOfOne.Value, indexOfNine.Value) : (indexOfNine.Value, indexOfOne.Value);

            var sum = 0;

            for(int index = start + 1; index < end; ++index)
            {
                if(_sudoku.Grid[row, index] is null)
                {
                    return true;
                }

                sum += _sudoku.Grid[row, index]!.Value;
            }

            return sum == _sudoku.RowSum[row];

        }

        private bool CheckColumnSum(int row, int column, int number)
        {
            var (indexOfOne, indexOfNine) = _sudoku.ColumnSandwichPairs[column];
            (indexOfOne, indexOfNine) = GetColumnPair(column);

            return (indexOfOne, indexOfNine) switch
            {
                (null, null) => true,
                (int oneIndex, null) when number == 9 => CheckFutureColumnSum(column, number, row, oneIndex, row),
                (null, int nineIndex) when number == 1 => CheckFutureColumnSum(column, number, row, nineIndex, row),
                (int one, int second) => CheckFutureColumnSum(column, number, row, one, second),
                _ => true,
            };

        }

        private bool CheckColumnSum(int column)
        {
            var (indexOfOne, indexOfNine) = _sudoku.ColumnSandwichPairs[column];
            
            if(indexOfOne is null || indexOfNine is null)
            {
                return true;
            }

            var (start, end) = indexOfOne < indexOfNine ? (indexOfOne.Value, indexOfNine.Value) : (indexOfNine.Value, indexOfOne.Value);

            var sum = 0;

            for(int index = start + 1; index < end; ++index)
            {
                if(_sudoku.Grid[index, column] is null)
                {
                    return true;
                }

                sum += _sudoku.Grid[index, column]!.Value;
            }

            return sum == _sudoku.ColumSum[column];
        }

        private SandwichPair GetRowPair(int row)
        {
            var rowPair = new SandwichPair();

            for(var cell = 0; cell < _sudoku.Grid.GetLength(1); ++cell)
            {
                var value = _sudoku.Grid[row, cell];

                if (value == 1)
                {
                    rowPair.IndexOfOne = cell;
                }
                else if (value == 9)
                {
                    rowPair.IndexOfNine = cell;
                }
            }

            return rowPair;
        }

        private SandwichPair GetColumnPair(int cell)
        {
            var columnPair = new SandwichPair();

            for(var row = 0; row < _sudoku.Grid.GetLength(0); ++row)
            {
                var value = _sudoku.Grid[row, cell];

                if (value == 1)
                {
                    columnPair.IndexOfOne = row;
                }
                else if(value  == 9)
                {
                    columnPair.IndexOfNine = row;
                }
            }

            return columnPair;
        }
        
        private (SandwichPair row, SandwichPair column) GetSandwichPair(int index)
        {
            var rowPair = new SandwichPair();
            var columnPair = new SandwichPair();

            for(var cell = 0; cell < _sudoku.Grid.GetLength(1); ++cell)
            {
                if (_sudoku.Grid[index, cell] == 1)
                {
                    rowPair.IndexOfOne = cell;
                }
                else if (_sudoku.Grid[index, cell] == 9)
                {
                    rowPair.IndexOfNine = cell;
                }
            }

            for(var row = 0; row < _sudoku.Grid.GetLength(0); ++row)
            {
                if (_sudoku.Grid[row, index] == 1)
                {
                    columnPair.IndexOfOne = row;
                }
                else if(_sudoku.Grid[row, index] == 9)
                {
                    columnPair.IndexOfNine = row;
                }
            }

            return (rowPair, columnPair);
        }


        public bool IsSudokuValid()
        {
            if(!_classicValidator.IsSudokuValid())
            {
                return false;
            }

            for(var index = 0; index < SudokuCalculator.SudokuSize; ++index)
            {
                if(CheckRowSum(index) is false || CheckColumnSum(index) is false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsRowValid(int row)
        {
            if (!_classicValidator.IsRowValid(row))
            {
                return false;
            }

            return CheckRowSum(row);
        }

        public bool IsColumnValid(int column)
        {
            if (!_classicValidator.IsColumnValid(column))
            {
                return false;
            }

            return CheckColumnSum(column);
        }

        public bool IsCubeValid(int cube)
            => _classicValidator.IsCubeValid(cube);
    }
}
