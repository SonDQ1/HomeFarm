using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControll : MonoBehaviour
{

    [SerializeField] GameObject panelIn, panelOut;
    [SerializeField] GameObject dialogExit;

    void Start()
    {
        Until.showQc();
        SoundController.instance._playMusicBgr(0, PlayerPrefs.GetFloat("volume" + "music"));
        StartCoroutine(delayEffIn());
    }
    //thoat game
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Until.showPanel = true;
            dialogExit.SetActive(true);
        }
    }
    public void btnPlayClick()
    {
        StartCoroutine(delayEffOut());
    }
    public void btnLinkGame1()
    {
        Application.OpenURL("market://details?id=farm.nongtrai.famer.lamvuon.farming.trangtrai.village.farmstory.farmschool.bigfarm.hayfarm.bigfarmcity");
    }
    public void btnLinkGame2()
    {
        Application.OpenURL("market://details?id=ngoisaothoitrang.ngoisaotrangdiem.girl.makeup.superstarmakeup");
    }
    IEnumerator delayEffIn()
    {
        panelIn.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        panelIn.SetActive(false);
    }
    IEnumerator delayEffOut()
    {
        panelOut.SetActive(true);
        yield return new WaitForSeconds(0.22f);
        SceneManager.LoadScene("GamePlay");
    }
}
