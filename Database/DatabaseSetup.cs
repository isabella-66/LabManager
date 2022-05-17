using Microsoft.Data.Sqlite;

namespace LabManager.Database;

class DatabaseSetup
{
    private DatabaseConfig databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig) //configuração necessária p fazer tabela, puxa do DatabaseConfig
    { 
        this.databaseConfig = databaseConfig; 
        CreateComputerTable();
    }

    public void CreateComputerTable()
    {
        var connection = new SqliteConnection("Data Source=database.db"); //database.db é um arquivo - string de conexão já configurada no DatabaseConfig
        connection.Open(); //abre conexão

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Computers(
                id int not null primary key,
                ram varchar(100) not null,
                processor varchar(100) not null
            );
            CREATE TABLE IF NOT EXISTS Labs(
                id int not null primary key,
                num_lab int not null,
                name varchar(100) not null,
                block int not null
            )
        ";

        command.ExecuteNonQuery(); //executa a tabela 
        connection.Close(); //fecha conexão
    }
}