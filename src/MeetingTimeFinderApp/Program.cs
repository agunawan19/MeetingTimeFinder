using System;
using System.Collections.Generic;
using MeetingTimeFinder;
using System.Linq;

namespace MeetingTimeFinderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a numeric argument.");
                Console.WriteLine("Usage: MeetingTimeFinderApp <num>");
                return;
            }

            var personDailyCalendars = GeneratePersonDailyCalendar();
            var meetingTimeFinderManager = new MeetingTimeFinderManager(personDailyCalendars);
            meetingTimeFinderManager.MeetingTimeToBeResolvedInMinutes = int.Parse(args[0]);
            var result = meetingTimeFinderManager.FindPossibleMeetingTime();

            foreach (var t in result)
            {
                Console.WriteLine($"{t.From.ToString("HH:mm")} - {t.To.ToString("HH:mm")}");
            }

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
                        new TimeBlock("09:15", "10:30"),
                        new TimeBlock("12:00", "13:00"),
                        new TimeBlock("16:00", "18:00"),
                    }
                },
                new PersonDailyCalendar(
                    "Person B",
                    new DateTime(2020, 3, 1, 10, 0, 0),
                    new DateTime(2020, 3, 1, 18, 30, 0))
                {
                    CalendarEvents = new List<ITimeBlock>
                    {
                        new TimeBlock("10:00", "11:30"),
                        new TimeBlock("12:30", "14:30"),
                        new TimeBlock("14:30", "15:00"),
                        new TimeBlock("16:00", "17:00"),
                    }
                }
            };

            return persons;
        }
    }
}
