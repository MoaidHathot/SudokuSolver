using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Engine
{
    public class CellChangedEventArgs
    {
        public int Row { get; }
        public int Column { get; }
        public int? Value { get; }

        public CellChangedEventArgs(int row, int column, int? value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
    }

}
