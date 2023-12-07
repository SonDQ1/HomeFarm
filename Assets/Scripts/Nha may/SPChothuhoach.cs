using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPChothuhoach : MonoBehaviour {

    [SerializeField] RectTransform rtGridlayout;
    [SerializeField] GameObject[] itemChothuhoach;
    public int idNhamay;
    public string tagNhamay;

    //item chờ thu hoạch giếng
    public bool checkGieng;
    public int idSpgieng;

    void Start () {
        if (checkGieng)
            loadItemGieng();
        else
            loadItemChothuhoach();
	}
    void loadItemGieng()
    {
        for (int i = 0; i < itemChothuhoach.Length; i++)
        {
            itemChothuhoach[i].SetActive(false);
        }
        itemChothuhoach[0].SetActive(true);
        itemChothuhoach[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
            GameManagerControll.instance.dataNongSan.data[idSpgieng].iconMake;
    }
    void loadItemChothuhoach()
    {
        for(int i = 0; i < itemChothuhoach.Length; i++)
        {
            itemChothuhoach[i].SetActive(false);
        }
        List<int> listThuhoach = PlayerPrefsController.instance.getListThuhoach(tagNhamay,idNhamay);
        //PlayerPrefsController.instance.setThuhoach(gameObject.tag, idLuoi, id);
        //Debug.Log("loadItemChothuhoach: " + listThuhoach.Count);
        for (int i = 0; i < listThuhoach.Count; i++)
        {
            if(i < itemChothuhoach.Length)
            {
                itemChothuhoach[i].SetActive(true);
                itemChothuhoach[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                    GameManagerControll.instance.dataNongSan.data[listThuhoach[i]].iconMake;
            }
        }
        rtGridlayout.sizeDelta = new Vector2(listThuhoach.Count, 1);
    }
}
