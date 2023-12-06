using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GiengController : MonoBehaviour
{
    public int idgieng;
    public int idSp;
    public int levelUnlock;
    string trangthai = "phattrien";
    public int timeDachay, timeClock, timeStart;
    [SerializeField] GameObject objChothuhoach;
    void Start()
    {
        if (gameObject.GetComponent<MoveObj>().checkInstanceObj)
        {
            //mới mua sẽ phát triển giếng luôn
            PlayerPrefsController.instance.setTimeStarGieng(idgieng, Until.timeNow());
            timeStart = PlayerPrefsController.instance.getTimeStarGieng(idgieng);
        }
        ptGieng();
    }

    #region onmouse down
    Vector3 vitricu;
    float mouseTime;
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject(0) == false && !Until.showPanel)
        {
            vitricu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseTime = Time.time;
        }
    }

    public void OnMouseUp()
    {
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
            if (EventSystem.current.IsPointerOverGameObject(0) == false &&
    Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
        && Time.time - mouseTime < 0.2f)
        {
            //check trạng thái cho thu hoạch hoạc show time
            GameManagerControll.instance.giengController = this;
            if (trangthai == "phattrien")
            {
                GameManagerControll.instance.nameConvat = Until.checkLanguage() ? "Giếng nước" : "Wells";
                GameManagerControll.instance.showCanvasTime(gameObject, timeClock, true);
            }
            else
            if (trangthai == "thuhoach" && checkThuhoach)
            {
                thuhoach();
            }
        }
    }
    #endregion

    #region function btnBuy -- change
    public int getKcpt()
    {
        int timept = GameManagerControll.instance.dataNongSan.data[idSp].timept;
        int kcpt = GameManagerControll.instance.dataNongSan.data[idSp].kcpt;
        float kc = 1;
        if (timeClock > timept * 0.7f)
            kc = kcpt;
        if (timeClock <= timept * 0.7f && timeClock > timept * 0.5f)
            kc = kcpt * 0.7f;
        if (timeClock <= timept * 0.5f && timeClock > timept * 0.3f)
            kc = kcpt * 0.5f;
        if (timeClock <= timept * 0.3f)
            kc = kcpt * 0.3f;
        if (kc < 1) kc = 1;
        return (int)kc;
    }

    public void buyKcpt()
    {
        int kc = getKcpt();
        if (kc <= PlayerPrefsController.instance.getDiamod())
        {
            PlayerPrefsController.instance.exceptDiamod(kc);
            PlayerPrefsController.instance.setTimeStarGieng(idgieng,0);
            timeClock = 0;
            StartCoroutine(chayPtGieng());
        }
        else
            GameManagerControll.instance.showObjText(gameObject.transform.position,
                Until.checkLanguage() ? "Không đủ kim cương" : "Not enough diamond", false);
    }
    public void speedTime()
    {
        int timespeed = (int)(timeClock * Until.speedtime);
        if (timespeed < Until.timeminspeed)
            timespeed = Until.timeminspeed;

        timeClock -= timespeed;

        Debug.Log("speedTime = " + (int)(timeClock * Until.speedtime));
        if (timeClock < 0) timeClock = 0;
        StartCoroutine(chayPtGieng());
    }
    public void changePosition(int idluoicu, int idluoimoi)
    {
        PlayerPrefsController.instance.setTimeStarGieng(idluoimoi, PlayerPrefsController.instance.getTimeStarGieng(idluoicu));
        //xóa dl
        PlayerPrefsController.instance.setTimeStarGieng(idluoicu, 0);
    }
    #endregion

    #region chạy phát triển
    public void ptGieng()
    {
        timeStart = PlayerPrefsController.instance.getTimeStarGieng(idgieng);
        timeDachay = Until.timeNow() - timeStart;
        if (timeDachay < 0)
            timeClock = 0;
        else
            timeClock = GameManagerControll.instance.dataNongSan.data[idSp].timept - timeDachay;

        StartCoroutine(chayPtGieng());
    }
    IEnumerator chayPtGieng()
    {
        if (timeClock <= 0)
        {
            trangthai = "thuhoach";
            checkThuhoach = true;
            sinhObjChothuhoach();
        }
        else trangthai = "phattrien";
        yield return new WaitForSeconds(1f);
        if (timeClock > 0)
        {
            timeClock--;
            StartCoroutine(chayPtGieng());
        }
    }
    #endregion

    #region thu hoạch
    GameObject objchothuhoach;
    bool checkThuhoach = true;
    public void thuhoach()
    {
        Destroy(objchothuhoach);
        checkThuhoach = false;
        StartCoroutine(delayRandomItem());
    }

    IEnumerator delayRandomItem()
    {
        GameManagerControll.instance.playEff(1, gameObject.transform.position);
        GameManagerControll.instance._sinhObjThuhoach(gameObject, idSp, "nhamay");
        PlayerPrefsController.instance.addKho(idSp, 2);
        yield return new WaitForSeconds(1f);
        int rd = Random.Range(1, 3);
        if (rd == 2)
        {
            GameManagerControll.instance.playEff(1, gameObject.transform.position);
            GameManagerControll.instance._sinhObjThuhoach(gameObject, idSp, "nhamay");
            PlayerPrefsController.instance.addKho(idSp, 2);
        }
        //tiếp tục chạy
        yield return new WaitForSeconds(0.5f);
        PlayerPrefsController.instance.setTimeStarGieng(idgieng, Until.timeNow());
        ptGieng();
    }

    void sinhObjChothuhoach()
    {
        if (objchothuhoach != null)
            Destroy(objchothuhoach);
        Vector2 pos = transform.position;
        pos.y -= 0.2f;
        objchothuhoach = Instantiate(objChothuhoach, pos, Quaternion.identity, transform) as GameObject;
        objchothuhoach.transform.GetChild(0).GetChild(0).GetComponent<MouseDownObjThuhoachNhamay>().giengController = this;
        objchothuhoach.GetComponent<SPChothuhoach>().idSpgieng = idSp;
        objchothuhoach.GetComponent<SPChothuhoach>().tagNhamay = gameObject.tag;
        objchothuhoach.GetComponent<SPChothuhoach>().checkGieng = true;
    }
    #endregion

}
