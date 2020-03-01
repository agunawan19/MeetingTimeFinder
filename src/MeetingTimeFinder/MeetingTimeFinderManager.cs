using System.Collections.Generic;
using System.Linq;

namespace MeetingTimeFinder
{
    public class MeetingTimeFinderManager
    {
        public IList<PersonDailyCalendar> PersonDailyCalendars { get; set; } = new List<PersonDailyCalendar>();
        public int MeetingTimeToBeResolvedInMinutes { get; set; } = 30;

        public MeetingTimeFinderManager()
        {

        }

        public MeetingTimeFinderManager(IList<PersonDailyCalendar> personDailyCalendars) =>
            PersonDailyCalendars = personDailyCalendars;

        public IList<ITimeBlock> FindPossibleMeetingTime()
        {
            var possibleMeetingTimes = new List<ITimeBlock>();
            var openTimeBlocks = PersonDailyCalendars
                .Select(personDailyCalendar => GetOpenTimeBlocks(personDailyCalendar).ToList()).ToList();
            var openTimeBlocks1 = openTimeBlocks[0];
            var openTimeBlocks2 = openTimeBlocks[1];

            foreach (var openTimeBlock1 in openTimeBlocks1)
            {
                foreach (var openTimeBlock2 in openTimeBlocks2)
                {
                    if (openTimeBlock1.IntersecWith(openTimeBlock2))
                    {
                        possibleMeetingTimes.Add(GetIntersectedTimeBlock(openTimeBlock1, openTimeBlock2));
                    }
                }
            }

            return possibleMeetingTimes;
        }

        private ITimeBlock GetIntersectedTimeBlock(ITimeBlock reference, ITimeBlock difference) =>
            new TimeBlock
            {
                From = reference.From.CompareTo(difference.From) == -1 ? difference.From : reference.From,
                To = reference.To.CompareTo(difference.To) == -1 ? reference.To : difference.To
            };

        public IEnumerable<ITimeBlock> GetOpenTimeBlocks(PersonDailyCalendar personDailyCalendar)
        {
            var openTimeBlocks = new List<ITimeBlock>();
            var calendarEvents = personDailyCalendar.CalendarEvents.GetEnumerator();
            var dailySchedule = personDailyCalendar.Schedule;
            ITimeBlock currentDailyEvent = null;
            ITimeBlock previousDailyEvent = null;
            ITimeBlock openTimeBlock = null;

            while (calendarEvents.MoveNext())
            {
                currentDailyEvent = calendarEvents.Current;
                var isEventFromStartedAfterScheduleFrom =
                    currentDailyEvent.From.CompareTo(dailySchedule.From) == 1;

                if (isEventFromStartedAfterScheduleFrom)
                {
                    var timeStartingPoint = previousDailyEvent?.To ?? dailySchedule.From;
                    openTimeBlock = new TimeBlock
                    {
                        From = timeStartingPoint,
                        To = timeStartingPoint + currentDailyEvent.From.Subtract(timeStartingPoint)
                    };
                    AddToList(openTimeBlocks, openTimeBlock);
                }

                previousDailyEvent = currentDailyEvent;
            }

            openTimeBlock = new TimeBlock
            {
                From = currentDailyEvent?.To ?? dailySchedule.From,
                To = dailySchedule.To
            };
            AddToList(openTimeBlocks, openTimeBlock);

            return openTimeBlocks;
        }

        private void AddToList(
            IList<ITimeBlock> openTimeBlocks,
            ITimeBlock openTimeBlock)
        {
            var hasEnoughTime = openTimeBlock.From.AddMinutes(MeetingTimeToBeResolvedInMinutes) <= openTimeBlock.To;

            if (hasEnoughTime)
            {
                openTimeBlocks.Add(openTimeBlock);
            }
        }
    }
}
