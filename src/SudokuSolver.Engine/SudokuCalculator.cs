using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Engine
{
    public static class SudokuCalculator
    {
        public const int SudokuSize = 9;
        public const int SudokuCubeSize = SudokuSize / 3;

        public static (int row, int column) GetStartingCubeCellFromCubeIndex(int cubeIndex)
            => (cubeIndex / SudokuCubeSize * SudokuCubeSize, cubeIndex * SudokuCubeSize % SudokuSize);

        public static (int row, int column) GetStartingCubeCell(int row, int column)
        {
            var cubeRow = (row / SudokuCubeSize) * SudokuCubeSize;
            var cubeCol = (column / SudokuCubeSize) * SudokuCubeSize;

            return (cubeRow, cubeCol);
        }
    }
}
