namespace LabManager.Models;
//model é mais estrutura de dados que representa computador

class Computer
{
    public int Id { get; set; }
    public string Ram { get; set; }
    public string Processor { get; set; }

    public Computer() {}

    public Computer(int id, string ram, string processor)
    {
        Id = id;
        Ram = ram;
        Processor = processor;
    }
}