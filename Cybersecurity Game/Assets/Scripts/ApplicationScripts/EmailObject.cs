using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmailObject
{
    public string senderName;

    public string senderMail;

    public string topic;

    public string content;

    public int link;

    public bool read = false;

    public bool scenario; //false = normal mail, true = scenario mail

    public int index;
}

[System.Serializable]
public class AttachmentObject
{
    public string linkName;

    public bool isFile;

    public bool isFatal;

    public string linkHover;
}
