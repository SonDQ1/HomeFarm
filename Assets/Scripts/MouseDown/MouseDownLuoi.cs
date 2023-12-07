using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownLuoi : MonoBehaviour
{
    public int idLuoi;
    public int countClick = 0;

    [SerializeField] GameObject caylaunam;
    GameObject canvasDiloag, dongho, load;
    bool dichuyen = false;
    Coroutine coroutine;
    float mouseTime;
    Vector3 vitricu;
    PolygonCollider2D boxluoi;
    private void Awake()
    {
        //int order = (int)(transform.position.y * (-10));
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 50);
        //gameObject.GetComponent<SpriteRenderer>().sortingOrder = order;
        boxluoi = gameObject.GetComponent<PolygonCollider2D>();
    }

    private void OnMouseDown()
    {
        if (!Until.showPanel)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false)
            {
                coroutine = StartCoroutine(delayMoundown());
                mouseTime = Time.time;
                vitricu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("MouseDownLuoi");
            }
        }
        //Debug.Log("MouseDownLuoi ="+ !Until.checkShowpanel+ !EventSystem.current.IsPointerOverGameObject()+ !Until.checkMayxuc);
    }
    IEnumerator delayMoundown()
    {
        yield return new WaitForSeconds(0.5f);
        dichuyen = true;
    }
    void OnMouseUp()
    {
        //Debug.Log("OnMouseUp ="+ !EventSystem.current.IsPointerOverGameObject()+ !Until.checkMayxuc+ !dichuyen);
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false && !dichuyen
                && Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
                && Time.time - mouseTime < 0.2f)
            {
                countClick++;
                //if (countClick == 1) SoundController.instance._playAudio(18);
                //Debug.Log("OnMouseUp " + countClick);
                //Debug.Log("countClick = " + countClick);
                if (countClick == 2)
                {
                    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    //    GameManagerScripts.instance._sinhvat(1, gameObject, idLuoi, true);
                    //Debug.Log("countClick = " + countClick);
                    //if (Until.clickItemcaylaunam)
                    //{
                    //    //chọn item cây lâu năm
                    //    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    //    GameManagerScripts.instance._sinhvat(1, gameObject, idLuoi, true);
                    //}
                    //else
                    //    if (Until.clickItemtrangtri)
                    //{
                    //    //chọn item trang trí
                    //    GameManagerScripts.instance._sinhvat(2, gameObject, idLuoi, true);
                    //}
                    //else if (Until.clickItemChuong)
                    //{
                    //    //chọn item chuồng
                    //    GameManagerScripts.instance._sinhvat(3, gameObject, idLuoi, true);
                    //    Until.idLuoi = idLuoi;
                    //}
                    //else
                    //{
                    //    //chọn item ô đất
                    //    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    //    GameManagerScripts.instance._sinhvat(0, gameObject, idLuoi, true);
                    //}
                    //if(!Until.clickItemtrangtri)
                    //    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                    countClick = 0;
                }
                else
                {
                    //if (Until.clickItemcaylaunam)
                    //    GameManagerScripts.instance._sinhvat(1, gameObject, idLuoi, false);
                    //else
                    //if (Until.clickItemtrangtri)
                    //    GameManagerScripts.instance._sinhvat(2, gameObject, idLuoi, false);
                    //else
                    //    if (Until.clickItemChuong)
                    //    GameManagerScripts.instance._sinhvat(3, gameObject, idLuoi, false);
                    //else
                    //    GameManagerScripts.instance._sinhvat(0, gameObject, idLuoi, false);
                }
            }
            if (coroutine != null)
            {
                dichuyen = false;
                StopCoroutine(coroutine);
            }
        }
    }

    public int getIdluoi()
    {
        return idLuoi;
    }
    public void endBoxLuoi(bool check)
    {
        boxluoi.enabled = check;
    }
}
