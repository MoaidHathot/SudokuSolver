using System;
using SudokuSolver.Engine;

namespace SudokuSolver.Console
{
    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
			TestSandwichSudoku();
        }

		static void TestSandwichSudoku()
		{
			var puzzles = new[]
			{
				GetSandwitchSudoku01(),
			};

			var (grid, rowSum, cellSum) = puzzles[0];

			var solver = new SudokuSolver.Engine.SudokuSolver();

			var sudoku = new SandwichSudoku(grid, rowSum, cellSum);
			var result = solver.Solve(sudoku);

			Console.WriteLine($"Valid: {solver.Validate(sudoku)}");
		}

		private static (int?[,] grid, int[] rowSum, int[] columnSum) GetSandwitchSudoku01()
		{
			var grid = new int?[9, 9]
			{
		{ null, null, null, null, null, null, null, null, null, },
		{ null, null, null, null, null, 2, null, null, null, },
		{ null, null, null, null, null, null, null, 6, null, },
		{ 9, null, 7, null, null, null, 5, null, null, },
		{ null, null, null, null, null, null, null, null, null, },
		{ null, null, 6, null, null, null, 2, null, 7, },
		{ null, 1, null, null, null, null, null, null, null, },
		{ null, null, null, 5, null, null, null, null, null, },
		{ null, null, null, null, null, null, null, null, null, },
			};

			var cellSum = new[] { 5, 11, 14, 15, 8, 35, 0, 0, 0 };
			var rowSum = new[] { 0, 26, 4, 32, 24, 0, 29, 18, 8 };

			return (grid, rowSum, cellSum);
		}
	}
}
