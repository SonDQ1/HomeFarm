using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSetting : MonoBehaviour {

    [SerializeField] Slider sldMusic, sldSound;

    private void OnEnable()
    {
        sldMusic.onValueChanged.AddListener(delegate { ValuesldMusicChange(); });
        sldSound.onValueChanged.AddListener(delegate { ValuessldSoundChange(); });

        //load
        sldMusic.value = PlayerPrefsController.instance.getVolume("music");
        sldSound.value = PlayerPrefsController.instance.getVolume("sound");
    }

    public void ValuesldMusicChange()
    {
        SoundController.instance._changeMusicVolume(sldMusic.value);
        PlayerPrefsController.instance.setVolume("music", sldMusic.value);
    }
    public void ValuessldSoundChange()
    {
        SoundController.instance._changeSoundVolume(sldSound.value);
        PlayerPrefsController.instance.setVolume("sound", sldSound.value);
    }
    public void btnExitClick()
    {
        GameManagerControll.instance.closeAllDialog();
        GameManagerControll.instance._showDialog(12);
    }
}
