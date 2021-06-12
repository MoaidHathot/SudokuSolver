using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SudokuSolver.Engine.Test
{
    [TestClass]
    public class NumberSetTests
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(10)]
        [DataRow(32)]
        [DataRow(100)]
        public void LengthTest_Positive(int length)
        {
            var set = new NumberSet(length);

            set.Length.Should().Be(length);
        }

        [TestMethod]
        [DataRow(-10)]
        [DataRow(-1)]
        [DataRow(-100)]
        public void LengthTest_Negative_ShouldThrow(int length)
        {
            Action action = () => _ = new NumberSet(length);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(0, -1)]
        [DataRow(2, 0)]
        [DataRow(2, -1)]
        [DataRow(3, 0)]
        [DataRow(3, -1)]
        [DataRow(3, -10)]
        public void NotZeroBasedTest_AddOne_ShouldThrow(int length, int add)
        {
            var set = new NumberSet(length);

            Action action = () => set.Add(add);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(10)]
        public void TestCount_NoAdd_ShouldBeZero(int length)
        {
            var set = new NumberSet(length);

            set.Should().BeEquivalentTo(new
            {
                Length = length,
                Count = 0
            });
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(10)]
        public void TestCount_AddOnce_ShouldBeOne(int length)
        {
            var set = new NumberSet(length);

            set.Add(1);

            set.Should().BeEquivalentTo(new
            {
                Length = length,
                Count = 1
            });

            set.IsExist(1).Should().BeTrue();
            set.IsMissing(1).Should().BeFalse();
            set.GetExistingNumbers().Should().Contain(1);
            set.GetMissingNumbers().Should().NotContain(1);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 1)]
        [DataRow(3, 2)]
        [DataRow(3, 3)]
        [DataRow(10, 1)]
        [DataRow(10, 1)]
        [DataRow(10, 2)]
        [DataRow(10, 3)]
        [DataRow(10, 10)]
        public void TestCount_AddOnceRemoveOnce_ShouldBeZero(int length, int add)
        {
            var set = new NumberSet(length);

            set.Add(add);
            set.Remove(add);
            set.Add(add);

            set.Should().BeEquivalentTo(new
            {
                Length = length,
                Count = 1
            });

            set.IsExist(add).Should().BeTrue();
            set.IsMissing(add).Should().BeFalse();
            set.GetExistingNumbers().Should().Contain(add);
            set.GetMissingNumbers().Should().NotContain(add);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 2)]
        [DataRow(10, 2)]
        public void TestCount_AddTwiceSame(int length, int add)
        {
            var set = new NumberSet(length);

            set.Add(add);
            set.Add(add);

            set.Should().BeEquivalentTo(new
            {
                Length = length,
                Count = 1
            });

            set.IsExist(add).Should().BeTrue();
            set.IsMissing(add).Should().BeFalse();
            set.GetExistingNumbers().Should().Contain(add);
            set.GetMissingNumbers().Should().NotContain(add);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 2)]
        [DataRow(10, 2)]
        public void TestCount_AddOnce_RemoveTwiceSame(int length, int add)
        {
            var set = new NumberSet(length);

            set.Add(add);
            set.Remove(add);
            set.Remove(add);

            set.Should().BeEquivalentTo(new
            {
                Length = length,
                Count = 0
            });

            set.IsExist(add).Should().BeFalse();
            set.IsMissing(add).Should().BeTrue();
            set.GetExistingNumbers().Should().NotContain(add);
            set.GetMissingNumbers().Should().Contain(add);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 2)]
        [DataRow(10, 2)]
        public void TestCount_AddNone_RemoveTwiceSame(int length, int add)
        {
            var set = new NumberSet(length);

            set.Remove(add);
            set.Remove(add);

            set.Should().BeEquivalentTo(new
            {
                Length = length,
                Count = 0
            });

            set.IsExist(add).Should().BeFalse();
            set.IsMissing(add).Should().BeTrue();
            set.GetExistingNumbers().Should().NotContain(add);
            set.GetMissingNumbers().Should().Contain(add);
        }
    }
}
