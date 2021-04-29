using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SudokuSolver.Engine.Test
{
    [TestClass]
    public class SudokuCalculatorTests
    {
        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(0, 1, 0, 0)]
        [DataRow(0, 2, 0, 0)]
        [DataRow(1, 0, 0, 0)]
        [DataRow(1, 1, 0, 0)]
        [DataRow(1, 2, 0, 0)]
        [DataRow(2, 0, 0, 0)]
        [DataRow(2, 1, 0, 0)]
        [DataRow(2, 2, 0, 0)]
        [DataRow(0, 3, 0, 3)]
        [DataRow(0, 4, 0, 3)]
        [DataRow(0, 5, 0, 3)]
        [DataRow(1, 3, 0, 3)]
        [DataRow(1, 4, 0, 3)]
        [DataRow(1, 5, 0, 3)]
        [DataRow(2, 3, 0, 3)]
        [DataRow(2, 4, 0, 3)]
        [DataRow(2, 5, 0, 3)]

        [DataRow(0, 6, 0, 6)]
        [DataRow(0, 7, 0, 6)]
        [DataRow(0, 8, 0, 6)]
        [DataRow(1, 6, 0, 6)]
        [DataRow(1, 7, 0, 6)]
        [DataRow(1, 8, 0, 6)]
        [DataRow(2, 6, 0, 6)]
        [DataRow(2, 7, 0, 6)]
        [DataRow(2, 8, 0, 6)]

        [DataRow(5, 5, 3, 3)]
        [DataRow(5, 2, 3, 0)]

        [DataRow(8, 8, 6, 6)]
        [DataRow(8, 2, 6, 0)]

        public void TestGetStartingCubeCell(int row, int column, int expectedStartingRow, int expectedStartingColumn)
        {
            var result = SudokuCalculator.GetStartingCubeCell(row, column);
            Assert.AreEqual((expectedStartingRow, expectedStartingColumn), result);
        }

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(1, 0, 3)]
        [DataRow(2, 0, 6)]
        [DataRow(3, 3, 0)]
        [DataRow(4, 3, 3)]
        [DataRow(5, 3, 6)]
        [DataRow(6, 6, 0)]
        [DataRow(7, 6, 3)]
        [DataRow(8, 6, 6)]
        public void TestGetStartingCubeCellFromCubeIndex(int cubeIndex, int expectedStartingRow, int expectedStartingColumn)
        {
            var result = SudokuCalculator.GetStartingCubeCellFromCubeIndex(cubeIndex);
            Assert.AreEqual((expectedStartingRow, expectedStartingColumn), result);
        }
    }
}
