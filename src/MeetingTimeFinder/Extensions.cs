using System.Collections.Generic;

namespace MeetingTimeFinder
{
    public static class Extensions
    {
        public static bool IntersectWith(this ITimeFrame reference, ITimeFrame other) =>
            reference.From < other.To && reference.To > other.From;

        public static int[] GetPivotPoints(int[] arr)
        {
            var pivotIndexes = new List<int>();

            if (arr.Length == 1)
            {
                pivotIndexes.Add(0);
                return pivotIndexes.ToArray();
            }

            if (arr.Length < 3)
            {
                pivotIndexes.Add(-1);
                return pivotIndexes.ToArray();
            }

            var leftSum = 0;
            var rightSum = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                rightSum += arr[i];
            }

            for (int i = 0, j = 1; j < arr.Length; i++, j++)
            {
                rightSum -= arr[j];
                leftSum += arr[i];

                if (leftSum == rightSum)
                {
                    pivotIndexes.Add(j);
                }
            }

            if (pivotIndexes.Count == 0)
            {
                pivotIndexes.Add(-1);
            }

            return pivotIndexes.ToArray();
        }
    }
}
