using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KhoController : MonoBehaviour
{
    public static KhoController instance;

    [Header("Data")]
    public DataNongSan dataNongSan;
    [Header("Click vào các tab")]
    [SerializeField] Image[] spTab;
    [SerializeField] GameObject[] tabScroll;
    [SerializeField] Sprite spTabOn, spTabOff;
    [SerializeField] GameObject txtNoVp;

    [SerializeField] GameObject item;
    [SerializeField] GameObject infoItem;
    [SerializeField] Transform tfBginfo;
    [SerializeField] GameObject contentKho;


    int idTabOdl = 0;
    int coutItem = 0;

    GameObject info;

    private void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        //load các sản phẩm từ data
        loadData();
        StartCoroutine(delayBtnTabClickOnenable());
        //check ngôn ngữ hiển thị
    }
    IEnumerator delayBtnTabClickOnenable()
    {
        yield return new WaitForSeconds(0.3f);
        btnTabClick(0);
    }
    #region button
    //tab click
    public void btnTabClick(int id)
    {
        txtNoVp.SetActive(false);
        if (info != null)
            Destroy(info);
        //tabScroll[idTabOdl].SetActive(false);
        spTab[idTabOdl].sprite = spTabOff;
        spTab[idTabOdl].SetNativeSize();
        idTabOdl = id;

        //tabScroll[id].SetActive(true);
        spTab[id].sprite = spTabOn;
        spTab[id].SetNativeSize();

        switch (id)
        {
            case 0:
                loadData();
                break;
            case 1:
                loadDataMaterials();
                break;
            case 2:
                loadDataProductrs();
                break;
        }
    }
    public void btnCloseClick()
    {
        if (info != null)
            Destroy(info);
        StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));
    }

    public void btnItemKhoClick(GameObject itemkho)
    {
        ItemKho itemKhocs = itemkho.GetComponent<ItemKho>();
        Vector2 pos = itemkho.transform.position;
        if (itemKhocs.checkLeft)
            pos.x -= 2.4f;
        if (info != null)
            Destroy(info);
        //show info sản phẩm, bán sản phẩm
        info = Instantiate(infoItem, pos, Quaternion.identity) as GameObject;
        info.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Until.checkLanguage() ? dataNongSan.data[itemKhocs.idItemkho].nameV : dataNongSan.data[itemKhocs.idItemkho].nameE;
        info.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = dataNongSan.data[itemKhocs.idItemkho].iconMake;
        info.transform.GetChild(0).GetChild(2).GetComponent<Image>().SetNativeSize();
        info.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = dataNongSan.data[itemKhocs.idItemkho].giaban + "";
        info.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<Text>().text = Until.checkLanguage() ? "Bán" : "Sell";
        info.GetComponent<SellItemKho>().idvp = itemKhocs.idItemkho;
        info.GetComponent<SellItemKho>().giavp = dataNongSan.data[itemKhocs.idItemkho].giaban;

        float sizeZoom = Camera.main.GetComponent<Camera>().orthographicSize;
        float size = 1f;
        if (sizeZoom > 3 && sizeZoom < 4) size = 1.3f;
        else if (sizeZoom >= 4 && sizeZoom <= 5) size = 1.5f;
        info.transform.localScale = new Vector2(size, size);
    }

    #endregion

    #region procedure - function 
    int countOld = 3;
    void loadData()
    {
        delItemKhoInScroll();
        //sinh các item có trong kho
        countOld = 3;
        for (int i = 0; i < dataNongSan.data.Length; i++)
        {
            if (PlayerPrefsController.instance.getVpKho(i) > 0)
            {
                GameObject itemkho = Instantiate(item, new Vector2(0, 0), Quaternion.identity, contentKho.transform) as GameObject;
                itemkho.transform.GetChild(0).GetComponent<Image>().sprite = dataNongSan.data[i].iconMake;
                itemkho.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                itemkho.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefsController.instance.getVpKho(i) + ""; //số lượng có trong kho
                itemkho.GetComponent<ItemKho>().idItemkho = i;
                itemkho.GetComponent<ItemKho>().posBtnKho = itemkho.transform.position;
                itemkho.GetComponent<Button>().onClick.AddListener(() => btnItemKhoClick(itemkho));
                //3,4;8,9;13;14
                if(checkLeft(coutItem))
                    itemkho.GetComponent<ItemKho>().checkLeft = true;
                coutItem++;
            }
        }
        changeSizeScroll();
        if (coutItem == 0)
        {
            txtNoVp.SetActive(true);
            txtNoVp.GetComponent<Text>().text = Until.checkLanguage() ? "Không có sản phẩm" : "No product";
        }
    }
    void loadDataMaterials()
    {
        delItemKhoInScroll();
        //sinh các item có trong kho
        countOld = 3;
        for (int i = 0; i < 18; i++)
        {
            if (PlayerPrefsController.instance.getVpKho(i) > 0)
            {
                GameObject itemkho = Instantiate(item, new Vector2(0, 0), Quaternion.identity, contentKho.transform) as GameObject;
                itemkho.transform.GetChild(0).GetComponent<Image>().sprite = dataNongSan.data[i].iconMake;
                itemkho.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                itemkho.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefsController.instance.getVpKho(i) + ""; //số lượng có trong kho
                itemkho.GetComponent<ItemKho>().idItemkho = i;
                itemkho.GetComponent<ItemKho>().posBtnKho = itemkho.transform.position;
                itemkho.GetComponent<Button>().onClick.AddListener(() => btnItemKhoClick(itemkho));
                if (checkLeft(coutItem))
                    itemkho.GetComponent<ItemKho>().checkLeft = true;
                
                coutItem++;
            }
        }
        
        changeSizeScroll();
        if (coutItem == 0)
        {
            txtNoVp.SetActive(true);
            txtNoVp.GetComponent<Text>().text = Until.checkLanguage() ? "Không có sản phẩm" : "No material";
        }
    }
    void loadDataProductrs()
    {
        delItemKhoInScroll();
        //sinh các item có trong kho
        countOld = 3;
        for (int i = 18; i < 77; i++)
        {
            if (PlayerPrefsController.instance.getVpKho(i) > 0)
            {
                GameObject itemkho = Instantiate(item, new Vector2(0, 0), Quaternion.identity, contentKho.transform) as GameObject;
                itemkho.transform.GetChild(0).GetComponent<Image>().sprite = dataNongSan.data[i].iconMake;
                itemkho.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                itemkho.transform.GetChild(1).GetComponent<Text>().text = PlayerPrefsController.instance.getVpKho(i) + ""; //số lượng có trong kho
                itemkho.GetComponent<ItemKho>().idItemkho = i;
                itemkho.GetComponent<ItemKho>().posBtnKho = itemkho.transform.position;
                itemkho.GetComponent<Button>().onClick.AddListener(() => btnItemKhoClick(itemkho));
                if (checkLeft(coutItem))
                    itemkho.GetComponent<ItemKho>().checkLeft = true;
                coutItem++;
            }
        }

        changeSizeScroll();
        if (coutItem == 0)
        {
            txtNoVp.SetActive(true);
            txtNoVp.GetComponent<Text>().text = Until.checkLanguage() ? "Không có sản phẩm" : "No product";
        }
    }

    bool checkLeft(int countItem)
    {
        //check sinh dialog bán sp trong shop=> sinh bên trái, bên phải
        if (coutItem == countOld)
            return true;
        else
            if (coutItem == countOld + 1)
            {
                countOld += 5;
                return true;
            }
        return false;
    }

    public void reloadƯhenCellItem()
    {
        switch (idTabOdl)
        {
            case 0:
                loadData();
                break;
            case 1:
                loadDataMaterials();
                break;
            case 2:
                loadDataProductrs();
                break;
        }
    }

    void changeSizeScroll()
    {
        int row = 0;
        if (coutItem > 15 && coutItem <= 20) row = 1;
        if (coutItem > 20 && coutItem <= 25) row = 2;
        if (coutItem > 25 && coutItem <= 30) row = 3;
        if (coutItem > 30 && coutItem <= 35) row = 4;
        if (coutItem > 35 && coutItem <= 40) row = 5;
        if (coutItem > 40 && coutItem <= 45) row = 6;
        if (coutItem > 45 && coutItem <= 50) row = 7;
        if (coutItem > 50 && coutItem <= 55) row = 8;
        if (coutItem > 55 && coutItem <= 60) row = 9;
        contentKho.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        contentKho.GetComponent<RectTransform>().sizeDelta = new Vector2(455, 335 + (107 * row));//297 * x = 460
        contentKho.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
    }

    //keo scroll tắt info
    public void dragScrollRect()
    {
        if (info != null)
            Destroy(info);
    }
    void delItemKhoInScroll()
    {
        //xóa hết các item trước đó
        for (int i = 0; i < contentKho.transform.childCount; i++)
        {
            Destroy(contentKho.transform.GetChild(i).gameObject);
        }
        coutItem = 0;
    }
    #endregion

    #region IEnumerator

    #endregion

}
