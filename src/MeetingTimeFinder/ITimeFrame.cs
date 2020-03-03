using System;

namespace MeetingTimeFinder
{
    public interface ITimeFrame : IEquatable<ITimeFrame>, IComparable, IComparable<ITimeFrame>
    {
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
