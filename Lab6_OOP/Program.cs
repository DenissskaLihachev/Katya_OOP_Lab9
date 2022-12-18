using System;
using MyList;

ListQueue listQueue = new();

void PrintEvent(string message) => Console.WriteLine(message);
Action CountingEve()
{
    int count = 1;
    void Inner()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\t\tНомер операции - [{count}]");
        Console.ResetColor();
        count++;
    }
    return Inner;
}

listQueue.Notify += PrintEvent;
var fn = CountingEve();

fn();   listQueue.Add(new Person("Баранова", "Елизавета", "Егоровна"));     Console.WriteLine();
fn();   listQueue.Add(new Person("Демидова", "Ксения", "Александровна"));   Console.WriteLine();
fn();   listQueue.Add(new Person("Лавров", "Роман", "Эмирович"));           Console.WriteLine();
fn();   listQueue.Add(new Person("Поляков", "Михаил", "Артемович"));        Console.WriteLine();

Person[] persons = new Person[3]
{
    new Person("One", "One", "One"),
    new Person("Two", null, "Two"),
    new Person("Three", "Three", null),
};
fn();   listQueue.Add(persons);

Console.WriteLine();

//////////////////////////////////////////////////////////////////

fn();   listQueue.Remove();
Console.WriteLine();

//////////////////////////////////////////////////////////////////

fn();   listQueue.Null(3);
Console.WriteLine();

//////////////////////////////////////////////////////////////////

fn();   listQueue.Sort();
Console.WriteLine();

//////////////////////////////////////////////////////////////////

fn();   listQueue.GetMassive();

Console.ReadKey();
Console.Clear();
