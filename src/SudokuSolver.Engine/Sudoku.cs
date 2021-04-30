using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using SudokuSolver.Engine.Extensions;

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

        private readonly HashSet<(int row, int column)> _emptyCells = new();

        public IReadOnlySet<(int row, int column)> EmptyCells => _emptyCells;

        public int FilledNumbersCount { get; private set; }

        public int?[,] Grid { get; }

        public Sudoku(int?[,] grid)
        {
            Grid = grid;
            InitializeSets(grid);
            FilledNumbersCount = CountFilledNumbers();
            
            FillEmptyCells(_emptyCells);
        }

        public (int row, int column)? GetEmptyCell()
            => _emptyCells.FirstOrDefault();

        public bool IsComplete()
            => FilledNumbersCount == SudokuCalculator.MaxItems;

        public bool ExistInRow(int row, int number)
            => _rowSets[row].IsExist(number);

        public bool ExistInColumn(int column, int number)
            => _columnSets[column].IsExist(number);

        public bool ExistInCube(int cube, int number)
            => _cubeSets[cube].IsExist(number);

        public IEnumerable<int> GetExistingInRow(int row)
            => _rowSets[row].GetExistingNumbers();

        public IEnumerable<int> GetExistingInColumn(int column)
            => _columnSets[column].GetExistingNumbers();

        public IEnumerable<int> GetExistingInCube(int cube)
            => _cubeSets[cube].GetExistingNumbers();

        public IEnumerable<int> GetMissingInRow(int row)
            => _rowSets[row].GetMissingNumbers();

        public IEnumerable<int> GetMissingInColumn(int column)
            => _columnSets[column].GetMissingNumbers();

        public IEnumerable<int> GetMissingInCube(int cube)
            => _cubeSets[cube].GetMissingNumbers();

        public int? this[int row, int column]
        {
            get => Grid[row, column];
            set => HandleSet(row, column, value);
        }

        private void HandleSet(int row, int column, int? value)
        {
            var (rowSet, columnSet, cubeSet) = (_rowSets[row], _columnSets[column], _cubeSets[SudokuCalculator.GetCubeFromCell(row, column)]);

            if (value is int number)
            {
                rowSet.Add(number);
                columnSet.Add(number);
                cubeSet.Add(number);

                Grid[row, column] = value;
                _emptyCells.Remove((row, column));

                ++FilledNumbersCount;
            }
            else
            {
                if (Grid[row, column] is int toRemove)
                {
                    rowSet.Remove(toRemove);
                    columnSet.Remove(toRemove);
                    cubeSet.Remove(toRemove);

                    Grid[row, column] = value;
                    _emptyCells.Add((row, column));

                    --FilledNumbersCount;
                }
            }
        }

        private void InitializeSets(int?[,] grid)
        {
            InitializeRowSets(grid, _rowSets);
            InitializeColumnSets(grid, _columnSets);
            InitializeCubeSets(grid, _cubeSets);
        }

        private void FillEmptyCells(HashSet<(int row, int column)> set)
        {
            for (var row = 0; row < RowCount; ++row)
            {
                for (var column = 0; column < ColumnCount; ++column)
                {
                    if (Grid[row, column] is null)
                    {
                        set.Add((row, column));
                    }
                }
            }
        }

        private int CountFilledNumbers()
            => _rowSets.Select(row => row.Count).Sum();

        private void InitializeRowSets(int?[,] grid, NumberSet[] rowSets)
        {
            FillSet(rowSets, SudokuCalculator.SudokuSize);

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

        private void InitializeColumnSets(int?[,] grid, NumberSet[] columnSets)
        {
            FillSet(columnSets, SudokuCalculator.SudokuSize);

            for (var columnIndex = 0; columnIndex < columnSets.Length; ++columnIndex)
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

        private void InitializeCubeSets(int?[,] grid, NumberSet[] cubeSets)
        {
            FillSet(cubeSets, SudokuCalculator.SudokuSize);

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

        private void FillSet(NumberSet[] set, int itemSize)
            => Enumerable.Range(0, set.Length).ForEach(index => set[index] = new NumberSet(itemSize));
    }
}
