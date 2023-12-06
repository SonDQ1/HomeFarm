using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockItem : MonoBehaviour
{
    public int levelUnlock;
    public string tagobj;
    public int idnha;
    public bool checkBuildingObj;
    [SerializeField] Sprite spLockItem;
    [SerializeField] Sprite spUnLockItem;
    //[SerializeField] GameObject txtCount;
    Image icon;
    GameObject coin;
    GameObject objLock;
    public bool checkTrangtri;
    public bool checkItemake;
    public bool checkPest;
    string txtlv = "Unlock Level ";

    private void OnEnable()
    {
        if (PlayerPrefsController.instance.getBuildingObj(tagobj, idnha) && checkBuildingObj)
        {
            levelUnlock = PlayerPrefsController.instance.getLevelUnlockObj(tagobj, idnha);
            //if (Until.checkLanguage())
            //{
            //    if (Until.checkTagNhamay(tagobj)) txtlv = "Khóa";
            //}
            //else
            //    if (Until.checkTagNhamay(tagobj)) txtlv = "Lock";
        }

        if (Until.checkLanguage())
        {
            txtlv = "Mở khóa cấp " + levelUnlock;
        }
        else
        {
            txtlv = "Unlock Level " + levelUnlock;
        }

        if (checkItemake)
        {
            icon = gameObject.transform.GetChild(0).GetComponent<Image>();
            objLock = gameObject.transform.GetChild(1).gameObject;
            objLock.transform.GetChild(0).GetComponent<Text>().text = txtlv;
        }
        else
        {
            icon = gameObject.transform.GetChild(1).GetComponent<Image>();
            if (checkTrangtri)
                icon = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();

            objLock = gameObject.transform.GetChild(2).gameObject;
            coin = gameObject.transform.GetChild(3).gameObject;
        }
        unlockItem(PlayerPrefsController.instance.getLevel());
    }
    #region unlock item
    void unlockItem(int level)
    {
        //if (!PlayerPrefsController.instance.getBuildingObj(tagobj, idnha))
        //    icon.sprite = spLockItem;
        //if (PlayerPrefsController.instance.getBuildingObj(tagobj, idnha) && Until.checkTagNhamay(tagobj))
        //{
        //    if (coin != null)
        //        coin.SetActive(false);
        //    objLock.SetActive(true);
        //    gameObject.GetComponent<Button>().enabled = false;
        //    objLock.transform.GetChild(0).GetComponent<Text>().text = txtlv;
        //}
        //else
        {
            if (coin != null)
                coin.SetActive(false);
            objLock.SetActive(true);
            gameObject.GetComponent<Button>().enabled = false;
            objLock.transform.GetChild(0).GetComponent<Text>().text = txtlv;

            if (PlayerPrefsController.instance.getLevel() >= levelUnlock)
            {
                //icon.sprite = spUnLockItem;
                //icon.SetNativeSize();
                if (coin != null)
                    coin.SetActive(true);
                gameObject.GetComponent<Button>().enabled = true;
                objLock.SetActive(false);
                //txtCount.GetComponent<Text>().text = "5/9";
                if (checkPest)
                {
                    //img swap
                    gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    if (PlayerPrefsController.instance.getBuyPets(idnha))
                    {
                        coin.SetActive(false);
                        if(idnha != PlayerPrefsController.instance.getPets())
                            gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    #endregion
}
