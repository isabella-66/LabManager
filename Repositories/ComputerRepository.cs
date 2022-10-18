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

    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig; //=> evita return quando tem uma linha só

    //cria lista fora do while, no while adiciona itens à lista e depois mostra a lista
    
    public bool ExistsById(int id) //devolve se computador existe ou não no banco de dados
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();
        var result = connection.ExecuteScalar<Boolean>("SELECT COUNT(id) FROM Computers WHERE id=$id;", new { Id = id });

        return result; 
    }

    //public List<Computer> GetAll() //devolve todas as linhas
    public IEnumerable<Computer> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var computers = connection.Query<Computer>("SELECT * FROM Computers"); //.ToList(); 

        return computers;
    }

    public Computer Save(Computer computer) //salva no banco e devolve para o computador
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        //pega valores do objeto computer e coloca na String INSERT
        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)", computer); //não mapeia diretamente um objeto se o parâmetro for antecedido por $
        
        return computer;
    }

    public Computer GetById(int id) //devolve uma linha
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id = @Id;", new { Id = id });

        return computer;
    }

    public Computer Update(Computer computer)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        connection.Execute("UPDATE Computers SET ram = @Ram, processor = @Processor WHERE id = @Id;", computer);
        
        return computer;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString); 
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id = @Id", new { Id = id });
    }
}