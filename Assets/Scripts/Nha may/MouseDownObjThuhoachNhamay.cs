using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownObjThuhoachNhamay : MonoBehaviour {

    public NhamayControll nhamayControll;
    public GiengController giengController;
    public OdatControll odatControll;
    public PetsControll petsControll;
    public int id;
    float mouseTime;
    Vector3 vitricu;
    public bool checkXeng;

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
                if(nhamayControll != null)
                    nhamayControll.thuhoach(id);
                if (giengController != null)
                    giengController.thuhoach();
                if (odatControll != null)
                    if (checkXeng)
                        odatControll.xoaCayll();
                    else
                        odatControll.chotuoinuoc();
                if (petsControll != null)
                    petsControll.thuhoach();
            }
        }
    }
}
