using BitBenderGames;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManagerControll : MonoBehaviour
{

    #region declare
    public static GameManagerControll instance;
    [Header("======UI=====")]
    [SerializeField] Text[] arTxtUi;
    [SerializeField] Image fillExp;

    [SerializeField] GameObject btnSetting, btnShop, btnRewards;

    [SerializeField] GameObject panelHatgiong;
    [SerializeField] GameObject[] barHatgiong;
    [SerializeField] GameObject[] loaiHatGiong;

    [Header("=====Data=====")]
    public DataNongSan dataNongSan;
    public int idStarTA, idEndTA;
    public int idBinhtuoinuoc;
    public int idCua, idThuocno, idXeng, idKeo;
    public int idSpeedTime;
    //[SerializeField] DataTrangtri dataTrangtri;
    [Header("=====Transform chứa obj=====")]
    [SerializeField] Transform tfOdat;
    [SerializeField] Transform tfOdat1;
    [SerializeField] Transform tfOdat2;
    [SerializeField] Transform tfTrangtri;
    [SerializeField] Transform tfNhamay;
    [SerializeField] Transform tfChuong;
    [SerializeField] Transform tfHangrao;
    [SerializeField] Transform tfHouse;
    [SerializeField] Animator animNhakho;

    [Header("=====Dialog show=====")]
    public GameObject[] dialog;
    public GameObject dialogDaily;

    [Header("=====Prefats=====")]
    [SerializeField] GameObject odatTrong;
    [SerializeField] GameObject odat2, odat3;
    [Header("-Canvas:")]
    [SerializeField] GameObject canvasTime;
    [SerializeField] GameObject objThuhoach, objDragTuithucan, objTuoinuoc, objLuoiliem, cvGiothuhoach;
    [Header("-Va chạm thu hoạch:")]
    [SerializeField] GameObject objLuoiliemThuhoach;
    [SerializeField] GameObject objTuithucan, binhtuoinuoc, objGiothuhoach;
    [SerializeField] GameObject objText, objAddCGD;
    [SerializeField] GameObject objMake;
    [SerializeField] GameObject animMoodat;
    public ObjShop[] objNha;
    public ObjShop[] objPes;
    public ObjShop[] objTrangtri;
    public ObjShop[] objWall;
    public ObjShop[] objRoad;
    public Sprite[] spCGD;

    [Header("=====Hiden button show dialog=====")]
    [SerializeField] Animator[] animBtn;
    [Header("Box của panel sản xuất - click show dialog mở thêm ô chứa")]
    [SerializeField] BoxCollider2D[] box2D;

    [Header("=====List Ô đất khóa=====")]
    [SerializeField] List<GameObject> odatKhoa;
    [SerializeField] List<GameObject> odatKhoa1;
    [SerializeField] List<GameObject> odatKhoa2;
    [SerializeField] Transform tfLuoi;
    [SerializeField] Transform objPosPes;
    public int[] targetExp;
    public Transform posKho, posExp, posGem, posCoin, posDiamod;
    [Header("=====Eff=====")]
    [SerializeField] GameObject[] arEff;

    [Header("=====null truyền từ class khác=====")]
    public OdatControll objOdatclick;
    public MouseDownConvat mouseDownConvat;
    public GiengController giengController;
    public MouseDownRac mouseDownRac;
    public GameObject objOdatKhoaclick;
    public MoveObj moveObj;
    public GameObject objInstance;

    [Header("=====Hướng dẫn=====")]
    [SerializeField] GameObject objOdat3;
    [SerializeField] GameObject posTick;
    [SerializeField] GameObject[] handHd;
    [SerializeField] GameObject[] objHandMap;
    [SerializeField] Transform posbtnshop;
    [SerializeField] GameObject dialogNVHD;
    [SerializeField] GameObject[] odatLuathuhoach;
    [Header("=====Thoát hướng dẫn=====")]
    [SerializeField] GameObject objOdat0;
    [SerializeField] GameObject objOrder;
    [SerializeField] GameObject objOdat2;
    [SerializeField] GameObject objOdatkhoa;
    [SerializeField] GameObject objBtnShop;

    [Header("=====Load thành tích=====")]
    [SerializeField] DialogNhachinh dialogNhachinh;
    [SerializeField] GameObject panelBongdem;
    [Header("=====Phá map=====")]
    [SerializeField] Sprite[] spVatpham;
    [SerializeField] GameObject objVatpham, cvPhamap;
    [Header("========Khinh khí cầu===========")]
    [SerializeField] GameObject objKhicau;
    public Transform[] tfKhicau;
    public int timeDelayKhicau;
    int timeDachay, timeClock, timeStart;
    public Transform posPets;

    public int rdvt;
    [Header("================================")]

    float fill;
    #endregion

    #region Start - Update
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        instance = this;
    }

    private void Start()
    {
        //bóng đêm
        //PlayerPrefs.DeleteAll();
        StartCoroutine(delayBongdem());
        //load targetExxp
        targetExp = new int[Until.maxLevel + 1];
        targetExp[0] = 0;
        targetExp[1] = 20;
        targetExp[2] = 30;
        for (int i = 3; i < targetExp.Length; i++)
        {
            if (i < 10) targetExp[i] = targetExp[i - 1] + targetExp[i - 2];
            else
                if (i <= 20)
                targetExp[i] += targetExp[i - 1] + targetExp[i - 7];
            else
                if (i > 20) targetExp[i] = targetExp[i - 1] + 50 * PlayerPrefsController.instance.getLevel();
        }
        if (PlayerPrefsController.instance.getFirstApp())
        {
            PlayerPrefsController.instance.setBuocHuongdan(1);
            PlayerPrefsController.instance.setThoatHD(1);
            //Debug.Log("Hướng dẫn Start=> " + Until.checkHuongdan + Until.huongdan);
            PlayerPrefsController.instance.setDayNextThuongngay(0);
            PlayerPrefsController.instance.setFirstApp();
            PlayerPrefsController.instance.levelUp();
            PlayerPrefsController.instance.addCoin(1000);
            PlayerPrefsController.instance.addGem(30);
            PlayerPrefsController.instance.addDiamod(10);
            PlayerPrefsController.instance.setSlOdat("Odat dat", 6);

            PlayerPrefsController.instance.setVolume("music", 1);
            PlayerPrefsController.instance.setVolume("sound", 1);

            //thêm 1 lúa để chuyển đơn hàng
            PlayerPrefsController.instance.addKho(0, 3);
            PlayerPrefsController.instance.newListOrder();
            fill = 0;
        }
        else
            fill = (float)PlayerPrefsController.instance.getExp() / targetExp[PlayerPrefsController.instance.getLevel()];

        SoundController.instance._changeMusicVolume(PlayerPrefsController.instance.getVolume("music"));
        SoundController.instance._changeSoundVolume(PlayerPrefsController.instance.getVolume("sound"));
        SoundController.instance._playMusicBgr(0, PlayerPrefsController.instance.getVolume("music"));

        //PlayerPrefsController.instance.addDiamod(10);
        //thoát khi chưa hết hướng dẫn
        loadThoatHD();

        loadPhanthuongngay();
        loadUIMain();
        loadOdatKhoa("Odat dat");
        loadOdatKhoa("Odat dat 1");
        loadOdatKhoa("Odat dat 2");
        //load id ô lưới
        loadObjLuoi();

        loadPest();

        //sinh khinh khí cầu quà tặng
        if (!PlayerPrefsController.instance.checkHuongdan())
        {
            chayKhicau();
        }

        //==========Test==========

        //PlayerPrefsController.newListOrder();

        //PlayerPrefsController.instance.addCoin(100000);
        //PlayerPrefsController.instance.addGem(5000);
        //PlayerPrefsController.instance.addDiamod(1000);
        //PlayerPrefs.SetInt("level", 40);

        //PlayerPrefsController.instance.addKho(0, 120);
        //PlayerPrefsController.instance.addKho(0, 50);
        //PlayerPrefsController.instance.addKho(1, 50);
        //PlayerPrefsController.instance.addKho(1, 50);
        //PlayerPrefsController.instance.addKho(idSpeedTime, 10);

    }
    //thoat game
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Application.Quit();
            if (!PlayerPrefsController.instance.checkHuongdan())
            {
                Until.showPanel = true;
                Debug.Log(dialog[10]);
                dialog[10].SetActive(true);
            }
            
        }
    }
    #endregion

    #region Load UI Main
    public void loadUIMain()
    {
        //Debug.Log("loadUIMain==================>>>>");
        arTxtUi[0].text = PlayerPrefsController.instance.getLevel() + "";
        arTxtUi[1].text = PlayerPrefsController.instance.getCoin() + "";
        arTxtUi[2].text = PlayerPrefsController.instance.getGem() + "";
        arTxtUi[3].text = PlayerPrefsController.instance.getDiamod() + "";
        arTxtUi[4].text = PlayerPrefsController.instance.getExp() + "/" + targetExp[PlayerPrefsController.instance.getLevel()];
        loadFillExp();
        AdApplovinController.Instance.ShowBanner();
    }

    void loadFillExp()
    {
        if (PlayerPrefsController.instance.getLevel() >= Until.maxLevel)
            fillExp.fillAmount = 1;
        else
            StartCoroutine(delayFillExp());
    }

    IEnumerator delayFillExp()
    {
        fillExp.fillAmount = fill;
        if (fill < (float)PlayerPrefsController.instance.getExp() / targetExp[PlayerPrefsController.instance.getLevel()])
            fill += 0.01f;
        yield return new WaitForSeconds(0.005f);
        if (fill < (float)PlayerPrefsController.instance.getExp() / targetExp[PlayerPrefsController.instance.getLevel()])
            StartCoroutine(delayFillExp());
    }
    public void hidenUIMain(bool check)
    {
        //btnSetting.GetComponent<Animator>().SetBool("hiden", check);
        btnShop.GetComponent<Animator>().SetBool("hiden", check);
        btnRewards.GetComponent<Animator>().SetBool("hiden", check);
    }
    #endregion

    #region Level up
    public void levelUp()
    {
        OrderController.instance.destroyObjSend();
        closeAllDialog();
        fill = 0;
        fillExp.fillAmount = 0;
        loadFillExp();
        PlayerPrefsController.instance.levelUp();
        if (PlayerPrefsController.instance.getLevel() >= 3)
            Until.showQc();
        _showDialog(5);
    }

    public void closeAllDialog()
    {
        //close all dialog
        for (int i = 0; i < dialog.Length; i++)
        {
            dialog[i].SetActive(false);
        }
    }
    public void LevelUp(int lv)
    {
        int exp = 0;
        if (lv >= 50)
            exp = targetExp[50] - PlayerPrefsController.instance.getExp();
        else exp = targetExp[lv] - PlayerPrefsController.instance.getExp();
        PlayerPrefsController.instance.addExp(exp);
        PlayerPrefsController.instance.setExp(0);
        destroyHand();
        loadUIMain();
    }
    #endregion

    #region sinh hạt giống ô đất
    public void _sinhhatgiong(Vector3 pos, int id)
    {
        MobileTouchCamera.checkCamFollow = true;
        //tắt shop cây trồng
        //_showBarshopcaytrong(false);
        //Debug.Log("Sinh hạt giống: " + id);
        GameObject hat = Instantiate(getHatgiong(), pos, Quaternion.identity) as GameObject;
        hat.GetComponent<SpriteRenderer>().sprite = dataNongSan.data[id].iconMake;
        hat.GetComponents<HatgiongForllow>()[0].idHatgiong = id;
    }
    //map idbtn với ảnh hạt giống
    int getIdHatMapIdbtn(int id)
    {
        if (Until.tagOdat == "odat2")
            return id - 39 + 7;
        if (Until.tagOdat == "odat3")
            return id - 39 + 13;
        return id;
    }

    GameObject getHatgiong()
    {
        if (Until.tagOdat == "odat2")
            return loaiHatGiong[1];
        if (Until.tagOdat == "odat3")
            return loaiHatGiong[2];
        return loaiHatGiong[0];
    }
    public void _showBarshopcaytrong(string odattag, bool check)
    {
        for (int i = 0; i < barHatgiong.Length; i++)
        {
            barHatgiong[i].SetActive(false);
        }
        panelHatgiong.SetActive(check);
        switch (odattag)
        {
            case "odat":
                barHatgiong[0].SetActive(check);
                break;
            case "odat2":
                barHatgiong[1].SetActive(check);
                break;
            case "odat3":
                barHatgiong[2].SetActive(check);
                break;

        }
        hidenButton(check);
        Until.showPanel = check;
        MobileTouchCamera.checkCamFollow = true;
    }

    public void hatgiongclick(int id)
    {
        objOdatclick.trongcay(id);
    }

    #endregion

    #region show dialog - hiden button
    public void _showDialog(int id)
    {
        dialog[id].SetActive(true);
        MobileTouchCamera.checkCamFollow = true;
        Until.showPanel = true;
        hidenButton(true);
    }

    int idPanelCu;
    public void _showDialog(int idNhamay, int id)
    {
        dialog[idNhamay].SetActive(true);
        dialog[idNhamay].transform.GetChild(0).GetChild(idPanelCu).gameObject.SetActive(false);

        dialog[idNhamay].transform.GetChild(0).GetChild(id).gameObject.SetActive(true);
        MobileTouchCamera.checkCamFollow = true;
        Until.showPanel = true;
        hidenButton(true);
        idPanelCu = id;
    }
    public void hidenButton(bool check)
    {
        //disbaleHandMap();
        for (int i = 0; i < animBtn.Length; i++)
        {
            animBtn[i].SetBool("hiden", check);
        }
    }

    public IEnumerator delayCloseDialog(GameObject dialog, bool checkShowpanel)
    {
        dialog.GetComponent<Animator>().SetTrigger("close");
        MobileTouchCamera.checkCamFollow = false;
        yield return new WaitForSeconds(0.5f);
        Until.showPanel = checkShowpanel;
        dialog.SetActive(false);
        hidenButton(false);

        //dialog thêm ô sản xuất
        if (kc != 0)
            setBox2DPanelSx(true);

        //check hướng dẫn trồng cây
        if (PlayerPrefsController.instance.checkHuongdan())
        {
            destroyHand();
            disbaleHandMap();
            if (PlayerPrefsController.instance.getBuocHuongdan() == 3)
                //if (Until.checkLanguage())
                //    showNVHD();
                //else
                    huongdantrongcay();
            //hd sản xuất
            if (PlayerPrefsController.instance.getBuocHuongdan() == 6)
            {
                //if (Until.checkLanguage())
                //    showNVHD();
                //else
                    huongdansanxuat();
            }
            //hd mua chuồng
            if (PlayerPrefsController.instance.getBuocHuongdan() == 7)
                //if (Until.checkLanguage())
                //    showNVHD();
                //else
                    huongdanxaynhamay();
        }
    }

    //dialog time
    GameObject dialogTime;
    public int kcptObj = 1;
    //con vật
    public int timeptConvat = 0;
    public string nameConvat = "";
    public bool checkConvat;
    public void showCanvasTime(GameObject obj, int timeLife, bool convat)
    {
        Vector3 pos = obj.transform.position;
        if (convat && (nameConvat == "Giếng nước" || nameConvat == "Wells"))
        {
            kcptObj = 1;
            pos.y += 1.5f;
        }
        else pos.y += 0.8f;

        checkConvat = convat;
        if (convat)
        {
            if (dialogTime != null)
                Destroy(dialogTime);
            if (nameConvat != "Pig" && nameConvat != "Lợn" && nameConvat != "Giếng nước" && nameConvat != "Wells")
                pos.x += 0.2f;
            dialogTime = Instantiate(canvasTime, pos, Quaternion.identity) as GameObject;
            //Debug.Log("showCanvasTime " + obj.name);
            dialogTime.GetComponent<DonghoGame>()._Dem(timeLife);
            if (nameConvat == "Giếng nước" || nameConvat == "Wells")
            {
                dialogTime.GetComponent<DonghoGame>().timeCay = dataNongSan.data[idBinhtuoinuoc].timept;
            }
            else
                dialogTime.GetComponent<DonghoGame>().timeCay = timeptConvat;
            dialogTime.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = nameConvat;
            Destroy(dialogTime, 3f);
        }
        else
        {
            int kcpt = objOdatclick.getKcpt();
            kcptObj = kcpt;
            if (dialogTime != null) Destroy(dialogTime);
            dialogTime = Instantiate(canvasTime, pos, Quaternion.identity, obj.transform) as GameObject;
            dialogTime.GetComponent<DonghoGame>()._Dem(timeLife);
            dialogTime.GetComponent<DonghoGame>().timeCay = dataNongSan.data[objOdatclick.idCay].timept;
            dialogTime.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = Until.checkLanguage() ?
                dataNongSan.data[objOdatclick.idCay].nameV : dataNongSan.data[objOdatclick.idCay].nameE;
            Destroy(dialogTime, 3f);
        }
        Vector3 temp = dialogTime.GetComponent<RectTransform>().localScale;
        dialogTime.GetComponent<RectTransform>().localScale = new Vector3(temp.x * Until.getSize(), temp.y * Until.getSize(), temp.z);
    }

    public void showCVPets(GameObject obj, int timeLife)
    {
        Vector3 pos = obj.transform.position;
        pos.y += 1;
        pos.x -= 0.2f;
        if (dialogTime != null) Destroy(dialogTime);

        dialogTime = Instantiate(canvasTime, pos, Quaternion.identity) as GameObject;
        dialogTime.GetComponent<DonghoGame>()._Dem(timeLife);
        dialogTime.GetComponent<DonghoGame>().timeCay = obj.GetComponent<PetsControll>().timept;
        dialogTime.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        dialogTime.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        dialogTime.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);

        Vector3 temp = dialogTime.GetComponent<RectTransform>().localScale;
        dialogTime.GetComponent<RectTransform>().localScale = new Vector3(temp.x * Until.getSize(), temp.y * Until.getSize(), temp.z);
        Destroy(dialogTime, 4f);
    }

    public void buyKcpt()
    {
        if (checkConvat)
        {
            if (mouseDownConvat != null)
                mouseDownConvat.buyKcpt();
            if (giengController != null)
                giengController.buyKcpt();
        }
        else
        {
            objOdatclick.buyKcpt();
        }
    }
    //tăng tốc - time
    public void speedTime()
    {
        if (checkConvat)
        {
            if (mouseDownConvat != null)
                mouseDownConvat.speedTime();
            if (giengController != null)
                giengController.speedTime();
        }
        else
        {
            objOdatclick.speedTime();
        }
        PlayerPrefsController.instance.removeVpKho(idSpeedTime, 1);
    }
    public void showObjText(Vector3 pos, string txt, bool checkCanvas)
    {
        pos.z = 0;

        GameObject objtxt = Instantiate(objText, pos, Quaternion.identity) as GameObject;
        objtxt.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = txt;
        Vector3 temp = objtxt.transform.GetChild(0).GetComponent<RectTransform>().localScale;
        objtxt.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(temp.x * Until.getSize(), temp.y * Until.getSize(), temp.z);

        if (checkCanvas)
            objtxt.transform.GetChild(0).GetComponent<Canvas>().sortingLayerName = "Canvas";
        Destroy(objtxt, 2f);
    }

    public void showObjAddCGD(Vector3 pos, int cgd, string coint_gem_diamod, bool checkCanvas)
    {
        pos.z = 0;

        GameObject objadd = Instantiate(objAddCGD, pos, Quaternion.identity) as GameObject;
        objadd.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "+ " + cgd;
        objadd.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = spCGD[0];
        if (coint_gem_diamod == "gem")
            objadd.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = spCGD[1];
        if (coint_gem_diamod == "diamod")
            objadd.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = spCGD[2];

        Vector3 temp = objadd.transform.GetChild(0).GetComponent<RectTransform>().localScale;
        objadd.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(temp.x * Until.getSize(), temp.y * Until.getSize(), temp.z);

        if (checkCanvas)
            objadd.transform.GetChild(0).GetComponent<Canvas>().sortingLayerName = "Canvas";
        Destroy(objadd, 2f);
    }

    #endregion

    #region sinh obj thu hoạch

    public void _sinhObjThuhoach(GameObject obj, int idsp, string nameObj)
    {
        GameObject objthuhoach = null;
        objthuhoach = Instantiate(objThuhoach, obj.transform.position, Quaternion.identity) as GameObject;
        switch (nameObj)
        {
            case "convat":
                objthuhoach.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = mouseDownConvat.sanpham;

                if (randomVatpham())
                {
                    int idvp = getIdVatpham();
                    objthuhoach.transform.GetChild(2).gameObject.SetActive(true);
                    objthuhoach.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idvp].iconMake;
                    PlayerPrefsController.instance.addKho(idvp, 1);
                }

                Destroy(objthuhoach, 5f);
                break;
            case "caytrong":
                objthuhoach.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[objOdatclick.idCay].iconMake;
                Destroy(objthuhoach, 5f);

                if (randomVatpham())
                {
                    int idvp = getIdVatpham();
                    objthuhoach.transform.GetChild(2).gameObject.SetActive(true);
                    objthuhoach.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idvp].iconMake;
                    PlayerPrefsController.instance.addKho(idvp, 1);
                }
                break;
            case "nhamay":
                objthuhoach.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idsp].iconMake;
                Destroy(objthuhoach, 5f);
                if (randomVatpham())
                {
                    int idvp = getIdVatpham();
                    objthuhoach.transform.GetChild(2).gameObject.SetActive(true);
                    objthuhoach.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idvp].iconMake;
                    PlayerPrefsController.instance.addKho(idvp, 1);
                }
                break;
            case "rac":
                objthuhoach.transform.GetChild(0).gameObject.SetActive(false);
                Destroy(objthuhoach, 5f);
                break;
            case "momap":
                objthuhoach.transform.GetChild(0).gameObject.SetActive(true);
                objthuhoach.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idThuocno].hinhanh;
                objthuhoach.transform.GetChild(1).gameObject.SetActive(false);
                objthuhoach.transform.GetChild(2).gameObject.SetActive(true);
                objthuhoach.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idCua].hinhanh;
                Destroy(objthuhoach, 5f);
                break;
            case "vatpham":
                objthuhoach.transform.GetChild(0).gameObject.SetActive(true);
                objthuhoach.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idsp].hinhanh;
                objthuhoach.transform.GetChild(1).gameObject.SetActive(false);
                objthuhoach.transform.GetChild(2).gameObject.SetActive(false);
                Destroy(objthuhoach, 5f);
                break;
            case "vatphamcv":
                objthuhoach.transform.GetChild(0).gameObject.SetActive(true);
                objthuhoach.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idsp].hinhanh;
                objthuhoach.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName ="Canvas";
                objthuhoach.transform.GetChild(1).gameObject.SetActive(false);
                objthuhoach.transform.GetChild(2).gameObject.SetActive(false);
                Destroy(objthuhoach, 5f);
                break;
        }
        SoundController.instance._playSound(3);
    }
    //sinh vat pham nhan
    public void sinhvatphamnhan(Vector3 pos, Sprite icon, Transform posend)
    {
        GameObject objthuhoach = null;
        objthuhoach = Instantiate(objThuhoach, pos, Quaternion.identity) as GameObject;
        objthuhoach.transform.GetChild(0).gameObject.SetActive(true);
        objthuhoach.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = icon;
        objthuhoach.transform.GetChild(0).GetComponent<GemFly>().pointerGem = posend;
        objthuhoach.transform.GetChild(0).GetComponent<GemFly>().checkVP = true;
        objthuhoach.transform.GetChild(1).gameObject.SetActive(false);
        objthuhoach.transform.GetChild(2).gameObject.SetActive(false);
        Destroy(objthuhoach, 5f);
    }
    //lưỡi liềm thu hoạch
    public void _sinhObjLuoiliem(GameObject obj)
    {

        Vector3 pos = obj.transform.position;
        pos.y += 0.5f;
        if (dialogTime != null)
            Destroy(dialogTime);
        if (objOdatclick.CompareTag("odat3"))
        {
            dialogTime = Instantiate(cvGiothuhoach, pos, Quaternion.identity, obj.transform) as GameObject;
        }
        else
            dialogTime = Instantiate(objLuoiliem, pos, Quaternion.identity, obj.transform) as GameObject;

        //hướng dẫn
        if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 1)
        {
            sinhHand(1, obj.transform.position);
        }
        else
            Destroy(dialogTime, 5f);
        Vector3 temp = dialogTime.GetComponent<RectTransform>().localScale;
        dialogTime.GetComponent<RectTransform>().localScale = new Vector3(temp.x * Until.getSize(), temp.y * Until.getSize(), temp.z);
    }

    public void _sinhluoiliemthuhoach(Vector3 pos)
    {
        MobileTouchCamera.checkCamFollow = true;
        if (objOdatclick.CompareTag("odat3"))
            Instantiate(objGiothuhoach, pos, objLuoiliemThuhoach.transform.rotation);
        else
            Instantiate(objLuoiliemThuhoach, pos, objLuoiliemThuhoach.transform.rotation);
    }
    //thuốc nổ - cưa - xẻng
    public void _sinhCVPhamap(GameObject obj, int idvatpham)
    {
        Vector3 pos = obj.transform.position;

        if (obj.name.Substring(0, 3) == "Cay")
            pos.y += 0.5f;

        if (dialogTime != null)
            Destroy(dialogTime);

        dialogTime = Instantiate(cvPhamap, pos, Quaternion.identity, obj.transform) as GameObject;
        //lấy id vật phẩm trong kho dựa vào tên obj
        int idvpkho = 0;
        if (obj.name.Substring(0, 3) == "Cay") //cưa
            idvpkho = idCua;
        else if (obj.name.Substring(0, 3) == "sto")//thuốc nổ
            idvpkho = idThuocno;
        else
            if (obj.name.Substring(0, 2) == "co")//kéo
            idvpkho = idKeo;
        else//xẻng
            idvpkho = idXeng;

        dialogTime.transform.GetChild(0).GetComponent<DragLuoiliem>().idtuita = idvatpham;
        dialogTime.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = spVatpham[idvatpham];
        dialogTime.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = PlayerPrefsController.instance.getVpKho(idvpkho) + "";

        Destroy(dialogTime, 5f);
        Vector3 temp = dialogTime.GetComponent<RectTransform>().localScale;
        dialogTime.GetComponent<RectTransform>().localScale = new Vector3(temp.x * Until.getSize() * obj.transform.localScale.x, temp.y * Until.getSize(), temp.z);
    }
    GameObject vatpham;
    public void _sinhVatpham(int idvatpham, Vector3 pos)
    {
        if (vatpham != null)
            Destroy(vatpham);
        MobileTouchCamera.checkCamFollow = true;
        vatpham = Instantiate(objVatpham, pos, Quaternion.identity) as GameObject;
        vatpham.GetComponent<HatgiongForllow>().idHatgiong = idvatpham;
        vatpham.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spVatpham[idvatpham];
    }
    //bình tưới nước
    public void _sinhCanvasTuoinuoc(GameObject obj)
    {
        Vector3 pos = obj.transform.position;
        pos.y += 0.5f;
        if (dialogTime != null) Destroy(dialogTime);
        dialogTime = Instantiate(objTuoinuoc, pos, Quaternion.identity, obj.transform) as GameObject;
        dialogTime.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefsController.instance.getVpKho(idBinhtuoinuoc) + "";

        Destroy(dialogTime, 4f);
        Vector3 temp = dialogTime.GetComponent<RectTransform>().localScale;
        dialogTime.GetComponent<RectTransform>().localScale = new Vector3(temp.x * Until.getSize(), temp.y * Until.getSize(), temp.z);
    }
    public void _sinhBinhTuoinuoc(Vector3 pos)
    {
        MobileTouchCamera.checkCamFollow = true;
        destroyHand();
        Instantiate(binhtuoinuoc, pos, objLuoiliemThuhoach.transform.rotation);
        objOdatclick.GetComponent<OdatControll>().destroyCanvastuoinuoc();
    }
    //sinh xẻng xóa cây
    public void _sinhXengxoacay(Vector3 pos)
    {
        MobileTouchCamera.checkCamFollow = true;
        destroyHand();
        GameObject objxengxoa = Instantiate(binhtuoinuoc, pos, objLuoiliemThuhoach.transform.rotation) as GameObject;
        objOdatclick.GetComponent<OdatControll>().destroyCanvastuoinuoc();
        objxengxoa.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManagerControll.instance.spVatpham[0];
        objxengxoa.GetComponent<HatgiongForllow>().checkXoacay = true;
    }
    GameObject tuita;
    public void _sinhtuithucan(int idtui, Vector3 pos)
    {
        MobileTouchCamera.checkCamFollow = true;
        destroyHand();
        if (tuita != null)
            Destroy(tuita);
        tuita = Instantiate(objTuithucan, pos, Quaternion.identity) as GameObject;
        tuita.GetComponent<HatgiongForllow>().idHatgiong = idtui;
        tuita.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[idtui].iconMake;
    }
    public void _sinhObjCanvasTuiTA(int idtui, Vector3 pos)
    {
        if (dialogTime != null) Destroy(dialogTime);
        dialogTime = Instantiate(objDragTuithucan, pos, Quaternion.identity) as GameObject;
        dialogTime.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[idtui].iconMake;
        dialogTime.transform.GetChild(0).GetComponent<DragLuoiliem>().idtuita = idtui;
        dialogTime.transform.GetChild(0).GetComponent<DragLuoiliem>().thucan = true;
        dialogTime.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = PlayerPrefsController.instance.getVpKho(idtui) + "";
        Destroy(dialogTime, 4f);
    }

    #endregion

    #region Mở ô đất khóa
    int coin, gem;
    public void setDialogMokhoaOdat(int c, int g)
    {
        coin = c;
        gem = g;
        dialog[4].transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = coin + "";
        dialog[4].transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = gem + "";
    }
    public void btnNoMoodatClick(GameObject dialog)
    {
        if (!PlayerPrefsController.instance.checkHuongdan())
        {
            objOdatKhoaclick.GetComponent<PolygonCollider2D>().enabled = true;
            StartCoroutine(delayCloseDialog(dialog, false));
        }
    }
    public void btnYesMoOdatClick()
    {
        if (PlayerPrefsController.instance.getCoin() >= coin)
        {
            if (PlayerPrefsController.instance.getGem() >= gem)
            {
                if (animodat == null)
                {
                    StartCoroutine(delayAnimMoodat(objOdatKhoaclick));

                    StartCoroutine(delayCloseDialog(dialog[4], false));
                }
                else
                    showObjText(dialog[4].transform.position, Until.checkLanguage() ? "Đang mở ô đất, thử lại khi mở xong" : "Open the soil box, try again when it's finished", true);

                if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 4)
                {
                    //Until.huongdan = 5;
                    //if(Until.checkLanguage())
                    //    showNVHD();
                    //else
                    //{
                        PlayerPrefsController.instance.setBuocHuongdan(5);
                        huongdanxaynhamay();
                        sinhKhicau();
                    //}
                }
            }
            else showObjText(dialog[4].transform.position, Until.checkLanguage() ? "Không đủ đá quý" : "Not enough gem", true);
        }
        else showObjText(dialog[4].transform.position, Until.checkLanguage() ? "Không đủ vàng" : "Not enough gold", true);
    }
    GameObject animodat;
    IEnumerator delayAnimMoodat(GameObject objOdatlick)
    {
        string nameLodat = objOdatlick.GetComponent<MouseDownOdatkhoa>().nameLodat;
        animodat = Instantiate(animMoodat, objOdatlick.transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(3f);
        Destroy(animodat);
        GameObject odatmoi = Instantiate(getOdat(nameLodat), objOdatlick.transform.position, Quaternion.identity, tfOdat) as GameObject;
        odatmoi.GetComponent<OdatControll>().idOdat = PlayerPrefsController.instance.getSlodat(nameLodat);
        //Debug.Log("getidOdat " + nameLodat + "= " + odatmoi.GetComponent<OdatControll>().idOdat);
        odatmoi.GetComponent<OdatControll>().loodat = nameLodat;
        odatmoi.GetComponent<OdatControll>().destroyCaytrong();

        PlayerPrefsController.instance.adSlOdat(nameLodat, 1);
        PlayerPrefsController.instance.adSlOdatkhoa(nameLodat, 1);
        PlayerPrefsController.instance.exceptCoin(coin);
        PlayerPrefsController.instance.exceptGem(gem);

        playEff(0, objOdatlick.transform.position);

        Destroy(getArOdatkhoa(nameLodat)[0]);
        getArOdatkhoa(nameLodat).RemoveAt(0);

        if (getArOdatkhoa(nameLodat).Count > 0)
            getArOdatkhoa(nameLodat)[0].transform.GetChild(0).gameObject.SetActive(true);

    }
    void loadOdatKhoa(string nameLodat)
    {
        int odat = 0;
        if (nameLodat == "Odat dat") odat = 6;
        for (int i = 0; i < PlayerPrefsController.instance.getSlodatkhoa(nameLodat); i++)
        {
            if (getArOdatkhoa(nameLodat).Count > 0)
            {
                GameObject odatmoi = Instantiate(getOdat(nameLodat), getArOdatkhoa(nameLodat)[0].transform.position, Quaternion.identity, getTflodat(nameLodat)) as GameObject;
                odatmoi.GetComponent<OdatControll>().destroyCaytrong();
                odatmoi.GetComponent<OdatControll>().idOdat = odat;
                odatmoi.GetComponent<OdatControll>().loodat = nameLodat;
                odat++;

                Destroy(getArOdatkhoa(nameLodat)[0]);
                getArOdatkhoa(nameLodat).RemoveAt(0);
            }
        }
        if (getArOdatkhoa(nameLodat).Count > 0)
            getArOdatkhoa(nameLodat)[0].transform.GetChild(0).gameObject.SetActive(true);
    }

    List<GameObject> getArOdatkhoa(string name)
    {
        switch (name)
        {
            case "Odat dat": return odatKhoa;
            case "Odat dat 1": return odatKhoa1;
            case "Odat dat 2": return odatKhoa2;
        }
        return null;
    }
    Transform getTflodat(string name)
    {
        switch (name)
        {
            case "Odat dat": return tfOdat;
            case "Odat dat 1": return tfOdat1;
            case "Odat dat 2": return tfOdat2;
        }
        return null;
    }
    GameObject getOdat(string name)
    {
        switch (name)
        {
            case "Odat dat": return odatTrong;
            case "Odat dat 1": return odat2;
            case "Odat dat 2": return odat3;
        }
        return null;
    }

    //mua vật phẩm
    public void showDialogBuyVatpham(int idvp)
    {
        Until.showPanel = true;
        dialog[15].SetActive(true);
    }

    public void btnCloseDialogVatpham(GameObject dialog)
    {
        StartCoroutine(delayCloseDialog(dialog, false));
    }
    public void btnYesDialogVatpham(GameObject dialog)
    {

        Vector2 centerCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (PlayerPrefsController.instance.getDiamod() > 0)
        {
            if (mouseDownRac != null)
            {
                PlayerPrefsController.instance.xoarac(mouseDownRac.mouseDownLockmap.idmap, mouseDownRac.idrac);
                PlayerPrefsController.instance.exceptDiamod(1);
                StartCoroutine(mouseDownRac.delayXoarac(mouseDownRac.idVpPhamap));
            }
            StartCoroutine(delayCloseDialog(dialog, false));
        }
        else
            showObjText(centerCamera,
                Until.checkLanguage() ? "Không đủ kim cương" : "Not enough diamod", true);

    }

    //int getIdvatpham(int idmaping)
    //{
    //    if (idmaping == 1) return idCua;
    //    return idThuocno;
    //}
    #endregion

    #region Nhà máy: Sinh obj item make nhà máy
    public void _sinhObjMake(Vector3 pos, int id, int idsp)
    {
        //Debug.Log("Sinh obj make");
        MobileTouchCamera.checkCamFollow = true;
        GameObject objmake = Instantiate(objMake, pos, objLuoiliemThuhoach.transform.rotation) as GameObject;
        objmake.GetComponent<SpriteRenderer>().sprite = dataNongSan.data[id].iconMake;
        objmake.GetComponent<HatgiongForllow>().idHatgiong = idsp;
        if (!Until.itemMakeBtn.checkMake())
        {
            //Debug.Log("Đổi màu");
            Color color;
            ColorUtility.TryParseHtmlString("#FFB3A7FF", out color);
            objmake.GetComponent<SpriteRenderer>().color = color;
        }
        objmake.transform.localScale = new Vector2(Until.getSize(), Until.getSize());
    }
    //dialog mở thêm ô sản xuất
    GameObject panelSx;
    public void _showDialogThemOsx(GameObject obj)
    {
        panelSx = obj;
        dialog[6].SetActive(true);
        MobileTouchCamera.checkCamFollow = true;
        Until.showPanel = true;
        hidenButton(true);
        setBox2DPanelSx(false);
    }
    int kc = 0;
    public void setDialogMokhoaSx(int k)
    {
        Until.checkShowDialogChild = true;
        kc = k;
        dialog[6].transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = kc + "";
    }
    public void btnYesMothemOsxClick()
    {
        if (PlayerPrefsController.instance.getDiamod() >= kc)
        {
            Until.checkShowDialogChild = false;
            //showObjText(dialog[5].transform.position, Until.checkLanguage() ? "Mở khóa thành công" : "Successful unlock", true);
            panelSx.GetComponent<LoadDialogNhamay>().themOsx();
            PlayerPrefsController.instance.exceptDiamod(kc);
            StartCoroutine(delayCloseDialog(dialog[6], true));
        }
        else showObjText(dialog[6].transform.position, Until.checkLanguage() ? "Không đủ kim cương" : "Not enough diamod", true);
    }
    public void setBox2DPanelSx(bool check)
    {
        for (int i = 0; i < box2D.Length; i++)
        {
            box2D[i].enabled = check;
        }
    }
    #endregion

    #region load data
    void loadObjLuoi()
    {
        for (int i = 0; i < tfLuoi.childCount; i++)
        {
            tfLuoi.GetChild(i).GetComponent<MouseDownLuoi>().idLuoi = i;
        }
        for (int i = 0; i < tfLuoi.childCount; i++)
        {
            //check trạng thái sinh obj tại lưới
            if (PlayerPrefsController.instance.getObjLuoi(i) > 0)
            {

                string tagobj = PlayerPrefsController.instance.getTagObjLuoi(i);
                int idobjluoi = PlayerPrefsController.instance.getIdObjLuoi(i);
                float objRotation = PlayerPrefsController.instance.getRotationLuoi(i);
                tfLuoi.GetChild(i).GetComponent<MouseDownLuoi>().endBoxLuoi(false);
                ObjShop[] arObj = getArrObj(tagobj);
                if (arObj != null)
                {
                    GameObject obj = Instantiate(arObj[idobjluoi].obj, tfLuoi.GetChild(i).position, Quaternion.identity) as GameObject;
                    setParent(obj);
                    obj.GetComponent<MoveObj>().idluoinow = i;
                    //Debug.Log("sinh vật: " + obj.tag+i);
                    if (obj.CompareTag("hangraoInstan"))
                        obj.GetComponent<MouseDownHangrao>().loadHangrao(objRotation);
                    else
                    {
                        if (objRotation == 0) objRotation = 1;
                        obj.transform.localScale = new Vector2(objRotation, 1);
                    }

                    if (obj.CompareTag("chuong"))
                    {
                        obj.GetComponent<ChuongController>().idChuong = i;
                    }
                    if (Until.checkTagNhamay(obj.tag))
                    {
                        //Debug.Log("Sinh nhà máy: " + obj.tag + PlayerPrefsController.instance.getIdNhamayLuoi(i));
                        obj.GetComponent<NhamayControll>().idLuoi = PlayerPrefsController.instance.getIdNhamayLuoi(i);
                    }
                    //giếng
                    if (obj.CompareTag("gieng"))
                    {
                        obj.GetComponent<GiengController>().idgieng = i;
                    }
                }
            }
        }
    }
    public void setParent(GameObject obj)
    {
        if (obj.CompareTag("trangtri"))
            obj.transform.SetParent(tfTrangtri);
        if (Until.checkTagNhamay(obj.tag) || obj.CompareTag("gieng"))
            obj.transform.SetParent(tfNhamay);
        if (obj.CompareTag("chuong"))
            obj.transform.SetParent(tfChuong);
        if (obj.CompareTag("hangraoInstan") || obj.CompareTag("roadInstan"))
            obj.transform.SetParent(tfHangrao);
    }

    bool randomVatpham()
    {
        int rd = UnityEngine.Random.Range(0, 10);
        if (rd == UnityEngine.Random.Range(0, 10))
            return true;
        return false;
    }
    int getIdVatpham()
    {
        int id = UnityEngine.Random.Range(idCua, idSpeedTime);
        return id;
    }

    void loadPest()
    {
        if (PlayerPrefsController.instance.getPetsShow())
        {
            Instantiate(objPes[PlayerPrefsController.instance.getPets()].obj, objPosPes.transform.position, Quaternion.identity, objPosPes.transform);
        }
    }

    ObjShop[] getArrObj(string tag)
    {
        switch (tag)
        {
            case "maythucan":
            case "maysua":
            case "lonuong":
            case "douong":
            case "thomay":
            case "nhamay":
            case "gieng":
            case "nuongthit":
            case "daubep":
            case "thucong":
            case "chuong": return objNha;
            case "trangtri": return objTrangtri;
            case "hangraoInstan": return objWall;
            case "roadInstan": return objRoad;
        }
        return null;
    }
    #endregion

    #region rotation obj - tick - remove - tắt box trang trí
    public void btnRotationClick()
    {
        moveObj.changRotation();
    }
    public void btnTickClick()
    {
        moveObj.saveData();
    }
    public void btnRemoveClick()
    {
        if (!PlayerPrefsController.instance.checkHuongdan())
        {
            moveObj.removeData();
            playEff(3, objInstance.transform.position);
            Until.checkInstanceObj = false;
            Until.moveObj = false;
            SoundController.instance._playSound(9);
            Destroy(objInstance);
        }
    }

    public void tatBoxClick(bool check)
    {
        
        foreach (Transform t in tfTrangtri)
        {
            t.GetComponent<MoveObj>().boxClick.enabled = check;
        }
        foreach (Transform t in tfChuong)
        {
            t.GetComponent<MoveObj>().boxClick.enabled = check;
        }
        foreach (Transform t in tfNhamay)
        {
            t.GetComponent<MoveObj>().boxClick.enabled = check;
        }
        foreach (Transform t in tfHangrao)
        {
            t.GetComponent<MoveObj>().boxClick.enabled = check;
        }
        foreach (Transform t in tfHouse)
        {
            t.GetComponent<PolygonCollider2D>().enabled = check;
        }
    }

    #endregion

    #region camview
    public void _camView(Vector3 pos)
    {
        Camera.main.transform.DOMove(new Vector3(pos.x, pos.y + 1.2f, -40), 0.5f);
    }
    #endregion

    #region play anim -- eff
    IEnumerator delayBongdem()
    {
        panelBongdem.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        Destroy(panelBongdem);
    }
    public void playAnimNhakho()
    {
        animNhakho.SetTrigger("addsp");
    }

    public void playEff(int id, Vector3 pos)
    {
        GameObject eff = Instantiate(arEff[id], pos, Quaternion.identity) as GameObject;
        Destroy(eff, 1.5f);
    }
    #endregion

    #region hướng dẫn
    public void loadThoatHD()
    {
        if (PlayerPrefsController.instance.getThoatHD())
        {
            //Debug.Log("PlayerPrefsController.instance.getBuocHuongdan() = " + PlayerPrefsController.instance.getBuocHuongdan());
            switch (PlayerPrefsController.instance.getBuocHuongdan())
            {
                //case 0: //giới thiệu
                //    if (Until.checkLanguage())
                //        showNVHD();
                //    else
                //    {
                //        PlayerPrefsController.instance.setBuocHuongdan(1);
                //        loadThoatHD();
                //    }
                //    break;
                case 1://hướng dẫn thu hoạch lần đầu
                    sinhHand(0, objOdat0.transform.position);
                    _camView(objOdat0.transform.position);
                    break;
                case 2://hướng dẫn send order
                    StartCoroutine(delayHuongdanOrder());
                    break;
                case 3://hướng dẫn trồng cây
                    huongdantrongcay();
                    break;
                case 4://hướng dẫn mở ô đất khóa
                    huongdanmoodat();
                    break;
                case 6://hướng dẫn sản xuất
                    StartCoroutine(delayHuongdanNhamay());
                    break;
                case 5://hướng dẫn xây dựng
                case 8:
                case 7:
                    huongdanxaynhamay();
                    break;
                case 9:
                    StartCoroutine(delayHuongdanNhamay());
                    break;
            }
        }
    }

    IEnumerator delayHuongdanNhamay()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject obj = null;
        if (PlayerPrefsController.instance.getBuocHuongdan() == 6 || PlayerPrefsController.instance.getBuocHuongdan() == 7)
        {
            obj = GameObject.FindGameObjectWithTag("maythucan").gameObject;
            sinhHand(0, obj.transform.position);
        }

        //hd cho ăn
        if (PlayerPrefsController.instance.getBuocHuongdan() == 9)
        {
            obj = GameObject.FindGameObjectWithTag("chuong").gameObject;
            sinhHand(0, obj.transform.position);
        }
        if (obj != null)
            _camView(obj.transform.position);
    }
    public void huongdanOrder()
    {
        //Until.huongdan = 2;
        PlayerPrefsController.instance.setBuocHuongdan(2);
        StartCoroutine(delayHuongdanOrder());
    }

    IEnumerator delayHuongdanOrder()
    {
        yield return new WaitForSeconds(1f);
        OrderController.instance.huongdan();
    }
    IEnumerator delayNVHD()
    {
        dialogNVHD.SetActive(true);
        Until.showPanel = true;
        //hidenUIMain(true);
        yield return new WaitForSeconds(0.5f);
        dialogNVHD.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void huongdantrongcay()
    {
        //Until.huongdan = 3;
        PlayerPrefsController.instance.setBuocHuongdan(3);
        Camera.main.GetComponent<Camera>().orthographicSize = 3;
        objOdat3.GetComponent<OdatControll>().destroyCaytrong();
        PlayerPrefsController.instance.removeCayOdat(objOdat3.GetComponent<OdatControll>().loodat, 2);
        _camView(objOdat3.transform.position);
        sinhHand(0, objOdat3.transform.position);
    }
    public void huongdanmoodat()
    {
        //Until.huongdan = 4;
        PlayerPrefsController.instance.setBuocHuongdan(4);
        StartCoroutine(delayMoodat());
    }

    IEnumerator delayMoodat()
    {
        yield return new WaitForSeconds(1f);
        _camView(odatKhoa[0].transform.position);
        sinhHand(0, odatKhoa[0].transform.position);
    }
    //xây dựng
    public void huongdansanxuat()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("maythucan").gameObject;
        sinhHand(0, obj.transform.position);
        _camView(obj.transform.position);
    }
    public void huongdanxaynhamay()
    {
        //hidenUIMain(false);
        activeHandMap(0);
    }
    public void huongdanclickdatnha()
    {
        activeHandMap(1);
    }
    public void showNVHD0()
    {
        StartCoroutine(delayNVHD());
    }
    public void huongdanthuhoachlua()
    {
        int count = 0;
        for (int i = 0; i < odatLuathuhoach.Length; i++)
        {
            if (odatLuathuhoach[i].GetComponent<OdatControll>().ttodat == "thuhoach")
            {
                sinhHand(0, odatLuathuhoach[i].transform.position);
                _camView(odatLuathuhoach[i].transform.position);
                break;
            }
            else count++;
        }
        if (count == odatLuathuhoach.Length)
        {
            //if(Until.checkLanguage())
            //    showNVHD();
            //else 
            huongdanOrder();
            Until.checkThuhoachlua = true;
        }
    }
    #endregion

    #region sinh bàn tay hướng dẫn
    GameObject hand;

    public void sinhHand(int id, Vector3 pos)
    {
        StartCoroutine(delaySinhhand(id, pos));
    }
    IEnumerator delaySinhhand(int id, Vector3 pos)
    {
        yield return new WaitForSeconds(0.5f);
        if (hand != null) Destroy(hand);
        hand = Instantiate(handHd[id], pos, Quaternion.identity) as GameObject;
        if (PlayerPrefsController.instance.getBuocHuongdan() == 1)
            hand.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void sinhHand(int id, Vector3 pos, Transform tf)
    {
        StartCoroutine(delaySinhhand(id, pos, tf));
    }
    IEnumerator delaySinhhand(int id, Vector3 pos, Transform tf)
    {
        yield return new WaitForSeconds(0.5f);
        if (hand != null) Destroy(hand);
        hand = Instantiate(handHd[id], pos, Quaternion.identity, tf) as GameObject;
        if (PlayerPrefsController.instance.getBuocHuongdan() == 1)
            hand.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    public void destroyHand()
    {
        if (hand != null)
            Destroy(hand);
        if (dialogTime != null)
            Destroy(dialogTime);
    }

    public void activeHandMap(int id)
    {
        disbaleHandMap();
        StartCoroutine(delayActiveSinhhand(id));
    }
    public void disbaleHandMap()
    {
        for (int i = 0; i < objHandMap.Length; i++)
            objHandMap[i].SetActive(false);
    }
    IEnumerator delayActiveSinhhand(int id)
    {
        yield return new WaitForSeconds(1f);
        objHandMap[id].SetActive(true);
    }
    #endregion

    #region load thành tích
    public void checkthanhtich()
    {
        Debug.Log("=======>checkThanhtich");
        dialogNhachinh.loadItemthanhtich();
    }
    #endregion

    #region Load phần thưởng ngày
    public void loadPhanthuongngay()
    {
        StartCoroutine(delayShowThuongNgay());
    }
    IEnumerator delayShowThuongNgay()
    {
        yield return new WaitForSeconds(1f);
        if (!PlayerPrefsController.instance.checkHuongdan())
        {
            DateTime datetime = DateTime.Now;
            int day = datetime.Day;
            int month = datetime.Month;
            int year = datetime.Year;
            //Debug.Log("Day now day: " + day);
            //Debug.Log("Day now month: " + month);
            //Debug.Log("Day now year: " + year);
            if (day == 1 && !PlayerPrefsController.instance.checkDayDaNhan(1))
                PlayerPrefsController.instance.setDayGame(0, month, year);

            //if (PlayerPrefsController.instance.getThuongngay() < day && PlayerPrefsController.instance.getNhanthuongngay() < day)
            if (day > PlayerPrefsController.instance.getDayGame(month, year))
                if (!PlayerPrefsController.instance.checkDayDaNhan(PlayerPrefsController.instance.getDayNextThuongngay()))
                    dialogDaily.SetActive(true);
        }
    }
    #endregion

    #region Khinh khi cầu

    public void chayKhicau()
    {
        if (PlayerPrefsController.instance.getChonhanKC())
        {
            Instantiate(objKhicau, tfKhicau[0].position, Quaternion.identity);
        }
        else
        {
            timeStart = PlayerPrefsController.instance.getTimeStarKhicau();
            timeDachay = Until.timeNow() - timeStart;
            if (timeDachay < 0)
                timeClock = 0;
            else
                timeClock = timeDelayKhicau - timeDachay;

            StartCoroutine(chayPtGieng());
        }
    }

    public void sinhKhicau()
    {
        PlayerPrefsController.instance.setTimeStartKhicau(Until.timeNow());
        rdvt = UnityEngine.Random.Range(1, tfKhicau.Length - 1);
        Instantiate(objKhicau, tfKhicau[rdvt].position, Quaternion.identity);
    }
    IEnumerator chayPtGieng()
    {
        if (timeClock <= 0)
        {
            sinhKhicau();
        }
        yield return new WaitForSeconds(1f);
        if (timeClock > 0)
        {
            timeClock--;
            StartCoroutine(chayPtGieng());
        }
    }
    #endregion

    #region OnDestroy
    private void OnDestroy()
    {
        if (PlayerPrefsController.instance.checkHuongdan())
        {
            PlayerPrefsController.instance.setThoatHD(1);
        }
    }
    #endregion
}
