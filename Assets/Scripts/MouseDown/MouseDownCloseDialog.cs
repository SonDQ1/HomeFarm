using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MouseDownCloseDialog : MonoBehaviour {

    public bool checkShowPanel;
    public bool dialogMoodat;
    public bool dialogMoosx;
    public bool dialogSanxuatthucan;
    private void OnMouseDown()
    {
        if(!Until.checkShowDialogChild)
            btnCloseClick();
    }
    private void OnEnable()
    {
        if (PlayerPrefsController.instance.checkHuongdan())
        {
            GameManagerControll.instance.destroyHand();
        }
    }
    public void btnCloseClick()
    {
        if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() != 3 
            && PlayerPrefsController.instance.getBuocHuongdan() != 6 
            && PlayerPrefsController.instance.getBuocHuongdan() != 7)
        {
            GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
                Until.checkLanguage() ? "Hãy hoàn thành hướng dẫn" : "Please complete the tutorial", true);
        }
        else
         if (dialogMoodat)
        {
            GameManagerControll.instance.btnNoMoodatClick(gameObject);
        }else
            StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, checkShowPanel));

        if (PlayerPrefsController.instance.checkHuongdan() && dialogSanxuatthucan)
        {
            PlayerPrefsController.instance.setBuocHuongdan(7);
        }

        if (dialogMoosx)
        {
            Until.checkShowDialogChild = false;
        }
    }

    public void btnYesExitClick()
    {
        SceneManager.LoadScene("Thank");
        if (PlayerPrefsController.instance.checkHuongdan())
        {
            PlayerPrefsController.instance.setThoatHD(1);
        }
    }
}
