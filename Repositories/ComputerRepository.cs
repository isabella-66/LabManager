//mesmo nomne model + repository
//fala com banco de dados para a tabela computer

using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories; //?

class ComputerRepository
{
    private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    //cria lista fora do while, no while adiciona itens à lista e depois mostra a lista
    public List<Computer> GetAll()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT id, ram, processor FROM Computers;"; //? SELECT *  - seleciona todos os campos

        var reader = command.ExecuteReader(); //? seleciona todos os campos; reader é um cursor

        var computers = new List<Computer>();

        while(reader.Read()) //preenche lista de computadores
        {
            var computer = new Computer(
                reader.GetInt32(0), reader.GetString(1), reader.GetString(2)
            );
            computers.Add(computer);
        }
        connection.Close();

        return computers;
    }

    public Computer Save(Computer computer) //salva no banco e devolve para o computador
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery();
        connection.Close();
        
        return computer;
    }
}