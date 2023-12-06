using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOrder{

    public string nameOrderV;
    public string nameOrderE;
    public int coint;
    public int exp;
    public int kcFinist;
    public List<ItemOrder> itemOrders;

    public ListOrder(string nameOrderV, string nameOrderE, int coint, int exp, int kcFinist,List<ItemOrder> itemOrders)
    {
        this.nameOrderV = nameOrderV;
        this.nameOrderE = nameOrderE;
        this.coint = coint;
        this.exp = exp;
        this.kcFinist = kcFinist;
        this.itemOrders = itemOrders;
    }
}
