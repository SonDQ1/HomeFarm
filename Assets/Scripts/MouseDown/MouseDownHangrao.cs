using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDownHangrao : MonoBehaviour {

    Vector2 locaSacle;
    [SerializeField] GameObject[] objHangrao;
    [SerializeField] string namehangrao;
    int idchange = 0;
    GameObject obj;
    MoveObj moveObj;
    private void Start()
    {
        idchange = 0;
        moveObj = gameObject.GetComponent<MoveObj>();
    }

    public void loadHangrao(float id)
    {
        if (gameObject.transform.childCount > 0)
            Destroy(gameObject.transform.GetChild(0).gameObject);
        if (obj != null)
            Destroy(obj);
        obj = Instantiate(objHangrao[(int)id], gameObject.transform.position, Quaternion.identity, gameObject.transform);
        moveObj = gameObject.GetComponent<MoveObj>();
        moveObj.mSprite = obj.transform.GetChild(0).GetComponent<SpriteRenderer>();
        moveObj.mSprite2 = obj.transform.GetChild(1).GetComponent<SpriteRenderer>();
        moveObj.anim = obj.transform.GetComponent<Animator>();
        moveObj.localscale = idchange;
        //Debug.Log("LoadHangrao localscale: " + gameObject.tag + idchange);
        StartCoroutine(delayLoadOrder());
    }

    public void rotation()
    {
        if (gameObject.transform.childCount > 0)
            Destroy(gameObject.transform.GetChild(0).gameObject);
        if (idchange < objHangrao.Length - 1)
            idchange++;
        else idchange = 0;
        if (obj != null) Destroy(obj);
        obj = Instantiate(objHangrao[idchange], gameObject.transform.position, Quaternion.identity, gameObject.transform);
        moveObj.mSprite = obj.transform.GetChild(0).GetComponent<SpriteRenderer>();
        moveObj.mSprite2 = obj.transform.GetChild(1).GetComponent<SpriteRenderer>();
        moveObj.anim = obj.transform.GetComponent<Animator>();
        moveObj.localscale = idchange;
        moveObj.anim.SetBool("move", true);
        Debug.Log("rotation: " +gameObject.tag+ idchange);

        StartCoroutine(delaySetOrder());
    }

    IEnumerator delaySetOrder()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SetOder>().sortingLayer("move");
        gameObject.GetComponent<SetOder>().reloadOrder();
    }
    IEnumerator delayLoadOrder()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SetOder>().reloadOrder();
    }
}
