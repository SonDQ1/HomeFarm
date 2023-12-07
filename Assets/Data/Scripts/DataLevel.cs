using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevel : ScriptableObject
{
    [System.Serializable]
    public class Item
    {
        public Sprite[] icons;
    }
    public Item[] data;
}
