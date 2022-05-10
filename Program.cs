// See https://aka.ms/new-console-template for more information
//Console.WriteLine(args);

//foreach (var arg in args)
//{
//    Console.WriteLine(arg);
//}


using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=database.db"); //database.db é um arquivo
connection.Open(); //abre conexão

var command = connection.CreateCommand();
command.CommandText = @"
    CREATE TABLE IF NOT EXISTS Computers(
        id int not null primary key,
        ram varchar(100) not null,
        processor varchar(100) not null
    );
";
command.ExecuteNonQuery(); //executa a tabela 

connection.Close(); //fecha conexão


//Routing || roteamento

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        connection = new SqliteConnection("Data Source=database.db"); 
        connection.Open();

        command = connection.CreateCommand();
        command.CommandText = "SELECT id, ram, processor FROM Computers;"; //SELECT *  - seleciona todos os campos

        var reader = command.ExecuteReader(); //seleciona todos os campos; reader é um cursor

        Console.WriteLine("Computer List");
        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        }

        reader.Close();
        connection.Close();
    }

    if(modelAction == "New")
    {

        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        connection = new SqliteConnection("Data Source=database.db"); 
        connection.Open();

        command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);
        command.ExecuteNonQuery();

        connection.Close();
    }
}