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

            TimeBlock timeBlockObj = obj as TimeBlock;
            if (timeBlockObj == null)
            {
                return false;
            }
            else
            {
                return Equals(timeBlockObj);
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

        public static bool operator ==(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (((object)timeBlock1) == null || ((object)timeBlock2) == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.Equals(timeBlock2);
        }

        public static bool operator !=(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (((object)timeBlock1) == null || ((object)timeBlock2) == null)
            {
                return !Object.Equals(timeBlock1, timeBlock2);
            }

            return !(timeBlock1.Equals(timeBlock2));
        }

        public static bool operator >=(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (((object)timeBlock1) == null || ((object)timeBlock2) == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.From.TimeOfDay >= timeBlock2.To.TimeOfDay;
        }

        public static bool operator <=(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (((object)timeBlock1) == null || ((object)timeBlock2) == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.To.TimeOfDay <= timeBlock2.From.TimeOfDay;
        }

        public static bool operator >(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (((object)timeBlock1) == null || ((object)timeBlock2) == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.From.TimeOfDay > timeBlock2.To.TimeOfDay;
        }

        public static bool operator <(TimeBlock timeBlock1, TimeBlock timeBlock2)
        {
            if (((object)timeBlock1) == null || ((object)timeBlock2) == null)
            {
                return Object.Equals(timeBlock1, timeBlock2);
            }

            return timeBlock1.To.TimeOfDay < timeBlock2.From.TimeOfDay;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ITimeBlock other)
        {
            throw new NotImplementedException();
        }
    }
}
