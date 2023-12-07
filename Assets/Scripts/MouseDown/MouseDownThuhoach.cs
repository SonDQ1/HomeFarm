using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDownThuhoach : MonoBehaviour {

    [SerializeField] OdatControll odatControll;

    private void OnMouseDown()
    {
        odatControll.OnMouseDown();
    }
    private void OnMouseUp()
    {
        odatControll.OnMouseUp();
    }
}
