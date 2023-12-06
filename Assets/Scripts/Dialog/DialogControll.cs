using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogControll : MonoBehaviour {

    [SerializeField] GameObject dialog;
    [SerializeField] GameObject handBtnShop;
    public bool btnShop;
    public GameManagerControll gameManager;

    public void enableDialog()
    {
        if (!Until.checkInstanceObj && !Until.moveObj)
        {
            if (PlayerPrefsController.instance.checkHuongdan())
            {
                GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
                Until.checkLanguage() ? "Hãy hoàn thành hướng dẫn" : "Please complete the tutorial", true);
                if (btnShop)
                {
                    handBtnShop.SetActive(false);
                    if (PlayerPrefsController.instance.getBuocHuongdan() == 5)
                    {
                        GameManagerControll.instance.hidenButton(true);
                        Until.showPanel = true;
                        MobileTouchCamera.checkCamFollow = true;
                        dialog.SetActive(true);
                        GameManagerControll.instance.destroyHand();
                    }

                    //mua chuồng
                    if (PlayerPrefsController.instance.getBuocHuongdan() == 7 || PlayerPrefsController.instance.getBuocHuongdan() == 8)
                    {
                        GameManagerControll.instance.hidenButton(true);
                        Until.showPanel = true;
                        MobileTouchCamera.checkCamFollow = true;
                        dialog.SetActive(true);
                        GameManagerControll.instance.destroyHand();
                    }
                }
            }
            else
            {
                GameManagerControll.instance.hidenButton(true);
                Until.showPanel = true;
                MobileTouchCamera.checkCamFollow = true;
                dialog.SetActive(true);
            }
        }
        else
            GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
                Until.checkLanguage() ? "Chưa hoàn thành đặt vật" : "Unfinished set", true);
    }

    public void closeDialog()
    {
        StartCoroutine(GameManagerControll.instance.delayCloseDialog(dialog, false));
    }
}
