using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Engine;
using SudokuSolver.Engine.Extensions;

namespace SudokuSolver.Test.Engine
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

            AssertionExtensions.Should((object) set).BeEquivalentTo(new
            {
                Length = length,
                Count = 0
            });

            AssertionExtensions.Should((bool) set.IsEmpty()).BeTrue();
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(10)]
        public void AddOnce_ShouldBeOne(int length)
        {
            var set = new NumberSet(length);

            set.Add(1);

            AssertionExtensions.Should((object) set).BeEquivalentTo(new
            {
                Length = length,
                Count = 1
            });

            AssertionExtensions.Should((bool) set.IsExist(1)).BeTrue();
            AssertionExtensions.Should((bool) set.IsMissing(1)).BeFalse();
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
        public void AddOnceRemoveOnce_ShouldBeZero(int length, int add)
        {
            var set = new NumberSet(length);

            set.Add(add);
            set.Remove(add);
            set.Add(add);

            AssertionExtensions.Should((object) set).BeEquivalentTo(new
            {
                Length = length,
                Count = 1
            });

            AssertionExtensions.Should((bool) set.IsExist(add)).BeTrue();
            AssertionExtensions.Should((bool) set.IsMissing(add)).BeFalse();
            set.GetExistingNumbers().Should().Contain(add);
            set.GetMissingNumbers().Should().NotContain(add);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 2)]
        [DataRow(10, 2)]
        public void AddTwiceSame(int length, int add)
        {
            var set = new NumberSet(length);

            set.Add(add);
            set.Add(add);

            AssertionExtensions.Should((object) set).BeEquivalentTo(new
            {
                Length = length,
                Count = 1
            });

            AssertionExtensions.Should((bool) set.IsExist(add)).BeTrue();
            AssertionExtensions.Should((bool) set.IsMissing(add)).BeFalse();
            set.GetExistingNumbers().Should().Contain(add);
            set.GetMissingNumbers().Should().NotContain(add);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 2)]
        [DataRow(10, 2)]
        public void AddOnce_RemoveTwiceSame(int length, int add)
        {
            var set = new NumberSet(length);

            set.Add(add);
            set.Remove(add);
            set.Remove(add);

            AssertionExtensions.Should((object) set).BeEquivalentTo(new
            {
                Length = length,
                Count = 0
            });

            AssertionExtensions.Should((bool) set.IsExist(add)).BeFalse();
            AssertionExtensions.Should((bool) set.IsMissing(add)).BeTrue();
            set.GetExistingNumbers().Should().NotContain(add);
            set.GetMissingNumbers().Should().Contain(add);
            AssertionExtensions.Should((bool) set.IsEmpty()).BeTrue();
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 2)]
        [DataRow(10, 2)]
        public void AddNone_RemoveTwiceSame(int length, int add)
        {
            var set = new NumberSet(length);

            set.Remove(add);
            set.Remove(add);

            AssertionExtensions.Should((object) set).BeEquivalentTo(new
            {
                Length = length,
                Count = 0
            });

            AssertionExtensions.Should((bool) set.IsExist(add)).BeFalse();
            AssertionExtensions.Should((bool) set.IsMissing(add)).BeTrue();
            set.GetExistingNumbers().Should().NotContain(add);
            set.GetMissingNumbers().Should().Contain(add);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(10)]
        public void TestIfFull(int length)
        {
            var set = new NumberSet(length);

            EnumerableExtensions.ForEach<int>(set.GetMissingNumbers(), i => set.Add(i));

            AssertionExtensions.Should((object) set).BeEquivalentTo(new
            {
                Length = length,
                Count = length,
            });

            Enumerable.Count<int>(set.GetExistingNumbers()).Should().Be(length);
            set.GetMissingNumbers().Should().BeEmpty();
            AssertionExtensions.Should((bool) set.IsFull()).BeTrue();
            AssertionExtensions.Should((bool) set.IsEmpty()).BeFalse();
        }
    }
}
