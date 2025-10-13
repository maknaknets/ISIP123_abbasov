
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
