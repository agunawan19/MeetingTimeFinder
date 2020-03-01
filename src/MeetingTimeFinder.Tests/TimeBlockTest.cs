using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace MeetingTimeFinder.Tests
{
    public class TimeBlockTest
    {
        [Fact]
        public void Intantiate_Class_With_Incorrect_Time_String_Throws_Exception()
        {
            var from = "0900";
            var to = "2000";

            Exception ex = Assert.Throws<FormatException>(() => new TimeBlock(from, to));
            var expected = "String '0900' was not recognized as a valid DateTime.";
            var actual = ex.Message;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Intantiate_Class_With_Correct_Time_String_Returns_Correct_Time()
        {
            var from = "09:00";
            var to = "20:00";
            var timeBlock = new TimeBlock(from, to);

            var expected = new TimeSpan(9, 0, 0);
            var actual = timeBlock.From.TimeOfDay;
            Assert.Equal(expected, actual);

            expected = new TimeSpan(20, 0, 0);
            Assert.Equal(expected, timeBlock.To.TimeOfDay);
        }

        [Theory]
        [MemberData(nameof(EqualTimeBlockData))]
        public void Equal_Operator_Returns_Correct_Boolean(TimeBlock firstTimeBlock, TimeBlock secondTimeBlock, bool expected)
        {
            var actual = firstTimeBlock == secondTimeBlock;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(EqualTimeBlockData))]
        public void Equal_Method_Returns_Correct_Boolean(TimeBlock firstTimeBlock, TimeBlock secondTimeBlock, bool expected)
        {
            var actual = firstTimeBlock.Equals(secondTimeBlock);

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> EqualTimeBlockData =>
            new List<object[]>
            {
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("08:00", "17:00"),
                    true,
                },
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("09:00", "18:00"),
                    false,
                },
            };

        [Theory]
        [MemberData(nameof(NotEqualTimeBlockData))]
        public void Not_Equal_Operator_Returns_Correct_Boolean(TimeBlock firstTimeBlock, TimeBlock secondTimeBlock, bool expected)
        {
            var actual = firstTimeBlock != secondTimeBlock;

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> NotEqualTimeBlockData =>
            new List<object[]>
            {
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("08:00", "17:00"),
                    false,
                },
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("09:00", "18:00"),
                    true,
                },
            };

        [Theory]
        [MemberData(nameof(GreaterOrEqualThanTimeBlockData), parameters: new object[] { 0, 5 })]
        public void GreaterOrEqualThan_Operator_Returns_Correct_Boolean(TimeBlock firstTimeBlock, TimeBlock secondTimeBlock, bool expected)
        {
            var actual = firstTimeBlock >= secondTimeBlock;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GreaterOrEqualThanTimeBlockData), parameters: new object[] { 1, 4 })]
        public void GreaterThan_Operator_Returns_Correct_Boolean(TimeBlock firstTimeBlock, TimeBlock secondTimeBlock, bool expected)
        {
            var actual = firstTimeBlock > secondTimeBlock;

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GreaterOrEqualThanTimeBlockData(
            int numSkips, int numTests)
        {
            var allData = new List<object[]>
            {
                new object[]
                {
                    new TimeBlock("17:00", "19:00"),
                    new TimeBlock("08:00", "17:00"),
                    true,
                },
                new object[]
                {
                    new TimeBlock("18:00", "19:00"),
                    new TimeBlock("08:00", "17:00"),
                    true,
                },
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("08:00", "17:00"),
                    false,
                },
                new object[]
                {
                    new TimeBlock("15:00", "17:00"),
                    new TimeBlock("08:00", "17:00"),
                    false,
                },
                new object[]
                {
                    new TimeBlock("07:00", "18:00"),
                    new TimeBlock("08:00", "17:00"),
                    false,
                }
            };

            return allData.Skip(numSkips).Take(numTests);
        }

        [Theory]
        [MemberData(nameof(LessOrEqualThanTimeBlockData), parameters: new object[] { 0, 5 })]
        public void LessOrEqualThan_Operator_Returns_Correct_Boolean(TimeBlock firstTimeBlock, TimeBlock secondTimeBlock, bool expected)
        {
            var actual = firstTimeBlock <= secondTimeBlock;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(LessOrEqualThanTimeBlockData), parameters: new object[] { 1, 4 })]
        public void LessThan_Operator_Returns_Correct_Boolean(TimeBlock firstTimeBlock, TimeBlock secondTimeBlock, bool expected)
        {
            var actual = firstTimeBlock < secondTimeBlock;

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> LessOrEqualThanTimeBlockData(
            int numSkips, int numTests)
        {
            var allData = new List<object[]>
            {
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("17:00", "19:00"),
                    true,
                },
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("18:00", "19:00"),
                    true,
                },
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("08:00", "17:00"),
                    false,
                },
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("15:00", "17:00"),
                    false,
                },
                new object[]
                {
                    new TimeBlock("05:00", "17:00"),
                    new TimeBlock("07:00", "18:00"),
                    false,
                }
            };

            return allData.Skip(numSkips).Take(numTests);
        }
    }
}
