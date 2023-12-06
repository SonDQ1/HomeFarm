using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOder : MonoBehaviour
{
    [SerializeField] int order;
    bool checkMove;
    void Start()
    {
        order = (int)(transform.position.y * (-10));

        if (checkMove)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -25);
        else
            if(gameObject.CompareTag("rac"))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, transform.position.y-1);
        else
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, transform.position.y);


        switch (gameObject.tag)
        {
            case "nha":
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = order+5;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order+4;
                break;
            case "nhamay":
            case "maythucan":
            case "maysua":
            case "lonuong":
            case "douong":
            case "thomay":
            case "nuongthit":
            case "daubep":
            case "thucong":
            case "nhakho":
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 3;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order + 2;
                break;
            case "chuong":
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 4;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order + 3;
                gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().sortingOrder = order+6;
                gameObject.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order+5;
                gameObject.transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order+5;
                gameObject.transform.GetChild(3).GetComponent<MeshRenderer>().sortingOrder = order+7;
                gameObject.transform.GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 6;
                gameObject.transform.GetChild(3).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order + 6;
                gameObject.transform.GetChild(4).GetComponent<MeshRenderer>().sortingOrder = order + 8;
                gameObject.transform.GetChild(4).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 7;
                gameObject.transform.GetChild(4).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order + 7;
                //if (gameObject.transform.childCount == 6)
                //{
                    gameObject.transform.GetChild(5).GetComponent<MeshRenderer>().sortingOrder = order + 9;
                    gameObject.transform.GetChild(5).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 8;
                    gameObject.transform.GetChild(5).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order + 8;
                //}
                break;

            case "cay":
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = order + 7;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 8;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order+6;
                break;
            case "trangtri":
            case "gieng":
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 6;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order + 5;
                break;
            case "hangraoInstan":
            case "roadInstan":
                gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 6;
                gameObject.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = order + 5;
                break;
            case "datkhoa":
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = order + 5;
                break;
            case "odat-khoa":
            case "odat":
            case "odat2":
            case "odat3":
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = order + 4;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 5;
                break;
            case "pets":
                gameObject.GetComponent< MeshRenderer>().sortingOrder = order + 10;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 9;
                break;
            case "rac":
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = order + 5;
                if(transform.childCount>0)
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order + 4;
                break;
        }
    }
    public void reloadOrder()
    {
        //Debug.Log("reloadOrder");
        Start();
    }
    public void sortingLayer(string nameLayer)
    {
        if (nameLayer == "move")
        {
            checkMove = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -25);
        }
        else
        {
            checkMove = false;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, transform.position.y);
        }
        switch (gameObject.tag)
        {
            case "nha":
                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                break;
            case "nhamay":
            case "maythucan":
            case "maysua":
            case "lonuong":
            case "douong":
            case "thomay":
            case "nuongthit":
            case "daubep":
            case "thucong":
            case "nhakho":
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                break;
            case "chuong":
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(3).GetComponent<MeshRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(3).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(4).GetComponent<MeshRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(4).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(4).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                //if (gameObject.transform.childCount == 6)
                //{
                    gameObject.transform.GetChild(5).GetComponent<MeshRenderer>().sortingLayerName = nameLayer;
                    gameObject.transform.GetChild(5).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                    gameObject.transform.GetChild(5).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                //}
                break;

            case "cay":
                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                break;
            case "trangtri":
            case "gieng":
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                break;
            case "hangraoInstan":
            case "roadInstan":
                //Debug.Log("sortingLayer: " + gameObject.name + "->" + nameLayer);
                gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                break;
            case "datkhoa":
                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                break;
            case "odat-khoa":
            case "odat":
            case "odat2":
            case "odat3":
                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                break;
            case "pets":
                gameObject.GetComponent<MeshRenderer>().sortingLayerName = nameLayer;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = nameLayer;
                break;
        }
    }
}
