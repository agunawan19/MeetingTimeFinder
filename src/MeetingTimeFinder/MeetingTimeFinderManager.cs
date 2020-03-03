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

        public IEnumerable<ITimeBlock> FindPossibleMeetingTime()
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
                        var intersectedTimeBlock = GetIntersectedTimeBlock(openTimeBlock1, openTimeBlock2);

                        if (HasEnoughTime(intersectedTimeBlock))
                        {
                            possibleMeetingTimes.Add(intersectedTimeBlock);
                        }
                    }
                }
            }

            return possibleMeetingTimes;
        }

        private ITimeBlock GetIntersectedTimeBlock(ITimeBlock reference, ITimeBlock other) =>
            new TimeBlock
            {
                From = reference.From.CompareTo(other.From) == -1 ? other.From : reference.From,
                To = reference.To.CompareTo(other.To) == -1 ? reference.To : other.To
            };

        public IEnumerable<ITimeBlock> GetOpenTimeBlocks(PersonDailyCalendar personDailyCalendar)
        {
            var openTimeBlocks = new List<ITimeBlock>();
            var dailySchedule = personDailyCalendar.Schedule;
            ITimeBlock currentEvent = null;
            ITimeBlock previousEvent = null;
            ITimeBlock openTimeBlock = null;

            foreach (var calendarEvents in personDailyCalendar.CalendarEvents)
            {
                currentEvent = calendarEvents;

                var isEventFromStartedAfterScheduleFrom =
                    currentEvent.From.CompareTo(dailySchedule.From) == 1;

                if (isEventFromStartedAfterScheduleFrom)
                {
                    var timeStartingPoint = previousEvent?.To ?? dailySchedule.From;
                    openTimeBlock = new TimeBlock
                    {
                        From = timeStartingPoint,
                        To = timeStartingPoint + currentEvent.From.Subtract(timeStartingPoint)
                    };
                    AddToList(openTimeBlocks, openTimeBlock);
                }

                previousEvent = currentEvent;
            }

            openTimeBlock = new TimeBlock
            {
                From = currentEvent?.To ?? dailySchedule.From,
                To = dailySchedule.To
            };
            AddToList(openTimeBlocks, openTimeBlock);

            return openTimeBlocks;
        }

        private void AddToList(
            IList<ITimeBlock> openTimeBlocks,
            ITimeBlock openTimeBlock)
        {
            if (HasEnoughTime(openTimeBlock))
            {
                openTimeBlocks.Add(openTimeBlock);
            }
        }

        private bool HasEnoughTime(ITimeBlock openTimeBlock) =>
            openTimeBlock.From.AddMinutes(MeetingTimeToBeResolvedInMinutes) <= openTimeBlock.To;
    }
}
