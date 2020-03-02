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
        public void Equal_Operator_Returns_Correct_Boolean(TimeBlock timeBlock1, TimeBlock timeBlock2, bool expected)
        {
            var actual = timeBlock1 == timeBlock2;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(EqualTimeBlockData))]
        public void Equal_Method_Returns_Correct_Boolean(TimeBlock timeBlock1, TimeBlock timeBlock2, bool expected)
        {
            var actual = timeBlock1.Equals(timeBlock2);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(EqualTimeBlockData))]
        public void Equal_Object_Method_Returns_Correct_Boolean(object timeBlock1, object timeBlock2, bool expected)
        {
            var actual = timeBlock1.Equals(timeBlock2);

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
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    new TimeBlock("17:00", "18:00"),
                    false,
                },
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    null,
                    false,
                },
            };

        [Theory]
        [MemberData(nameof(NotEqualTimeBlockData))]
        public void Not_Equal_Operator_Returns_Correct_Boolean(TimeBlock timeBlock1, TimeBlock timeBlock2, bool expected)
        {
            var actual = timeBlock1 != timeBlock2;

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
                new object[]
                {
                    new TimeBlock("08:00", "17:00"),
                    null,
                    true,
                },
            };

        [Theory]
        [MemberData(nameof(GreaterOrEqualThanTimeBlockData), parameters: new object[] { 0, 7 })]
        public void GreaterOrEqualThan_Operator_Returns_Correct_Boolean(TimeBlock timeBlock1, TimeBlock timeBlock2, bool expected)
        {
            var actual = timeBlock1 >= timeBlock2;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GreaterOrEqualThanTimeBlockData), parameters: new object[] { 1, 6 })]
        public void GreaterThan_Operator_Returns_Correct_Boolean(TimeBlock timeBlock1, TimeBlock timeBlock2, bool expected)
        {
            var actual = timeBlock1 > timeBlock2;

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
                },
                new object[]
                {
                    new TimeBlock("07:00", "18:00"),
                    null,
                    false,
                },
                new object[]
                {
                    null,
                    new TimeBlock("07:00", "18:00"),
                    false,
                },
            };

            return allData.Skip(numSkips).Take(numTests);
        }

        [Theory]
        [MemberData(nameof(LessOrEqualThanTimeBlockData), parameters: new object[] { 0, 5 })]
        public void LessOrEqualThan_Operator_Returns_Correct_Boolean(TimeBlock timeBlock1, TimeBlock timeBlock2, bool expected)
        {
            var actual = timeBlock1 <= timeBlock2;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(LessOrEqualThanTimeBlockData), parameters: new object[] { 1, 4 })]
        public void LessThan_Operator_Returns_Correct_Boolean(TimeBlock timeBlock1, TimeBlock timeBlock2, bool expected)
        {
            var actual = timeBlock1 < timeBlock2;

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> LessOrEqualThanTimeBlockData(
            int numSkips,
            int numTests)
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

        [Theory]
        [InlineData(new[] { "01:00", "09:00" }, new[] { "09:00", "20:00" }, -1)]
        [InlineData(new[] { "02:00", "22:00" }, new[] { "09:00", "20:00" }, -1)]
        [InlineData(new[] { "01:00", "09:00" }, new[] { "02:00", "22:00" }, -1)]
        [InlineData(new[] { "03:00", "20:00" }, new[] { "09:00", "20:00" }, -1)]
        [InlineData(new[] { "09:00", "10:00" }, new[] { "09:00", "20:00" }, -1)]
        [InlineData(new[] { "09:00", "20:00" }, new[] { "09:00", "20:00" }, 0)]
        [InlineData(new[] { "10:00", "20:00" }, new[] { "09:00", "20:00" }, 1)]
        [InlineData(new[] { "10:00", "21:00" }, new[] { "09:00", "20:00" }, 1)]
        public void CompareTo_Returns_Correct_Result(IList<string> fromTo1, IList<string> fromTo2, int expected)
        {
            var timeBlock1 = new TimeBlock(fromTo1[0], fromTo1[1]);
            var timeBlock2 = new TimeBlock(fromTo2[0], fromTo2[1]);

            Assert.Equal(expected, timeBlock1.CompareTo(timeBlock2));
        }

        [Theory]
        [MemberData(nameof(CompareToListSortBlockData))]
        public void CompareTo_List_Sort_Test_Returns_Correct_Result(
            List<TimeBlock> actual,
            List<TimeBlock> expected)
        {
            actual.Sort();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> CompareToListSortBlockData =>
            new List<object[]>
            {
                new object[] {
                    new List<TimeBlock>
                    {
                        new TimeBlock("21:00", "23:00"),
                        new TimeBlock("20:00", "23:00"),
                        new TimeBlock("09:00", "20:00"),
                        new TimeBlock("02:00", "22:00"),
                        new TimeBlock("01:00", "09:00"),
                        new TimeBlock("03:00", "20:00"),
                        new TimeBlock("10:00", "21:00"),
                        new TimeBlock("10:00", "20:00"),
                        new TimeBlock("09:00", "10:00"),
                        new TimeBlock("09:00", "20:00"),
                    },
                    new List<TimeBlock>
                    {
                        new TimeBlock("01:00", "09:00"),
                        new TimeBlock("02:00", "22:00"),
                        new TimeBlock("03:00", "20:00"),
                        new TimeBlock("09:00", "10:00"),
                        new TimeBlock("09:00", "20:00"),
                        new TimeBlock("09:00", "20:00"),
                        new TimeBlock("10:00", "20:00"),
                        new TimeBlock("10:00", "21:00"),
                        new TimeBlock("20:00", "23:00"),
                        new TimeBlock("21:00", "23:00"),
                    },
                },
            };

        [Theory]
        [InlineData(new[] { "06:00", "07:00" }, new[] { "08:00", "09:00" }, false )]
        [InlineData(new[] { "07:00", "08:00" }, new[] { "08:00", "09:00" }, false )]
        [InlineData(new[] { "07:00", "09:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "07:00", "20:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "07:00", "21:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "08:00", "09:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "08:00", "20:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "08:00", "21:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "09:00", "10:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "09:00", "20:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "09:00", "21:00" }, new[] { "08:00", "20:00" }, true )]
        [InlineData(new[] { "20:00", "21:00" }, new[] { "08:00", "20:00" }, false )]
        [InlineData(new[] { "21:00", "22:00" }, new[] { "08:00", "20:00" }, false )]
        public void IntersectWith_Returns_Correct_Result(
            IList<string> fromToPair1,
            IList<string> fromToPair2,
            bool expected)
        {
            const byte from = 0;
            const byte to = 1;
            var actual = new TimeBlock(fromToPair1[from], fromToPair1[to])
                .IntersecWith(new TimeBlock(fromToPair2[from], fromToPair2[to]));

            Assert.Equal(expected, actual);
        }
    }
}
