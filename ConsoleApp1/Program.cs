
using System;
using System.Collections.Generic;
using System.Linq;

// Абстрактный класс Person (Наследование и Абстракция)
public abstract class Person
{
    // Инкапсуляция
    private string id;
    private string name;
    private int age;
    private string contactInfo;

    public Person(string id, string name, int age, string contactInfo)
    {
        this.id = id;
        this.name = name;
        this.age = age;
        this.contactInfo = contactInfo;
    }
    // Свойства (Инкапсуляция)
    public string Id => id;
    public string Name => name;
    public int Age => age;
    public string ContactInfo => contactInfo;

    // Абстрактный метод для полиморфизма
    public abstract string GetDetails();
}
// Класс Student наследуется от Person
public class Student : Person
{
    private List<Course> enrolledCourses;

    public Student(string id, string name, int age, string contactInfo)
        : base(id, name, age, contactInfo)
    {
        enrolledCourses = new List<Course>();
    }

    public void EnrollCourse(Course course)
    {
        if (!enrolledCourses.Contains(course))
            enrolledCourses.Add(course);
    }

    public IReadOnlyList<Course> EnrolledCourses => enrolledCourses.AsReadOnly();

    public override string GetDetails()
    {
        return $"Student: {Name}, ID: {Id}, Age: {Age}, Contact: {ContactInfo}, Courses: {enrolledCourses.Count}";
    }
}

// Класс Teacher наследуется от Person
public class Teacher : Person
{
    private List<Course> teachingCourses;

    public Teacher(string id, string name, int age, string contactInfo)
        : base(id, name, age, contactInfo)
    {
        teachingCourses = new List<Course>();
    }

    public void AssignCourse(Course course)
    {
        if (!teachingCourses.Contains(course))
            teachingCourses.Add(course);
    }

    public IReadOnlyList<Course> TeachingCourses => teachingCourses.AsReadOnly();

    public override string GetDetails()
    {
        return $"Teacher: {Name}, ID: {Id}, Age: {Age}, Contact: {ContactInfo}, Teaching courses: {teachingCourses.Count}";
    }
}

// Класс Course
public class Course
{
    private string courseId;
    private string title;
    private Teacher teacher;
    private List<Student> enrolledStudents;

    public Course(string courseId, string title)
    {
        this.courseId = courseId;
        this.title = title;
        enrolledStudents = new List<Student>();
    }

    public string CourseId => courseId;
    public string Title => title;
    public Teacher Teacher
    {
        get => teacher;
        set
        {
            teacher = value;
            if (value != null && !value.TeachingCourses.Contains(this))
                value.AssignCourse(this);
        }
    }

    public void EnrollStudent(Student student)
    {
        if (!enrolledStudents.Contains(student))
        {
            enrolledStudents.Add(student);
            student.EnrollCourse(this);
        }
    }

    public IReadOnlyList<Student> EnrolledStudents => enrolledStudents.AsReadOnly();

    public string GetDetails()
    {
        return $"Course: {Title}, ID: {CourseId}, Teacher: {(Teacher?.Name ?? "Not assigned")}, Students: {enrolledStudents.Count}";
    }
}
// Класс UniversitySystem для управления всеми сущностями
public class UniversitySystem
{
    private List<Student> students;
    private List<Teacher> teachers;
    private List<Course> courses;

    public UniversitySystem()
    {
        students = new List<Student>();
        teachers = new List<Teacher>();
        courses = new List<Course>();
    }

    public void AddStudent(string id, string name, int age, string contactInfo)
    {
        students.Add(new Student(id, name, age, contactInfo));
    }

    public void AddTeacher(string id, string name, int age, string contactInfo)
    {
        teachers.Add(new Teacher(id, name, age, contactInfo));
    }

    public void AddCourse(string courseId, string title)
    {
        courses.Add(new Course(courseId, title));
    }

    public Student FindStudent(string id) => students.FirstOrDefault(s => s.Id == id);
    public Teacher FindTeacher(string id) => teachers.FirstOrDefault(t => t.Id == id);
    public Course FindCourse(string courseId) => courses.FirstOrDefault(c => c.CourseId == courseId);

