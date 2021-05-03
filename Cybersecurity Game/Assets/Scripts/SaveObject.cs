using System.Collections;
using System.Collections.Generic;

public class SaveObject
{
    public int saveid;

    public int day;

    public int budget;

    public int reputation;

    public string[] purchases;

    public string password1;

    public string password2;

    public int[] email;

    public int[] scenarioMail;

    public bool[] readEmail;

    public bool[] readSmail;

    public Scenario scenario;

    public string scenarioDetail;
}

public enum Scenario
{
    None = 0,
    Password = 1,
    Phishing = 2,
    Malware = 3,
    Ransom = 4
}

public enum Target
{
    You = 0,
    CEO = 1,
    Employee1 = 2,
    Employee2 = 3,
    Meeting = 4,
    IT = 5
}

public enum MalwareType
{
    Virus = 0,
    Adware = 1,
    Trojan = 2,
    Ransom = 3,
    None = -1
}
