using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnPointerClickCloseDialog : MonoBehaviour, IPointerClickHandler
{
    public GameObject dialog;
    public bool checkDialog, checkCanvas;
    //hướng dẫn
    public GameObject txtChat;

    bool delayClick = false;

    private void OnEnable()
    {
        delayClick = false;
        Until.showPanel = true;
        randomNhanvat();

        switch (PlayerPrefsController.instance.getChat())
        {
            case 1:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                     Until.checkLanguage() ? "Đầu tiên bạn hãy thu hoạch theo hướng dẫn nhé." : "First, harvesting follow the instructions.";
                break;
            case 2:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                   Until.checkLanguage() ? "Hãy bắt đầu thôi nào." : "Let's start.";
                break;
            case 4:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Nhấn vào bảng đơn hàng và chuyển đơn hàng theo yêu cầu để nhận lại tiền và kinh nghiệm" : "Click to received orders from  the broad, transport and earn money and experiences";
                break;
            case 5:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Chúc mừng bạn lên cấp, hãy tiếp tục trồng trọt nhé" : "Congratulations on leveling up, keep on farming";
                break;
            case 6:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Nhấn vào tấm biển để mở thêm ô đất trồng mới" : "Click on the sign to open a new farming slot";
                break;
            case 7:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Tốt lắm, bây giờ hãy vào cửa hàng xây dựng nhà máy và sản xuất" : "Very well, now head to constructing store and the manufacturing workshop";
                break;
            case 8:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Chúc mừng bạn lên cấp, tiếp tục nhấn vào nhà máy và sản xuất thức ăn cho vật nuôi" : "Continue to click on the factory and produce pet food";
                break;
            case 9:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Rất tốt, hãy tiếp tục xây dựng chuồng gà" : "Very well, keep building the chicken coop";
                break;
            case 10:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Nhấn vào chuồng, kéo thức ăn cho các con vật ăn nhé" : "Click on the cage, pull the food for the animals to eat";
                break;
            case 11:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Bạn có quà từ những người bạn phương xa, hãy nhấn vào khinh khí cầu để nhận nó" : "You have gifts from faraway friends, press the balloon to receive it";
                break;
            case 12:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Chúc mừng bạn đã hoàn thành hướng dẫn, hãy tiếp tục xây dựng Nông trại vui vẻ của bạn nhé" : "Congratulations on completing the tutorial, please continue to build your Happy farm";
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick " + PlayerPrefsController.instance.getChat());
        if (!delayClick )
        {
            if(PlayerPrefsController.instance.getChat() > 2)
                delayClick = true;
            if (PlayerPrefsController.instance.checkHuongdan())
            {
                StartCoroutine(delayShowTXT());
            }
            else
            {
                if (checkDialog)
                    GameManagerControll.instance.delayCloseDialog(dialog, checkCanvas);
                else
                {
                    if (dialog != null)
                        dialog.SetActive(false);
                }
            }
        }
    }

    IEnumerator delayShowTXT()
    {
        txtChat.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        txtChat.SetActive(true);
        switch (PlayerPrefsController.instance.getChat())
        {
            case 0:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                    Until.checkLanguage() ? "Tôi sẽ hướng dẫn bạn trồng cây, chăn nuôi và xây dựng." : "I will guide you in planting, breeding and building.";
                PlayerPrefsController.instance.setChat(2);
                randomNhanvat();
                break;
            case 1:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                     Until.checkLanguage() ? "Đầu tiên bạn hãy thu hoạch theo hướng dẫn nhé." : "First, harvesting follow the instructions.";
                PlayerPrefsController.instance.setChat(2);
                randomNhanvat();
                break;
            case 2:
                txtChat.transform.GetChild(0).GetComponent<Text>().text =
                   Until.checkLanguage() ? "Hãy bắt đầu thôi nào." : "Let's start.";
                PlayerPrefsController.instance.setChat(3);
                randomNhanvat();
                break;
            case 3:
                StartCoroutine(delayCloseNVHD());
                PlayerPrefsController.instance.setBuocHuongdan(1);
                GameManagerControll.instance.loadThoatHD();
                PlayerPrefsController.instance.setChat(4);

                break;
            case 4://hướng dẫn chuyển đơn hàng
                StartCoroutine(delayCloseNVHD());
                GameManagerControll.instance.huongdanOrder();
                PlayerPrefsController.instance.setChat(5);
                break;
            case 5://tăng level-> hướng dẫn trồng cây
                StartCoroutine(delayCloseNVHD());
                if (PlayerPrefsController.instance.getBuocHuongdan() == 3)
                    GameManagerControll.instance.huongdantrongcay();
                PlayerPrefsController.instance.setChat(6);
                break;
            case 6://hướng dẫn mở ô đất
                StartCoroutine(delayCloseNVHD());
                GameManagerControll.instance.huongdanmoodat();
                PlayerPrefsController.instance.setChat(7);
                break;
            case 7://hướng dẫn xây nhà máy
                StartCoroutine(delayCloseNVHD());
                PlayerPrefsController.instance.setBuocHuongdan(5);
                GameManagerControll.instance.huongdanxaynhamay();
                PlayerPrefsController.instance.setChat(8);
                GameManagerControll.instance.sinhKhicau();
                break;
            case 8://hướng dẫn sản xuất xây nhà máy
                StartCoroutine(delayCloseNVHD());
                GameManagerControll.instance.huongdansanxuat();
                PlayerPrefsController.instance.setChat(9);
                break;
            case 9://hướng dẫn xây chuồng
                StartCoroutine(delayCloseNVHD());
                GameManagerControll.instance.huongdanxaynhamay();
                PlayerPrefsController.instance.setChat(10);
                break;
            case 10://hướng dẫn cho ăn
                StartCoroutine(delayCloseNVHD());
                PlayerPrefsController.instance.setBuocHuongdan(9);
                GameObject obj = GameObject.FindGameObjectWithTag("chuong").gameObject;
                GameManagerControll.instance.sinhHand(0, obj.transform.position);
                GameManagerControll.instance._camView(obj.transform.position);
                PlayerPrefsController.instance.setChat(11);
                break;
            case 11://nhận quà từ kinh khí cầu
                StartCoroutine(delayCloseNVHD());

                GameObject khicau = GameObject.FindGameObjectWithTag("khicau").gameObject;
                GameManagerControll.instance.sinhHand(0, khicau.transform.position);
                GameManagerControll.instance._camView(khicau.transform.position);

                PlayerPrefsController.instance.setChat(12);
                break;
            case 12://nhận quà từ kinh khí cầu
                StartCoroutine(delayCloseNVHD());
                //kết thúc hướng dẫn
                GameManagerControll.instance.destroyHand();
                GameManagerControll.instance.disbaleHandMap();
                //Until.checkHuongdan = false;
                PlayerPrefsController.instance.setThoatHD(0);
                PlayerPrefsController.instance.setHuongdan(1);
                PlayerPrefsController.instance.setBuocHuongdan(0);
                PlayerPrefsController.instance.setChat(12);
                GameManagerControll.instance.loadPhanthuongngay();
                break;
        }
    }

    IEnumerator delayCloseNVHD()
    {
        dialog.GetComponent<Animator>().SetTrigger("hiden");
        txtChat.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        dialog.SetActive(false);
        Until.showPanel = false;
    }

    void randomNhanvat()
    {
        //Debug.Log("randomNhanvat");
        //random avatar nhân vật
        dialog.transform.GetChild(0).gameObject.SetActive(false);
        dialog.transform.GetChild(1).gameObject.SetActive(false);
        dialog.transform.GetChild(Random.Range(0, 2)).gameObject.SetActive(true);
    }
}