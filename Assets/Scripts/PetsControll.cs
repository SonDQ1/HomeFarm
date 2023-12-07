using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetsControll : MonoBehaviour {

    public int idconvat;
    public int timept;
    [SerializeField] GameObject objChothuhoach;
    public bool checkInstance = false;
    string trangthai = "null";
    public int timeDachay, timeClock, timeStart;
    AudioSource audioSource;
    void Start () {

        objChothuhoach.SetActive(false);
        trangthai = PlayerPrefsController.instance.getTTPets(idconvat);
        if (checkInstance)
        {
            PlayerPrefsController.instance.setTimeStartPets(idconvat, Until.timeNow());
            checkInstance = false;
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
            playSoundDV();
            if (trangthai == "thuhoach")
            {
                thuhoach();
            }
            else
                if (trangthai == "phattrien")
            {
                //show cv time
                GameManagerControll.instance.showCVPets(gameObject, timeClock);
            }
        }
    }
    #endregion

    #region chạy phát triển
    public void ptGieng()
    {
        timeStart = PlayerPrefsController.instance.getTimeStarPets(idconvat);
        timeDachay = Until.timeNow() - timeStart;
        if (timeDachay < 0)
            timeClock = 0;
        else
            timeClock = timept - timeDachay;

        StartCoroutine(chayPtGieng());
    }
    IEnumerator chayPtGieng()
    {
        if (timeClock <= 0)
        {
            trangthai = "thuhoach";
            objChothuhoach.SetActive(true);
        }
        else
        {
            if(trangthai != "phattrien")
            {
                trangthai = "phattrien";
                PlayerPrefsController.instance.setTTPets(idconvat, trangthai);
            }
        }
        yield return new WaitForSeconds(1f);
        if (timeClock > 0)
        {
            timeClock--;
            StartCoroutine(chayPtGieng());
        }
    }

    public void thuhoach()
    {
        trangthai = "null";
        PlayerPrefsController.instance.setTTPets(idconvat, trangthai);
        objChothuhoach.SetActive(false);
        StartCoroutine(delayRandomItem());
    }
    public void playSoundDV()
    {
        audioSource = GetComponents<AudioSource>()[0];
        audioSource.volume = PlayerPrefsController.instance.getVolume("sound");
        audioSource.Play();
    }
    IEnumerator delayRandomItem()
    {
        GameManagerControll.instance.playEff(1, gameObject.transform.position);
        GameManagerControll.instance.sinhvatphamnhan(gameObject.transform.position, 
            objChothuhoach.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite
            , GameManagerControll.instance.posKho);
        //PlayerPrefsController.instance.addKho(idSp, 2);
        //nhận thưởng
        getRewars();
        //tiếp tục chạy
        yield return new WaitForSeconds(0.5f);
        PlayerPrefsController.instance.setTimeStartPets(idconvat, Until.timeNow());
        ptGieng();
    }

    void getRewars()
    {
        int sl = 0;
        string nameRewas = "";
        switch (idconvat)
        {
            case 0:
                sl = 2;
                break;
            case 1:
                sl = 3;
                break;
            case 2:
                sl = 5;
                break;
            case 3:
                sl = 7;
                break;
            case 4:
                sl = 9;
                break;
            case 5:
                sl = 15;
                break;
        }
        nameRewas = Until.checkLanguage() ? " thuốc tăng trưởng" : " grow-up pill";
        PlayerPrefsController.instance.addKho(GameManagerControll.instance.idSpeedTime, sl);
        //switch (idconvat)
        //{
        //    case 0:
        //        sl = Random.Range(2,5);
        //        nameRewas = Until.checkLanguage() ? " thuốc tăng trưởng" : " grow-up pill";
        //        PlayerPrefsController.instance.addKho(GameManagerControll.instance.idSpeedTime,sl);
        //        break;
        //    case 1:
        //        sl = PlayerPrefsController.instance.getLevel() * 100;
        //        nameRewas = Until.checkLanguage() ? " vàng" : " gold";
        //        PlayerPrefsController.instance.addCoin(sl);
        //        break;
        //    case 2:
        //        sl = Random.Range(2, 11);
        //        nameRewas = Until.checkLanguage() ? " đá quý" : " gem";
        //        PlayerPrefsController.instance.addGem(sl);
        //        break;

        //    case 3:
        //        sl = Random.Range(2, 6);
        //        nameRewas = Until.checkLanguage() ? " kim cương" : " diamod";
        //        PlayerPrefsController.instance.addDiamod(sl);
        //        break;
        //}
        GameManagerControll.instance.showObjText(transform.position, "+ " + sl + nameRewas, false);

    }
    #endregion
}
