using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownLockmap : MonoBehaviour {

    public int idmap;
    [SerializeField] int level;
    [SerializeField] int coin;
    //[SerializeField] int gem;
    bool click;
    Vector3 vitricu;
    float mouseTime;
    private void Start()
    {
        //load id rác
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).GetComponent<MouseDownRac>().idrac = i;
        }
        loadBoxClick();
        //load rác đã xóa
        StartCoroutine(loadracDaxoa());
    }
    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject(0) == false && !Until.showPanel)
        {
            vitricu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseTime = Time.time;
        }
    }
    private void OnMouseUp()
    {
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
            if (EventSystem.current.IsPointerOverGameObject(0) == false &&
            Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
            && Time.time - mouseTime < 0.2f)
        {
            SoundController.instance._playSound(0);
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            //chưa đạt đến level thì show thông báo, ngược lại show hộp thoại mua ô đất
            if (checkLevel())
            {
                LockmapControll.instance.showDialogBuy(idmap, coin, level, gameObject.transform.position);
            }
            else
            if (!click)
            {
                StartCoroutine(delayClick());
                //SoundController.instance._playAudio(17);
                GameManagerControll.instance.showObjText(target, Until.checkLanguage() ? "Mở khóa cấp " + level : "Unlock level " + level, false);
            }
        }
    }
    IEnumerator delayClick()
    {
        click = true;
        yield return new WaitForSeconds(1f);
        click = false;
    }
    bool checkLevel()
    {
        if (PlayerPrefsController.instance.getLevel() >= level)
            return true;
        return false;
    }

    public void _camView()
    {
        Camera.main.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -50), 0.5f);
    }

    IEnumerator loadracDaxoa()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i= 0; i < transform.childCount; i++)
        {
            if (PlayerPrefsController.instance.getXoarac(idmap, i))
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
    public void loadBoxClick()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (PlayerPrefs.GetInt("lockmap" + idmap) == 1)
                transform.GetChild(i).GetComponent<PolygonCollider2D>().enabled = true;
            else transform.GetChild(i).GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
