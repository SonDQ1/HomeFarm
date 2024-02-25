using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardVideo : MonoBehaviour
{

    [SerializeField] Text[] btnReward;
    //[SerializeField] MobileRewardVideoAd mobileRewardVideoAd;
    [SerializeField] Sprite[] iconReward;
    int exp;
    int gem;
    int coint;
    int diamod;

    public void OnEnable()
    {
        exp = Random.Range(10 * PlayerPrefsController.instance.getLevel(), 30 * PlayerPrefsController.instance.getLevel());
        gem = Random.Range(5, 2 * PlayerPrefsController.instance.getLevel());
        coint = Random.Range(50 * PlayerPrefsController.instance.getLevel(), 100 * PlayerPrefsController.instance.getLevel());
        diamod = Random.Range(2, PlayerPrefsController.instance.getLevel());
        btnReward[0].text = "+ " + exp;
        btnReward[1].text = "+ " + gem;
        btnReward[2].text = "+ " + coint;
        btnReward[3].text = "+ " + diamod;
    }

    public void btnViewVideoClick(int id)
    {
        //mobileRewardVideoAd.setDialogConguration(id, iconReward[id], exp, gem, coint, diamod);
        //mobileRewardVideoAd.ButtonGift(id);
        AdApplovinController.Instance.ShowRewardedAd(() =>
           {
            PlayerPrefsController.instance.addGem(gem);
            PlayerPrefsController.instance.addExp(exp);
            PlayerPrefsController.instance.addDiamod(diamod);
           });
    }
}
