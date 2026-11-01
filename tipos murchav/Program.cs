
using System;
using System.Collections.Generic;

namespace PracticalOOP
{
    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Day:D2}/{Month:D2}/{Year}";
        }
    }

    public class Passport
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Date BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string Nationality { get; set; }
        public string PersonalStatus { get; set; }
        public string Address { get; set; }

        private string[] childrenIds;
        private int childCount;

        public Passport(string id, string firstName, string lastName, Date birthDate,
                        string birthCountry, string nationality, string personalStatus, string address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            BirthCountry = birthCountry;
            Nationality = nationality;
            PersonalStatus = personalStatus;
            Address = address;

            childrenIds = new string[3];
            childCount = 0;
        }

        public bool AddChild(string childId)
        {
            if (childCount < 3)
            {
                childrenIds[childCount] = childId;
                childCount++;
                return true;
            }
            return false;
        }

        public bool RemoveChild(string childId)
        {
            for (int i = 0; i < childCount; i++)
            {
                if (childrenIds[i] == childId)
                {
                    for (int j = i; j < childCount - 1; j++)
                    {
                        childrenIds[j] = childrenIds[j + 1];
                    }
                    childrenIds[childCount - 1] = null;
                    childCount--;
                    return true;
                }
            }
            return false;
        }

        public string[] GetChildrenIds()
        {
            string[] result = new string[childCount];
            Array.Copy(childrenIds, result, childCount);
            return result;
        }
    }

    public class Person
    {
        public Passport PassportInfo { get; set; }

        public Person(Passport passportInfo)
        {
            PassportInfo = passportInfo;
        }

        public bool HasSharedChildrenWith(Person other)
        {
            if (other == null || other.PassportInfo == null || this.PassportInfo == null)
                return false;

            string[] myChildren = this.PassportInfo.GetChildrenIds();
            string[] otherChildren = other.PassportInfo.GetChildrenIds();

            foreach (var child1 in myChildren)
            {
                foreach (var child2 in otherChildren)
                {
                    if (child1 == child2)
                        return true;
                }
            }
            return false;
        }
    }

    public class Employee
    {
        public Passport PassportInfo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double HourlySalary { get; set; }
        public double HoursWorked { get; set; }

        public Employee(Passport passport, string email, string phone, double salary, double hours)
        {
            PassportInfo = passport;
            Email = email;
            Phone = phone;
            HourlySalary = salary;
            HoursWorked = hours;
        }

        public Employee(Passport passport)
        {
            PassportInfo = passport;
            Email = "Unknown";
            Phone = "Unknown";
            HourlySalary = 0.0;
            HoursWorked = 0.0;
        }

        public double GrossSalary() => HourlySalary * HoursWorked;

        public double NettoSalary()
        {
            double gross = GrossSalary();
            double deductions = (gross * 0.25) + (gross * 0.031) + (gross * 0.05);
            return gross - deductions;
        }

        public double IncomeTax() => GrossSalary() * 0.25;
        public double SocialSecurityTax() => GrossSalary() * 0.05;

        public override string ToString()
        {
            string name = PassportInfo != null ? $"{PassportInfo.FirstName} {PassportInfo.LastName}" : "N/A";
            return $"Employee Details:\nEmployee Name : {name}\nEmployee Neto salary: {NettoSalary():F2} NIS";
        }
    }

    public class Room
    {
        public string Type { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }

        public Room(string type, double length, double width)
        {
            Type = type;
            Length = length;
            Width = width;
        }

        public Room(Room other)
        {
            Type = other.Type;
            Length = other.Length;
            Width = other.Width;
        }

        public double GetArea() => Length * Width;
    }

    public class Apartment
    {
        public string OwnerName { get; set; }
        public List<Room> Rooms { get; set; }

        public Apartment(string ownerName, List<Room> rooms)
        {
            OwnerName = ownerName;
            Rooms = rooms ?? new List<Room>();
        }

        public double CalculateTotalArea()
        {
            double total = 0;
            foreach (var room in Rooms)
            {
                total += room.GetArea();
            }
            return total;
        }

        public string GetCategory()
        {
            double area = CalculateTotalArea();
            if (area <= 70) return "Small";
            if (area <= 110) return "Medium";
            return "Large";
        }

        public double CalculateHouseCommitteeFee()
        {
            double area = CalculateTotalArea();
            string category = GetCategory();

            if (category == "Small") return area * 0.8;
            if (category == "Medium") return (70 * 0.8) + ((area - 70) * 1.0);
            return (70 * 0.8) + (40 * 1.0) + ((area - 110) * 1.2);
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"בעל הדירה: {OwnerName}");
            Console.WriteLine($"שטח דירה כולל: {CalculateTotalArea():F2} מ\"ר | קטגוריה: {GetCategory()}");
            Console.WriteLine($"תשלום ועד בית: {CalculateHouseCommitteeFee():F2} ש\"ח");
        }
    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }

        public Address(string city, string street, int number)
        {
            City = city;
            Street = street;
            Number = number;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"כתובת: {Street} {Number}, {City}");
        }
    }

    public class Building
    {
        public Address BuildingAddress { get; set; }
        public List<Apartment> Apartments { get; set; }

        public Building(Address address, List<Apartment> apartments)
        {
            BuildingAddress = address;
            Apartments = apartments ?? new List<Apartment>();
        }

        public double CalculateTotalCommitteeFund()
        {
            double totalFund = 0;
            foreach (var apt in Apartments)
            {
                totalFund += apt.CalculateHouseCommitteeFee();
            }
            return totalFund;
        }

        public void DisplayDetails()
        {
            BuildingAddress.DisplayDetails();
            Console.WriteLine($"מספר דירות בבניין: {Apartments.Count}");
            Console.WriteLine($"סך הכל קופת ועד הבית: {CalculateTotalCommitteeFund():F2} ש\"ח");
        }
    }

    public class Engine
    {
        public string LicensePlate { get; set; }
        public int ProductionYear { get; set; }

        public Engine(string licensePlate, int productionYear)
        {
            LicensePlate = licensePlate;
            ProductionYear = productionYear;
        }
    }

    public class Carriage
    {
        public int SerialNumber { get; set; }
        public int PassengerCapacity { get; set; }

        public Carriage(int serialNumber, int passengerCapacity)
        {
            SerialNumber = serialNumber;
            PassengerCapacity = passengerCapacity;
        }
    }

    public class Train
    {
        public Engine TrainEngine { get; set; }
        private Carriage[] carriages;
        private int currentCarriageCount;
        public int MaxCarriages { get; }

        public Train(Engine engine, int maxCarriages)
        {
            TrainEngine = engine;
            MaxCarriages = maxCarriages;
            carriages = new Carriage[maxCarriages];
            currentCarriageCount = 0;
        }

        public void ReplaceEngine(Engine newEngine) => TrainEngine = newEngine;

        public bool AddCarriage(Carriage carriage)
        {
            if (currentCarriageCount < MaxCarriages)
            {
                carriages[currentCarriageCount] = carriage;
                currentCarriageCount++;
                return true;
            }
            return false;
        }

        public int GetTotalPassengers()
        {
            int total = 0;
            for (int i = 0; i < currentCarriageCount; i++)
            {
                total += carriages[i].PassengerCapacity;
            }
            return total;
        }

        public double GetAveragePassengersPerCarriage()
        {
            if (currentCarriageCount == 0) return 0.0;
            return (double)GetTotalPassengers() / currentCarriageCount;
        }

        public void DisplayTrainDetails()
        {
            Console.WriteLine($"קטר מספר: {TrainEngine.LicensePlate} (שנת ייצור: {TrainEngine.ProductionYear})");
            Console.WriteLine($"מספר קרונות בפועל: {currentCarriageCount} / {MaxCarriages}");
            Console.WriteLine($"סה\"כ נוסעים ברכבת: {GetTotalPassengers()}");
            Console.WriteLine($"ממוצע נוסעים לקרון: {GetAveragePassengersPerCarriage():F2}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            targil1();
            Console.WriteLine("\n----------------------------------------\n");

            targil2();
            Console.WriteLine("\n----------------------------------------\n");

            targil3();
            Console.WriteLine("\n----------------------------------------\n");

            targil4();
        }

        static void targil1()
        {
            Console.WriteLine("=== תרגיל 1: זוגות עם ילדים משותפים ===");

            Person[] people = new Person[5];

            Passport p1 = new Passport("101", "ישראל", "ישראלי", new Date(1, 1, 1980), "ישראל", "יהודי", "נשוי", "תל אביב");
            p1.AddChild("C1");
            p1.AddChild("C2");
            people[0] = new Person(p1);

            Passport p2 = new Passport("102", "שרה", "ישראלי", new Date(5, 5, 1982), "ישראל", "יהודי", "נשואה", "תל אביב");
            p2.AddChild("C1");
            p2.AddChild("C2");
            people[1] = new Person(p2);

            Passport p3 = new Passport("103", "דוד", "כהן", new Date(10, 10, 1975), "ישראל", "יהודי", "גרוש", "חיפה");
            p3.AddChild("C3");
            people[2] = new Person(p3);

            Passport p4 = new Passport("104", "רחל", "כהן", new Date(12, 12, 1978), "ישראל", "יהודי", "גרושה", "חיפה");
            p4.AddChild("C3");
            people[3] = new Person(p4);

            Passport p5 = new Passport("105", "משה", "לוי", new Date(20, 3, 1990), "ישראל", "יהודי", "רווק", "ירושלים");
            people[4] = new Person(p5);

            Console.WriteLine("זוגות מבוגרים עם ילדים משותפים:");
            for (int i = 0; i < people.Length; i++)
            {
                for (int j = i + 1; j < people.Length; j++)
                {
                    if (people[i].HasSharedChildrenWith(people[j]))
                    {
                        Console.WriteLine($"- {people[i].PassportInfo.FirstName} {people[i].PassportInfo.LastName} ו-{people[j].PassportInfo.FirstName} {people[j].PassportInfo.LastName}");
                    }
                }
            }
        }

        static void targil2()
        {
            Console.WriteLine("=== תרגיל 2: TestEmployee ===");

            Employee[] employees = new Employee[3];
            double totalEmployerPay = 0;
            double totalSocialSecurity = 0;
            double totalIncomeTax = 0;

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"\nהכנס פרטים עבור עובד {i + 1}:");
                Console.Write("שם פרטי: ");
                string fName = Console.ReadLine();
                Console.Write("שם משפחה: ");
                string lName = Console.ReadLine();
                Console.Write("אימייל: ");
                string email = Console.ReadLine();
                Console.Write("טלפון: ");
                string phone = Console.ReadLine();
                Console.Write("שכר לשעה: ");
                double hourly = double.Parse(Console.ReadLine());
                Console.Write("מספר שעות עבודה: ");
                double hours = double.Parse(Console.ReadLine());

                Passport pass = new Passport($"ID-{i + 1}", fName, lName, new Date(1, 1, 1990), "ישראל", "ישראלי", "רווק", "כתובת");
                employees[i] = new Employee(pass, email, phone, hourly, hours);

                totalEmployerPay += employees[i].GrossSalary();
                totalSocialSecurity += employees[i].SocialSecurityTax();
                totalIncomeTax += employees[i].IncomeTax();
            }

            Console.WriteLine("\n--- פרטי העובדים ומשכורת נטו ---");
            foreach (var emp in employees)
            {
                Console.WriteLine(emp.ToString());
                Console.WriteLine("-----------------------------");
            }

            Console.WriteLine("\n--- דוח מסכם המעביד ---");
            Console.WriteLine($"סך הכל תשלום מעביד לעובדים: {totalEmployerPay:F2} ש\"ח");
            Console.WriteLine($"סך הכל העברה לביטוח לאומי: {totalSocialSecurity:F2} ש\"ח");
            Console.WriteLine($"סך הכל העברה למס הכנסה: {totalIncomeTax:F2} ש\"ח");
        }

        static void targil3()
        {
            Console.WriteLine("=== תרגיל 3: בניין וחישוב ועד בית ===");

            Address addr = new Address("תל אביב", "הרצל", 15);
            List<Apartment> apartments = new List<Apartment>();

            List<Room> r1 = new List<Room> { new Room("סלון", 6, 5), new Room("חדר שינה", 5, 4) };
            apartments.Add(new Apartment("ישראל ישראלי", r1));

            List<Room> r2 = new List<Room> { new Room("סלון", 7, 5), new Room("חדר שינה 1", 5, 4), new Room("חדר שינה 2", 4, 4), new Room("מטבח", 4, 3) };
            apartments.Add(new Apartment("רחל כהן", r2));

            List<Room> r3 = new List<Room> { new Room("סלון גדול", 8, 6), new Room("חדר ילדים 1", 5, 4), new Room("חדר ילדים 2", 5, 4), new Room("חדר עבודה", 4, 4), new Room("מטבח", 5, 4) };
            apartments.Add(new Apartment("דוד לוי", r3));

            Building b = new Building(addr, apartments);

            Console.WriteLine("\n--- פירוט הדירות ---");
            foreach (var apt in b.Apartments)
            {
                apt.DisplayDetails();
                Console.WriteLine("---");
            }

            Console.WriteLine("\n--- סיכום קופת ועד הבית ---");
            b.DisplayDetails();
        }

        static void targil4()
        {
            Console.WriteLine("=== תרגיל 4: ניהול רכבת, קטר וקרונות ===");

            Engine e1 = new Engine("ENG-9988", 2018);
            Train train = new Train(e1, 5);

            train.AddCarriage(new Carriage(101, 60));
            train.AddCarriage(new Carriage(102, 50));
            train.AddCarriage(new Carriage(103, 70));

            Console.WriteLine("--- נתוני רכבת קיימים ---");
            train.DisplayTrainDetails();

            Console.WriteLine("\nמבצע החלפת קטר והוספת קרון...");
            Engine e2 = new Engine("ENG-1122", 2023);
            train.ReplaceEngine(e2);
            train.AddCarriage(new Carriage(104, 80));

            Console.WriteLine("\n--- נתוני רכבת לאחר עדכון ---");
            train.DisplayTrainDetails();
        }
    }
}