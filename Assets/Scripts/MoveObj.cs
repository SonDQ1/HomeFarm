using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveObj : MonoBehaviour
{
    public int idluoinow;
    public bool demMove;
    public bool dcdat = true;
    public bool checkMove;
    public GameObject imgmuiten, posCurrent, postarget, posOdl;
    public Transform tfLuoi;
    public float localscale = 0;
    public bool checkInstanceObj;

    public PolygonCollider2D boxClick;
    public SpriteRenderer mSprite, conSprite, mSprite2;
    public Animator anim;
    // coint - gem
    public int coint, gem;
    Coroutine stopdem, animMove;
    GameObject effectmove, groupBtnMove;
    Vector3 posStart;
    Vector2 offset;
    Color color, noColor;
    Vector3 vitricu;
    bool checkVacham = false;
    public bool checkSave = false;

    bool delayDestroyRigit = false;

    //lỗi bấm và nhả nhanh
    bool checkMouseUpMove = false;

    private Vector3 dist;
    float posX, posY;

    void Start()
    {
        tfLuoi = GameObject.Find("Grounds").transform;
        groupBtnMove = GameObject.Find("groupBtnMove").gameObject;
        posCurrent = tfLuoi.GetChild(idluoinow).gameObject;
        posOdl = posCurrent;
        ColorUtility.TryParseHtmlString("#FF1717", out color);
        ColorUtility.TryParseHtmlString("#FFFFFF", out noColor);
        localscale = 0;
        //Debug.Log("MoveObj localscale: " + localscale);
        //new
        if (gameObject.CompareTag("roadInstan") || gameObject.CompareTag("hangraoInstan"))
        {
            mSprite = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            mSprite2 = gameObject.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
            anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        }
        else
        {
            mSprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            mSprite2 = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
            anim = gameObject.GetComponent<Animator>();
        }

        if (checkInstanceObj)
        {
            _addRigitboby();
            GameManagerControll.instance.tatBoxClick(false);
            if (boxClick != null)
                boxClick.enabled = false;
            anim.SetBool("move", true);
            demMove = true;
            checkMove = true;
            showButtonMove(true);
            gameObject.GetComponent<SetOder>().sortingLayer("move");
            GameManagerControll.instance.setParent(gameObject);
            GameManagerControll.instance.moveObj = gameObject.GetComponent<MoveObj>();
        }
    }
    float mouseTime;
    void OnMouseDown()
    {
        if (!Until.showPanel)
        {
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                dist = Camera.main.WorldToScreenPoint(transform.position);
                posX = Input.mousePosition.x - dist.x;
                posY = Input.mousePosition.y - dist.y;

                if (demMove == false && !Until.moveObj)
                {
                    offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    posStart = posCurrent.transform.position;

                    checkMouseUpMove = false;
                    if (!checkInstanceObj && !Until.checkInstanceObj)
                        stopdem = StartCoroutine(chaydem());
                    mouseTime = Time.time;
                    checkEndpolygon = true;
                    vitricu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }
    }

    IEnumerator chaydem()
    {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("chaydem BEGIN move");
        float vt = Vector3.Distance(vitricu, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (vt < 0.2f && Time.time - mouseTime > 0.4f)
        {
            loadSpriteAnim();
            mSprite.sortingLayerName = "move";
            mSprite2.sortingLayerName = "move";
            //Until.moveObj = true;
            //GameManagerControll.instance.tatBoxClick(false);
            

            if (conSprite != null)
            {
                conSprite.sortingLayerName = "move";
            }
            if (effectmove == null)
            {
                //sinh mũi tên lên cao so với vị trí chuồng, nhà
                effectmove = Instantiate(imgmuiten, getPosMuiten(), Quaternion.identity) as GameObject;
                //mũi tên full sau 1s setanim
                animMove = StartCoroutine(showAnim());
            }
            else if (effectmove != null)
            {
                effectmove.SetActive(true);
            }
        }
    }

    IEnumerator showAnim()
    {
        //tắt canvas thức ăn
        if (gameObject.CompareTag("chuong"))
            GameManagerControll.instance.destroyHand();
        yield return new WaitForSeconds(0.8f);
        if (!checkMouseUpMove)
        {
			GameManagerControll.instance.tatBoxClick(false);
            if (boxClick != null)
                boxClick.enabled = false;

            Until.moveObj = true;
            anim.SetBool("move", true);
            anim.SetBool("put", false);
            checkMove = true;
            demMove = true;
            checkVacham = false;

            showButtonMove(true);

            gameObject.GetComponent<SetOder>().sortingLayer("move");
            GameManagerControll.instance.moveObj = gameObject.GetComponent<MoveObj>();
            GameManagerControll.instance.objInstance = gameObject;
        }
    }

    void loadSpriteAnim()
    {
        if (gameObject.CompareTag("roadInstan") || gameObject.CompareTag("hangraoInstan"))
        {
            mSprite = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            mSprite2 = gameObject.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
            anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        }
    }

    Vector2 getPosMuiten()
    {
        Vector2 temp = transform.position;
        if (Until.checkTagNhamay(gameObject.tag) || gameObject.CompareTag("chuong"))
            temp.y += 2f;
        if (gameObject.CompareTag("trangtri"))
            temp.y += 1f;
        return temp;
    }
    
    bool checkEndpolygon = true;


    void OnMouseDrag()
    {
        if (!Until.showPanel)
        {
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                if (demMove == true)
                {
                    MobileTouchCamera.checkCamFollow = true;
                    delayDestroyRigit = true;
                    loadSpriteAnim();
                    _addRigitboby();
                    checkMove = true;
                    if (boxClick != null)
                        boxClick.enabled = false;

                    Vector3 cusPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, 0);
                    Vector3 worlPoint = Camera.main.ScreenToWorldPoint(cusPos);
                    worlPoint.z = transform.position.z;
                    transform.position = worlPoint;

                    //Vector2 vDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //transform.position = new Vector2(vDrag.x, vDrag.y) + offset;

                    //gameObject.transform.position = getTarget();

                    if (!checkVacham)
                    {
                        int layermask = LayerMask.GetMask("ground");
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, layermask);
                        if (hit.collider != null)
                        {
                            postarget = hit.collider.gameObject;
                            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, postarget.transform.position, 0.5f);
                            Debug.DrawLine(transform.position, hit.transform.position, Color.red);
                        }
                    }

                    if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 5)
                    {
                        GameManagerControll.instance.destroyHand();
                    }
                }
            }
        }
    }

    void OnMouseUp()
    {
        vitricu = new Vector3();
        if (demMove == true)
        {
            checkMouseUpMove = false;
            //demMove = false;
            if (postarget != null && dcdat == true)
            {
                if (!checkVacham)
                {
                    iTween.MoveTo(this.gameObject, iTween.Hash("position", postarget.transform.position, "time", .3f, "easetype", iTween.EaseType.linear));
                    //new pos
                    //Debug.Log("Đặt vật: " + postarget.GetComponent<MouseDownLuoi>().idLuoi);
                    posCurrent.GetComponent<MouseDownLuoi>().endBoxLuoi(true);
                    postarget.GetComponent<MouseDownLuoi>().endBoxLuoi(false);

                    idluoinow = postarget.GetComponents<MouseDownLuoi>()[0].idLuoi;

                    posCurrent = tfLuoi.GetChild(idluoinow).gameObject;
                    demMove = true;
                    MobileTouchCamera.checkCamFollow = false;
                    StartCoroutine(delayReload());

                    //lần đầu sinh ra obj, chưa có posOdl
                    if (checkInstanceObj)
                        posOdl = posCurrent;

                    if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 5)
                    {
                        GameManagerControll.instance.huongdanclickdatnha();
                    }
                }
                else
                {
                    Debug.Log("Va chạm vật => quay về vị trí ban đầu :) ");
                    iTween.MoveTo(this.gameObject, iTween.Hash("position", posOdl.transform.position, "time", .3f, "easetype", iTween.EaseType.linear));
                    demMove = true;
                    StartCoroutine(changePosZMove());
                    MobileTouchCamera.checkCamFollow = false;
                }

            }
            else if (dcdat == false)
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("position", posStart, "time", .3f, "easetype", iTween.EaseType.linear));
                //back
            }
            //if (posCurrent != null)
            //    StartCoroutine(SaveData(dcdat));
        }
        else
        if (demMove == false)
        {
            StartCoroutine(delayCheckMouseUpMove());
            //Debug.Log("End move");
            if (stopdem != null && checkMove)
            {
                StopCoroutine(stopdem);
                if(animMove != null)
                    StopCoroutine(animMove);

				GameManagerControll.instance.tatBoxClick(true);
                Debug.Log("tatBoxClick:true");
                if (boxClick != null)
                    boxClick.enabled = true;

                Until.moveObj = false;
            }

            //trả về trạng thái ban đầu khi chưa nhấc lên của obj
            loadSpriteAnim();
            mSprite.sortingLayerName = "Default";
            mSprite2.sortingLayerName = "Default";
            gameObject.GetComponent<SetOder>().sortingLayer("Default");

            checkMove = false;

            //Debug.Log("boxClick.enabled = true;");
            if (conSprite != null)
            {
                conSprite.sortingLayerName = "Default";
            }
        }

        if (effectmove != null) Destroy(effectmove);
        //destroy rigitbody - mở boxclick
        StartCoroutine(delayDestroyRigitbody());
    }

    IEnumerator delayCheckMouseUpMove()
    {
        checkMouseUpMove = true;
        yield return new WaitForSeconds(1f);
        checkMouseUpMove = false;
    }

    IEnumerator delayDestroyRigitbody()
    {
        yield return new WaitForSeconds(0.5f);
        if(!delayDestroyRigit)
            _DestroyRigitbody();

    }

    IEnumerator changePosZMove()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }
    IEnumerator delayReload()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<SetOder>().reloadOrder();
    }
    public void saveData()
    {
        if (postarget != null && checkSave)
        {
            SoundController.instance._playSound(4);
            if (PlayerPrefsController.instance.checkHuongdan() && gameObject.CompareTag("maythucan"))
            {
                GameManagerControll.instance.LevelUp(3);
                PlayerPrefsController.instance.setBuocHuongdan(6);
                GameManagerControll.instance.destroyHand();
                //show nv hd và sản xuất
            }
            //hd cho ăn
            if (PlayerPrefsController.instance.getBuocHuongdan() == 8)
            {
                //if(Until.checkLanguage())
                //    GameManagerControll.instance.showNVHD();
                //else
                //{
                    PlayerPrefsController.instance.setBuocHuongdan(9);
                    GameObject obj = GameObject.FindGameObjectWithTag("chuong").gameObject;
                    GameManagerControll.instance.sinhHand(0, obj.transform.position);
                    GameManagerControll.instance._camView(obj.transform.position);
                //}
            }

            GameManagerControll.instance.tatBoxClick(true);
            if (boxClick!= null)
                boxClick.enabled = true;
            removeData();

            int idobj = gameObject.GetComponents<ObjIdMapPosInstance>()[0].id;
            int idluoitarget = postarget.GetComponents<MouseDownLuoi>()[0].idLuoi;
            if (gameObject.tag != "hangraoInstan")
                localscale = gameObject.transform.localScale.x;

            postarget.GetComponent<MouseDownLuoi>().endBoxLuoi(false);
            if (checkInstanceObj)
            {
                PlayerPrefsController.instance.exceptCoin(coint);
                PlayerPrefsController.instance.exceptGem(gem);
            }

            //set level xây tiếp theo: levelgoc+10;
            if (Until.checkTagNhamay(gameObject.tag) || gameObject.CompareTag("chuong") || gameObject.CompareTag("gieng"))
            {
                //skhi sinh mới đối tượng
                if (checkInstanceObj)
                {
                    if (!PlayerPrefsController.instance.getBuildingObj(gameObject.tag, idobj))
                    {
                        PlayerPrefsController.instance.setBuildingObj(gameObject.tag, idobj, 1);
                        if(gameObject.CompareTag("gieng"))
                        {
                            PlayerPrefsController.instance.setLevelUnlockObj(gameObject.tag, idobj, gameObject.GetComponents<GiengController>()[0].levelUnlock + getLevelBuild(idobj));
                            //Debug.Log("Level next gieng: "+ gameObject.GetComponents<GiengController>()[0].levelUnlock + getLevelBuild(idobj));
                        }
                        else
                            if (Until.checkTagNhamay(gameObject.tag))
                                PlayerPrefsController.instance.setLevelUnlockObj(gameObject.tag, idobj, gameObject.GetComponents<NhamayControll>()[0].levelUnlock + getLevelBuild(idobj));
                        else
                            if (gameObject.CompareTag("chuong"))
                                PlayerPrefsController.instance.setLevelUnlockObj(gameObject.tag, idobj, gameObject.GetComponents<ChuongController>()[0].levelUnlock + getLevelBuild(idobj));
                    }
                    else
                        PlayerPrefsController.instance.setLevelUnlockObj(gameObject.tag, idobj, getLevelBuild(idobj));
                    //Debug.Log("Level unlock[" + gameObject.tag + "] = " + PlayerPrefsController.instance.getLevelUnlockObj(gameObject.tag, idobj));

                    if (Until.checkTagNhamay(gameObject.tag))
                    {
                        //gameObject.GetComponent<NhamayControll>().idLuoi = idluoinow;
                        //gameObject.GetComponent<NhamayControll>().changeNhamay(posOdl.GetComponents<MouseDownLuoi>()[0].idLuoi, idluoinow);
                        int idnhamay = PlayerPrefsController.instance.getIdNha(gameObject.tag + gameObject.GetComponent<ObjIdMapPosInstance>().id);
                        //gameObject.GetComponent<NhamayControll>().idLuoi = idnhamay;
                        idnhamay = gameObject.GetComponent<NhamayControll>().idLuoi;

                        PlayerPrefsController.instance.saveIdNhamayLuoi(idluoinow, idnhamay);
                        //Debug.Log("save" + gameObject.tag + idnhamay + "=>" + idluoinow);

                        PlayerPrefsController.instance.addIdNha(gameObject.tag + gameObject.GetComponent<ObjIdMapPosInstance>().id);
                    }
                }

                //nhaf mays
                if (Until.checkTagNhamay(gameObject.tag))
                {
                    int idnhamay = gameObject.GetComponent<NhamayControll>().idLuoi;
                    PlayerPrefsController.instance.saveIdNhamayLuoi(idluoinow, idnhamay);
                    //Debug.Log("save" + gameObject.tag + idnhamay + "=>" + idluoinow);
                }
                //chuyển đổi trạng thái phát triển con vật
                if (gameObject.CompareTag("chuong"))
                {
                    //ban đầu cho thêm thức ăn khi mua chuồng
                    if (PlayerPrefsController.instance.getLandauxaychuong(gameObject.name))
                    {
                        PlayerPrefsController.instance.landauxaychuong(gameObject.name, 1);
                        choThucan();
                        //Debug.Log("landauxaychuong "+gameObject.name.Substring(0, 9));
                    }
                    gameObject.GetComponent<ChuongController>().setActiveConvat(true);
                    gameObject.GetComponent<ChuongController>().idChuong = idluoinow;
                    gameObject.GetComponent<ChuongController>().changeAnimail(posOdl.GetComponents<MouseDownLuoi>()[0].idLuoi, idluoinow);

                }
                //chuyển đổi trạng thái phát triển giếng
                if (gameObject.CompareTag("gieng"))
                {
                    gameObject.GetComponent<GiengController>().idgieng = idluoinow;
                    gameObject.GetComponent<GiengController>().changePosition(posOdl.GetComponents<MouseDownLuoi>()[0].idLuoi, idluoinow);
                }
            }
            checkInstanceObj = false;

            anim.SetBool("move", false);
            anim.SetBool("put", true);

            gameObject.GetComponent<SetOder>().sortingLayer("Default");

            _DestroyRigitbody();
            Until.checkInstanceObj = false;
            demMove = false;
            checkMove = false;
            Until.moveObj = false;
            showButtonMove(false);
            posOdl = postarget;

            PlayerPrefsController.instance.saveObjLuoi(gameObject.tag, idobj, localscale, idluoitarget);
            gameObject.GetComponent<SetOder>().reloadOrder();
            //Debug.Log("SavaData: " + gameObject.tag + "[" + idobj + "-" + localscale + "]->" + idluoitarget);

        }
        else
            GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
                Until.checkLanguage() ? "Không đủ chỗ trống" : "There is no space", true);
    }

    int getLevelBuild(int idobj)
    {
        return (PlayerPrefsController.instance.getLevelUnlockObj(gameObject.tag, idobj) + 7);
    }
    public void changRotation()
    {
        switch (gameObject.tag)
        {
            case "hangraoInstan":
                gameObject.GetComponent<MouseDownHangrao>().rotation();
                gameObject.GetComponent<SetOder>().reloadOrder();
                break;
            case "roadInstan":
            case "trangtri":
            case "gieng":
            case "chuong":
                if (gameObject.transform.localScale.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                break;
        }

        //check nhà máy
        if (Until.checkTagNhamay(gameObject.tag))
        {
            if (gameObject.transform.localScale.x > 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    public void removeData()
    {
        //Debug.Log("Xóa obj lưới: " + posOdl.GetComponent<MouseDownLuoi>().idLuoi);
        showButtonMove(false);
        posOdl.GetComponent<MouseDownLuoi>().endBoxLuoi(true);
        PlayerPrefsController.instance.removeObjLuoi(posOdl.GetComponents<MouseDownLuoi>()[0].idLuoi);
    }

    public void _addRigitboby()
    {
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            if (gameObject.CompareTag("chuong"))
            {
                gameObject.GetComponent<ChuongController>().setActiveConvat(false);
            }

            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }

    public void _DestroyRigitbody()
    {
        delayDestroyRigit = false;
        MobileTouchCamera.checkCamFollow = false;
        //Debug.Log("_DestroyRigitbody");
        //gameObject.GetComponent<SetOder>().sortingLayer("Default");
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }

    Vector3 getTarget()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 posCam = new Vector2(cursorPosition.x, cursorPosition.y);
        Vector3 target = new Vector3(posCam.x, posCam.y, 0);
        target = new Vector3(((int)(posCam.x / 0.6f)) * 0.63f, ((int)(posCam.y / 0.3f)) * 0.31f, 0);
        return target;
    }

    //check va chạm với vật đã có từ trước
    bool checkTrigger = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (!checkTrigger)
        //{
        //    checkTrigger = true;
        if (checkMove && demMove)
        {
            checkVacham = checkVachamTag(collision.tag);
            if (checkVacham && !boxClick.enabled)
            {
                if (gameObject.CompareTag("hangraoInstan") || gameObject.CompareTag("roadInstan"))
                    gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = color;
                else
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
                checkSave = false;
            }
            //Debug.Log(gameObject.name + " OnTriggerStay2D => " + collision.name);
        }
        //}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (checkTrigger)
        //{
        //    checkTrigger = false;
        if (checkMove)
        {
            checkVacham = checkVachamTag(collision.tag);
            if (!checkVacham && !boxClick.enabled)
            {
                if (gameObject.CompareTag("hangraoInstan") || gameObject.CompareTag("roadInstan"))
                    gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = noColor;
                else
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = noColor;
                checkSave = true;
            }
            //Debug.Log(gameObject.name+ " OnTriggerExit2D => " + collision.name);
        }
        //}
    }
    bool checkVachamTag(string tag)
    {
        switch (tag)
        {
            case "nhamay":
            case "maythucan":
            case "maysua":
            case "lonuong":
            case "douong":
            case "thomay":
            case "nuongthit":
            case "daubep":
            case "thucong":
            case "nha":
            case "nhakho":
            case "odat":
            case "odat2":
            case "odat3":
            case "odat-khoa":
            case "cay":
            case "chuong":
            case "lockmap":
            case "rac":
            case "hangraoInstan":
            case "roadInstan":
            case "trangtri":
            case "gieng":
                return true;
        }

        if (gameObject.CompareTag("roadInstan") && CompareTag("pets"))
            return false;
        else if (CompareTag("pets"))
            return true;
        return false;
    }

    //set btn move
    public void showButtonMove(bool check)
    {
        if (!checkInstanceObj && checkMove && (Until.checkTagNhamay(gameObject.tag) || gameObject.CompareTag("chuong")))
        {
            groupBtnMove.transform.GetChild(0).gameObject.SetActive(false);
            groupBtnMove.transform.GetChild(1).gameObject.SetActive(check);
            groupBtnMove.transform.GetChild(2).gameObject.SetActive(check);
            groupBtnMove.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("hiden", !check);
            groupBtnMove.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("hiden", !check);
        }
        else
        {
            groupBtnMove.transform.GetChild(0).gameObject.SetActive(check);
            groupBtnMove.transform.GetChild(1).gameObject.SetActive(check);
            groupBtnMove.transform.GetChild(2).gameObject.SetActive(check);
            groupBtnMove.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("hiden", !check);
            groupBtnMove.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("hiden", !check);
            groupBtnMove.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("hiden", !check);
        }
    }

    void choThucan()
    {
        switch (gameObject.name.Substring(0, 10))
        {
            case "1.chuongga":
                PlayerPrefsController.instance.addKho(19,4);
                break;
            case "2.chuonglo":
                PlayerPrefsController.instance.addKho(20, 4);
                break;
            case "4.chuongbo":
                PlayerPrefsController.instance.addKho(21, 4);
                break;
            case "7.chuongcu":
                PlayerPrefsController.instance.addKho(22, 4);
                break;
            case "11.chuongg":
                PlayerPrefsController.instance.addKho(23, 4);
                break;
            case "13.chuongd":
                PlayerPrefsController.instance.addKho(24, 4);
                break;
            case "15.chuongb":
                PlayerPrefsController.instance.addKho(25, 4);
                break;
        }
    }
}
