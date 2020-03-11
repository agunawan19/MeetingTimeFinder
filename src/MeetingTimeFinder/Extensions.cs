using System.Collections.Generic;

namespace MeetingTimeFinder
{
    public static class Extensions
    {
        public static bool IntersecWith(this ITimeFrame reference, ITimeFrame other) =>
            reference.From < other.To && reference.To > other.From;

        public static int[] GetPivotPoints(int[] arr)
        {
            var pivotPoints = new List<int>();

            if (arr.Length < 3)
            {
                pivotPoints.Add(-1);
                return pivotPoints.ToArray();
            }

            for (int removedNumber = 0; removedNumber < arr.Length; removedNumber++)
            {
                var leftSum = 0;
                var rightSum = 0;

                for (int i = 1; i < arr.Length - removedNumber; i++)
                {
                    rightSum += arr[i];
                }

                for (int i = 0, j = 1; j < arr.Length - removedNumber; i++, j++)
                {
                    rightSum -= arr[j];
                    leftSum += arr[i];

                    if (leftSum == rightSum)
                    {
                        pivotPoints.Add(arr[i + 1]);
                    }
                }
            }

            if (pivotPoints.Count == 0)
            {
                pivotPoints.Add(-1);
            }

            if (pivotPoints.Count > 1)
            {
                pivotPoints.Reverse();
            }

            return pivotPoints.ToArray();
        }
    }
}
