using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuaKhiCau : MonoBehaviour
{
    Vector3 vitricu;
    float mouseTime;
    int vitri = 0;

    public float speed = 1;
    [SerializeField] CircleCollider2D[] boxClick;
    [SerializeField] Sprite iconCoin, iconDiamod;
    Transform[] tfKhicau;
    int idvp = 0;
    int rdvtdi = 0;
    float maxSpeed;
    private void Start()
    {
        maxSpeed = speed;
        tfKhicau = GameManagerControll.instance.tfKhicau;
        tatBoxClick(true);
    }

    private void LateUpdate()
    {
        if (vitri == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, tfKhicau[0].position, speed * Time.deltaTime);

            //sắp đến điểm dừng giảm tốc độ
            if (Vector3.Distance(transform.position, tfKhicau[0].position) < 0.5f)
            {
                if (speed > 0)
                {
                    speed -= speed * Time.deltaTime;
                    if (speed <= 0)
                    {
                        speed = 0;
                    }
                }
            }

            if (Vector3.Distance(transform.position, tfKhicau[0].position) == 0)
            {
                vitri = -1;
                PlayerPrefsController.instance.setChonhanKC(1);
                tatBoxClick(false);
            }
        }
        else
            if (vitri == 1)
        {
            //tắng tốc độ dần dần
            if (speed < maxSpeed)
            {
                if (speed < 0.2f)
                    speed = 0.2f;
                speed += speed * Time.deltaTime;
            }
            else speed = maxSpeed;

            if (rdvtdi != 0 && rdvtdi != GameManagerControll.instance.rdvt)
            {
                transform.position = Vector3.MoveTowards(transform.position, tfKhicau[rdvtdi].position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, tfKhicau[rdvtdi].position) == 0)
                {
                    GameManagerControll.instance.chayKhicau();

                    Destroy(gameObject);
                }
            }
            else rdvtdi = Random.Range(1, tfKhicau.Length - 1);
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
                if (PlayerPrefsController.instance.checkHuongdan())
                {
                    //if (PlayerPrefsController.instance.getChat() == 12)
                    //{
                    //    GameManagerControll.instance.destroyHand();
                    //    nhanqua();
                    //    //hướng dẫn nhận quà khinh khí cầu
                    //    GameManagerControll.instance.showNVHD();

                    //}

                    if (PlayerPrefsController.instance.getBuocHuongdan() == 10)
                    {
                        GameManagerControll.instance.destroyHand();
                      //  nhanqua();
                        GameManagerControll.instance.destroyHand();
                        GameManagerControll.instance.disbaleHandMap();
                        //Until.checkHuongdan = false;
                        PlayerPrefsController.instance.setThoatHD(0);
                        PlayerPrefsController.instance.setHuongdan(1);
                        PlayerPrefsController.instance.setBuocHuongdan(0);
                        GameManagerControll.instance.loadPhanthuongngay();
                    }
                }
                else
                if (idvp >= 0)
                {
                    //Nhận quà ngẫu nhiên: kim cương
                    nhanqua();
                }
            }
    }

    void tatBoxClick(bool check)
    {
        boxClick[0].enabled = !check;
        boxClick[1].enabled = !check;
    }
    void nhanqua()
    {
        PlayerPrefsController.instance.setChonhanKC(0);
        GameManagerControll.instance.playEff(1, gameObject.transform.position);
        //random: kim cương, coin
        idvp = getIdVatpham();
        int sl = 0;
        string nameRewas = "";
        Transform posFly = null;
        Sprite icon = null;
        switch (idvp)
        {
            case 0:
                sl = Random.Range(PlayerPrefsController.instance.getLevel() * 50, PlayerPrefsController.instance.getLevel() * 80);
                nameRewas = Until.checkLanguage() ? " vàng" : " gold";
                PlayerPrefsController.instance.addCoin(sl);
                posFly = GameManagerControll.instance.posCoin;
                icon = iconCoin;
                break;
            case 1:
                sl = Random.Range(1, 3);
                nameRewas = Until.checkLanguage() ? " kim cương" : " diamod";
                PlayerPrefsController.instance.addDiamod(sl);
                posFly = GameManagerControll.instance.posDiamod;
                icon = iconDiamod;
                break;
        }
        GameManagerControll.instance.sinhvatphamnhan(gameObject.transform.position, icon, posFly);
        GameManagerControll.instance.showObjText(transform.position, "+ " + sl + nameRewas, false);
        idvp = -1;
        vitri = 1;

    }
    int getIdVatpham()
    {
        //int id = Random.Range(GameManagerControll.instance.idCua, GameManagerControll.instance.idSpeedTime + 1);
        int id = Random.Range(0, 2);
        return id;
    }
}
