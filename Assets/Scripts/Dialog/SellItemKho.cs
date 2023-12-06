using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItemKho : MonoBehaviour {

    [SerializeField] Text txtSl, txtCoin;
    public Image imgCoin; 
    public int idvp;
    public int giavp;
    int slvpkho = 0;
    int slban;
    public bool buyItem;
    private void OnEnable()
    {
        slban = 1;
        txtSl.text = 1 + "";
    }

    public void btnCongClick()
    {
        if (buyItem)
        {
            if (slban*giavp < PlayerPrefsController.instance.getDiamod())
            {
                slban++;
                txtSl.text = slban + "";
                txtCoin.text = slban * giavp + "";
            }
        }
        else
        {
            slvpkho = PlayerPrefsController.instance.getVpKho(idvp);
            if (slban < slvpkho)
            {
                slban++;
                txtSl.text = slban + "";
                txtCoin.text = slban * giavp + "";
            }
        }
        
    }
    public void btnTruClick()
    {
        if (buyItem)
        {
            if (slban > 1)
            {
                slban--;
                txtSl.text = slban + "";
                txtCoin.text = slban * giavp + "";
            }
        }
        else
        {
            slvpkho = PlayerPrefsController.instance.getVpKho(idvp);
            if (slban > 1)
            {
                slban--;
                txtSl.text = slban + "";
                txtCoin.text = slban * giavp + "";
            }
        }
    }
    public void btnCellClick()
    {
        if (buyItem)
        {
            PlayerPrefsController.instance.exceptDiamod(slban * giavp);
            PlayerPrefsController.instance.addKho(idvp, slban);
            GameManagerControll.instance.loadUIMain();
            
            //Until.checkInstanceObj = true;
            GameObject obj = new GameObject();
            obj.transform.position = Until.centerCamera();
            GameManagerControll.instance._sinhObjThuhoach(obj, idvp, "vatphamcv");

            Destroy(gameObject);
        }
        else
        {
            PlayerPrefsController.instance.removeVpKho(idvp, slban);
            KhoController.instance.reloadƯhenCellItem();
            //cộng coin
            PlayerPrefsController.instance.addCoin(slban * giavp);
            GameManagerControll.instance.loadUIMain();
            Destroy(gameObject);
        }
    }
}
