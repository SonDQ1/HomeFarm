using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownOdatkhoa : MonoBehaviour {

    float mouseTime;
    Vector3 vitricu;
    public int levelUnlock;
    public string nameLodat;
    bool click;
    private void OnMouseDown()
    {
        if (!Until.showPanel)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false)
            {
                mouseTime = Time.time;
                vitricu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("MouseDownLuoi");
            }
        }
    }
    private void OnMouseUp()
    {
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false
                && Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
                && Time.time - mouseTime < 0.2f && gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                if(PlayerPrefsController.instance.getLevel() >= levelUnlock)
                {
                    if (PlayerPrefsController.instance.checkHuongdan())
                    {
                        if (PlayerPrefsController.instance.getBuocHuongdan() == 4 && levelUnlock == 2)
                        {
                            Until.nameLodat = nameLodat;
                            GameManagerControll.instance.setDialogMokhoaOdat(getCoinBuyOdatByLevel(), getGemBuyOdatByLevel());
                            GameManagerControll.instance._showDialog(4);
                            GameManagerControll.instance.objOdatKhoaclick = gameObject;
                            GameManagerControll.instance.activeHandMap(2);
                        }
                    }
                    else
                    {
                        Until.nameLodat = nameLodat;
                        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                        GameManagerControll.instance.setDialogMokhoaOdat(getCoinBuyOdatByLevel(), getGemBuyOdatByLevel());
                        GameManagerControll.instance.objOdatKhoaclick = gameObject;
                        GameManagerControll.instance._showDialog(4);
                    }
                }
                else
                    if (!click)
                {
                    StartCoroutine(delayClick());
                    GameManagerControll.instance.showObjText(gameObject.transform.position,
                        Until.checkLanguage() ? "Mở khóa cấp " + levelUnlock : "Unlock level " + levelUnlock, false);
                }
            }
        }
    }
    int getCoinBuyOdatByLevel()
    {
        if (PlayerPrefsController.instance.getSlodatkhoa(nameLodat) >= 1)
            return (PlayerPrefsController.instance.getSlodatkhoa(nameLodat) +1) * 20;
        return 20;
    }

    int getGemBuyOdatByLevel()
    {
        return PlayerPrefsController.instance.getSlodatkhoa(nameLodat) + 1;
    }

    IEnumerator delayClick()
    {
        click = true;
        yield return new WaitForSeconds(1f);
        click = false;
    }

    #region camview
    public void _camView()
    {
        Camera.main.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f, -40), 0.5f);
    }
    #endregion
}
