using System;

namespace MeetingTimeFinder
{
    public interface ITimeBlock : IEquatable<ITimeBlock>, IComparable, IComparable<ITimeBlock>
    {
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
