// See https://aka.ms/new-console-template for more information
//Console.WriteLine(args);

//foreach (var arg in args)
//{
//    Console.WriteLine(arg);
//}

using LabManager.Database;
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

        var connection = new SqliteConnection("Data Source=database.db"); 
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);
        command.ExecuteNonQuery(); //?

        connection.Close();
    }
}

if (modelName == "Labs")
{
    if (modelAction == "List")
    {
        var connection = new SqliteConnection("Data Source=database.db"); 
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Labs;";

        var reader = command.ExecuteReader();

        Console.WriteLine("Lab List");
        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
        }

        reader.Close();
        connection.Close();
    }

    if (modelAction == "New") 
    {
        int id = Convert.ToInt32(args[2]);
        int num_lab = Convert.ToInt32(args[3]);
        string name = args[4];
        int block = Convert.ToInt32(args[5]);

        var connection = new SqliteConnection("Data Source=database.db"); 
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Labs VALUES($id, $num_lab, $name, $block);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$num_lab", num_lab);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);
        command.ExecuteNonQuery();

        connection.Close();
    }
}