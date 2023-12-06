using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DonghoGame : MonoBehaviour
{
    public Text txtTime, txtKcpt, txtSpeedTime;
    public Image imgLoad;
    public long timeCay;
    long time;
    bool khoa;

    private void Start()
    {
        imgLoad.fillAmount = getPercen(timeCay, time);
        txtSpeedTime.text = PlayerPrefsController.instance.getVpKho(GameManagerControll.instance.idSpeedTime) + "";
    }

    public void _Dem(long timeLife)
    {
        if (khoa == false)
        {
            khoa = true;
            time = timeLife;
            StartCoroutine(demthoigian());
            Invoke("tat", 5f);
        }
    }
    IEnumerator demthoigian()
    {
        int h = Mathf.RoundToInt(time / 3600);
        int m = Mathf.RoundToInt((time % 3600) / 60);
        int s = Mathf.RoundToInt((time % 3600) % 60);
        if (h > 0)
            txtTime.text = h + "h" + m + "m" + s + "s";
        else
            if (m > 0)
            txtTime.text = m + "m" + s + "s";
        else
            txtTime.text = s + "s";

        txtKcpt.text = GameManagerControll.instance.kcptObj + "";
        imgLoad.fillAmount = getPercen(timeCay, time);
        yield return new WaitForSeconds(1f);
        time--;
        if (time > 0)
            StartCoroutine(demthoigian());
    }

    void tat()
    {
        //StopCoroutine(dongho);
        khoa = false;
        gameObject.SetActive(false);
    }

    //===========================dùng kc===========================
    public void _btnBuyClick()
    {
        tat();
        GameManagerControll.instance.buyKcpt();
        time = 0;
    }

    public void _btnSpeedTimeClick()
    {
        if (PlayerPrefsController.instance.getVpKho(GameManagerControll.instance.idSpeedTime) > 0)
        {
            tat();
            GameManagerControll.instance.speedTime();
        }
        else
        {
            GameManagerControll.instance.showObjText(Until.centerCamera(), Until.checkLanguage() ? "Không đủ thuốc tăng trưởng" : "Not enough growth pills", false);
        }
    }

    float getPercen(long timeLife, long timeDachay)
    {
        return (float)timeDachay / timeLife;
    }
}
