using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemThanhtich : MonoBehaviour
{
    //lưu id - position sinh ra trong contenthanhtich
    public int pos;
    public int idthanhtich;
    public DialogNhachinh dialogNhachinh;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => dialogNhachinh.btnThanhTichClick(pos,idthanhtich));
    }
}
