using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataThanhtich : ScriptableObject
{
    [System.Serializable]
    public struct Thanhtich
    {
        public string nameV, nameE;
        public int exp;
        public int diamod;
        public int gem;
        public Sprite icon;
        public ItemOrder[] itemOrders;
    }
    public Thanhtich[] data;
}