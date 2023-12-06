using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huongdan : MonoBehaviour {

    public static Huongdan instance;
    public Transform posOrderhd, posBtnSend;
	void Start () {
        if (instance == null) instance = this;
		if(PlayerPrefsController.instance.checkHuongdan() && PlayerPrefsController.instance.getBuocHuongdan() == 2)
        {
            GameManagerControll.instance.sinhHand(0, posOrderhd.position);
        }
	}
}
