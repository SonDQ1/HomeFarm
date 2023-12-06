using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClickPlaySound : MonoBehaviour {

    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => playSound());
    }

    void playSound()
    {
        SoundController.instance._playSound(22);
    }
}
