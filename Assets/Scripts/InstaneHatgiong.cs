using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems  ;
using UnityEngine.UI;

public class InstaneHatgiong : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler,IEndDragHandler
{

    Vector3 vitricu;
    [SerializeField] int idhatgiong;
    public int levelUnlock;
    public ScrollRect scrollRect;

    public void OnDrag(PointerEventData eventData)
    {
        scrollRect.OnDrag(eventData);
        float x = vitricu.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float y = Mathf.Abs(vitricu.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Debug.Log("OnDrag = "+x);
        if (!Until.checkInstanHatgiong)
        {
            if (y > 0.3f && y < 2f && !gameObject.transform.GetChild(2).gameObject.activeInHierarchy)
            {
                Until.checkInstanHatgiong = true;
                GameManagerControll.instance._sinhhatgiong(gameObject.transform.position,idhatgiong);
                scrollRect.OnEndDrag(eventData);
            }
            //else
            //    if (x > 0)
            //    DialogCaytrong.instance.dragScrollHo(1);
            //else DialogCaytrong.instance.dragScrollHo(-1);
        }
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
