using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragLuoiliem : MonoBehaviour, IDragHandler
{
    bool sinhLuoiliem;
    public bool thucan;
    public bool tuoinuoc;
    public bool vatpham;
    public bool xoacay;
    public int idtuita;
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("DragLuoiliem");
        if (thucan)
        {
            GameManagerControll.instance._sinhtuithucan(idtuita, gameObject.transform.position);
        }
        else
        if (tuoinuoc)
            GameManagerControll.instance._sinhBinhTuoinuoc(gameObject.transform.position);
        else
            if(vatpham)
                GameManagerControll.instance._sinhVatpham(idtuita, gameObject.transform.position);
        else
            if (xoacay)
                GameManagerControll.instance._sinhXengxoacay(gameObject.transform.position);
        else
            GameManagerControll.instance._sinhluoiliemthuhoach(gameObject.transform.position);

        if (PlayerPrefsController.instance.checkHuongdan() && !thucan)
            GameManagerControll.instance.destroyHand();
        if (!thucan)
            Destroy(gameObject);
    }
}
