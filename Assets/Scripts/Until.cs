using BitBenderGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Until : MonoBehaviour {

    public static bool checkLanguage() {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
            return true;
        else
            return false;
    }
    public static bool onSound = true;

    public static bool checkThuhoachlua = false;

    public static bool showPanel = false;
    public static bool checkInstanHatgiong;
    public static bool checkInstanItemMake;
    public static int doubleClick = 0;
    public static int maxLevel = 50;

    public static int idItemMake = 0;
    public static int idNhamayClick = 0;
    public static float speedtime = 0.5f;
    public static int timeminspeed = 30;
    public static string tagNhamayClick = "";

    //public static int chat = 0;
    //public static bool checkHuongdan = false;
    //public static int huongdan = 1;

    public static int timeNow()
    {
        DateTime datetime = DateTime.Now;
        int month = datetime.Month;
        int day = datetime.Day;
        int hour = datetime.Hour;
        int minute = datetime.Minute;
        int second = datetime.Second;
        int TimeNow = month * 30 * 24 * 60 * 60 + (day * 24 * 60 * 60) + (hour * 60 * 60) + (minute * 60) + second;
        return TimeNow;
    }

    //public static List<itemcho> listMake;
    //public static Coroutine coroutinePtsp;
    public static NhamayControll nhamayControll;
    public static ChuongController chuongController;
    public static ItemMakeBtn itemMakeBtn;

    public static bool checkInstanceObj = false;
    public static bool moveObj = false;
    public static bool checkShowDialogChild = false;

    public static string nameLodat = "Odat dat";
    public static string tagOdat = "odat";
    public static void showQc()
    {
        try
        {
            AdApplovinController.Instance.ShowRewardedAd(null);
        }
        catch { }
    }

    public static bool checkTagNhamay(string tag)
    {
        switch (tag)
        {
            case "nhamay":
            case "maythucan":
            case "maysua":
            case "lonuong":
            case "douong":
            case "thomay":
            case "nuongthit":
            case "daubep":
            case "thucong":
                return true;
        }

        return false;
    }

    public static float getSize()
    {
        float sizeZoom = Camera.main.GetComponent<Camera>().orthographicSize;
        float size = 1f;
        if (sizeZoom > 3 && sizeZoom < 4) size = 1.3f;
        else if (sizeZoom >= 4 && sizeZoom <= 5) size = 1.6f;
        return size;
    }
    public static Vector2 centerCamera()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
    }
}
