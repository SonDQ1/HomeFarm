using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NhamayControll : MonoBehaviour
{
    #region declare
    public int idLuoi;
    public int levelUnlock;
    public int idPanelNhamay;
    [SerializeField] GameObject objChothuhoach;

    //ban đầu sinh ra null
    GameObject panelSX;
    //"Obj make"
    GameObject objMake;
    GameObject[] btnLock;
    Text txtTime, txtDiamod;
    Image icon, imgLoad;
    GameObject objTimeLoad, btnBuy;

    //GameObject info;
    //int idBtn;
    List<int> listMake;
    //List<int> listMakeTimeStar;
    List<int> listthuhoach;
    #endregion

    private void Start()
    {
        loadGameObject();
        //btnBuy.GetComponent<Button>().onClick.AddListener(() => btnBuyKcpt());
        loadItemThuhoach();
        loadListMake();

        //hướng dẫn kéo nhà
        if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 5)
        {
            Vector2 temp = gameObject.transform.position;
            temp.y += 0.5f;
            GameManagerControll.instance.sinhHand(4, temp);
        }
    }

    #region button
    //btn buy
    public int getKcpt()
    {
        float kc = 1;
        if (timeClock > GameManagerControll.instance.dataNongSan.data[idSP].timept * 0.7f)
            kc = GameManagerControll.instance.dataNongSan.data[idSP].kcpt;
        if (timeClock <= GameManagerControll.instance.dataNongSan.data[idSP].timept * 0.7f && timeClock > GameManagerControll.instance.dataNongSan.data[idSP].timept * 0.5f)
            kc = GameManagerControll.instance.dataNongSan.data[idSP].kcpt * 0.7f;
        if (timeClock <= GameManagerControll.instance.dataNongSan.data[idSP].timept * 0.5f && timeClock > GameManagerControll.instance.dataNongSan.data[idSP].timept * 0.3f)
            kc = GameManagerControll.instance.dataNongSan.data[idSP].kcpt * 0.5f;
        if (timeClock <= GameManagerControll.instance.dataNongSan.data[idSP].timept * 0.3f)
            kc = GameManagerControll.instance.dataNongSan.data[idSP].kcpt * 0.3f;
        if (kc < 1) kc = 1;
        return (int)kc;
    }

    public void btnBuyKcpt()
    {
        int kc = getKcpt();
        //check kc
        if (kc <= PlayerPrefsController.instance.getDiamod())
        {
            PlayerPrefsController.instance.exceptDiamod(kc);
            PlayerPrefsController.instance.setTimeStartSanpham(gameObject.tag, idLuoi, 0);
            timeClock = 0;
            chaysp = chayPtSP(idSP);
            StartCoroutine(chaysp);
            //chaysp = null;
            //StartCoroutine(chayPtSP(idSP));
            loadListMake();
            //Debug.Log("btnBuyKcpt=> " + idSP+"-"+gameObject.tag+idLuoi);
        }
        else
            GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0))
                , Until.checkLanguage() ? "Không đủ kim cương" : "Not enough diamond", true);
    }
    #endregion

    #region function
    //load gameobject
    void loadGameObject()
    {
        panelSX = GameManagerControll.instance.dialog[3].transform.GetChild(0).GetChild(idPanelNhamay).gameObject;

        objMake = panelSX.transform.GetChild(1).GetChild(0).gameObject;
        icon = objMake.transform.GetChild(1).GetComponent<Image>();
        btnBuy = objMake.transform.GetChild(2).gameObject;
        txtDiamod = btnBuy.transform.GetChild(0).GetComponent<Text>();
        objTimeLoad = objMake.transform.GetChild(3).gameObject;
        imgLoad = objTimeLoad.transform.GetChild(0).GetComponent<Image>();
        txtTime = imgLoad.transform.GetChild(0).GetComponent<Text>();
        btnLock = new GameObject[3];
        for (int i = 0; i < btnLock.Length; i++)
        {
            btnLock[i] = panelSX.transform.GetChild(1).GetChild(i + 1).gameObject;
        }
    }

    //load sp khi start
    //void loadListMake(bool checkShow)
    //list temp lưu các id đang tạo để xóa trạng thái khi thu hoạch
    List<int> vpDahoanthanh;
    void loadListMake()
    {
        
        listMake = PlayerPrefsController.instance.getListMake(gameObject.tag, idLuoi);
        vpDahoanthanh = new List<int>();
        //Debug.Log(name+"==>>>loadListMake= " + listMake.Count);
        if (listMake.Count > 0)
        {
            int timesp = PlayerPrefsController.instance.getTimeStartSP(gameObject.tag, idLuoi);
            int timeconlai = 0;
            int timedachay = 0;
            for (int i = 0; i < listMake.Count; i++)
            {
                timesp += GameManagerControll.instance.dataNongSan.data[listMake[i]].timept;
                //listMakeTimeStar.Add(timesp);
                //PlayerPrefsController.instance.setCheckTimeStart(listMake[i], -1);
                //Debug.Log("listMakeTimeStar["+i+"->"+ listMake[i] + "]= " + timesp);
                if (Until.timeNow() >= timesp)
                {
                    PlayerPrefsController.instance.setTimeStartSanpham(tag, idLuoi, timesp);
                    //PlayerPrefsController.instance.setCheckTimeStart(listMake[i], 1);
                    vpDahoanthanh.Add(listMake[i]);
                }
                else
                {
                    timeconlai = timesp - Until.timeNow();
                    timedachay = GameManagerControll.instance.dataNongSan.data[listMake[i]].timept - timeconlai;
                    PlayerPrefsController.instance.setTimeStartSanpham(tag, idLuoi, Until.timeNow() - timedachay);
                    break;
                }
            }

            //load sp hoàn thành
            //Debug.Log("vpDahoanthanh =  " + vpDahoanthanh.Count);
            if (vpDahoanthanh.Count > 0)
            {
                for (int i = 0; i < vpDahoanthanh.Count; i++)
                {
                    sinhthuhoach(vpDahoanthanh[i]);
                }
            }
           
            if (listMake.Count > 0)
                ptSanpham(listMake[0]);

            //load lại ảnh trên ô chờ tạo
            int pos = 0; //vị trí mảng btnlock
            //Debug.Log("listMake.Count = " + listMake.Count);
            for (int i = 1; i < listMake.Count; i++)
            {
                if (PlayerPrefsController.instance.getOsx(gameObject.tag, idLuoi, pos) == 1)
                {
                    btnLock[pos].transform.GetChild(0).gameObject.SetActive(true);
                    btnLock[pos].transform.GetComponent<BoxCollider2D>().enabled = false;
                    btnLock[pos].transform.GetChild(0).GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[listMake[i]].iconMake;
                    pos++;
                }
            }
        }
    }

    bool checkDahoanthanh(int idvp)
    {
        for(int i = 0; i < vpDahoanthanh.Count; i++)
        {
            if (idvp == vpDahoanthanh[i])
            {
                vpDahoanthanh[i] = 0;
                return true;
            }
        }
        return false;
    }
    void loadItemThuhoach()
    {
        listthuhoach = PlayerPrefsController.instance.getListThuhoach(gameObject.tag, idLuoi);
        //Debug.Log("loadItemThuhoach = " + listthuhoach.Count);
        if (listthuhoach.Count > 0)
        {
            sinhObjChothuhoach(gameObject, idLuoi);
        }
    }
    void showTimeBtnBuy(bool check)
    {
        icon.gameObject.SetActive(check);
        objTimeLoad.SetActive(check);
        btnBuy.SetActive(check);
    }
    public void loadNextItemMake()
    {
        Debug.Log("loadNextItemMake==================>>>>");
        listMake = PlayerPrefsController.instance.getListMake(gameObject.tag, idLuoi);
        if (listMake.Count > 0)
        {
            if (listMake.Count == 1)
            {
                btnLock[0].transform.GetChild(0).gameObject.SetActive(false);
                btnLock[0].GetComponent<BoxCollider2D>().enabled = true;
            }
            if (listMake.Count == 2)
            {
                btnLock[0].transform.GetChild(0).GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[listMake[1]].iconMake;
                btnLock[1].transform.GetChild(0).gameObject.SetActive(false);
                btnLock[1].GetComponent<BoxCollider2D>().enabled = true;
            }
            if (listMake.Count == 3)
            {
                btnLock[0].transform.GetChild(0).GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[listMake[1]].iconMake;
                btnLock[1].transform.GetChild(0).GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[listMake[2]].iconMake;
                btnLock[2].transform.GetChild(0).gameObject.SetActive(false);
                btnLock[2].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            showTimeBtnBuy(false);
        }
    }

    //public void checkMake(int idspmake,Sprite ic)
    public void checkMake(Sprite ic)
    {
        listMake = PlayerPrefsController.instance.getListMake(gameObject.tag, idLuoi);
        //Debug.Log("checkMake - listMake: " + listMake.Count);
        if (listMake.Count > 0)
        {
            if (!icon.IsActive())
            {
                PlayerPrefsController.instance.setTimeStartSanpham(gameObject.tag, idLuoi, Until.timeNow());
                //chaysp = null;
                ptSanpham(listMake[0]);
                //Debug.Log("checkMake addPTSP");
            }
            else
                for (int i = 0; i < btnLock.Length; i++)
                {
                    if (btnLock[i].transform.GetChild(1).gameObject.activeInHierarchy == false)
                        if (!btnLock[i].transform.GetChild(0).gameObject.activeInHierarchy)
                        {
                            btnLock[i].transform.GetChild(0).GetComponent<Image>().sprite = ic;
                            btnLock[i].transform.GetChild(0).gameObject.SetActive(true);
                            break;
                        }
                }
        }
    }

    #endregion

    #region phát triển sản phẩm
    public int idSP, timeSP;
    public int timeClock, timeStart, timeDachay;
    string trangthai = "null";
    IEnumerator chaysp;

    void ptSanpham(int idsp)
    {
        //Debug.Log("ptSanpham: " + gameObject.tag+idLuoi+"-"+ idsp);
        idSP = idsp;
        icon.sprite = GameManagerControll.instance.dataNongSan.data[idSP].iconMake;
        icon.SetNativeSize();
        txtTime.text = getFormatTime(GameManagerControll.instance.dataNongSan.data[idSP].timept) + "";
        timeSP = GameManagerControll.instance.dataNongSan.data[idSP].timept;
        showTimeBtnBuy(true);
        timeStart = PlayerPrefsController.instance.getTimeStartSP(gameObject.tag, idLuoi);
        timeDachay = Until.timeNow() - timeStart;
        if (timeDachay < 0)
            timeClock = 0;
        else
            timeDachay = Until.timeNow() - timeStart;

        if (timeDachay < 0)
            timeClock = 0;
        else
            timeClock = GameManagerControll.instance.dataNongSan.data[idSP].timept - timeDachay;


        if (chaysp == null)
        {
            chaysp = chayPtSP(idSP);
            StartCoroutine(chaysp);
        }
    }

    IEnumerator chayPtSP(int id)
    {
        if (timeClock > 0)
        {
            if(trangthai != "phattrien")
            {
                trangthai = "phattrien";
                gameObject.GetComponent<Animator>().SetBool("run", true);
            }
        }
        else
        {
            //Debug.Log("Nhamaycontroll THUHOACH: " + listMake[0]);
            trangthai = "thuhoach";
            gameObject.GetComponent<Animator>().SetBool("run", false);
            checkThuhoach = true;
            //sinh obj chờ thu hoạch
            //Debug.Log("Add listthuhoach " + gameObject.tag + idLuoi + id);

            PlayerPrefsController.instance.setThuhoach(gameObject.tag, idLuoi, id);

            sinhObjChothuhoach(gameObject, idLuoi);
            chaysp = null;

            loadNextItemMake();
            showTimeBtnBuy(false);
            if (listMake.Count > 0)
            {
                //Debug.Log("Nhamaycontroll REMOVE: " + listMake[0]);
                PlayerPrefsController.instance.removeListMake(gameObject.tag, idLuoi, listMake.Count);
                listMake.RemoveAt(0);
            }
           
            if (listMake.Count > 0)
            {
                loadNextItemMake();
                StopAllCoroutines();
                //Debug.Log("listMake[" + 0 + "]= " + listMake[0]+ PlayerPrefsController.instance.checkTime(listMake[0]));
                //Debug.Log("listMake[" + 0 + "]= " + listMake[0] + checkDahoanthanh(listMake[0]));
                //if (PlayerPrefsController.instance.checkTime(listMake[0]))
                //if (!checkDahoanthanh(listMake[0]))
                    PlayerPrefsController.instance.setTimeStartSanpham(gameObject.tag, idLuoi, Until.timeNow());
                
                ptSanpham(listMake[0]);
            }
            else
                showTimeBtnBuy(false);
        }
        if (panelSX.activeInHierarchy)
            panelSX.GetComponent<LoadDialogNhamay>().chayDongho();
        //Debug.Log("Chaypt=>" + gameObject.tag + idLuoi);
        yield return new WaitForSeconds(1f);
        if (timeClock > 0)
        {
            timeClock--;
            StartCoroutine(chayPtSP(idSP));
        }
    }

    void sinhthuhoach(int id)
    {
        trangthai = "thuhoach";
        //gameObject.GetComponent<Animator>().SetBool("run", false);
        checkThuhoach = true;
        //sinh obj chờ thu hoạch
        //Debug.Log("Add listthuhoach " + gameObject.tag + idLuoi + id);

        PlayerPrefsController.instance.setThuhoach(gameObject.tag, idLuoi, id);

        sinhObjChothuhoach(gameObject, idLuoi);
        //showTimeBtnBuy(false);
        if (listMake.Count > 0)
        {
            //Debug.Log("Nhamaycontroll REMOVE: " + listMake.Count);
            PlayerPrefsController.instance.removeListMake(gameObject.tag, idLuoi, listMake.Count);
            listMake.RemoveAt(0);
        }
        loadNextItemMake();
    }
    #endregion

    #region Thu hoạch
    GameObject objchothuhoach;
    bool checkThuhoach = true;
    public void thuhoach()
    {
        Destroy(objchothuhoach);
        listthuhoach = PlayerPrefsController.instance.getListThuhoach(gameObject.tag, idLuoi);
        checkThuhoach = false;
        if (listthuhoach.Count > 0)
            StartCoroutine(delaySinhVP(listthuhoach.Count));
    }
    public void thuhoach(int id)
    {
        int idtemp = id;
        if(objchothuhoach != null)
        {
            listthuhoach = PlayerPrefsController.instance.getListThuhoach(gameObject.tag, idLuoi);
            if (id >= listthuhoach.Count)
                idtemp = listthuhoach.Count - 1;

            objchothuhoach.transform.GetChild(0).GetChild(id).gameObject.SetActive(false);
            GameManagerControll.instance.playEff(1, gameObject.transform.position);
            GameManagerControll.instance._sinhObjThuhoach(gameObject, listthuhoach[idtemp], "nhamay");

            if (listthuhoach[idtemp] >= 19 && listthuhoach[idtemp] <= 25)
                PlayerPrefsController.instance.addKho(listthuhoach[idtemp], 4);
            else
                PlayerPrefsController.instance.addKho(listthuhoach[idtemp], 1);

            PlayerPrefsController.instance.removeItemListThuhoach(gameObject.tag, idLuoi, idtemp);
        }
    }
    IEnumerator delaySinhVP(int count)
    {
        count--;
        if (count >= 0)
        {
            GameManagerControll.instance.playEff(1, gameObject.transform.position);
            GameManagerControll.instance._sinhObjThuhoach(gameObject, listthuhoach[count], "nhamay");
            if (listthuhoach[count] >= 19 && listthuhoach[count] <= 25)
                PlayerPrefsController.instance.addKho(listthuhoach[count], 4);
            else
                PlayerPrefsController.instance.addKho(listthuhoach[count], 1);
        }
        else
        {
            //chaysp = null;
            vpDahoanthanh = new List<int>();
            PlayerPrefsController.instance.newListThuhoach(gameObject.tag, idLuoi);
        }
        yield return new WaitForSeconds(0.3f);
        if (count >= 0)
            StartCoroutine(delaySinhVP(count));
    }

    void sinhObjChothuhoach(GameObject nhamay, int idNhamay)
    {
        if (objchothuhoach != null) Destroy(objchothuhoach);
        objchothuhoach = Instantiate(objChothuhoach, nhamay.transform.position, Quaternion.identity, nhamay.transform) as GameObject;
        objchothuhoach.transform.GetChild(0).GetChild(0).GetComponent<MouseDownObjThuhoachNhamay>().nhamayControll = gameObject.GetComponent<NhamayControll>();
        objchothuhoach.transform.GetChild(0).GetChild(1).GetComponent<MouseDownObjThuhoachNhamay>().nhamayControll = gameObject.GetComponent<NhamayControll>();
        objchothuhoach.transform.GetChild(0).GetChild(2).GetComponent<MouseDownObjThuhoachNhamay>().nhamayControll = gameObject.GetComponent<NhamayControll>();
        objchothuhoach.transform.GetChild(0).GetChild(3).GetComponent<MouseDownObjThuhoachNhamay>().nhamayControll = gameObject.GetComponent<NhamayControll>();
        objchothuhoach.GetComponent<SPChothuhoach>().idNhamay = idNhamay;
        objchothuhoach.GetComponent<SPChothuhoach>().tagNhamay = gameObject.tag;
    }

    #endregion

    #region onmousedown nhà máy
    float mouseTime;
    Vector3 vitricu;
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
                
                if (PlayerPrefsController.instance.checkHuongdan())
                {
                    if (PlayerPrefsController.instance.getBuocHuongdan() == 6)
                    {
                        GameManagerControll.instance.destroyHand();
                        PlayerPrefsController.instance.setBuocHuongdan(7);
                        GameManagerControll.instance.activeHandMap(3);

                        //show dialog make sp
                        Until.idNhamayClick = idLuoi;
                        Until.tagNhamayClick = gameObject.tag;
                        Until.nhamayControll = gameObject.GetComponent<NhamayControll>();
                        GameManagerControll.instance._showDialog(3, idPanelNhamay);
                        Until.showPanel = true;
                        panelSX.GetComponent<LoadDialogNhamay>().nhamayControll = gameObject.GetComponent<NhamayControll>();
                        panelSX.GetComponent<LoadDialogNhamay>().idNhamay = idLuoi;
                        panelSX.GetComponent<LoadDialogNhamay>().tagNhamay = gameObject.tag;
                        panelSX.GetComponent<LoadDialogNhamay>().loadDefault();
                        panelSX.GetComponent<LoadDialogNhamay>().loadBtnLock();
                        panelSX.GetComponent<LoadDialogNhamay>().chayDongho();
                        loadListMake();
                    }
                }
                else
                {
                    if (PlayerPrefsController.instance.getListThuhoach(gameObject.tag, idLuoi).Count > 0 && checkThuhoach)
                    {
                        //thu hoạch
                        thuhoach();
                    }
                    else
                    {
                        //show dialog make sp
                        Until.idNhamayClick = idLuoi;
                        Until.tagNhamayClick = gameObject.tag;
                        Until.nhamayControll = gameObject.GetComponent<NhamayControll>();
                        GameManagerControll.instance._showDialog(3, idPanelNhamay);
                        Until.showPanel = true;
                        panelSX.GetComponent<LoadDialogNhamay>().nhamayControll = gameObject.GetComponent<NhamayControll>();
                        panelSX.GetComponent<LoadDialogNhamay>().idNhamay = idLuoi;
                        panelSX.GetComponent<LoadDialogNhamay>().tagNhamay = gameObject.tag;
                        panelSX.GetComponent<LoadDialogNhamay>().loadDefault();
                        panelSX.GetComponent<LoadDialogNhamay>().loadBtnLock();
                        panelSX.GetComponent<LoadDialogNhamay>().chayDongho();
                        loadListMake();
                    }
                }
            }

        }
    }
    #endregion

    #region đồng hồ
    
    string getFormatTime(int time)
    {
        string f = "";
        int h = Mathf.RoundToInt(time / 3600);
        int m = Mathf.RoundToInt((time % 3600) / 60);
        int s = Mathf.RoundToInt((time % 3600) % 60);
        if (h > 0)
            f = h + ":" + m + ":" + s;
        else
            if (m > 0)
        {
            if (s > 0)
            {
                f = m + ":" + s;
                if (s < 10) f = m + ":0" + s;
            }
            else f = m + ":00";
        }
        else
            f = s + "";
        return f;
    }
    #endregion

    #region change sang ô mới

    public void changeNhamay(int idluoicu, int idluoimoi)
    {
        //load btn ô sản xuất
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefsController.instance.getOsx(gameObject.tag, idluoicu, i) == 1)
            {
                PlayerPrefsController.instance.themOsx(gameObject.tag, idluoimoi, i, 1);
                PlayerPrefsController.instance.themOsx(gameObject.tag, idluoicu, i, 0);
            }
        }
    }
    #endregion
}
