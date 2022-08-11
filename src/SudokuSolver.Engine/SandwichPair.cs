namespace SudokuSolver.Engine
{
    public partial class SandwichSudoku
    {
        public class SandwichPair
        {
            public int? IndexOfOne { get; set; }
            public int? IndexOfNine { get; set; }

            public void Deconstruct(out int? one, out int? nine)
            {
                one = IndexOfOne;
                nine = IndexOfNine;
            }
        }
    }
}
