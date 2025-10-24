namespace Travail1_bon;

public class Person
{
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public int Id { get; set; }

    public Person(string nom, string prenom, int id)
    {
        Nom = nom;
        Prenom = prenom;
        Id = id;
    }

    public string GetFullName()
    {
        return Prenom + " " + Nom;
    }
}
