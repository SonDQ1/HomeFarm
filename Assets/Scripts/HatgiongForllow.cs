using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatgiongForllow : MonoBehaviour
{

    public int idHatgiong;
    //check để tránh follow
    public bool checkItemMake;
    public bool checkXoacay;
    private void Start()
    {
        if (!checkItemMake)
            GameManagerControll.instance._showBarshopcaytrong("", false);
        if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 3)
        {
            GameManagerControll.instance.destroyHand();
        }
    }
    void Update()
    {
        Vector3 vDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(vDrag.x, vDrag.y);
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator delayInstance()
    {
        yield return new WaitForSeconds(0.5f);
        Until.checkInstanItemMake = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.CompareTag("luoiliem") && collision.CompareTag("odat"))
            if (collision.gameObject.GetComponent<OdatControll>().ttodat == "thuhoach")
                gameObject.GetComponent<Animator>().SetBool("thuhoach", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.CompareTag("luoiliem") && collision.CompareTag("odat"))
            gameObject.GetComponent<Animator>().SetBool("thuhoach", false);
    }

    public void OnDestroy()
    {
        if (!checkItemMake)
            MobileTouchCamera.checkCamFollow = false;
        Until.checkInstanHatgiong = false;
        GameManagerControll.instance.StartCoroutine(delayInstance());

        if (PlayerPrefsController.instance.checkHuongdan() && gameObject.CompareTag("luoiliem") && PlayerPrefsController.instance.getBuocHuongdan() == 1)
        {
            if (Until.checkThuhoachlua)
            {
                //if (Until.checkLanguage())
                //    GameManagerControll.instance.showNVHD(); //hd order
                //else
                    GameManagerControll.instance.huongdanOrder();
            }
            else
            {
                GameManagerControll.instance.huongdanthuhoachlua();
            }
        }

        if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 3)
        {
            //if (Until.checkLanguage())
            //    GameManagerControll.instance.showNVHD();//hd mở ô đất
            //else
                GameManagerControll.instance.huongdanmoodat();
        }

        if (PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 9)
        {
            //if (Until.checkLanguage())
            //    GameManagerControll.instance.showNVHD(); //kết thúc hướng dẫn
            //else
            //{
                PlayerPrefsController.instance.setBuocHuongdan(10);
                GameObject khicau = GameObject.FindGameObjectWithTag("khicau").gameObject;
                GameManagerControll.instance.sinhHand(0, khicau.transform.position, khicau.transform);
                GameManagerControll.instance._camView(khicau.transform.position);
            //}
        }
    }
}
