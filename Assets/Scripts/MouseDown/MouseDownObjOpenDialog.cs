using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownObjOpenDialog : MonoBehaviour
{

    float mouseTime;
    Vector3 vitricu;
    public int idDialog;
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
                if (PlayerPrefsController.instance.checkHuongdan())
                {
                    if(PlayerPrefsController.instance.getBuocHuongdan() == 2 && idDialog == 2)
                    {
                        GameManagerControll.instance._showDialog(idDialog);
                    }
                }else
                {
                    if (idDialog == 2)
                        OrderController.instance.load();
                    GameManagerControll.instance._showDialog(idDialog);
                }
            }

        }
    }
}
