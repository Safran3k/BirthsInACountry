using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birts_In_Vas_Country
{
    class Program
    {
        /*
         * X-XXXXXX-XXXX <-- Identity number
         * X - Means gender, and the first two digits of the year of birth
         * XXXXXX - Means last two digits of the year of birth, month and day (yymmdd)
         * XXXX - The first three digits means, born on the same day to distinguish, and the last digit, for control purposes, is made up of other numbers
        */

        class Persons
        {
            string id;
            int _firstNumber;
            int _yyyy;
            int _mm;
            int _dd;
            int _sameDayDis;
            int _numCheck;

            public Persons(string line)
            {
                string[] lineData = line.Split('-');
                Id = line;
                FirstNumber = int.Parse(lineData[0]);
                Yyyy = int.Parse(lineData[1].ToString().Substring(0, 2));
                Mm = int.Parse(lineData[1].ToString().Substring(2, 2));
                Dd = int.Parse(lineData[1].ToString().Substring(4, 2));
                SameDayDis = int.Parse(lineData[2].ToString().Substring(0, 3));
                NumCheck = int.Parse(lineData[2].ToString().Substring(3, 1));
                if (!CdvCheck) { throw new Exception($"\tError with ID#{line}!"); }
            }

            public string Id { get => id; set => id = value; }
            public int FirstNumber { get => _firstNumber; set => _firstNumber = value; }
            public int Yyyy { get => _yyyy; set => _yyyy = value; }
            public int Mm { get => _mm; set => _mm = value; }
            public int Dd { get => _dd; set => _dd = value; }
            public int SameDayDis { get => _sameDayDis; set => _sameDayDis = value; }
            public int NumCheck { get => _numCheck; set => _numCheck = value; }

            //Task 3.
            bool CdvCheck
            {
                get
                {
                    int sum = 0;
                    string persID = id.Remove(1, 1).Remove(7, 1);
                    for (int i = 0; i < persID.Length; i++)
                    {
                        sum += int.Parse(persID[i].ToString()) * (10 - i);
                    }
                    return _numCheck == sum % 11;
                }
            }

            public bool Mans
            {
                get
                {
                    return _firstNumber == 1 || _firstNumber == 3;
                }
            }

            public int Year
            {
                get
                {
                    return _firstNumber < 3 ? 1900 + _yyyy : 2000 + _yyyy;
                }
            }

            public bool LeapDay
            {
                get
                {
                    return Year % 4 == 0 && _mm == 2 && _dd == 24;
                }
            }
        }

        private static List<Persons> personsList = new List<Persons>();

        static void Main(string[] args)
        {
            Task_2();
            Console.WriteLine();
            Task_5();
            Console.WriteLine();
            Task_6();
            Console.WriteLine();
            Task_7();
            Console.WriteLine();
            Task_8();
            Console.WriteLine();
            Task_9();


            Console.ReadKey();
        }

        private static void Task_9()
        {
            Console.WriteLine("Task 9.");

            foreach (var item in personsList.GroupBy(g => g.Year))
            {
                Console.WriteLine("\t{0} - {1} numbers", item.Key, item.Count());
            }
        }

        private static void Task_8()
        {
            Console.WriteLine("Task 8.");

            bool leapDayBaby = false;
            foreach (Persons item in personsList)
            {
                if (item.LeapDay)
                {
                    leapDayBaby = true;
                    break;
                }
            }
            Console.WriteLine($"{(leapDayBaby ? "" : "Not ")} Born baby on leap-day!");
        }

        private static void Task_7()
        {
            Console.WriteLine("Task 7.");
            Console.WriteLine("The investigated period: {0} - {1}", personsList.Min(x => x.Year), personsList.Max(y => y.Year));
        }

        private static void Task_6()
        {
            Console.WriteLine("Task 6.");
            Console.WriteLine("Numbers of mans: {0}", personsList.Count(x => x.Mans));
        }

        private static void Task_5()
        {
            Console.WriteLine("Task 5.");
            Console.WriteLine("Numbers of babies: {0}", personsList.Count);
        }

        private static void Task_2()
        {
            Console.WriteLine("Task 2.\nStorage of data.\n");

            //4. feladat
            Console.WriteLine("Task 4.\nCheck:");
            foreach (var item in File.ReadAllLines("vas.txt"))
            {
                try
                {
                    personsList.Add(new Persons(item));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }
    }
}
