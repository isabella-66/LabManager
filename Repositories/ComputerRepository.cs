//mesmo nome model + repository
//fala com banco de dados para a tabela computer

using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    //cria lista fora do while, no while adiciona itens à lista e depois mostra a lista
    
    public bool ExistsById(int id) //devolve se computador existe ou não no banco de dados
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(id) FROM Computers WHERE id=$id;";
        command.Parameters.AddWithValue("$id", id);

        //var reader = command.ExecuteReader();
        //reader.Read();
        //var result = reader.GetBoolean(0);
        var result = Convert.ToBoolean(command.ExecuteScalar()); //quando vai mostrar um valor só como resultado no SQL

        return result;
    }

    //public List<Computer> GetAll() //devolve todas as linhas
    public IEnumerable<Computer> GetAll()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var computers = connection.Query<Computer>("SELECT * FROM Computers"); //.ToList(); 

        connection.Close();

        return computers;
    }

    public Computer Save(Computer computer) //salva no banco e devolve para o computador
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        //pega valores do objeto computer e coloca na String INSERT
        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)", computer); //não mapeia diretamente um objeto se o parâmetro for antecedido por $

        connection.Close();
        
        return computer;
    }

    public Computer GetById(int id) //devolve uma linha
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$id", id);
        command.CommandText = "SELECT * FROM Computers WHERE id = $id;";
        
        //devolver os dados do computador com aquele id
        var reader = command.ExecuteReader();
        reader.Read();
        var computer = new Computer(
            reader.GetInt32(0), reader.GetString(1), reader.GetString(2)
        );

        connection.Close();

        return computer;
    }

    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Computers SET ram = $ram, processor = $processor WHERE id = $id;";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery();
        connection.Close();
        
        return computer;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var command = connection.CreateCommand();
        command.Parameters.AddWithValue("$id", id);
        command.CommandText = "DELETE FROM Computers WHERE id = $id;";

        command.ExecuteNonQuery();
        connection.Close();
    }
}