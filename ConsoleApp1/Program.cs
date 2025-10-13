
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