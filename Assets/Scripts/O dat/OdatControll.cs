using BitBenderGames;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OdatControll : MonoBehaviour
{
    #region Declared
    public int idOdat;
    [SerializeField] GameObject objHatroi;
    [SerializeField] DataNongSan dataNongSan;
    [SerializeField] GameObject objChotuoinuoc;
    [SerializeField] GameObject objXeng, animXengxuc;

    [SerializeField] BoxCollider2D boxClick;
    public string loodat;

    public string ttodat = "null";
    SpriteRenderer anhCay;
    Animator animOdat;

    public int idCay;
    int kcpt;

    AudioSource audio;
    Caytrong caytrong;
    GameObject tuoinuoc;
    #endregion

    private void Start()
    {
        animOdat = gameObject.GetComponent<Animator>();
        anhCay = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        audio = gameObject.GetComponent<AudioSource>();

        //load cây trên ô đất
        loadCay();
        StartCoroutine(delayCheckHuongdan());
        if (gameObject.tag != "odat3")
            boxClick.enabled = false;
    }

    #region onMouseDown ô đất
    float mouseTime;
    Vector3 vitricu;
    bool checkShowShopcaytrong = false;
    public void OnMouseDown()
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
    public void OnMouseUp()
    {
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false
                && Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
                && Time.time - mouseTime < 0.2f)
            {
                //hướng dẫn
                if (PlayerPrefsController.instance.checkHuongdan())
                {
                    if (ttodat == "null" && idOdat == 2 && PlayerPrefsController.instance.getBuocHuongdan() == 3 && !checkShowShopcaytrong)
                    {
                        checkShowShopcaytrong = true;
                        GameManagerControll.instance._showBarshopcaytrong(gameObject.tag, true);
                        GameManagerControll.instance.sinhHand(2, gameObject.transform.position);
                    }
                    if (PlayerPrefsController.instance.getBuocHuongdan() == 1 && idOdat <= 2)
                    {
                        //gán ô đất đang click
                        GameManagerControll.instance.objOdatclick = gameObject.GetComponent<OdatControll>();
                        if (ttodat == "thuhoach")
                        {
                            //thu hoạch
                            GameManagerControll.instance._sinhObjLuoiliem(gameObject);
                        }
                    }
                }
                else
                {
                    //gán ô đất đang click
                    GameManagerControll.instance.objOdatclick = gameObject.GetComponent<OdatControll>();
                    Until.tagOdat = gameObject.tag;
                    if (ttodat == "null")
                        GameManagerControll.instance._showBarshopcaytrong(gameObject.tag, true);
                    if (ttodat == "caypt")
                    {
                        //show đồng hồ
                        GameManagerControll.instance.showCanvasTime(gameObject, timeClock, false);
                    }
                    else if (ttodat == "thuhoach")
                    {
                        //thu hoạch
                        GameManagerControll.instance._sinhObjLuoiliem(gameObject);
                    }
                    else
                        if (ttodat == "tuoinuoc")
                    {
                        //Debug.Log("Chờ tưới nước");
                        chotuoinuoc();
                    }
                    else if(ttodat == "cayheo")
                    {
                        //Debug.Log("Chờ tưới nước");
                        xoaCayll();
                    }
                }

            }
        }
    }
    #endregion

    #region Trồng cây, phát triển

    public int timeDachay, timeClock, timeStart;
    public int timeDachayHeo, timeClockHeo, timeStartHeo;
    int timeCay;
    public void trongcay(int id)
    {
        if (ttodat == "null")
        {
            if (PlayerPrefsController.instance.getCoin() >= dataNongSan.data[id].giamua)
            {
                //Debug.Log("trongcay: " + Until.tagOdat + "-" + id);
                idCay = id;
                caytrong = GameManagerControll.instance.dataNongSan.data[id].caytrong;
                playAudio();
                GameObject hat = Instantiate(objHatroi, gameObject.transform.position, Quaternion.identity) as GameObject;
                hat.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[id].iconMake;
                Destroy(hat, 1.5f);
                //ảnh cây
                anhCay.sprite = caytrong.getSpGd1();
                animOdat.SetBool("trongcay", true);

                //save cây trồng trên ô đất
                PlayerPrefsController.instance.saveCayOdat(loodat, idOdat, idCay);
                PlayerPrefsController.instance.exceptCoin(dataNongSan.data[id].giamua);
                GameManagerControll.instance.loadUIMain();
                //cây phát triển
                caypt(id);
            }
            else
            {
                GameManagerControll.instance.showObjText(gameObject.transform.position,
                    Until.checkLanguage() ? "Không đủ vàng" : "Not enough gold", false);
            }
        }
    }
    void caypt(int id)
    {
        //Debug.Log("caypt " + gameObject.tag + "-" + id);
        if (gameObject.tag != "odat3")
            boxClick.enabled = false;
        else boxClick.enabled = true;
        timeCay = dataNongSan.data[id].timept;
        ttodat = "caypt";
        timeStart = PlayerPrefsController.instance.getTimeStarCay(loodat, idOdat);
        timeDachay = Until.timeNow() - timeStart;
        if (timeDachay < 0)
            timeClock = 0;
        else
            timeClock = timeCay - timeDachay;

        StartCoroutine(chayCaypt(id));
    }

    IEnumerator chayCaypt(int id)
    {
        //Debug.Log("timeClock: " + timeClock);
        if (timeClock > 0)
        {
            //phát triển 6 giai đoạn odat3
            if (gameObject.CompareTag("odat3"))
            {
                //cây đã lớn - phát triển lại
                //Debug.Log("Cây lớn " + PlayerPrefsController.instance.getCayllLon(loodat, idOdat));
                if (PlayerPrefsController.instance.getCayllLon(loodat, idOdat))
                {
                    if (timeClock >= (timeCay - (timeCay / 2)))
                    {
                        if (anhCay.sprite != caytrong.getSpGd4())
                        {
                            anhCay.sprite = caytrong.getSpGd4();
                            GameManagerControll.instance.playEff(0, gameObject.transform.position);
                        }
                    }
                    else
                    {
                        if (anhCay.sprite != caytrong.getSpGd5())
                        {
                            anhCay.sprite = caytrong.getSpGd5();
                            GameManagerControll.instance.playEff(0, gameObject.transform.position);
                        }
                    }
                }
                //trồng cây mới
                else
                {
                    if (timeClock >= (timeCay - timeCay / 5))
                    {
                        if (anhCay.sprite != caytrong.getSpGd1())
                        {
                            anhCay.sprite = caytrong.getSpGd1();
                            GameManagerControll.instance.playEff(0, gameObject.transform.position);
                        }
                    }
                    else
                        if (timeClock >= (timeCay - (timeCay / 5) * 2))
                    {
                        if (anhCay.sprite != caytrong.getSpGd2())
                        {
                            anhCay.sprite = caytrong.getSpGd2();
                            GameManagerControll.instance.playEff(0, gameObject.transform.position);
                        }
                    }
                    else
                        if (timeClock >= (timeCay - (timeCay / 5) * 3))
                    {
                        if (anhCay.sprite != caytrong.getSpGd3())
                        {
                            anhCay.sprite = caytrong.getSpGd3();
                            GameManagerControll.instance.playEff(0, gameObject.transform.position);
                        }
                    }
                    else
                        if (timeClock >= (timeCay - (timeCay / 5) * 4))
                    {
                        if (anhCay.sprite != caytrong.getSpGd4())
                        {
                            //Debug.Log("=================getSpGd4================");
                            anhCay.sprite = caytrong.getSpGd4();
                            GameManagerControll.instance.playEff(0, gameObject.transform.position);
                        }
                    }
                    else
                    {
                        if (anhCay.sprite != caytrong.getSpGd5())
                        {
                            anhCay.sprite = caytrong.getSpGd5();
                            GameManagerControll.instance.playEff(0, gameObject.transform.position);
                        }
                    }
                }
            }
            //phát triển 4 giai đoạn 2 loại: odat và odat2
            else
            {
                //thay ảnh cây dựa vào thời gian phát triển
                if (timeClock >= (timeCay - timeCay / 3))
                {
                    if (anhCay.sprite != caytrong.getSpGd1())
                    {
                        anhCay.sprite = caytrong.getSpGd1();
                        GameManagerControll.instance.playEff(0, gameObject.transform.position);
                    }
                }
                else
                    if (timeClock >= (timeCay - (timeCay / 3) * 2))
                {
                    if (anhCay.sprite != caytrong.getSpGd2())
                    {
                        anhCay.sprite = caytrong.getSpGd2();
                        GameManagerControll.instance.playEff(0, gameObject.transform.position);
                    }
                }

                else
                {
                    if (anhCay.sprite != caytrong.getSpGd3())
                    {
                        anhCay.sprite = caytrong.getSpGd3();
                        GameManagerControll.instance.playEff(0, gameObject.transform.position);
                    }
                }
            }
        }
        else
        {
            //cây chín
            if (gameObject.CompareTag("odat3"))
            {
                //Debug.Log("cây chín: " + gameObject.name);
                if (anhCay.sprite != caytrong.getSpGd6())
                    anhCay.sprite = caytrong.getSpGd6();
            }
            else
            {
                if (anhCay.sprite != caytrong.getSpGd4())
                    anhCay.sprite = caytrong.getSpGd4();
            }
            GameManagerControll.instance.playEff(0, gameObject.transform.position);
            //objChothuhoach.SetActive(true);
            ttodat = "thuhoach";
            boxClick.enabled = true;
        }
        yield return new WaitForSeconds(1f);
        if (timeClock > 0)
        {
            timeClock--;
            StartCoroutine(chayCaypt(id));
        }
    }

    public int getKcpt()
    {
        float kc = 1;
        if (timeClock > timeCay * 0.7f)
            kc = dataNongSan.data[idCay].kcpt;
        if (timeClock <= timeCay * 0.7f && timeClock > timeCay * 0.5f)
            kc = dataNongSan.data[idCay].kcpt * 0.7f;
        if (timeClock <= timeCay * 0.5f && timeClock > timeCay * 0.3f)
            kc = dataNongSan.data[idCay].kcpt * 0.5f;
        if (timeClock <= timeCay * 0.3f)
            kc = dataNongSan.data[idCay].kcpt * 0.3f;
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
            PlayerPrefsController.instance.setTimeStartCay(loodat, idOdat, 0);
            timeClock = 0;
            StartCoroutine(chayCaypt(idCay));
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
        StartCoroutine(chayCaypt(idCay));
    }
    //thuhoach
    void thuhoach()
    {
        if (gameObject.CompareTag("odat3"))
        {
            boxClick.enabled = true;
            ttodat = "tuoinuoc";
            destroyCanvastuoinuoc();
            anhCay.sprite = caytrong.getSpGd4();
            animOdat.SetBool("trongcay", true);
            //Debug.Log("Thuhoach " + gameObject.name+"->"+idCay);
            PlayerPrefsController.instance.setTimeStartCay(loodat, idOdat, 0);
            PlayerPrefsController.instance.setTuoinuocCayll(loodat, idOdat, 1);
            PlayerPrefsController.instance.setCayllLon(loodat, idOdat, 1);
            PlayerPrefsController.instance.addKho(idCay, 1);

            PlayerPrefsController.instance.setTimeStartHeo(loodat, idOdat, Until.timeNow());
            chayCayheo();
        }
        else
        {
            destroyCaytrong();
            boxClick.enabled = false;
            PlayerPrefsController.instance.setTimeStartCay(loodat, idOdat, 0);
            PlayerPrefsController.instance.removeCayOdat(loodat, idOdat);
        }
        
        GameManagerControll.instance.playEff(1, gameObject.transform.position);
        GameManagerControll.instance._sinhObjThuhoach(gameObject, 0, "caytrong");
        PlayerPrefsController.instance.addKho(idCay, 1);
    }
    public void destroyCaytrong()
    {
        //Debug.Log("destroyCaytrong " + idOdat+gameObject.name);
        ttodat = "null";
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
    //chạy cây héo
    bool chayheo = false;
    void chayCayheo()
    {
        chayheo = true;
        timeStartHeo = PlayerPrefsController.instance.getTimeStartHeo(loodat, idOdat);
        timeDachayHeo = Until.timeNow() - timeStartHeo;
        if (timeDachayHeo < 0)
            timeClockHeo = 0;
        else
            timeClockHeo = dataNongSan.data[idCay].timept / 2 - timeDachayHeo;
        StartCoroutine(chaycayheo());
    }

    IEnumerator chaycayheo()
    {
        if (timeClockHeo > 0)
        {
            //Debug.Log("chaycayheo: getSpGd4" + gameObject.name);
            if (anhCay.sprite != caytrong.getSpGd4())
            {
                anhCay.sprite = caytrong.getSpGd4();
            }
        }
        else
        {
            if (anhCay.sprite != caytrong.getSpGd7())
            {
                ttodat = "cayheo";
                //Debug.Log("Cay heo " + gameObject.tag + "-" + idOdat);
                PlayerPrefsController.instance.setCayheo(loodat, idOdat, 1);
                //show xẻng xúc cây
                sinhObjXeng();
                anhCay.sprite = caytrong.getSpGd7();
            }
        }
        yield return new WaitForSeconds(1f);
        if (timeClockHeo > 0 && chayheo)
        {
            timeClockHeo--;
            StartCoroutine(chaycayheo());
        }
    }
    #endregion

    #region Tưới nước cho cây lâu năm
    public void chotuoinuoc()
    {
        //Destroy(tuoinuoc);
        destroyCanvastuoinuoc();
        GameManagerControll.instance._sinhCanvasTuoinuoc(gameObject);
    }
    public void xoaCayll()
    {
        if (tuoinuoc != null) Destroy(tuoinuoc);
        PlayerPrefsController.instance.setTimeStartCay(loodat, idOdat, 0);
        PlayerPrefsController.instance.removeCayOdat(loodat, idOdat);
        StartCoroutine(delayXoacayll());
    }

    IEnumerator delayXoacayll()
    {
        GameObject animxeng = Instantiate(animXengxuc, gameObject.transform.position, Quaternion.identity, transform) as GameObject;
        Destroy(animxeng, 2.2f);
        yield return new WaitForSeconds(2f);
        if (gameObject.CompareTag("odat3"))
        {
            //Debug.Log("Xoa cay " + gameObject.name);
            PlayerPrefsController.instance.setTuoinuocCayll(loodat, idOdat, 0);
            PlayerPrefsController.instance.setCayllLon(loodat, idOdat, 0);
            PlayerPrefsController.instance.setTimeStartHeo(loodat, idOdat, 0);
            PlayerPrefsController.instance.setCayheo(loodat, idOdat, 0);
        }
        GameManagerControll.instance.playEff(1, gameObject.transform.position);

        timeClockHeo = -1;
        if (tuoinuoc != null) Destroy(tuoinuoc);
        GameManagerControll.instance.destroyHand();
        destroyCaytrong();
    }
    void sinhObjTuoinuoc()
    {
        if (tuoinuoc != null) Destroy(tuoinuoc);
        tuoinuoc = Instantiate(objChotuoinuoc, transform.position, Quaternion.identity, transform) as GameObject;
        tuoinuoc.transform.GetChild(0).GetChild(0).GetComponent<MouseDownObjThuhoachNhamay>().odatControll = this;
    }
    void sinhObjXeng()
    {
        if (tuoinuoc != null) Destroy(tuoinuoc);
        tuoinuoc = Instantiate(objXeng, transform.position, Quaternion.identity, transform) as GameObject;
        tuoinuoc.transform.GetChild(0).GetChild(0).GetComponent<MouseDownObjThuhoachNhamay>().odatControll = this;
    }

    //sau 5s nếu chưa tưới nước thì sẽ tự hiện lại canvas tưới nước
    public void destroyCanvastuoinuoc()
    {
        StartCoroutine(delayDestroyCanvastuoinuoc());
    }

    IEnumerator delayDestroyCanvastuoinuoc()
    {
        if(tuoinuoc != null)
            Destroy(tuoinuoc);
        yield return new WaitForSeconds(5f);
        if (PlayerPrefsController.instance.getTuoinuocCayll(loodat, idOdat))
            if (tuoinuoc == null)
                sinhObjTuoinuoc();
    }

    void tuoinuocphattrien()
    {
        if (PlayerPrefsController.instance.getVpKho(GameManagerControll.instance.idBinhtuoinuoc) > 0 && ttodat != "cayheo")
        {
            if (tuoinuoc != null)
                Destroy(tuoinuoc);
            ttodat = "caypt";
            int timeptlai = dataNongSan.data[idCay].timept / 3;
            chayheo = false;
            GameObject binhnuoc = Instantiate(objHatroi, gameObject.transform.position, Quaternion.identity) as GameObject;
            binhnuoc.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dataNongSan.data[GameManagerControll.instance.idBinhtuoinuoc].iconMake;
            Destroy(binhnuoc, 1.5f);
            PlayerPrefsController.instance.setTimeStartCay(loodat, idOdat, Until.timeNow() - timeptlai);
            PlayerPrefsController.instance.setTuoinuocCayll(loodat, idOdat, 0);
            caypt(idCay);
            PlayerPrefsController.instance.removeVpKho(GameManagerControll.instance.idBinhtuoinuoc, 1);
            //Debug.Log("tuoinuocphattrien " + gameObject.name + "->" + idCay);
            //Debug.Log("getTimeStarCay " + PlayerPrefsController.instance.getTimeStarCay(loodat, idOdat));
        }
        else
        {
            sinhObjTuoinuoc();
            GameManagerControll.instance.showObjText(transform.position, Until.checkLanguage() ? "Không đủ nước tưới cây" : "Not enough water to water plants", false);
        }
    }
    #endregion

    #region Check va chạm: hạt giống - trồng cây, lưỡi liềm - thu hoạch
    bool vachamtuoicay;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //hạt loaij1 - ô đất 1
        if (collision.CompareTag("hatgiong") && gameObject.CompareTag("odat"))
        {
            int idCay = collision.gameObject.GetComponents<HatgiongForllow>()[0].idHatgiong;
            trongcay(idCay);
        }
        //hạt loaij1 - ô đất 1
        if (collision.CompareTag("hatgiong2") && gameObject.CompareTag("odat2"))
        {
            int idCay = collision.gameObject.GetComponents<HatgiongForllow>()[0].idHatgiong;
            trongcay(idCay);
        }
        //hạt loaij1 - ô đất 1
        if (gameObject.CompareTag("odat3"))
        {
            if (collision.CompareTag("hatgiong3"))
            {
                int idCay = collision.gameObject.GetComponents<HatgiongForllow>()[0].idHatgiong;
                trongcay(idCay);
            }
            if (collision.CompareTag("binhtuoinuoc") && !vachamtuoicay)
            {
                bool checkXoacay = collision.gameObject.GetComponents<HatgiongForllow>()[0].checkXoacay;
                if (checkXoacay )
                {
                    if(GameManagerControll.instance.objOdatclick.idOdat == idOdat) {
                        collision.gameObject.GetComponents<CircleCollider2D>()[0].enabled = false;
                        //StopCoroutine(chaycayheo());
                        xoaCayll();
                    }
                }
                else
                {
                    StartCoroutine(delayVachamtuoicay());
                    if (PlayerPrefsController.instance.getTuoinuocCayll(loodat, idOdat)
                        && !PlayerPrefsController.instance.getCayheo(loodat, idOdat))
                        tuoinuocphattrien();
                }
            }
        }
        

        if (ttodat == "thuhoach")
        {
            GameManagerControll.instance.objOdatclick = gameObject.GetComponent<OdatControll>();
            if (collision.CompareTag("luoiliem") && !gameObject.CompareTag("odat3"))
                thuhoach();
            if (collision.CompareTag("giothuhoach") && gameObject.CompareTag("odat3"))
                thuhoach();
        }
    }

    IEnumerator delayVachamtuoicay()
    {
        vachamtuoicay = true;
        yield return new WaitForSeconds(2f);
        vachamtuoicay = false;
    }
    void playAudio()
    {
        audio.volume = PlayerPrefsController.instance.getVolume("sound");
        audio.Play();
    }
    #endregion

    #region loadCay
    void loadCay()
    {
        if (PlayerPrefsController.instance.checkCayTrenodat(loodat, idOdat))
        {
            //ảnh cây
            idCay = PlayerPrefsController.instance.getIdcay(loodat, idOdat);
            caytrong = dataNongSan.data[idCay].caytrong;

            if (gameObject.CompareTag("odat3"))
            {
                if (PlayerPrefsController.instance.getCayllLon(loodat, idOdat))
                    anhCay.sprite = caytrong.getSpGd4();
                else
                    anhCay.sprite = caytrong.getSpGd1();
                animOdat.SetBool("trongcay", true);
                //Debug.Log("loadCay " + gameObject.tag + "-" + PlayerPrefsController.instance.getTuoinuocCayll(loodat, idOdat));
                if (PlayerPrefsController.instance.getTuoinuocCayll(loodat, idOdat))
                {
                    ttodat = "tuoinuoc";
                    chayCayheo();
                    if (ttodat != "cayheo")
                        sinhObjTuoinuoc();
                }
                else
                {
                    caypt(idCay);
                }
            }
            else
            {
                anhCay.sprite = caytrong.getSpGd1();
                animOdat.SetBool("trongcay", true);
                //cây phát triển
                caypt(idCay);
            }

        }
    }
    #endregion

    #region camview
    public void _camView()
    {
        Camera.main.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.2f, -40), 0.5f);
    }
    #endregion

    #region hướng dẫn

    IEnumerator delayCheckHuongdan()
    {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("Hướng dẫn odat=> " + Until.checkHuongdan + Until.huongdan);
        if (PlayerPrefsController.instance.checkHuongdan() )
        {
            //Until.huongdan = 1;
            //Debug.Log("Hướng dẫn");
            huongdan();
        }
    }

    public void huongdan()
    {
        if (idOdat == 0 || idOdat == 1 || idOdat == 2)
        {
            //ảnh cây
            MobileTouchCamera.checkCamFollow = true;
            if (idOdat == 0 && PlayerPrefsController.instance.getChat() == 3)
            {
                GameManagerControll.instance.sinhHand(0, gameObject.transform.position);
                GameManagerControll.instance._camView(gameObject.transform.position);
            }
            caytrong = dataNongSan.data[0].caytrong;
            anhCay.sprite = caytrong.getSpGd1();
            animOdat.SetBool("trongcay", true);
            //save cây trồng trên ô đất
            PlayerPrefsController.instance.saveCayOdat(loodat, idOdat, 0);
            PlayerPrefsController.instance.setTimeStartCay(loodat, idOdat, 0);
            caypt(0);
        }
    }

    #endregion
}
