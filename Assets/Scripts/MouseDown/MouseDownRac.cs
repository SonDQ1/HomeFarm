using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownRac : MonoBehaviour
{
    public MouseDownLockmap mouseDownLockmap;
    public int idrac;
    public int idVpPhamap;
    Vector3 vitricu;
    float mouseTime;

    private void Start()
    {
        if (GetComponent<Animator>())
        {
            StartCoroutine(delayAnim());
        }
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject(0) == false && !Until.showPanel)
        {
            vitricu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseTime = Time.time;
        }
    }

    public void OnMouseUp()
    {
        if (!Until.showPanel && !Until.checkInstanceObj && !Until.moveObj)
            if (EventSystem.current.IsPointerOverGameObject(0) == false &&
    Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f
        && Time.time - mouseTime < 0.2f)
        {
            SoundController.instance._playSound(0);
            GameManagerControll.instance.mouseDownRac = this;
            loadIdvp();
            GameManagerControll.instance._sinhCVPhamap(gameObject, idVpPhamap);
        }
    }
    //va chạm với vật phẩm, sinh anim và destroy
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("vatpham") && vacham)
        {
            StartCoroutine(delayVacham());
            if (checkMapIdvpName(collision.GetComponents<HatgiongForllow>()[0].idHatgiong, gameObject.name))
            {
                Vector2 centerCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (gameObject.name.Substring(0, 3) == "sto") //thuốc nổ
                {
                    GameManagerControll.instance.mouseDownRac = this;
                    loadIdvp();
                    if (PlayerPrefsController.instance.getVpKho(GameManagerControll.instance.idThuocno) > 0)
                    {
                        PlayerPrefsController.instance.xoarac(mouseDownLockmap.idmap, idrac);
                        PlayerPrefsController.instance.removeVpKho(GameManagerControll.instance.idThuocno, 1);
                        StartCoroutine(delayXoarac(collision.GetComponents<HatgiongForllow>()[0].idHatgiong));
                    }
                    else
                    {
                        //GameManagerControll.instance.showObjText(centerCamera,
                        //    Until.checkLanguage() ? "Không đủ thuốc nổ" : "Not enough explosives", false);
                        collision.GetComponent<CircleCollider2D>().enabled = false;
                        GameManagerControll.instance.showDialogBuyVatpham(idVpPhamap);
                    }
                }
                else //cưa
                if (gameObject.name.Substring(0, 3) == "Cay")
                {
                    GameManagerControll.instance.mouseDownRac = this;
                    loadIdvp();
                    if (PlayerPrefsController.instance.getVpKho(GameManagerControll.instance.idCua) > 0)
                    {
                        PlayerPrefsController.instance.xoarac(mouseDownLockmap.idmap, idrac);
                        PlayerPrefsController.instance.removeVpKho(GameManagerControll.instance.idCua, 1);
                        StartCoroutine(delayXoarac(collision.GetComponents<HatgiongForllow>()[0].idHatgiong));
                    }
                    else
                    {
                        collision.GetComponent<CircleCollider2D>().enabled = false;
                        GameManagerControll.instance.showDialogBuyVatpham(idVpPhamap);
                        //GameManagerControll.instance.showObjText(centerCamera,
                        //        Until.checkLanguage() ? "Không đủ cưa" : "Not enough saws", false);
                    }
                }
                else //xẻng
                if (gameObject.name.Substring(0, 2) == "co")
                {
                    GameManagerControll.instance.mouseDownRac = this;
                    loadIdvp();
                    if (PlayerPrefsController.instance.getVpKho(GameManagerControll.instance.idKeo) > 0)
                    {
                        PlayerPrefsController.instance.xoarac(mouseDownLockmap.idmap, idrac);
                        PlayerPrefsController.instance.removeVpKho(GameManagerControll.instance.idKeo, 1);
                        StartCoroutine(delayXoarac(collision.GetComponents<HatgiongForllow>()[0].idHatgiong));
                    }
                    else
                    {
                        collision.GetComponent<CircleCollider2D>().enabled = false;
                        GameManagerControll.instance.showDialogBuyVatpham(idVpPhamap);
                        //GameManagerControll.instance.showObjText(centerCamera,
                        //        Until.checkLanguage() ? "Không đủ cưa" : "Not enough saws", false);
                    }
                }
                else
                {
                    GameManagerControll.instance.mouseDownRac = this;
                    loadIdvp();
                    if (PlayerPrefsController.instance.getVpKho(GameManagerControll.instance.idXeng) > 0)
                    {
                        PlayerPrefsController.instance.xoarac(mouseDownLockmap.idmap, idrac);
                        PlayerPrefsController.instance.removeVpKho(GameManagerControll.instance.idXeng, 1);
                        StartCoroutine(delayXoarac(collision.GetComponents<HatgiongForllow>()[0].idHatgiong));
                    }
                    else
                    {
                        collision.GetComponent<CircleCollider2D>().enabled = false;
                        GameManagerControll.instance.showDialogBuyVatpham(idVpPhamap);
                        //GameManagerControll.instance.showObjText(centerCamera,
                        //        Until.checkLanguage() ? "Không đủ cưa" : "Not enough saws", false);
                    }
                }
            }
        }
    }

    bool checkMapIdvpName(int idvp, string nameobj)
    {
        switch (idvp)
        {
            case 0:
                if (nameobj.Substring(0, 3) == "nam" || nameobj.Substring(0, 3) == "ho ")
                    return true;
                break;
            case 1:
                if (nameobj.Substring(0, 3) == "sto")
                    return true;
                break;
            case 2:
                if (nameobj.Substring(0, 3) == "Cay")
                    return true;
                break;
            case 3:
                if (nameobj.Substring(0, 3) == "co1" || nameobj.Substring(0, 3) == "co2")
                    return true;
                break;
        }
        return false;
    }

    void loadIdvp()
    {
        //Debug.Log("loadIdvp");
        idVpPhamap = 0;
        switch (gameObject.name.Substring(0, 3))
        {
            case "nam":
            case "ho ":
                idVpPhamap = 0;
                break;
            case "sto":
                idVpPhamap = 1;
                break;
            case "Cay":
                idVpPhamap = 2;
                break;
            case "co1":
            case "co2":
                idVpPhamap = 3;
                break;
        }
    }

    bool vacham = true;
    IEnumerator delayVacham()
    {
        vacham = false;
        yield return new WaitForSeconds(2f);
        vacham = true;
    }

    public IEnumerator delayXoarac(int idvp)
    {
        //dựa vào idvp chạy anim tương ứng
        GameObject anim = Instantiate(LockmapControll.instance.animvatpham[idvp], gameObject.transform.position, Quaternion.identity) as GameObject;
        Destroy(anim, 1.2f);
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        if (gameObject.name.Substring(0, 3) == "Cay")
        {
            SoundController.instance._playSound(19);
            gameObject.GetComponent<Animator>().SetTrigger("caydo");
        }
        yield return new WaitForSeconds(1f);
        if (gameObject.name.Substring(0, 3) == "sto")
        {
            SoundController.instance._playSound(20);
            GameManagerControll.instance.playEff(4, transform.position);
        }
        else
        {
            SoundController.instance._playSound(18);
            GameManagerControll.instance.playEff(0, transform.position);
        }
        yield return new WaitForSeconds(0.7f);
        GameManagerControll.instance._sinhObjThuhoach(gameObject, 0, "rac");
        PlayerPrefsController.instance.addExp(1);
        Destroy(gameObject);
    }

    IEnumerator delayAnim()
    {
        GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        GetComponent<Animator>().enabled = true;
    }
}
