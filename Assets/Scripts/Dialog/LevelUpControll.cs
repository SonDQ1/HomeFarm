using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpControll : MonoBehaviour {

    [SerializeField] Text txtCoint,txtGem, txtKc,txtLv;
    [SerializeField] GameObject[] itemRewards;
    [SerializeField] DataLevel dataLevel;

    private void OnEnable()
    {
        SoundController.instance._playSound(1);
        txtLv.text = PlayerPrefsController.instance.getLevel() + "";
        loadItem();
        GameManagerControll.instance.destroyHand();
    }

    void loadItem()
    {
        for (int i = 0; i < itemRewards.Length; i++)
            itemRewards[i].SetActive(false);

        int coint = Random.Range(PlayerPrefsController.instance.getLevel() * 50,PlayerPrefsController.instance.getLevel()*100);
        int gem = Random.Range(PlayerPrefsController.instance.getLevel(), PlayerPrefsController.instance.getLevel() * 2);
        int diamod = Random.Range(2, 6);

        txtCoint.text = "+ " + coint;
        txtGem.text = "+ " + gem;
        txtKc.text = "+ " + diamod;

        PlayerPrefsController.instance.addCoin(coint);
        PlayerPrefsController.instance.addGem(gem);
        PlayerPrefsController.instance.addDiamod(diamod);

        int lv = PlayerPrefsController.instance.getLevel();
        if (dataLevel.data[lv].icons.Length > 0)
        {
            for(int i = 0; i< dataLevel.data[lv].icons.Length; i++)
            {
                itemRewards[i].SetActive(true);
                itemRewards[i].GetComponent<Image>().sprite = dataLevel.data[lv].icons[i];
                itemRewards[i].GetComponent<Image>().SetNativeSize();
            }
        }
    }
}
