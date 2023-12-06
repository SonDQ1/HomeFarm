using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogDaily : MonoBehaviour {

    [SerializeField] GameObject[] arBtnDaily;
    [SerializeField] int[] rewardDay;
    int day = 0;
    int month = 0;
    int year = 0;
    void Start () {
        DateTime datetime = DateTime.Now;
        day = datetime.Day;
        month = datetime.Month;
        year = datetime.Year;

        Until.showPanel = true;
        ////nếu cách 1 ngày thì quay lại ngày next là 0
        //if(day - PlayerPrefsController.instance.getNhanthuongngay()>1)
        //{
        //    PlayerPrefsController.instance.setDayNextThuongngay(0);
        //    PlayerPrefsController.instance.setThuongNgay(0);
        //    PlayerPrefsController.instance.setDanhanThuongngay(0);
        //}

        for (int i = 0; i < arBtnDaily.Length; i++)
        {
            arBtnDaily[i].GetComponent<CanvasGroup>().alpha = 0.7f;
            arBtnDaily[i].GetComponent<Button>().enabled = false;
        }
        arBtnDaily[PlayerPrefsController.instance.getDayNextThuongngay()].GetComponent<CanvasGroup>().alpha = 1f;
        arBtnDaily[PlayerPrefsController.instance.getDayNextThuongngay()].GetComponent<Button>().enabled = true;
    }

    public void btnDailyClick(int id)
    {
        Debug.Log("btnDailyClick: "+id);
        GameManagerControll.instance.destroyHand();
        Vector3 centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (!PlayerPrefsController.instance.checkDayDaNhan(PlayerPrefsController.instance.getDayNextThuongngay()))
        {
            switch (id)
            {
                case 0:
                    PlayerPrefsController.instance.addCoin(rewardDay[id]);
                    GameManagerControll.instance.showObjAddCGD(arBtnDaily[id].transform.position, rewardDay[id], "coint", true);
                    break;
                case 1:
                    PlayerPrefsController.instance.addDiamod(rewardDay[id]);
                    GameManagerControll.instance.showObjAddCGD(arBtnDaily[id].transform.position, rewardDay[id], "diamod", true);
                    break;
                case 2:
                    PlayerPrefsController.instance.addGem(rewardDay[id]);
                    GameManagerControll.instance.showObjAddCGD(arBtnDaily[id].transform.position, rewardDay[id], "gem", true);
                    break;
                case 3:
                    PlayerPrefsController.instance.addCoin(rewardDay[id]);
                    GameManagerControll.instance.showObjAddCGD(arBtnDaily[id].transform.position, rewardDay[id], "coint", true);
                    break;
                case 4:
                    PlayerPrefsController.instance.addDiamod(rewardDay[id]);
                    GameManagerControll.instance.showObjAddCGD(arBtnDaily[id].transform.position, rewardDay[id], "diamod", true);
                    break;
                case 5:
                    PlayerPrefsController.instance.addGem(rewardDay[id]);
                    GameManagerControll.instance.showObjAddCGD(arBtnDaily[id].transform.position, rewardDay[id], "gem", true);
                    break;
                case 6:
                    PlayerPrefsController.instance.addDiamod(rewardDay[id]);
                    GameManagerControll.instance.showObjAddCGD(arBtnDaily[id].transform.position, rewardDay[id], "diamod", true);
                    break;
            }

            //PlayerPrefsController.instance.setDanhanThuongngay(day);
            PlayerPrefsController.instance.setDayDaNhan(id, 1);
            PlayerPrefsController.instance.setDayNextThuongngay(id+1);
            PlayerPrefsController.instance.setDayGame(day, month, year);
            StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));
        }
        else
            GameManagerControll.instance.showObjText(centerScreen,
                Until.checkLanguage()?"Đã nhận thưởng": "Received reward", true);
    }
}
