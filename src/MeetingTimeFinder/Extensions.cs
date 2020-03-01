namespace MeetingTimeFinder
{
    public static class Extensions
    {
        public static bool IntersecWith(this ITimeBlock reference, ITimeBlock difference) =>
            reference.From <= difference.To && reference.To >= difference.From;
    }
}
