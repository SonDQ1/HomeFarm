using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VachamOsx : MonoBehaviour
{

    [SerializeField] Image icon;
    bool checkShowText;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("objMake") && !icon.IsActive() && Until.itemMakeBtn.checkMake())
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            //thêm vp muốn tạo vào list - destroy obj vp
            //lưu mảng các vp cần tạo
            PlayerPrefsController.instance.addListVp(Until.tagNhamayClick, Until.idNhamayClick, collision.GetComponent<HatgiongForllow>().idHatgiong);
            //Debug.Log("Make: " + collision.GetComponent<HatgiongForllow>().idHatgiong);
            Until.itemMakeBtn.removeSPMake();
            Until.nhamayControll.checkMake(collision.GetComponent<SpriteRenderer>().sprite);
            //Until.nhamayControll.checkMake(collision.GetComponent<HatgiongForllow>().idHatgiong,collision.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        }
        else
        if (!Until.itemMakeBtn.checkMake())
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
                Until.checkLanguage() ? "Không đủ nguyên liệu" : "Not enough material", true);
        }
    }
}