    public void PrintAllStudents() => students.ForEach(s => Console.WriteLine(s.GetDetails()));
    public void PrintAllTeachers() => teachers.ForEach(t => Console.WriteLine(t.GetDetails()));
    public void PrintAllCourses() => courses.ForEach(c => Console.WriteLine(c.GetDetails()));
}
// Консольное меню
public class Program
{
    private static UniversitySystem university = new UniversitySystem();

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== University Management System ===");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Teacher");
            Console.WriteLine("3. Add Course");
            Console.WriteLine("4. Enroll Student to Course");
            Console.WriteLine("5. Assign Teacher to Course");
            Console.WriteLine("6. View Student Details");
            Console.WriteLine("7. View Course Details");
            Console.WriteLine("8. List All Students");
            Console.WriteLine("9. List All Teachers");
            Console.WriteLine("10. List All Courses");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    AddTeacher();
                    break;
                case "3":
                    AddCourse();
                    break;
                case "4":
                    EnrollStudentToCourse();
                    break;
                case "5":
                    AssignTeacherToCourse();
                    break;
                case "6":
                    ViewStudentDetails();
                    break;
                case "7":
                    ViewCourseDetails();
                    break;
                case "8":
                    university.PrintAllStudents();
                    break;
                case "9":
                    university.PrintAllTeachers();
                    break;
                case "10":
                    university.PrintAllCourses();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    private static void AddStudent()
    {
        Console.Write("Enter Student ID: ");
        string id = Console.ReadLine();
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Enter Contact Info: ");
        string contactInfo = Console.ReadLine();
        university.AddStudent(id, name, age, contactInfo);
        Console.WriteLine("Student added successfully!");
    }

    private static void AddTeacher()
    {
        Console.Write("Enter Teacher ID: ");
        string id = Console.ReadLine();
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Enter Contact Info: ");
        string contactInfo = Console.ReadLine();
        university.AddTeacher(id, name, age, contactInfo);
        Console.WriteLine("Teacher added successfully!");
    }

    private static void AddCourse()
    {
        Console.Write("Enter Course ID: ");
        string courseId = Console.ReadLine();
        Console.Write("Enter Course Title: ");
        string title = Console.ReadLine();
        university.AddCourse(courseId, title);
        Console.WriteLine("Course added successfully!");
    }

    private static void EnrollStudentToCourse()
    {
        Console.Write("Enter Student ID: ");
        string studentId = Console.ReadLine();
        Console.Write("Enter Course ID: ");
        string courseId = Console.ReadLine();

        Student student = university.FindStudent(studentId);
        Course course = university.FindCourse(courseId);

        if (student != null && course != null)
        {
            course.EnrollStudent(student);
            Console.WriteLine("Student enrolled successfully!");
        }
        else
        {
            Console.WriteLine("Student or Course not found!");
        }
    }

    private static void AssignTeacherToCourse()
    {
        Console.Write("Enter Teacher ID: ");
        string teacherId = Console.ReadLine();
        Console.Write("Enter Course ID: ");
        string courseId = Console.ReadLine();

        Teacher teacher = university.FindTeacher(teacherId);
        Course course = university.FindCourse(courseId);

        if (teacher != null && course != null)
        {
            course.Teacher = teacher;
            Console.WriteLine("Teacher assigned successfully!");
        }
        else
        {
            Console.WriteLine("Teacher or Course not found!");
        }
    }

    private static void ViewStudentDetails()
    {
        Console.Write("Enter Student ID: ");
        string studentId = Console.ReadLine();
        Student student = university.FindStudent(studentId);

        if (student != null)
        {
            Console.WriteLine(student.GetDetails());
            Console.WriteLine("Enrolled Courses:");
            foreach (var course in student.EnrolledCourses)
                Console.WriteLine($"- {course.Title} (ID: {course.CourseId})");
        }
        else
        {
            Console.WriteLine("Student not found!");
        }
    }

    private static void ViewCourseDetails()
    {
        Console.Write("Enter Course ID: ");
        string courseId = Console.ReadLine();
        Course course = university.FindCourse(courseId);

        if (course != null)
        {
            Console.WriteLine(course.GetDetails());
            Console.WriteLine("Enrolled Students:");
            foreach (var student in course.EnrolledStudents)
                Console.WriteLine($"- {student.Name} (ID: {student.Id})");
        }
        else
        {
            Console.WriteLine("Course not found!");
        }
    }
}
