using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogCaytrong : MonoBehaviour
{
    public static DialogCaytrong instance;
    [SerializeField] ScrollRect[] scrollRect;
    //[SerializeField] Sprite[] spLockItem;
    //[SerializeField] Sprite[] spUnLockItem;
    [SerializeField] Transform[] tfContent;

    private void Start()
    {
        instance = this;
    }

    //void Update()
    //{
    //    if (Input.GetMouseButton(0) && gameObject.activeSelf &&
    //            !RectTransformUtility.RectangleContainsScreenPoint(
    //                gameObject.GetComponent<RectTransform>(),
    //                Input.mousePosition,
    //                Camera.main))
    //    {
    //        StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject));
    //    }
    //}

    private void OnEnable()
    {
        unlockItem(PlayerPrefsController.instance.getLevel());
        if (PlayerPrefsController.instance.checkHuongdan())
            GameManagerControll.instance.destroyHand();
    }
    private void OnMouseDown()
    {
        closeDialog();
    }
    public void closeDialog()
    {
        if(!PlayerPrefsController.instance.checkHuongdan())
            StartCoroutine(GameManagerControll.instance.delayCloseDialog(gameObject, false));
    }

    #region btn - drag
    int getIdScrollRect()
    {
        switch (Until.tagOdat)
        {
            case "odat2": return 1;
            case "odat3":return 2;
        }
        return 0;
    }
    public void btnNext()
    {
        if (scrollRect[getIdScrollRect()].velocity.x == 0)
            scrollRect[getIdScrollRect()].horizontalNormalizedPosition += 0.01f;
        scrollRect[getIdScrollRect()].velocity = new Vector2(-1000f, 0f);
    }

    public void dragScrollHo(int i)
    {
        if (i > 0)
        {
            scrollRect[getIdScrollRect()].horizontalNormalizedPosition += 0.01f;
            scrollRect[getIdScrollRect()].velocity = new Vector2(-350f, 0f);
        }
        else
        {
            scrollRect[getIdScrollRect()].horizontalNormalizedPosition -= 0.01f;
            scrollRect[getIdScrollRect()].velocity = new Vector2(350f, 0f);
        }
    }

    public void btnHatgiongclick(int id)
    {
        //Debug.Log("btnHatgiongclick " + getIdMapTfContent(id));
        if (!getContent().GetChild(getIdMapTfContent(id)).transform.GetChild(2).gameObject.activeInHierarchy)
        {
            GameManagerControll.instance.hatgiongclick(id);
            closeDialog();
            //Debug.Log("btnHatgiongclick: " + Until.tagOdat+"->" + id);
        }
    }
    //click vào btnhatgiong -> map child tfcontent hạt giống
    public static int getIdMapTfContent(int idbtn)
    {
        if (Until.tagOdat == "odat2")
            if(idbtn <= 12)return idbtn - 7;

        if (Until.tagOdat == "odat3") return idbtn - 13;
        return idbtn;
    }
    #endregion

    #region unlock item
    void unlockItem(int level)
    {
        for(int i = 1; i < getContent().childCount; i++)
        {
            //btnHatgiong[i].transform.GetChild(0).GetComponent<Image>().sprite = spLockItem[i];
            //getContent().GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = spUnLockItem[i];
            getContent().GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
            getContent().GetChild(i).transform.GetChild(2).gameObject.SetActive(true);
            getContent().GetChild(i).transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Until.checkLanguage() ? "Mở khóa cấp " + getContent().GetChild(i).GetComponents<InstaneHatgiong>()[0].levelUnlock : "Unlock Level " + getContent().GetChild(i).GetComponents<InstaneHatgiong>()[0].levelUnlock;

            if (PlayerPrefsController.instance.getLevel() >=
                getContent().GetChild(i).GetComponents<InstaneHatgiong>()[0].levelUnlock)
            {
                //btnHatgiong[i].transform.GetChild(0).GetComponent<Image>().sprite = spUnLockItem[i];
                getContent().GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
                getContent().GetChild(i).transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    Transform getContent()
    {
        if (Until.tagOdat == "odat2") return tfContent[1];
        if (Until.tagOdat == "odat3") return tfContent[2];
        return tfContent[0];
    }
    #endregion
}
