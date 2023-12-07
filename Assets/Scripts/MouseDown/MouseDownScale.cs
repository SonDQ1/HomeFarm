using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownScale : MonoBehaviour
{

    float mouseTime;
    Vector3 vitricu;
    float scale = 1.05f;

    [SerializeField] GameObject dialogShop;
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
                if (!PlayerPrefsController.instance.checkHuongdan())
                {
                    SoundController.instance._playSound(0);
                    //Debug.Log("MouseDownScale "+gameObject.name);
                    scale = gameObject.transform.localScale.x;
                    if (scale < 0) scale = -1.05f;
                    else scale = 1.05f;
                    iTween.ScaleTo(this.gameObject, iTween.Hash("scale", new Vector3(scale, Mathf.Abs(scale), 0), "time", 0.05f, "easetype", iTween.EaseType.linear));
                    StartCoroutine(scaleSmall());
                    //click cây + exp
                    if (gameObject.name == "nhapets")
                    {
                        //click nhà pets show shop pets
                        dialogShop.SetActive(true);
                        Until.showPanel = true;
                        dialogShop.GetComponent<ShopController>().btnTabClick(1);
                    }

                    if (gameObject.name == "mouseDown levelup")
                    {
                        GameManagerControll.instance.LevelUp(PlayerPrefsController.instance.getLevel() + 1);
                        PlayerPrefsController.instance.addCoin(1000);
                        PlayerPrefsController.instance.addGem(50);
                        PlayerPrefsController.instance.addDiamod(10);

                        for (int i = 0; i < 20; i++)
                            PlayerPrefsController.instance.addKho(i, 5);

                        PlayerPrefsController.instance.addKho(GameManagerControll.instance.idCua, 10);
                        PlayerPrefsController.instance.addKho(GameManagerControll.instance.idKeo, 10);
                        PlayerPrefsController.instance.addKho(GameManagerControll.instance.idXeng, 10);
                        PlayerPrefsController.instance.addKho(GameManagerControll.instance.idThuocno, 10);
                    }
                }
            }
        }
    }

    IEnumerator scaleSmall()
    {
        if (scale < 0) scale = -1f;
        else scale = 1;
        yield return new WaitForSeconds(0.1f);
        iTween.ScaleTo(this.gameObject, iTween.Hash("scale", new Vector3(scale, Mathf.Abs(scale), 0), "time", 0.1f, "easetype", iTween.EaseType.linear));
    }
}
