using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMakeBtn : MonoBehaviour {
    [Header("Nhập dữ liệu vào để show")]
    public ItemOrder[] itemNedd;

    public bool checkMake()
    {
        bool checkItem0, checkItem1, checkItem2;
        if (itemNedd[0].soluong > 0)
        {
            if (itemNedd[0].soluong <= PlayerPrefsController.instance.getVpKho(itemNedd[0].idVatphams))
                checkItem0 = true;
            else
                checkItem0 = false;
        }
        else checkItem0 = true;

        if (itemNedd[1].soluong > 0)
        {
            if (itemNedd[1].soluong <= PlayerPrefsController.instance.getVpKho(itemNedd[1].idVatphams))
                checkItem1 = true;
            else
                checkItem1 = false;
        }
        else checkItem1 = true;
        if (itemNedd[2].soluong > 0)
        {
            if (itemNedd[2].soluong <= PlayerPrefsController.instance.getVpKho(itemNedd[2].idVatphams))
                checkItem2 = true;
            else
                checkItem2 = false;
        }
        else checkItem2 = true;

        return checkItem0 && checkItem1 && checkItem2;
    }

    public void removeSPMake()
    {
        for(int i=0; i < itemNedd.Length; i++)
        {
            if (itemNedd[i].soluong > 0)
            {
                PlayerPrefsController.instance.removeVpKho(itemNedd[i].idVatphams, itemNedd[i].soluong);
            }
        }
    }
}
