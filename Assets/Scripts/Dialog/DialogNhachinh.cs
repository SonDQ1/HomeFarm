using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogNhachinh : MonoBehaviour
{
    [SerializeField] DataThanhtich dataThanhtich;
    [SerializeField] GameObject itemtt;
    [SerializeField] GameObject btnShowThanhtich;
    [SerializeField] GameObject dialogThanhtich;
    [SerializeField] GameObject contentThanhtich;
    [SerializeField] ScrollRect scrollRect;

    float mouseTime;
    Vector3 vitricu;

    private Button[] m_buttons;
    private int m_index;
    private float m_verticalPosition;

    private void Start()
    {
        loadItemthanhtich();
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
                if (!PlayerPrefsController.instance.checkHuongdan())
                {
                    btnShowThanhtichClick();
                }
            }
        }
    }
    public void loadItemthanhtich()
    {
        deleteItemThanhtich();
        int countThanhtich = 0;
        int posItem = 0;
        int countHoanthanh = 0;
        for (int i = 0; i < dataThanhtich.data.Length; i++)
        {
            //nếu đã nhận thì không thêm vào dialog
            if (PlayerPrefsController.instance.getThanhtich(i) == 0)
            {
                //tổng số lượng
                int tongCan = 0, tongKho = 0;
                int tongKho1 = 0;
                for (int j = 0; j < dataThanhtich.data[i].itemOrders.Length; j++)
                {
                    tongCan += dataThanhtich.data[i].itemOrders[j].soluong;
                    tongKho1 = PlayerPrefsController.instance.getVpThanhtich(dataThanhtich.data[i].itemOrders[j].idVatphams);
                    if (tongKho1 > dataThanhtich.data[i].itemOrders[j].soluong) tongKho1 = dataThanhtich.data[i].itemOrders[j].soluong;
                    tongKho += tongKho1;
                }
                //sinh item thành tích
                GameObject item = Instantiate(itemtt, contentThanhtich.transform.position, Quaternion.identity, contentThanhtich.transform) as GameObject;

                //add sự kiện click
                //Debug.Log("Sinh itemThanhtich: " + posItem); 
                item.GetComponent<ItemThanhtich>().pos = posItem;
                item.GetComponent<ItemThanhtich>().idthanhtich = i;
                item.GetComponent<ItemThanhtich>().dialogNhachinh = this;

                item.transform.GetChild(0).GetComponent<Text>().text = Until.checkLanguage() ? dataThanhtich.data[i].nameV : dataThanhtich.data[i].nameE;
                item.transform.GetChild(1).GetComponent<Image>().sprite = dataThanhtich.data[i].icon;
                if (tongKho >= tongCan)
                    item.transform.GetChild(2).GetComponent<Text>().text = tongCan + "/" + tongCan;
                else
                    item.transform.GetChild(2).GetComponent<Text>().text = tongKho + "/" + tongCan;

                item.transform.GetChild(3).GetComponent<Text>().text = dataThanhtich.data[i].exp + "";
                int diamod = dataThanhtich.data[i].diamod;
                item.transform.GetChild(4).GetComponent<Text>().text = diamod + "";

                item.transform.GetChild(5).GetChild(0).GetComponent<Image>().fillAmount = (float)tongKho / tongCan;
                posItem++;
                ////đã đủ
                if (tongKho >= tongCan)
                {
                    item.transform.GetChild(6).gameObject.SetActive(true);
                    if (PlayerPrefsController.instance.getThanhtich(i) == 0)
                        countThanhtich++;
                }
                if (countThanhtich > 0 && btnShowThanhtich != null)
                    btnShowThanhtich.SetActive(true);
                else
                    if (btnShowThanhtich != null)
                    btnShowThanhtich.SetActive(false);
            }
            else
                countHoanthanh++;
        }

        //hoàn thành tất cả các mục trong thành tích thì xóa dữ liệu, cho lặp lại từ đầu
        if (countHoanthanh == dataThanhtich.data.Length)
        {
            //xóa cờ check hoàn thành
            for (int i = 0; i < dataThanhtich.data.Length; i++)
                PlayerPrefsController.instance.nhanThanhtich(i, 0);
            //xóa lưu data đã thu hoạch
            for (int i = 0; i < GameManagerControll.instance.dataNongSan.data.Length; i++)
                PlayerPrefsController.instance.removeVpThanhtich(i);
        }
        //Debug.Log("loadItemthanhtich: " + posItem);
        changeSizeScroll(posItem);
    }

    public void btnThanhTichClick(int idpos, int idthanhtich)
    {
        Debug.Log("btnThanhTichClick: " + idpos);
        string st = "";

        if (contentThanhtich.transform.GetChild(idpos).transform.GetChild(6).gameObject.activeInHierarchy)
        {
            StartCoroutine(delayDestroyThanhtich(idpos, idthanhtich));
        }
        else
            st = Until.checkLanguage() ? "Vui lòng hoàn thành yêu cầu" : "Please complete the request";
        GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
            st, true);
    }

    public void btnShowThanhtichClick()
    {
        //loadItemthanhtich();
        Until.showPanel = true;
        dialogThanhtich.SetActive(true);
        CenterToItemComplete();
    }
    IEnumerator delayDestroyThanhtich(int idpos, int idthanhtich)
    {
        contentThanhtich.transform.GetChild(idpos).GetComponent<Animator>().SetTrigger("hiden");
        yield return new WaitForSeconds(0.22f);
        btnShowThanhtich.SetActive(false);
        PlayerPrefsController.instance.addExp(dataThanhtich.data[idthanhtich].exp);
        PlayerPrefsController.instance.addDiamod(dataThanhtich.data[idthanhtich].diamod);
        PlayerPrefsController.instance.nhanThanhtich(idthanhtich, 1);

        string st = Until.checkLanguage() ? "Đã nhận: " + dataThanhtich.data[idthanhtich].exp + " exp và " + dataThanhtich.data[idthanhtich].diamod + " kim cương" :
            "Received: " + dataThanhtich.data[idthanhtich].exp + " exp and " + dataThanhtich.data[idthanhtich].diamod + " diamod";

        GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
            st, true);

        loadItemthanhtich();

        CenterToItemComplete();
    }
    void changeSizeScroll(int coutItem)
    {
        int row = coutItem - 3;
        if (row > 0)
        {
            //contentThanhtich.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            contentThanhtich.GetComponent<RectTransform>().sizeDelta = new Vector2(425, 327 + (108 * row));
            contentThanhtich.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
        }
    }

    void deleteItemThanhtich()
    {
        foreach (Transform t in contentThanhtich.transform)
        {
            Destroy(t.gameObject);
        }
    }

    //di chuyển đến 1 phần tử con
    public void CenterToItemComplete()
    {
        for (int i = 0; i < contentThanhtich.transform.childCount; i++)
        {
            if (contentThanhtich.transform.GetChild(i).GetChild(6).gameObject.activeInHierarchy)
            {
                //contentThanhtich.GetComponent<RectTransform>().transform.localPosition =
                //    Vector3.Lerp(contentThanhtich.transform.position, new Vector3(contentThanhtich.transform.position.x,
                //    Mathf.Abs(contentThanhtich.transform.GetChild(i).GetComponent<RectTransform>().localPosition.y),0), Time.deltaTime);
                //Debug.Log("CenterToItemComplete " + i);
                contentThanhtich.GetComponent<RectTransform>().transform.localPosition = new Vector2(contentThanhtich.transform.position.x,
                    Mathf.Abs(contentThanhtich.transform.GetChild(i).GetComponent<RectTransform>().localPosition.y));
                break;
            }
        }
    }
}
