using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;

namespace SudokuSolver.Engine
{
    internal class Sudoku
    {
        public int RowCount => SudokuCalculator.SudokuSize;
        public int ColumnCount => SudokuCalculator.SudokuSize;
        public int CubeCount => SudokuCalculator.SudokuSize;

        private readonly NumberSet[] _rowSets = new NumberSet[SudokuCalculator.SudokuSize];
        private readonly NumberSet[] _columnSets = new NumberSet[SudokuCalculator.SudokuSize];
        private readonly NumberSet[] _cubeSets = new NumberSet[SudokuCalculator.SudokuSize];

        private int?[,] _grid;

        public Sudoku(int?[,] grid)
        {
            _grid = grid;
            InitializeSets(grid);
        }

        private void InitializeSets(int?[,] grid)
        {
            InitializeRowSets(grid, _rowSets);
            InitializeColumnSets(grid, _columnSets);
            InitializeCubeSets(grid, _cubeSets);
        }

        private void InitializeRowSets(int?[,] grid, NumberSet[] rowSets)
        {
            if (rowSets == null) throw new ArgumentNullException(nameof(rowSets));
            for (var rowIndex = 0; rowIndex < rowSets.Length; ++rowIndex)
            {
                var row = rowSets[rowIndex];

                for (var columnIndex = 0; columnIndex < row.Length; ++columnIndex)
                {
                    if (grid[rowIndex, columnIndex] is { } value)
                    {
                        row.Add(value);
                    }
                }
            }
        }

        private void InitializeColumnSets(int?[,] grid, IReadOnlyList<NumberSet> columnSets)
        {
            for (var columnIndex = 0; columnIndex < columnSets.Count; ++columnIndex)
            {
                var column = columnSets[columnIndex];

                for (var rowIndex = 0; rowIndex < column.Length; ++rowIndex)
                {
                    if (grid[rowIndex, columnIndex] is { } value)
                    {
                        column.Add(value);
                    }
                }
            }
        }

        private void InitializeCubeSets(int?[,] grid, IReadOnlyList<NumberSet> cubeSets)
        {
            for (var cubeIndex = 0; cubeIndex < SudokuCalculator.SudokuSize; ++cubeIndex)
            {
                var cube = cubeSets[cubeIndex];
                var indices = SudokuCalculator.GetStartingCubeCellFromCubeIndex(cubeIndex);

                var rowLimit = indices.row + SudokuCalculator.SudokuCubeSize;
                var columnLimit = indices.column + SudokuCalculator.SudokuCubeSize;

                for (var rowIndex = indices.row; rowIndex < rowLimit; ++rowIndex)
                {
                    for (var columnIndex = indices.column; columnIndex < columnLimit; ++columnIndex)
                    {
                        if (grid[rowIndex, columnIndex] is { } value)
                        {
                            cube.Add(value);
                        }
                    }
                }
            }
        }


    }
}
