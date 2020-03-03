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

        public TimeFrame(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public override int GetHashCode()
        {
            return new { From, To }.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            TimeFrame timeFrame = obj as TimeFrame;
            if (timeFrame == null)
            {
                return false;
            }
            else
            {
                return Equals(timeFrame);
            }
        }

        public bool Equals(ITimeFrame other)
        {
            if (other == null)
            {
                return false;
            }

            return From == other.From && To == other.To;
        }

        public static bool operator ==(TimeFrame operand1, TimeFrame operand2)
        {
            if (operand1 as object == null || operand2 as object == null)
            {
                return Object.Equals(operand1, operand2);
            }

            return operand1.Equals(operand2);
        }

        public static bool operator !=(TimeFrame operand1, TimeFrame operand2)
        {
            if (operand1 as object == null || operand2 as object == null)
            {
                return !Object.Equals(operand1, operand2);
            }

            return !operand1.Equals(operand2);
        }

        public static bool operator >=(TimeFrame timeFrame1, TimeFrame timeFrame2)
        {
            if (timeFrame1 as object == null || timeFrame2 as object == null)
            {
                return Object.Equals(timeFrame1, timeFrame2);
            }

            return timeFrame1.From.TimeOfDay >= timeFrame2.To.TimeOfDay;
        }

        public static bool operator <=(TimeFrame timeFrame1, TimeFrame timeFrame2)
        {
            if (timeFrame1 as object == null || timeFrame2 as object == null)
            {
                return Object.Equals(timeFrame1, timeFrame2);
            }

            return timeFrame1.To.TimeOfDay <= timeFrame2.From.TimeOfDay;
        }

        public static bool operator >(TimeFrame timeFrame1, TimeFrame timeFrame2)
        {
            if (timeFrame1 as object == null || timeFrame2 as object == null)
            {
                return Object.Equals(timeFrame1, timeFrame2);
            }

            return timeFrame1.From.TimeOfDay > timeFrame2.To.TimeOfDay;
        }

        public static bool operator <(TimeFrame timeFrame1, TimeFrame timeFrame2)
        {
            if (timeFrame1 as object == null || timeFrame2 as object == null)
            {
                return Object.Equals(timeFrame1, timeFrame2);
            }

            return timeFrame1.To.TimeOfDay < timeFrame2.From.TimeOfDay;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            TimeFrame other = obj as TimeFrame;

            if (other != null)
            {
                if (From != other.From || To != other.To)
                {
                    if (From <= other.From)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }

                return 0;
            }
            else
            {
                throw new ArgumentException("Object is not a timeFrame");
            }
        }

        public int CompareTo(ITimeFrame other)
        {
            if (other == null) return 1;

            if (From != other.From || To != other.To)
            {
                if (From <= other.From)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}
