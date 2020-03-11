using System.Collections.Generic;
using Xunit;
using static MeetingTimeFinder.Extensions;

namespace MeetingTimeFinder.Tests
{
    public class PivotPointTests
    {
        [Theory]
        [InlineData(new[] { 1, 1 }, new[] { -1 })]
        [InlineData(new[] { 1, 3, 1 }, new[] { 3 })]
        [InlineData(new[] { 1, 3, 2 }, new[] { -1 })]
        [InlineData(new[] { 2, 3, 1, 1 }, new[] { 3 })]
        [InlineData(new[] { 2, 3, 4, 1, 4, 5 }, new[] { 4, 1 })]
        [InlineData(new[] { 1, 3, 1, 9, 2, 1, 2 }, new[] { 3, 9, 1 })]
        public void GetPivotPoints_Returns_Correct_Result(int[] integers, int[] expected)
        {
            var actual = GetPivotPoints(integers);

            Assert.Equal(expected, actual);
        }
    }
}
