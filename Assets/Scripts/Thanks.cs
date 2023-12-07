using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thanks : MonoBehaviour {
    
	void Start () {
        Until.showQc();
        StartCoroutine(delayExitGame());
	}

    IEnumerator delayExitGame()
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
