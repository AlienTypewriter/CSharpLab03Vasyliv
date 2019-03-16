using System;
using System.Windows;

namespace WpfApp1
{
    class Person
    {
        private string name;
        private string surname;
        private string email;
        private string birthdate;
        private readonly bool isAdult;
        private readonly bool isBirthday;
        private readonly string chineseSign;
        private readonly string sunSign;
        private int[] sunSignChangeDays = new int[12]
        {
            21,21,21,21,21,22,23,24,23,24,23,22
        };
        private string[] chineseSigns = new string[12]
        {
            "Rat", "Ox" , "Tiger", "Rabbit", "Dragon", "Snake",
        "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig"
        };
        string[] sunSigns = new string[12]
        {
            "Capricorn","Aquerius", "Pisces", "Aries", "Taurus" , "Gemini", "Cancer",
            "Leo", "Virgo","Libra", "Scoprio", "Sagittarius"
        };

        public Person(string name, string surname, string email, DateTime birthdate)
        {
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.birthdate = birthdate.ToShortDateString();
            DateTime now = DateTime.Now;
            isBirthday = now.Day == birthdate.Day && now.Month == birthdate.Month;
            isAdult = now > birthdate.AddYears(18);
            int inEuropian = 0;
            for (int i = 0; i < 12; i++)
            {
                if (birthdate.Month > i + 1 || (birthdate.Month == i + 1 && birthdate.Day >= sunSignChangeDays[i]))
                {
                    inEuropian = i + 1;
                }
            }
            inEuropian %= 12;
            int inChinese = (birthdate.Year - 4 - ((birthdate < new DateTime(birthdate.Year, 2, 5)) ? 1 : 0)) % 12;
            chineseSign = chineseSigns[inChinese];
            sunSign = sunSigns[inEuropian];
            MessageBox.Show(this.print());
        }
        Person(string name, string surname, string email) : this(name, surname, email, DateTime.Now) { }
        Person(string name, string surname, DateTime birthdate) : this(name, surname, null, birthdate) { }
        public string print()
        {
            return "Name -- " + name + "\nSurname -- " + surname + "\nEmail -- " + email + "\nDate of birth -- " + birthdate +
                "\nIs an adult? -- " + isAdult + "\nSelebrates birthday today? -- " + isBirthday +
                "\nChinese Zodiac sign -- " + chineseSign + "\nSun Zodiac sign -- " + sunSign;
        }
    }
}
