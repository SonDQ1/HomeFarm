using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header("Click vào các tab")]
    [SerializeField] Image[] spTab;
    [SerializeField] GameObject[] tabScroll;
    [SerializeField] Sprite spTabOn, spTabOff;

    [SerializeField] GameObject objPosPes;
    [SerializeField] GameObject objBuyItem;
    [SerializeField] Sprite spDiamod;

    [Header("Tranform chứa obj sinh ra")]
    [SerializeField] Transform tfTrangtri;
    [SerializeField] Transform tfHangrao;
    //hướng dẫn
    [SerializeField] Transform posNhamay, posChuong;
    [SerializeField] GameObject handHd;
    GameObject objInstance;
    int idTabOdl = 0;
    Vector2 postext;
    GameObject objShowBuyItem;
    private void OnEnable()
    {
        if (PlayerPrefsController.instance.checkHuongdan())
        {
            GameManagerControll.instance.disbaleHandMap();
            huongdan();
        }
    }
    private void OnDisable()
    {
        destroyObjShowBuyItem();
    }

    #region btn
    //tab click
    public void btnTabClick(int id)
    {
        if (!PlayerPrefsController.instance.checkHuongdan())
        {
            tabScroll[idTabOdl].SetActive(false);
            spTab[idTabOdl].sprite = spTabOff;
            spTab[idTabOdl].SetNativeSize();
            idTabOdl = id;

            tabScroll[id].SetActive(true);
            spTab[id].sprite = spTabOn;
            spTab[id].SetNativeSize();
            destroyObjShowBuyItem();
        }
    }

    //buildding
    public void btnItemBuildClick(GameObject objbtn)
    {
        if (!Until.checkInstanceObj)
        {
            //chỉ cho xây 1 lần
            {
                int id = objbtn.GetComponent<UnlockItem>().idnha;
                string tagNhamayClick = objbtn.GetComponent<UnlockItem>().tagobj;
                postext = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (PlayerPrefsController.instance.getCoin() >= GameManagerControll.instance.objNha[id].coint)
                {
                    if (PlayerPrefsController.instance.getGem() >= GameManagerControll.instance.objNha[id].gem)
                    {
                        Vector2 centerCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                        //sinh đối tượng  lên map
                        objInstance = Instantiate(GameManagerControll.instance.objNha[id].obj, centerCamera, Quaternion.identity) as GameObject;
                        if (Until.checkTagNhamay(objInstance.tag))
                            objInstance.GetComponent<NhamayControll>().idLuoi = PlayerPrefsController.instance.getIdNha(objInstance.tag + objInstance.GetComponent<ObjIdMapPosInstance>().id);
                        objInstance.GetComponent<MoveObj>().checkInstanceObj = true;
                        objInstance.GetComponent<MoveObj>().coint = GameManagerControll.instance.objNha[id].coint;
                        objInstance.GetComponent<MoveObj>().gem = GameManagerControll.instance.objNha[id].gem;

                        GameManagerControll.instance.objInstance = objInstance;
                        Until.checkInstanceObj = true;
                        StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));

                        #region xây 1 nhà máy
                        //if (Until.checkTagNhamay(tagNhamayClick))
                        //{
                        //    if (!PlayerPrefsController.instance.getBuildingObj(objbtn.GetComponent<UnlockItem>().tagobj,0))
                        //    {
                        //        objInstance = Instantiate(GameManagerControll.instance.objNha[id].obj, centerCamera, Quaternion.identity) as GameObject;
                        //        objInstance.GetComponent<MoveObj>().checkInstanceObj = true;
                        //        objInstance.GetComponent<MoveObj>().coint = GameManagerControll.instance.objNha[id].coint;
                        //        objInstance.GetComponent<MoveObj>().gem = GameManagerControll.instance.objNha[id].gem;
                        //        GameManagerControll.instance.objInstance = objInstance;
                        //        GameManagerControll.instance.objInstance = objInstance;
                        //        Until.checkInstanceObj = true;
                        //        StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));

                        //    }
                        //    else GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
                        //        Until.checkLanguage() ? "Khóa" : "Lock", true);
                        //    //setid cho nhà máy
                        //    //gameObject.GetComponent<NhamayControll>().idLuoi =
                        //    //PlayerPrefsController.instance.getIdNha(tagNhamayClick + gameObject.GetComponent<ObjIdMapPosInstance>().id);
                        //}
                        //else
                        //{
                        //    objInstance = Instantiate(GameManagerControll.instance.objNha[id].obj, centerCamera, Quaternion.identity) as GameObject;
                        //    objInstance.GetComponent<MoveObj>().checkInstanceObj = true;
                        //    objInstance.GetComponent<MoveObj>().coint = GameManagerControll.instance.objNha[id].coint;
                        //    objInstance.GetComponent<MoveObj>().gem = GameManagerControll.instance.objNha[id].gem;
                        //    GameManagerControll.instance.objInstance = objInstance;
                        //    Until.checkInstanceObj = true;
                        //    StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));

                        //}
                        #endregion

                        //if (PlayerPrefsController.instance.checkHuongdan())
                        //{
                        //    GameManagerControll.instance.destroyHand();
                        //}

                    }
                    else
                        GameManagerControll.instance.showObjText(postext, Until.checkLanguage() ? "Không đủ đá quý" : "Not enough gem", true);
                }
                else
                    GameManagerControll.instance.showObjText(postext, Until.checkLanguage() ? "Không đủ vàng" : "Not enough gold", true);
            }
        }
    }
    //petst
    public void btnItemPestClick(int id)
    {
        postext = Until.centerCamera();
        GameManagerControll.instance.destroyHand();
        if (PlayerPrefsController.instance.getBuyPets(id))
        {
            StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));
            //sinh đối tượng  lên map
            if (objPosPes.transform.childCount > 0)
                Destroy(objPosPes.transform.GetChild(0).gameObject);
            objInstance = Instantiate(GameManagerControll.instance.objPes[id].obj, objPosPes.transform.position, Quaternion.identity, objPosPes.transform);
            if (id != PlayerPrefsController.instance.getPets())
                objInstance.GetComponent<PetsControll>().checkInstance = true;
            PlayerPrefsController.instance.setPetsShow(1);
            PlayerPrefsController.instance.setPets(id);
            GameManagerControll.instance._camView(objPosPes.transform.position);
        }
        else
        {
            if (PlayerPrefsController.instance.getCoin() >= GameManagerControll.instance.objPes[id].coint)
            {
                if (PlayerPrefsController.instance.getGem() >= GameManagerControll.instance.objPes[id].gem)
                {
                    StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));
                    //sinh đối tượng  lên map
                    if (objPosPes.transform.childCount > 0)
                        Destroy(objPosPes.transform.GetChild(0).gameObject);
                    objInstance = Instantiate(GameManagerControll.instance.objPes[id].obj, objPosPes.transform.position, Quaternion.identity, objPosPes.transform);

                    objInstance.GetComponent<PetsControll>().checkInstance = true;

                    PlayerPrefsController.instance.setPetsShow(1);
                    PlayerPrefsController.instance.setPets(id);
                    GameManagerControll.instance._camView(objPosPes.transform.position);

                    PlayerPrefsController.instance.exceptCoin(GameManagerControll.instance.objPes[id].coint);
                    PlayerPrefsController.instance.exceptGem(GameManagerControll.instance.objPes[id].gem);
                    PlayerPrefsController.instance.setBuyPets(id);

                }
                else
                    GameManagerControll.instance.showObjText(postext, Until.checkLanguage() ? "Không đủ đá quý" : "Not enough gem", true);
            }
            else
                GameManagerControll.instance.showObjText(postext, Until.checkLanguage() ? "Không đủ vàng" : "Not enough gold", true);
        }
        objInstance.GetComponent<PetsControll>().playSoundDV();
    }
    //Trangtri
    public void btnItemTrangtriClick(int id)
    {
        if (!Until.checkInstanceObj)
        {
            postext = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (PlayerPrefsController.instance.getCoin() >= GameManagerControll.instance.objTrangtri[id].coint)
            {
                StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));
                Vector2 centerCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                //sinh đối tượng  lên map
                objInstance = Instantiate(GameManagerControll.instance.objTrangtri[id].obj, centerCamera, Quaternion.identity, tfTrangtri);
                objInstance.GetComponent<MoveObj>().checkInstanceObj = true;
                objInstance.GetComponent<MoveObj>().coint = GameManagerControll.instance.objTrangtri[id].coint;
                GameManagerControll.instance.objInstance = objInstance;
                //PlayerPrefsController.instance.exceptCoin(GameManagerControll.instance.objTrangtri[id].coint);
                Until.checkInstanceObj = true;

            }
            else
                GameManagerControll.instance.showObjText(postext, Until.checkLanguage() ? "Không đủ vàng" : "Not enough gold", true);
        }
    }
    //Wall
    public void btnItemWallClick(int id)
    {
        if (!Until.checkInstanceObj)
        {
            postext = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (PlayerPrefsController.instance.getCoin() >= GameManagerControll.instance.objWall[id].coint)
            {
                StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));
                Vector2 centerCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                //sinh đối tượng  lên map
                objInstance = Instantiate(GameManagerControll.instance.objWall[id].obj, centerCamera, Quaternion.identity, tfHangrao);
                objInstance.GetComponent<MoveObj>().checkInstanceObj = true;
                objInstance.GetComponent<MoveObj>().localscale = 0;
                objInstance.GetComponent<MoveObj>().coint = GameManagerControll.instance.objWall[id].coint;
                GameManagerControll.instance.objInstance = objInstance;
                //PlayerPrefsController.instance.exceptCoin(GameManagerControll.instance.objWall[id].coint);
                Until.checkInstanceObj = true;

            }
            else
                GameManagerControll.instance.showObjText(postext, Until.checkLanguage() ? "Không đủ vàng" : "Not enough gold", true);
        }
    }
    //road
    public void btnItemRoadClick(int id)
    {
        if (!Until.checkInstanceObj)
        {
            postext = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (PlayerPrefsController.instance.getCoin() >= GameManagerControll.instance.objRoad[id].coint)
            {
                StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));
                Vector2 centerCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                //sinh đối tượng  lên map
                objInstance = Instantiate(GameManagerControll.instance.objRoad[id].obj, centerCamera, Quaternion.identity, tfHangrao);
                objInstance.GetComponent<MoveObj>().checkInstanceObj = true;
                objInstance.GetComponent<MoveObj>().coint = GameManagerControll.instance.objRoad[id].coint;
                GameManagerControll.instance.objInstance = objInstance;
                //PlayerPrefsController.instance.exceptCoin(GameManagerControll.instance.objRoad[id].coint);
                Until.checkInstanceObj = true;

            }
            else
                GameManagerControll.instance.showObjText(postext, Until.checkLanguage() ? "Không đủ vàng" : "Not enough gold", true);
        }
    }

    //item vp
    public void btnItemItemsClick(int id)
    {
        if (!Until.checkInstanceObj)
        {
            postext = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (PlayerPrefsController.instance.getDiamod() >= GameManagerControll.instance.dataNongSan.data[id].kcpt)
            {
                //new
                Vector2 pos = tabScroll[4].transform.GetChild(0).GetChild(0).GetChild(id - 78).position;
                pos.y += 1f;
                pos.x -= 0.5f;
                destroyObjShowBuyItem();
                objShowBuyItem = Instantiate(objBuyItem, pos, Quaternion.identity) as GameObject;
                objShowBuyItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Until.checkLanguage() ? GameManagerControll.instance.dataNongSan.data[id].nameV : GameManagerControll.instance.dataNongSan.data[id].nameE;
                objShowBuyItem.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = GameManagerControll.instance.dataNongSan.data[id].iconMake;
                objShowBuyItem.transform.GetChild(0).GetChild(2).GetComponent<Image>().SetNativeSize();
                objShowBuyItem.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = GameManagerControll.instance.dataNongSan.data[id].giamua + "";
                objShowBuyItem.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<Text>().text = Until.checkLanguage() ? "Mua" : "Buy";
                objShowBuyItem.GetComponent<SellItemKho>().buyItem = true;
                objShowBuyItem.GetComponent<SellItemKho>().imgCoin.sprite = spDiamod;
                objShowBuyItem.GetComponent<SellItemKho>().idvp = id;
                objShowBuyItem.GetComponent<SellItemKho>().giavp = GameManagerControll.instance.dataNongSan.data[id].giamua;

                float sizeZoom = Camera.main.GetComponent<Camera>().orthographicSize;
                float size = 1f;
                if (sizeZoom > 3 && sizeZoom < 4) size = 1.3f;
                else if (sizeZoom >= 4 && sizeZoom <= 5) size = 1.5f;
                objShowBuyItem.transform.localScale = new Vector2(size, size);

            }
            else
                GameManagerControll.instance.showObjText(postext, Until.checkLanguage() ? "Không đủ kim cương" : "Not enough diamond", true);
        }
    }

    void destroyObjShowBuyItem()
    {
        if (objShowBuyItem != null)
            Destroy(objShowBuyItem);
    }

    public void ondragScrollItem()
    {
        destroyObjShowBuyItem();
    }
    #endregion

    #region delayIEnumerator


    #endregion

    #region Hướng dẫn
    void huongdan()
    {
        if (PlayerPrefsController.instance.getBuocHuongdan() == 5)
            GameManagerControll.instance.sinhHand(0, posNhamay.transform.position);
        else
            if (PlayerPrefsController.instance.getBuocHuongdan() == 7 || PlayerPrefsController.instance.getBuocHuongdan() == 8)
        {
            //Until.huongdan = 8;
            PlayerPrefsController.instance.setBuocHuongdan(8);
            //MobileTouchCamera.checkCamFollow = true;
            handHd.SetActive(true);
        }
    }
    #endregion
}
