using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Engine.Extensions
{
    internal static class ValidatorExtensions
    {
        public static (bool valid, bool complete) GetSudokuState(this ISudokuValidator validator, ClassicSudoku sudoku)
        {
            var valid = validator.IsSudokuValid();

            if (!valid)
            {
                return (false, false);
            }

            return (valid, validator.IsSudokuComplete());
        }

        public static bool IsCrossValid(this ISudokuValidator validator, ClassicSudoku sudoku, int row, int column)
            => validator.IsRowValid(row) && validator.IsColumnValid(column);
    }
}
