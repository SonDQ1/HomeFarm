using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockmapControll : MonoBehaviour
{

    public static LockmapControll instance;
    [SerializeField] GameObject[] arrLockmap;
    [SerializeField] GameObject dialogBuy;
    [SerializeField] Text txtCoin, txtGem;
    public GameObject[] animvatpham;

    int coin = 0;
    int gem = 0;
    int idmap = -1;
    Vector3 poiteff;
    void Start()
    {
        instance = this;

        //load unlock map
        for (int i = 0; i < arrLockmap.Length; i++)
        {
            if (PlayerPrefs.GetInt("lockmap" + i) == 1)
                //arrLockmap[i].SetActive(false);
                arrLockmap[i].GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    public void showDialogBuy(int idmap, int coin, int gem, Vector3 point)
    {
        txtCoin.text = coin + "";
        txtGem.text = gem + "";
        this.coin = coin;
        this.gem = gem;
        this.idmap = idmap;
        this.poiteff = point;
        dialogBuy.SetActive(true);
        Until.showPanel = true;
    }

    public void btnNoBuyClick()
    {
        StartCoroutine(GameManagerControll.instance.delayCloseDialog(dialogBuy, false));
    }

    public void btnYesBuyClick()
    {
        if (PlayerPrefsController.instance.getCoin() >= coin)
        {
            if (PlayerPrefsController.instance.getGem() >= gem)
            {
                Until.showPanel = false;
                dialogBuy.SetActive(false);
                PlayerPrefsController.instance.exceptCoin(coin);
                PlayerPrefsController.instance.exceptGem(gem);
                //unlock map
                StartCoroutine(delayUnlockMap());
            }
            else
            {
                //show text enogth coin
                Vector3 target = Camera.main.transform.position;
                target.z = 0;
                GameManagerControll.instance.showObjText(target, Until.checkLanguage() ? "Không đủ đá quý" : "Not enough gem", true);
            }

        }
        else
        {
            //show text enogth coin
            Vector3 target = Camera.main.transform.position;
            target.z = 0;
            GameManagerControll.instance.showObjText(target, Until.checkLanguage() ? "Không đủ vàng" : "Not enough gold", true);
        }
    }

    IEnumerator delayUnlockMap()
    {
        //chạy eff
        arrLockmap[idmap].GetComponent<PolygonCollider2D>().enabled = false;
        arrLockmap[idmap].GetComponent<MouseDownLockmap>()._camView();
        //SoundController.instance._playSound(21);
        GameManagerControll.instance.playEff(5, arrLockmap[idmap].transform.position);
        yield return new WaitForSeconds(1f);
        //arrLockmap[idmap].SetActive(false);
        PlayerPrefs.SetInt("lockmap" + idmap, 1);
        GameManagerControll.instance._sinhObjThuhoach(arrLockmap[idmap], 0, "momap");
        PlayerPrefsController.instance.addKho(GameManagerControll.instance.idCua, 1);
        PlayerPrefsController.instance.addKho(GameManagerControll.instance.idThuocno, 2);

        arrLockmap[idmap].GetComponent<PolygonCollider2D>().enabled = false;
        arrLockmap[idmap].GetComponent<MouseDownLockmap>().loadBoxClick();
    }
}
