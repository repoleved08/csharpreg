using System;
using System.IO;
using System.Collections.Generic;

namespace RegisterLogin
{
    public class Course
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Course(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    // public enum Course
    // {
    //     C# Full Stack Development,
    //     JavaScript Full-Stack Development,
    //     QA/QE,
    //     WordPress Full-Stack Development
    // }
    // public class CourseDTO
    // {
    //     public string Name { get; set; }
    //     public decimal Price { get; set; }
    //     public Course Course { get; set; }

    //     public CourseDTO()
    //     {
    //         Course = Course;
    //         Price = Price;
    //     }
    // }
    class Program
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static string filePath = @"Data\user.txt";
        static Dictionary<string, string> users = new Dictionary<string, string>();
        static Dictionary<string, string> admins = new Dictionary<string, string>();
        static Dictionary<int, Course> courses = new Dictionary<int, Course>
        {
            { 1, new Course("C# Full Stack Development", 100) },
            { 2, new Course("JavaScript Full-Stack Development", 150) },
            { 3, new Course("QA/QE", 200) },
            { 4, new Course("WordPress Full-Stack Development", 250) }
        };


        // List<CourseDTO> coursesDTO = new List<CourseDTO>() {
        //     new CourseDTO() {Name="C# Full Stack Development", Price=40000},
        //     new CourseDTO() {Name="Javascript Full Stack Development", Price=50000},
        //     new CourseDTO() {Name="Networking IPV6", Price=10000},
        //     new CourseDTO() {Name="VueJS Frontend", Price=20000},
        // };

        static void Main(string[] args)
        {
            LoadUsers();

            Console.WriteLine("Are you a normal user or an admin? (u/a)");
            string userType = Console.ReadLine();

            Console.WriteLine("Do you want to register or login? (r/l)");
            string choice = Console.ReadLine();

            if (userType == "u")
            {
                if (choice == "r")
                {
                    RegisterUser(users);
                }
                else if (choice == "l")
                {
                    if (LoginUser(users))
                    {
                        ShowCourses();
                    }
                }
            }
            else if (userType == "a")
            {
                if (choice == "r")
                {
                    RegisterUser(admins);
                }
                else if (choice == "l")
                {
                    LoginUser(admins);
                }
            }
        }

        static void LoadUsers()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        if (parts[2] == "u")
                        {
                            users.Add(parts[0], parts[1]);
                        }
                        else if (parts[2] == "a")
                        {
                            admins.Add(parts[0], parts[1]);
                        }
                    }
                }
            }
        }

        static void RegisterUser(Dictionary<string, string> userDict)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            if (userDict.ContainsKey(username))
            {
                Console.WriteLine("User already exists!");
            }
            else
            {
                userDict.Add(username, password);
                string userType = userDict == users ? "u" : "a";
                File.AppendAllText(filePath, username + "," + password + "," + userType + Environment.NewLine);
                Console.WriteLine("User registered successfully!");
            }
        }

        static bool LoginUser(Dictionary<string, string> userDict)
        {
            System.Console.WriteLine("-----Login Page---");
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            if (userDict.ContainsKey(username) && userDict[username] == password)
            {
                Console.WriteLine("Login successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid username or password!");
                return false;
            }
        }

        static void ShowCourses()
        {
            Console.WriteLine("Here is a list of courses you can purchase:");
            Console.WriteLine("Please select a course:");
            // foreach (KeyValuePair<int, string> course in courses)
            // {
            //     Console.WriteLine(course.Key + ". " + course.Value);
            // };

            // int choice = Convert.ToInt32(Console.ReadLine());
            // string selectedCourse = courses[choice];
            // Console.WriteLine("You have selected: " + selectedCourse);
            foreach (KeyValuePair<int, Course> course in courses)
            {
                Console.WriteLine(course.Key + ". " + course.Value.Name + " - " + course.Value.Price);
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            Course selectedCourse = courses[choice];
            Console.WriteLine("You have selected: " + selectedCourse.Name);


        }
    }
}
