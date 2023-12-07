using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChuongController : MonoBehaviour
{

    public static ChuongController instance;
    public int idChuong;
    public int levelUnlock;

    [SerializeField] GameObject objTuithucan;
    [SerializeField] GameObject[] convat;
    //[SerializeField] SpriteRenderer sprChuong;
    //[SerializeField] Sprite spChuong, spChuongTA;

    [SerializeField] GameObject objDragTuithucan;
    [SerializeField] int idtuita;
    float mouseTime;
    Vector3 vitricu;
    GameObject objCanvasTuiTA;
    void Start()
    {
        instance = this;
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
    private void OnMouseUp()
    {
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false
                && Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
                && Time.time - mouseTime < 0.2f)
            {
                transform.GetChild(2).GetComponent<MouseDownConvat>().playSoundAnimal();
                if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 9)
                {
                    GameManagerControll.instance.sinhHand(5, gameObject.transform.position);
                }
                if (checkThuhoachConvat())
                {
                    //thuhoach
                    thuhoachconvat();
                }
                else
                    showCanvasTA();

                Until.chuongController = gameObject.GetComponent<ChuongController>();
            }
        }
    }
    public void showCanvasTA()
    {
        GameManagerControll.instance._sinhObjCanvasTuiTA(idtuita, gameObject.transform.position);
    }
    public void sinhTuiThucAn(Vector3 pos, Sprite sp)
    {
        GameObject tuithucan = Instantiate(objTuithucan, pos, Quaternion.identity) as GameObject;
        tuithucan.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sp;
        Destroy(tuithucan, 1.5f);
    }
    //set active con vật
    public void setActiveConvat(bool check)
    {
        convat[0].SetActive(check);
        convat[1].SetActive(check);
        convat[2].SetActive(check);
        if(convat.Length==4)
            convat[3].SetActive(check);
    }

    public void changeAnimail(int idluoicu, int idluoimoi)
    {
        for (int i = 0; i < convat.Length; i++)
        {
            convat[i].GetComponent<MouseDownConvat>().changeConvat(idluoicu, idluoimoi);
        }
    }

    bool checkThuhoachConvat()
    {
        for (int i = 0; i < convat.Length; i++)
        {
            string trangthai = convat[i].GetComponent<MouseDownConvat>().trangthai;
            bool checkThuhoach = convat[i].GetComponent<MouseDownConvat>().checkThuhoach;
            if (trangthai == "thuhoach" && checkThuhoach)
            {
                return true;
            }
        }
        return false;
    }
    int idconvat = 0;
    void thuhoachconvat()
    {
        StartCoroutine(delayThuhoach());
    }

    IEnumerator delayThuhoach()
    {
        string trangthai = convat[idconvat].GetComponent<MouseDownConvat>().trangthai;
        bool checkThuhoach = convat[idconvat].GetComponent<MouseDownConvat>().checkThuhoach;
        if (trangthai == "thuhoach" && checkThuhoach)
        {
            convat[idconvat].GetComponent<MouseDownConvat>().thuhoach();
        }
        yield return new WaitForSeconds(0.2f);
        idconvat++;
        if (idconvat < convat.Length)
        {
            StartCoroutine(delayThuhoach());
        }
        else idconvat = 0;
    }
}
