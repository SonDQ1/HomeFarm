using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItemMake : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] LoadDialogNhamay loadDialogNhamay;
    Vector3 vitricu;
    public int idsp;
    public ScrollRect scrollRect;

    public void OnDrag(PointerEventData eventData)
    {
        if(!Until.checkInstanItemMake)
            scrollRect.OnDrag(eventData);
        float x = vitricu.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float y = Mathf.Abs(vitricu.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Debug.Log("OnDrag = "+x);
        if (!Until.checkInstanItemMake)
        {
            //Debug.Log("x = " + x);
            if (y > 0.5f && !gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                Until.checkInstanItemMake = true;
                Until.itemMakeBtn = gameObject.GetComponent<ItemMakeBtn>();
                GameManagerControll.instance._sinhObjMake(gameObject.transform.position, idsp, idsp);
                scrollRect.OnEndDrag(eventData);
            }
            //else
            //{
            //    if (x > 0)
            //        loadDialogNhamay.dragScrollHo(1);
            //    else loadDialogNhamay.dragScrollHo(-1);
            //}
        }
        if (PlayerPrefsController.instance.checkHuongdan())
            GameManagerControll.instance.disbaleHandMap();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        vitricu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.OnEndDrag(eventData);
    }
}