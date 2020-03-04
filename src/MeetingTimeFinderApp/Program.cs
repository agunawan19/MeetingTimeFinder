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
            string date1 = "2001-01-01";
            object date2 = "";
            var dateCompareResult = date1.Equals(date2);

            ITimeFrame test1 = new TimeFrame("08:00", "10:00");
            object test2 = "";
            //object test2 = new string[] { "A", "B" };
            var testResult = test1.Equals(test2);
          

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
                    CalendarEvents = new List<ITimeFrame>
                    {
                        new TimeFrame("09:15", "10:30"),
                        new TimeFrame("12:00", "13:00"),
                        new TimeFrame("16:00", "18:00"),
                    }
                },
                new PersonDailyCalendar(
                    "Person B",
                    new DateTime(2020, 3, 1, 10, 0, 0),
                    new DateTime(2020, 3, 1, 18, 30, 0))
                {
                    CalendarEvents = new List<ITimeFrame>
                    {
                        new TimeFrame("10:00", "11:30"),
                        new TimeFrame("12:30", "14:30"),
                        new TimeFrame("14:30", "15:00"),
                        new TimeFrame("16:00", "17:00"),
                    }
                }
            };

            return persons;
        }
    }
}
