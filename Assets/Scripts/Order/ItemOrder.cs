using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemOrder
{
    public int idVatphams;
    public int soluong;

    public ItemOrder(int idvp, int sl)
    {
        idVatphams = idvp;
        soluong = sl;
    }
}
