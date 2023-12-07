using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownConvat : MonoBehaviour
{

    public int idLuoi;
    [SerializeField] string nameV, nameE;
    public int idConvat;
    [SerializeField] int timept;
    [SerializeField] int kcpt;
    [SerializeField] Sprite thucan;
    public int idthucan;
    public Sprite sanpham;
    public int idSP;

    [SerializeField] GameObject objhothuhoach;
    [SerializeField] ChuongController chuongController;
    float mouseTime;
    Vector3 vitricu;
    GameObject spThucan;

    public string trangthai = "null";

    private void Start()
    {
        idLuoi = chuongController.idChuong;
        spThucan = transform.GetChild(1).gameObject;
        loadConvat();
    }

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
    public void playSoundAnimal()
    {
        switch (nameE)
        {
            case "Chicken":
                SoundController.instance._playSound(5);
                break;
            case "Pig":
                SoundController.instance._playSound(6);
                break;
            case "Cows":
                SoundController.instance._playSound(7);
                break;
            case "Sheep":
                SoundController.instance._playSound(8);
                break;
            case "Broiler":
                SoundController.instance._playSound(23);
                break;
            case "Goat":
                SoundController.instance._playSound(24);
                break;
            case "Brown beef":
                SoundController.instance._playSound(25);
                break;
        }
    }
    private void OnMouseUp()
    {
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false && Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
                && Time.time - mouseTime < 0.2f)
            {
                playSoundAnimal();
                //Debug.Log("trangthai "+gameObject.name+"= " + trangthai);
                //Debug.Log("Cho ăn= " + PlayerPrefsController.getChoan(idLuoi, idConvat));
                GameManagerControll.instance.mouseDownConvat = gameObject.GetComponent<MouseDownConvat>();
                if (trangthai == "phattrien")
                {
                    //show đồng hồ
                    checkThuhoach = false;
                    GameManagerControll.instance.kcptObj = kcpt;
                    GameManagerControll.instance.timeptConvat = timept;
                    GameManagerControll.instance.nameConvat = Until.checkLanguage() ? nameV : nameE;
                    GameManagerControll.instance.showCanvasTime(gameObject, timeClock, true);
                }
                else
                    if (trangthai == "thuhoach" && checkThuhoach)
                {
                    thuhoach();
                }
                else
                    chuongController.showCanvasTA();

            }
        }
    }

    #region Trồng cây, phát triển

    public int timeDachay, timeClock, timeStart;
    public bool checkThuhoach;
    //control con vat
    public void _choAn()
    {
        //Debug.Log("Cho an: " + idLuoi + idConvat);
        //sinh obj túi thức ăn
        chuongController.sinhTuiThucAn(gameObject.transform.position, thucan);
        PlayerPrefsController.instance.setTimeStartConvat(idLuoi, idConvat, Until.timeNow());
        PlayerPrefsController.instance.setChoan(idLuoi, idConvat, 1);
        gameObject.GetComponent<SkeletonAnimation>().AnimationName = "eat";
        convatPt();
    }
    //con vật phát triển
    void convatPt()
    {
        trangthai = "phattrien";

        timeStart = PlayerPrefsController.instance.getTimeStartConvat(idLuoi, idConvat);
        timeDachay = Until.timeNow() - timeStart;
        if (timeDachay < 0)
            timeClock = 0;
        else
            timeClock = timept - timeDachay;

        //thức ăn trong ô chứa
        spThucan.SetActive(true);
        StartCoroutine(chayPt());
    }

    IEnumerator chayPt()
    {
        if (timeClock > 0)
        {
            if(GetComponent<SkeletonAnimation>().AnimationName != "eat")
                gameObject.GetComponent<SkeletonAnimation>().AnimationName = "eat";
        }
        else
        {
            gameObject.GetComponent<SkeletonAnimation>().AnimationName = "stand";
            //thức ăn trong ô chứa
            spThucan.SetActive(false);

            trangthai = "thuhoach";
            checkThuhoach = true;
            objhothuhoach.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        if (timeClock > 0)
        {
            timeClock--;
            StartCoroutine(chayPt());
        }
    }
    public int getKcpt()
    {
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
        //check kc

        if (kc <= PlayerPrefsController.instance.getDiamod())
        {
            PlayerPrefsController.instance.exceptDiamod(kc);
            PlayerPrefsController.instance.setTimeStartConvat(idLuoi, idConvat, 0);
            timeClock = 0;
            StartCoroutine(chayPt());
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
        StartCoroutine(chayPt());
    }
    //thuhoach
    public void thuhoach()
    {
        checkThuhoach = false;
        StartCoroutine(delayTrangthai());
        objhothuhoach.SetActive(false);

        GameManagerControll.instance.mouseDownConvat = gameObject.GetComponent<MouseDownConvat>();
        GameManagerControll.instance.playEff(1, gameObject.transform.position);
        GameManagerControll.instance._sinhObjThuhoach(gameObject, 0, "convat");
        PlayerPrefsController.instance.setTimeStartConvat(idLuoi, idConvat, 0);
        PlayerPrefsController.instance.setChoan(idLuoi, idConvat, 0);
        PlayerPrefsController.instance.addKho(idSP, 1);
        //Debug.Log("Thu hoạch");
    }

    IEnumerator delayTrangthai()
    {
        yield return new WaitForSeconds(1f);
        trangthai = "null";
    }
    #endregion

    #region load con vật
    void loadConvat()
    {
        if (PlayerPrefsController.instance.getChoan(idLuoi, idConvat) == 1)
        {
            convatPt();
        }
    }
    #endregion

    #region change con vật
    public void changeConvat(int idluoicu, int idluoimoi)
    {
        PlayerPrefsController.instance.setTimeStartConvat(idluoimoi, idConvat, PlayerPrefsController.instance.getTimeStartConvat(idluoicu, idConvat));
        PlayerPrefsController.instance.setChoan(idluoimoi, idConvat, PlayerPrefsController.instance.getChoan(idluoicu, idConvat));

        //xóa dl
        PlayerPrefsController.instance.setTimeStartConvat(idluoicu, idConvat, 0);
        PlayerPrefsController.instance.setChoan(idluoicu, idConvat, 0);
    }
    #endregion

    #region Va chạm túi thức ăn -> cho ăn
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("tuithucan") && trangthai == "null")
        {
            //Debug.Log("OnTriggerStay2D " + gameObject.name);
            //cho ăn
            if (collision.GetComponent<HatgiongForllow>().idHatgiong == idthucan)
            {
                if (PlayerPrefsController.instance.getVpKho(idthucan) >= 1)
                {
                    PlayerPrefsController.instance.removeVpKho(idthucan, 1);
                    _choAn();
                }
                else
                if (!checkShowText)
                {
                    StartCoroutine(delayShowText());
                    collision.GetComponent<CircleCollider2D>().enabled = false;
                    GameManagerControll.instance.showObjText(gameObject.transform.position,
                        Until.checkLanguage() ? "Không đủ thức ăn" : "Not enough food", false);
                }
            }
        }
    }
    bool checkShowText;
    IEnumerator delayShowText()
    {
        checkShowText = true;
        yield return new WaitForSeconds(1.5f);
        checkShowText = false;
    }
    #endregion
}
