using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{

    public static OrderController instance;
    [SerializeField] GameObject[] arTick;
    [SerializeField] Animator[] animBtn;
    [SerializeField] GameObject[] arBtnOrder;
    [SerializeField] GameObject itemOrders, btnFinish;
    [SerializeField] Text txtNameOrder, txtCoint, txtExp, txtKchoanthanh;
    [SerializeField] GameObject dialogFinish;
    [SerializeField] Text txtDialogFinish;
    [SerializeField] GameObject objSendOrder;
    [SerializeField] Transform poseff;
    GameObject objSend;
    //hướng dẫn
    [SerializeField] Transform posOrder;
    [SerializeField] GameObject dialogOrder;
    [SerializeField] GameObject btnCompleteOrder;

    List<ListOrder> listOrders;

    int idBtnClick = 0;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //listOrders = PlayerPrefsController.instance.getListOrder();
        load();
    }

    public void btnCompleteOrderClick()
    {
        GameManagerControll.instance._showDialog(2);
        load();
    }
    public void btnOrderClick(int id)
    {
        //Debug.Log("btnOrderClick -> " + id);
        idBtnClick = id;
        //listOrders = PlayerPrefsController.instance.getListOrder();
        animBtn[id].SetTrigger("click");
        for (int i = 0; i < itemOrders.transform.childCount; i++)
        {
            itemOrders.transform.GetChild(i).gameObject.SetActive(false);
        }
        //load item
        txtNameOrder.text = Until.checkLanguage() ? listOrders[id].nameOrderV : listOrders[id].nameOrderE;
        txtCoint.text = listOrders[id].coint + "";
        txtExp.text = listOrders[id].exp + "";
        if (listOrders[id].kcFinist > 0)
            btnFinish.SetActive(true);
        else btnFinish.SetActive(false);
        txtKchoanthanh.text = listOrders[id].kcFinist + "";

        //show các item của 1 order
        for (int i = 0; i < listOrders[id].itemOrders.Count; i++)
        {
            itemOrders.transform.GetChild(i).gameObject.SetActive(true);
            itemOrders.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite =
                GameManagerControll.instance.dataNongSan.data[listOrders[id].itemOrders[i].idVatphams].iconMake;

            int slkho = PlayerPrefsController.instance.getVpKho(listOrders[id].itemOrders[i].idVatphams);

            Color colorRed;
            ColorUtility.TryParseHtmlString("#F00000FF", out colorRed);
            Color colorWhite;
            ColorUtility.TryParseHtmlString("#FFFFFF", out colorWhite);

            if (slkho < listOrders[id].itemOrders[i].soluong)
                itemOrders.transform.GetChild(i).GetChild(1).GetComponent<Text>().color = colorRed;
            else
            {
                slkho = listOrders[id].itemOrders[i].soluong;
                itemOrders.transform.GetChild(i).GetChild(1).GetComponent<Text>().color = colorWhite;
            }

            itemOrders.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = slkho + "";
            itemOrders.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = "/" + listOrders[id].itemOrders[i].soluong;
            
        }

        if (PlayerPrefsController.instance.checkHuongdan() && dialogOrder.activeInHierarchy)
        {
            GameManagerControll.instance.sinhHand(0, Huongdan.instance.posBtnSend.position);
        }
    }
    //check lặp item trong 1 order
    bool checkLap(List<ItemOrder> itemOrders, int idvp)
    {
        int cout = 0;
        for (int i = 0; i < itemOrders.Count; i++)
        {
            if (itemOrders[i].idVatphams == idvp)
            {
                cout++;
            }
        }
        if (cout > 1) return true;
        return false;
    }


    public void checkOrder()
    {
        //duyệt 6 order trong bảng, kiểm tra nếu idorder trên order == data.idorder
        //thì duyệt mảng con item nedd, check kho trả kết quả true - false;
        //hiện dấu tick
        //load gem hoàn thành
        Debug.Log("=======>checkOrder");
        listOrders = PlayerPrefsController.instance.getListOrder();
        List<int> listCheckOrderHoanthanh = new List<int>();
        btnCompleteOrder.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            //load dữ liệu trên itemorder
            arBtnOrder[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = listOrders[i].coint + "";
            arBtnOrder[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = listOrders[i].exp + "";

            int countCheck = 0;
            for (int j = 0; j < listOrders[i].itemOrders.Count; j++)
            {
                int slkho = PlayerPrefsController.instance.getVpKho(listOrders[i].itemOrders[j].idVatphams);
                if (slkho >= listOrders[i].itemOrders[j].soluong)
                    countCheck++;
            }
            if (countCheck == listOrders[i].itemOrders.Count && countCheck > 0)
                listCheckOrderHoanthanh.Add(i);
        }

        for (int i = 0; i < arTick.Length; i++)
        {
            arTick[i].SetActive(false);
            arTick[i].transform.GetChild(0).gameObject.SetActive(false);
            arBtnOrder[i].SetActive(false);
            arBtnOrder[i].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            btnFinish.SetActive(false);
        }

        if (PlayerPrefsController.instance.getLevel() <= arBtnOrder.Length)
            for (int i = 0; i < PlayerPrefsController.instance.getLevel(); i++)
            {
                arBtnOrder[i].SetActive(true);
                arTick[i].SetActive(true);
            }
        else
            for (int i = 0; i < arTick.Length; i++)
            {
                arBtnOrder[i].SetActive(true);
                arTick[i].SetActive(true);
            }

        for (int i = 0; i < listCheckOrderHoanthanh.Count; i++)
        {
            arTick[listCheckOrderHoanthanh[i]].transform.GetChild(0).gameObject.SetActive(true);
            arBtnOrder[listCheckOrderHoanthanh[i]].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            if(arTick[listCheckOrderHoanthanh[i]].transform.GetChild(0).gameObject.activeInHierarchy && !PlayerPrefsController.instance.checkHuongdan())
                btnCompleteOrder.SetActive(true);
        }
        //Debug.Log("listCheckOrderHoanthanh.Count = " + listCheckOrderHoanthanh.Count);
    }

    public void btnSendClick()
    {
        if (arBtnOrder[idBtnClick].transform.GetChild(0).GetChild(2).gameObject.activeInHierarchy)
        {
            if (PlayerPrefsController.instance.checkHuongdan())
            {
                //Until.huongdan = 3;
                PlayerPrefsController.instance.setBuocHuongdan(3);
                GameManagerControll.instance.LevelUp(2);
            }

            if (objSend != null) Destroy(objSend);
            objSend = Instantiate(objSendOrder, poseff.position, Quaternion.identity) as GameObject;
            objSend.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[listOrders[idBtnClick].itemOrders[0].idVatphams].iconMake;
            Destroy(objSend, 1.3f);

            PlayerPrefsController.instance.addCoin(listOrders[idBtnClick].coint);
            PlayerPrefsController.instance.addExp(listOrders[idBtnClick].exp);
            //trừ vp kho
            for (int i = 0; i < listOrders[idBtnClick].itemOrders.Count; i++)
            {
                PlayerPrefsController.instance.removeVpKho(listOrders[idBtnClick].itemOrders[i].idVatphams,
                    listOrders[idBtnClick].itemOrders[i].soluong);
            }
            PlayerPrefsController.instance.newOrder(idBtnClick);
            load();
        }
        else
            GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
                Until.checkLanguage() ? "Không đủ hàng hóa" : "Not enough goods", true);
    }
    public void destroyObjSend()
    {
        if (objSend != null)
            Destroy(objSend);
    }
    public void btnFinishClick()
    {
        dialogFinish.SetActive(true);
        txtDialogFinish.text = listOrders[idBtnClick].kcFinist + "";
    }

    public void btnYesFinish()
    {
        if (listOrders[idBtnClick].kcFinist <= PlayerPrefsController.instance.getDiamod())
        {
            PlayerPrefsController.instance.addCoin(listOrders[idBtnClick].coint);
            PlayerPrefsController.instance.addExp(listOrders[idBtnClick].exp);
            PlayerPrefsController.instance.exceptDiamod(listOrders[idBtnClick].kcFinist);
            //trừ vp kho
            for (int i = 0; i < listOrders[idBtnClick].itemOrders.Count; i++)
            {
                PlayerPrefsController.instance.removeVpKho(listOrders[idBtnClick].itemOrders[i].idVatphams,
                    listOrders[idBtnClick].itemOrders[i].soluong);
            }

            PlayerPrefsController.instance.newOrder(idBtnClick);
            load();
            checkOrder();

            StartCoroutine(delayCloseDialog(dialogFinish));

            if (objSend != null) Destroy(objSend);
            objSend = Instantiate(objSendOrder, poseff.position, Quaternion.identity) as GameObject;
            objSend.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[listOrders[idBtnClick].itemOrders[0].idVatphams].iconMake;
            Destroy(objSend, 1.3f);
        }
        else
            GameManagerControll.instance.showObjText(dialogFinish.transform.position,
                Until.checkLanguage() ? "Không đủ kim cương" : "Not enough diamod", true);


    }

    public void btnNoFinishClick()
    {
        StartCoroutine(delayCloseDialog(dialogFinish));
    }

    IEnumerator delayCloseDialog(GameObject dialog)
    {
        dialog.GetComponent<Animator>().SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        dialog.SetActive(false);
    }

    public void load()
    {
        Debug.Log("=====OrderController=========>");
        checkOrder();
        //check đơn hàng nào hoàng thành thì click vào đơn hàng đó
        if(!PlayerPrefsController.instance.checkHuongdan())
            StartCoroutine(delayClickDonhang());
        else btnOrderClick(0);
    }

    IEnumerator delayClickDonhang()
    {
        yield return new WaitForSeconds(0.2f);
        bool checkClick = false;
        for (int i = 0; i < arBtnOrder.Length; i++)
            if (arBtnOrder[i].transform.GetChild(0).GetChild(2).gameObject.activeInHierarchy)
            {
                btnOrderClick(i);
                checkClick = true;
                break;
            }
        if (!checkClick)
            btnOrderClick(0);
    }

    #region camview
    public void _camView()
    {
        Camera.main.transform.DOMove(new Vector3(posOrder.position.x, posOrder.position.y + 1.2f, -40), 0.8f);
    }
    #endregion

    #region hướng dẫn
    public void huongdan()
    {
        if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 2)
        {
            _camView();
            checkOrder();
            btnOrderClick(0);
            GameManagerControll.instance.sinhHand(0, posOrder.position);
        }
    }
    #endregion
}
