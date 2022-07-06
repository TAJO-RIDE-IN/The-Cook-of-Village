using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string name => _name;
    
    [SerializeField] private string _name;
}
