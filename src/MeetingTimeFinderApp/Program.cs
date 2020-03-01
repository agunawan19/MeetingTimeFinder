using System;
using System.Collections.Generic;
using MeetingTimeFinder;

namespace MeetingTimeFinderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var personDailyCalendars = GeneratePersonDailyCalendar();
            var meetingTimeFinderManager = new MeetingTimeFinderManager(personDailyCalendars);
            meetingTimeFinderManager.FindPossibleMeetingTime();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static List<PersonDailyCalendar> GeneratePersonDailyCalendar()
        {
            var persons = new List<PersonDailyCalendar>
            {
                new PersonDailyCalendar(
                    "Person A",
                    new DateTime(2020, 3, 1, 9, 0, 0),
                    new DateTime(2020, 3, 1, 20, 0, 0))
                {
                    CalendarEvents = new List<ITimeBlock>
                    {
                        new TimeBlock(
                            new DateTime(2020, 3, 1, 9, 15, 0),
                            new DateTime(2020, 3, 1, 10, 30, 0)),
                        new TimeBlock(
                            new DateTime(2020, 3, 1, 12, 0, 0),
                            new DateTime(2020, 3, 1, 13, 0, 0)),
                        new TimeBlock(
                            new DateTime(2020, 3, 1, 16, 0, 0),
                            new DateTime(2020, 3, 1, 18, 0, 0)),
                    }
                },
                new PersonDailyCalendar(
                    "Person B",
                    new DateTime(2020, 3, 1, 10, 0, 0),
                    new DateTime(2020, 3, 1, 18, 30, 0))
                {
                    CalendarEvents = new List<ITimeBlock>
                    {
                        new TimeBlock(
                            new DateTime(2020, 3, 1, 10, 0, 0),
                            new DateTime(2020, 3, 1, 11, 30, 0)),
                        new TimeBlock(
                            new DateTime(2020, 3, 1, 12, 30, 0),
                            new DateTime(2020, 3, 1, 14, 30, 0)),
                        new TimeBlock(
                            new DateTime(2020, 3, 1, 14, 30, 0),
                            new DateTime(2020, 3, 1, 15, 0, 0)),
                        new TimeBlock(
                            new DateTime(2020, 3, 1, 16, 0, 0),
                            new DateTime(2020, 3, 1, 17, 0, 0)),
                    }
                }
            };

            return persons;
        }
    }
}
