﻿var me = Person.New
        .Called("Hikari")
        .WorksAsA("Developer")
        .Born(DateTime.UtcNow)
        .Build();
Console.WriteLine(me);


public class Person
{
    public string Name;
    public string Position;
    public DateTime DateOfBirth;

    public class Builder : PersonBirthDateBuilder<Builder>
    {
        internal Builder() { }
    }

    public static Builder New => new();

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class PersonBuilder
{
    protected Person person = new();

    public Person Build()
    {
        return person;
    }
}

public class PersonInfoBuilder<SELF> : PersonBuilder
  where SELF : PersonInfoBuilder<SELF>
{
    public SELF Called(string name)
    {
        person.Name = name;
        return (SELF)this;
    }
}

public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
  where SELF : PersonJobBuilder<SELF>
{
    public SELF WorksAsA(string position)
    {
        person.Position = position;
        return (SELF)this;
    }
}

// here's another inheritance level
// note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

public class PersonBirthDateBuilder<SELF> : PersonJobBuilder<PersonBirthDateBuilder<SELF>>
  where SELF : PersonBirthDateBuilder<SELF>
{
    public SELF Born(DateTime dateOfBirth)
    {
        person.DateOfBirth = dateOfBirth;
        return (SELF)this;
    }
}