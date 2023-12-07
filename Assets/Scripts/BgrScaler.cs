using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgrScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Vector3 vtScaler = transform.lossyScale;

        //tinh bien
        float hight_b = sp.bounds.size.y;
        float with_b = sp.bounds.size.x;

        //can chinh anh full man hinh
        float hight = Camera.main.orthographicSize * 2f;
        float with = hight * Screen.width / Screen.height;

        //transform.localScale = new Vector3(with, hight, 0); => voiw 3d
        vtScaler.y = hight / hight_b;
        vtScaler.x = with / with_b;
        transform.localScale = vtScaler;

    }

}
