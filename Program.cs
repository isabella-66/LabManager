using LabManager.Models;

SystemContext context = new SystemContext();
context.Database.EnsureCreated(); 

//New
var c1 = new Computer(1, "1G", "i3");
context.Computers.Add(c1);
context.SaveChanges();

var c2 = new Computer(2, "2G", "i5");
context.Computers.Add(c2);
context.SaveChanges();

var c3 = new Computer(3, "3G", "i7");
context.Computers.Add(c3);
context.SaveChanges();

//List
IEnumerable<Computer> computers = context.Computers;
foreach(var computer in computers)
{
    Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
}

//Show
var find = context.Computers.Find(1);
Console.WriteLine($"{find.Id}, {find.Ram}, {find.Processor}");

//Update
find.Ram = "4GB";
find.Processor = "i9";
context.Computers.Update(find);
context.SaveChanges();

//Delete
context.Computers.Remove(find);
context.SaveChanges();

//List again
computers = context.Computers;
foreach(var computer in computers)
{
    Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
}