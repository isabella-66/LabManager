﻿// See https://aka.ms/new-console-template for more information
//Console.WriteLine(args);

//foreach (var arg in args)
//{
//    Console.WriteLine(arg);
//}

using LabManager.Database;
using LabManager.Models;
using LabManager.Repositories;
using Microsoft.Data.Sqlite;

var databaseConfig = new DatabaseConfig(); //usando var, porque é mais fácil de ver - não precisa especificar, ao colocar, VS automaticamente sabe
new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

//Routing || roteamento

var modelName = args[0];
var modelAction = args[1]; //?

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        } 
    }

    if(modelAction == "New")
    {

        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);
    }

    if(modelAction == "Show")
    {

        int id = Convert.ToInt32(args[2]); //id requisitada

        var computer = computerRepository.GetById(id); //pegando id requisitada para pesquisa pelo Computer
        Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor); //mostrando resultado da pesquisa

    }
}