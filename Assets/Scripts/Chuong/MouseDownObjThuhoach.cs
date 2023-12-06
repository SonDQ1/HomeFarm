using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownObjThuhoach : MonoBehaviour
{

    public MouseDownConvat mouseDownConvat;
    public PetsControll petsControll;
    float mouseTime;
    Vector3 vitricu;

    private void OnMouseDown()
    {
        if (!Until.showPanel)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false)
            {
                mouseTime = Time.time;
                vitricu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
    private void OnMouseUp()
    {
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false
                && Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
                && Time.time - mouseTime < 0.2f)
            {
                if(mouseDownConvat)
                    mouseDownConvat.thuhoach();
                if (petsControll)
                    petsControll.thuhoach();
            }
        }
    }
}
