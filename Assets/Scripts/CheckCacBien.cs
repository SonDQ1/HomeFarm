using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCacBien : MonoBehaviour {

    public bool checkShowPanel, checkDichuyen;
    public bool checkCamFollow;
	void Update () {
        checkShowPanel = Until.showPanel;
        checkDichuyen = Until.moveObj;

        checkCamFollow = MobileTouchCamera.checkCamFollow;
    }
}
