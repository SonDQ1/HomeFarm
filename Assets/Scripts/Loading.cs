using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Image imgLoading;
    private float fillMount;
    public Text txtLoading;

    void Start()
    {
        fillMount = 0;
        StartCoroutine(delayFillMount());
    }

    IEnumerator delayFillMount()
    {
        for (int i = 0; i < 100; i++)
        {
            fillMount += 0.01f;
            imgLoading.fillAmount = fillMount;
            if (fillMount > 0.97f)
                txtLoading.text = "100%";
            else
                txtLoading.text = (int)(fillMount * 100) + "%";
            yield return new WaitForSeconds(3 / 100);
        }

        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainMenu");
    }
}
