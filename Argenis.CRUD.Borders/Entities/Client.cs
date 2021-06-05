using System;

namespace Argenis.CRUD.Borders.Entities
{
    public class Client
    {
        public Client(string name, int age, Guid id)
        {
            Name = name;
            Age = age;
            Id = id;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public Guid Id { get; set; }
    }
}
