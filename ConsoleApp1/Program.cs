
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
