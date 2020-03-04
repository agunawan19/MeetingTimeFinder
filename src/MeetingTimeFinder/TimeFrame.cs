using System;

namespace MeetingTimeFinder
{
    public class TimeFrame : ITimeFrame
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public TimeFrame()
        {
        }

        public TimeFrame(string from, string to)
        {
            const string timeFormat = "HH:mm";

            try
            {
                From = DateTime.ParseExact(from, timeFormat, null);
                To = DateTime.ParseExact(to, timeFormat, null);
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TimeFrame(DateTime from, DateTime to) => (From, To) = (from, to);

        public override int GetHashCode() => new { From, To }.GetHashCode();

        public override bool Equals(object obj) =>
            obj is TimeFrame otherTimeFrame && this == otherTimeFrame;

        public bool Equals(ITimeFrame other) =>
            other is ITimeFrame otherTimeFrame &&
            (From, To) == (otherTimeFrame.From, otherTimeFrame.To);

        public static bool operator ==(TimeFrame operand1, TimeFrame operand2) =>
            operand1 is TimeFrame timeFrame1 && operand2 is TimeFrame timeFrame2 ?
            timeFrame1.Equals(timeFrame2) :
            Equals(operand1, operand2);

        public static bool operator !=(TimeFrame operand1, TimeFrame operand2) =>
            operand1 is TimeFrame timeFrame1 && operand2 is TimeFrame timeFrame2 ?
            !timeFrame1.Equals(operand2) :
            !Equals(operand1, operand2);

        public static bool operator >=(TimeFrame operand1, TimeFrame operand2) =>
            operand1 is TimeFrame timeFrame1 && operand2 is TimeFrame timeFrame2 ?
            timeFrame1.From.TimeOfDay >= timeFrame2.To.TimeOfDay :
            Equals(operand1, operand2);

        public static bool operator <=(TimeFrame operand1, TimeFrame operand2) =>
            operand1 is TimeFrame timeFrame1 && operand2 is TimeFrame timeFrame2 ?
            timeFrame1.To.TimeOfDay <= timeFrame2.From.TimeOfDay :
            Equals(operand1, operand2);

        public static bool operator >(TimeFrame operand1, TimeFrame operand2) =>
            operand1 is TimeFrame timeFrame1 && operand2 is TimeFrame timeFrame2 ?
            timeFrame1.From.TimeOfDay > timeFrame2.To.TimeOfDay :
            Equals(operand1, operand2);

        public static bool operator <(TimeFrame operand1, TimeFrame operand2) =>
            operand1 is TimeFrame timeFrame1 && operand2 is TimeFrame timeFrame2 ?
            timeFrame1.To.TimeOfDay < timeFrame2.From.TimeOfDay :
            Equals(operand1, operand2);

        public int CompareTo(object obj)
        {
            if (obj is null) return 1;

            if (!(obj is TimeFrame))
            {
                throw new ArgumentException(
                    paramName: nameof(obj),
                    message: $"Object must be of type {typeof(TimeFrame).Name}.");
            }

            if (!Equals(obj))
            {
                return From <= ((ITimeFrame)obj).From ? -1 : 1;
            }

            return 0;
        }

        public int CompareTo(ITimeFrame other)
        {
            if (other is null) return 1;

            if (!Equals(other))
            {
                return From <= other.From ? -1 : 1;
            }

            return 0;
        }
    }
}
