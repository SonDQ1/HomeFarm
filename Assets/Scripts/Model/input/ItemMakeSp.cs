using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakeSp : MonoBehaviour
{

    [Header("Nhập dữ liệu")]
    [SerializeField] GameObject[] objItemNedd;
    [SerializeField] Text txtName, txtTimept, txtSlKho;

    [Header("Null - set từ class khác")]
    public string nameSp;
    public int timePt;
    public int slKho;
    public ItemOrder[] itemNedd;


    private void Start()
    {
        txtName.text = nameSp;
        txtTimept.text = getFormatTime(timePt);
        Color color;
        ColorUtility.TryParseHtmlString("#FF2929FF", out color);

        for (int i = 0; i < itemNedd.Length; i++)
        {
            objItemNedd[i].SetActive(false);
            if (itemNedd[i].soluong > 0)
            {
                objItemNedd[i].SetActive(true);
                objItemNedd[i].transform.GetChild(1).GetComponent<Text>().text = "/"+itemNedd[i].soluong;
                objItemNedd[i].transform.GetChild(0).GetComponent<Text>().text = PlayerPrefsController.instance.getVpKho(itemNedd[i].idVatphams)+"";
                if (PlayerPrefsController.instance.getVpKho(itemNedd[i].idVatphams) < itemNedd[i].soluong)
                    objItemNedd[i].transform.GetChild(0).GetComponent<Text>().color = color;

                objItemNedd[i].GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[itemNedd[i].idVatphams].hinhanh;
                txtSlKho.text = slKho + "";
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && gameObject.activeSelf &&
                !RectTransformUtility.RectangleContainsScreenPoint(
                    gameObject.GetComponent<RectTransform>(),
                    Input.mousePosition,
                    Camera.main))
        {
            Destroy(gameObject);
        }
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
            if (s > 0) f = m + ":" + s;
            else f = m + ":00";
        }
        else
            f = s + "";
        return f;
    }
}
