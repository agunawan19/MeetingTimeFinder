using System;

namespace MeetingTimeFinder
{
    public class TimeBlock : ITimeBlock
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public TimeBlock()
        {

        }

        public TimeBlock(string from, string to)
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

        public TimeBlock(DateTime from, DateTime to)
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

            TimeBlock timeBlock = obj as TimeBlock;
            if (timeBlock == null)
            {
                return false;
            }
            else
            {
                return Equals(timeBlock);
            }
        }

        public bool Equals(ITimeBlock other)
        {
            if (other == null)
            {
                return false;
            }

            return From == other.From && To == other.To;
        }

        public static bool operator ==(TimeBlock operand1, TimeBlock operand2)
        {
            if (operand1 as object == null || operand2 as object == null)
            {
                return Object.Equals(operand1, operand2);
            }

            return operand1.Equals(operand2);
        }

        public static bool operator !=(TimeBlock operand1, TimeBlock operand2)
        {
            if (operand1 as object == null || operand2 as object == null)
            {
                return !Object.Equals(operand1, operand2);
            }

            return !operand1.Equals(operand2);
        }

        public static bool operator >=(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (timeBlock1 as object == null || timeBlock2 as object == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.From.TimeOfDay >= timeBlock2.To.TimeOfDay;
        }

        public static bool operator <=(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (timeBlock1 as object == null || timeBlock2 as object == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.To.TimeOfDay <= timeBlock2.From.TimeOfDay;
        }

        public static bool operator >(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (timeBlock1 as object == null || timeBlock2 as object == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.From.TimeOfDay > timeBlock2.To.TimeOfDay;
        }

        public static bool operator <(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (timeBlock1 as object == null || timeBlock2 as object == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.To.TimeOfDay < timeBlock2.From.TimeOfDay;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            TimeBlock other = obj as TimeBlock;

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
                throw new ArgumentException("Object is not a TimeBlock");
            }
        }

        public int CompareTo(ITimeBlock other)
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
