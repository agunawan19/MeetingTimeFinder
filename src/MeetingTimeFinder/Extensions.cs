namespace MeetingTimeFinder
{
    public static class Extensions
    {
        public static bool IntersecWith(this ITimeFrame reference, ITimeFrame other) =>
            reference.From < other.To && reference.To > other.From;
    }
}
