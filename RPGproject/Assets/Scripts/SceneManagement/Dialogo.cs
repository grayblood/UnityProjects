using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogo
{
    [SerializeField] List<string> lines;

    public List<string> Lines
    {
        get { return lines; }
    }
}
