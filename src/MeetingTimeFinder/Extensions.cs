namespace MeetingTimeFinder
{
    public static class Extensions
    {
        public static bool IntersecWith(this ITimeBlock reference, ITimeBlock other) =>
            reference.From < other.To && reference.To > other.From;
    }
}
