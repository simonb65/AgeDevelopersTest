using System;

namespace AglDevelopersTest
{
    // Format of data provided 
    public enum Gender
    {
        Male,
        Female
    }

    public enum PetType
    {
        Cat,
        Dog,
        Fish
    }

    public class Pet
    {
        public string Name { get; set; }
        public PetType Type { get; set; }
    }
    public class Owner
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public Pet[] Pets { get; set; }
    }
}
