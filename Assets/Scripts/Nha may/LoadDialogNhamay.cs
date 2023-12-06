using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDialogNhamay : MonoBehaviour
{

    //public NhamayControll nhamayControll;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject infoItemMake, itemDangsx;
    [SerializeField] Image imgLoad;
    [SerializeField] Text txtTime, txtDiamod;
    public GameObject[] btnLock;
    int idBtn;
    int kc = 2;
    GameObject info;
    //truyền từ nhà máy sang
    public NhamayControll nhamayControll;
    public int idNhamay;
    public string tagNhamay;

    //private void OnEnable()
    //{
    //    loadDefault();
    //    loadBtnLock();
    //}

    #region button
    public void btnUnlockClick(int id)
    {
        //Debug.Log("btnUnlockClick = " + id);
        if (btnLock[id].transform.GetChild(1).gameObject.activeInHierarchy)
        {
            idBtn = id;
            kc = 2;
            if (id == 1) kc = 3;
            if (id == 2) kc = 7;
            GameManagerControll.instance.setDialogMokhoaSx(kc);
            GameManagerControll.instance._showDialogThemOsx(gameObject);
        }
    }
    public void btnItemClick(GameObject objItemMake)
    {
        if (info != null) Destroy(info);
        info = Instantiate(infoItemMake, objItemMake.transform.position, Quaternion.identity) as GameObject;
        info.GetComponent<ItemMakeSp>().itemNedd = objItemMake.GetComponent<ItemMakeBtn>().itemNedd;
        info.GetComponent<ItemMakeSp>().nameSp = Until.checkLanguage() ?
            GameManagerControll.instance.dataNongSan.data[objItemMake.GetComponent<DragItemMake>().idsp].nameV :
            GameManagerControll.instance.dataNongSan.data[objItemMake.GetComponent<DragItemMake>().idsp].nameE;
        info.GetComponent<ItemMakeSp>().timePt = GameManagerControll.instance.dataNongSan.data[objItemMake.GetComponent<DragItemMake>().idsp].timept;
        info.GetComponent<ItemMakeSp>().slKho = PlayerPrefsController.instance.getVpKho(objItemMake.GetComponent<DragItemMake>().idsp);

        float sizeZoom = Camera.main.GetComponent<Camera>().orthographicSize;
        float size = 1f;
        if (sizeZoom > 3 && sizeZoom < 4) size = 1.3f;
        else if (sizeZoom >= 4 && sizeZoom <= 5) size = 1.5f;
        info.transform.localScale = new Vector2(size, size);
    }

    public void btnNextClick()
    {
        //if (scrollRect.velocity.x == 0)
            scrollRect.horizontalNormalizedPosition += 0.01f;
        scrollRect.velocity = new Vector2(-1000f, 0f);
    }

    public void btnBuyClick()
    {
        nhamayControll.btnBuyKcpt();
        GameManagerControll.instance.disbaleHandMap();
    }
    #endregion

    public void desTroyInfo()
    {
        Destroy(info);
    }
    public void themOsx()
    {
        btnLock[idBtn].transform.GetChild(0).gameObject.SetActive(false);
        btnLock[idBtn].transform.GetChild(1).gameObject.SetActive(false);
        btnLock[idBtn].transform.GetComponent<BoxCollider2D>().enabled = true;
        if (idBtn + 1 < 3)
            btnLock[idBtn + 1].SetActive(true);
        PlayerPrefsController.instance.themOsx(tagNhamay, idNhamay, idBtn, 1);
        Debug.Log("Thêm ô sx: " + tagNhamay + idNhamay + idBtn + "= " + PlayerPrefsController.instance.getOsx(Until.tagNhamayClick, Until.idNhamayClick, idBtn));
    }

    #region load các ô tạo sản phẩm
    public void loadDefault()
    {
        itemDangsx.transform.GetChild(1).gameObject.SetActive(false);
        itemDangsx.transform.GetChild(2).gameObject.SetActive(false);
        itemDangsx.transform.GetChild(3).gameObject.SetActive(false);

        btnLock[0].transform.GetChild(0).gameObject.SetActive(false);
        btnLock[0].transform.GetChild(1).gameObject.SetActive(true);
        btnLock[1].SetActive(false);
        btnLock[1].transform.GetChild(0).gameObject.SetActive(false);
        btnLock[1].transform.GetChild(1).gameObject.SetActive(true);
        btnLock[2].SetActive(false);
        btnLock[2].transform.GetChild(0).gameObject.SetActive(false);
        btnLock[2].transform.GetChild(1).gameObject.SetActive(true);
    }
    public void loadBtnLock()
    {
        for (int i = 0; i < btnLock.Length; i++)
        {
            btnLock[i].transform.GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log("loadBtnLock: " + tagNhamay + idNhamay + i+"= " + PlayerPrefsController.instance.getOsx(Until.tagNhamayClick, Until.idNhamayClick, i));
            if (PlayerPrefsController.instance.getOsx(tagNhamay, idNhamay, i) == 1)
            {
                if (btnLock[i].transform.GetChild(0).GetComponent<Image>() == null)
                    btnLock[i].transform.GetChild(0).gameObject.SetActive(false);
                btnLock[i].transform.GetChild(1).gameObject.SetActive(false);
                btnLock[i].transform.GetComponent<BoxCollider2D>().enabled = true;
                if (i + 1 < 3) btnLock[i + 1].SetActive(true);
            }
        }
    }

    #endregion
    public void dragScrollHo(int i)
    {
        if (i > 0)
        {
            scrollRect.horizontalNormalizedPosition += 0.01f;
            scrollRect.velocity = new Vector2(-200f, 0f);
        }
        else
        {
            scrollRect.horizontalNormalizedPosition -= 0.01f;
            scrollRect.velocity = new Vector2(200f, 0f);
        }
    }

    #region đồng hồ
    public void chayDongho()
    {
        StartCoroutine(chayDh());
    }

    IEnumerator chayDh()
    {
        demthoigian();
        yield return new WaitForSeconds(1f);
        if (nhamayControll.timeClock > 0)
            StartCoroutine(chayDh());
    }

    //đồng hôd
    public void demthoigian()
    {
        int h = Mathf.RoundToInt(nhamayControll.timeClock / 3600);
        int m = Mathf.RoundToInt((nhamayControll.timeClock % 3600) / 60);
        int s = Mathf.RoundToInt((nhamayControll.timeClock % 3600) % 60);

        if (h > 0)
            txtTime.text = h + ":" + m + ":" + s;
        else
            if (m > 0)
        {
            if (m < 10)
            {
                if (s < 10) txtTime.text = "0" + m + ":0" + s;
                else
                    txtTime.text = "0" + m + ":" + s;
            }
            else
                if (s < 10) txtTime.text = m + ":0" + s;
            else
                txtTime.text = m + ":" + s;
        }
        else
                if (s < 10) txtTime.text = "00:0" + s + "";
        else
            txtTime.text = "00:" + s + "";

        if(nhamayControll.getKcpt() == 0 || nhamayControll == null)
            txtDiamod.text = 1 + "";
        else
            txtDiamod.text = nhamayControll.getKcpt() + "";
        imgLoad.fillAmount = getPercen(nhamayControll.timeSP, nhamayControll.timeClock);
    }

    float getPercen(long timeLife, long timeDachay)
    {
        return (float)timeDachay / timeLife;
    }
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
}
