using System;

namespace Person.Models;

public class PersonModel
{
    public int Id { get; init; }
    public string Name { get; private set; }
    public bool IsActive {get; set; }

    public PersonModel(string name)
    {
        Name = name;
        IsActive = true;
    }
    public void UpdateName(string name)
    {
        Name = name;
    }
    public void SetInactive(){
        IsActive = false;
    }
}
