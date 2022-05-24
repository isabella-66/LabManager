using Dapper;
using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories; 

class LabRepository
{
    private DatabaseConfig databaseConfig; //para saber qual banco acessar

    public LabRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    public List<Lab> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString); //coloca using na var, qnd para de usar ela, o método é fehcado automaticamente
        connection.Open(); //não precisa do Close graças ao using
        //List<Lab> labs = connection.Query<Lab>("SELECT * FROM Labs;").ToList(); - dá para fazer direto no return, pois n tem mais o Close para atrapalhar
        return connection.Query<Lab>("SELECT * FROM Labs;").ToList();
    }

    public Lab Save(Lab lab) //salva no banco e devolve para o computador
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString); //coloca using na var, qnd para de usar ela, o método é fehcado automaticamente
        connection.Open();
        connection.Execute("INSERT INTO Labs VALUES(@id, @number, @name, @block);", lab); //sql que quer e objeto que quer
        return lab;
    }
}