using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Engine
{
    public partial class SandwichSudoku : ClassicSudoku
    {
        public int[] RowSum { get; }
        public int[] ColumSum { get; }

        public SandwichPair[] RowSandwichPairs { get; }
        public SandwichPair[] ColumnSandwichPairs { get; }

        public SandwichSudoku(int?[,] grid, int[] rowSum, int[] columnSum, ISudokuValidator? validator = null)
            : base(grid, validator)
        {
            Validator = validator ?? new SandwichSudokuValidator(this);

            RowSum = rowSum;
            ColumSum = columnSum;

            (RowSandwichPairs, ColumnSandwichPairs) = GetSandwichPairs();
        }
       
        private (SandwichPair[] rows, SandwichPair[] columns) GetSandwichPairs()
        {
            var rows = new SandwichPair[SudokuCalculator.SudokuSize];
            var columns = new SandwichPair[SudokuCalculator.SudokuSize];

            for(var index = 0; index < SudokuCalculator.SudokuSize; ++index)
            {
                var result = GetSandwichPair(index);
                
                rows[index] = result.row;
                columns[index] = result.column;
            }

            return (rows, columns);
        }

        protected override void HandleSet(int row, int column, int? value)
        {
            base.HandleSet(row, column, value);

            //if(value is 1)
            //{
            //    RowSandwichPairs[row].IndexOfOne = column;
            //    ColumnSandwichPairs[column].IndexOfOne = row;
            //}
            //else if(value is 9)
            //{
            //    RowSandwichPairs[row].IndexOfNine = column;
            //    ColumnSandwichPairs[column].IndexOfNine = row;
            //}

            //if(value is null)
            //{
            //    if(RowSandwichPairs[row].IndexOfOne == column)
            //    {
            //        RowSandwichPairs[row].IndexOfOne = null;
            //    }
            //    else if(RowSandwichPairs[row].IndexOfNine == column)
            //    {
            //        RowSandwichPairs[row].IndexOfNine = null;
            //    }

            //    if(ColumnSandwichPairs[column].IndexOfNine == row)
            //    {
            //        ColumnSandwichPairs[column].IndexOfNine = null;
            //    }
            //    else if (ColumnSandwichPairs[column].IndexOfOne == row)
            //    {
            //        ColumnSandwichPairs[column].IndexOfOne = null;
            //    }
            //}
        }

        private (SandwichPair row, SandwichPair column) GetSandwichPair(int index)
        {
            var rowPair = new SandwichPair();
            var columnPair = new SandwichPair();

            for(var cell = 0; cell < Grid.GetLength(1); ++cell)
            {
                if (Grid[index, cell] == 1)
                {
                    rowPair.IndexOfOne = cell;
                }
                else if (Grid[index, cell] == 9)
                {
                    rowPair.IndexOfNine = index;
                }
            }

            for(var row = 0; row < Grid.GetLength(0); ++row)
            {
                if (Grid[row, index] == 1)
                {
                    columnPair.IndexOfOne = row;
                }
                else if(Grid[row, index] == 9)
                {
                    columnPair.IndexOfNine = row;
                }
            }

            return (rowPair, columnPair);
        }
    }
}
