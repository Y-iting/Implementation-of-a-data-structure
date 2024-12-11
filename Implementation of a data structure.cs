using System;
using System.Collections.Generic;
using System.IO;

public class Student
{
    public string Name { get; set; }
    public string ID { get; set; }
    public double Grade { get; set; }

    public Student(string name, string id, double grade)
    {
        Name = name;
        ID = id;
        Grade = grade;
    }
}

public class StudentManager
{
    private List<Student> students;

    public StudentManager()
    {
        students = new List<Student>();
    }

    public void AddStudent(string name, string id, double grade)
    {
        students.Add(new Student(name, id, grade));
    }

    public void DisplayStudents()
    {
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Grade: {student.Grade}");
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.ID},{student.Name},{student.Grade}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            foreach (var line in File.ReadLines(filename))
            {
                var parts = line.Split(',');
                AddStudent(parts[1], parts[0], double.Parse(parts[2]));
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        StudentManager manager = new StudentManager();
        manager.AddStudent("Alice", "S001", 88.5);
        manager.AddStudent("Bob", "S002", 92.3);

        Console.WriteLine("Students:");
        manager.DisplayStudents();

        // Save to file
        manager.SaveToFile("students.txt");

        // Load from file
        StudentManager newManager = new StudentManager();
        newManager.LoadFromFile("students.txt");
        Console.WriteLine("\nStudents loaded from file:");
        newManager.DisplayStudents();
    }

}
