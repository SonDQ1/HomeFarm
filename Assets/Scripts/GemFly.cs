using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GemFly : MonoBehaviour
{
    [SerializeField] int numberPointsGem;
    private int indexArrayGem;
    [SerializeField] float speed;
    [SerializeField] bool isRun;
    [SerializeField] GameObject objGem;
    [SerializeField] Transform pointerBottom;
    public Transform pointerGem;
    [SerializeField] Vector3[] positionGem;
    [SerializeField] Vector3 PoiterGem;

    public bool checkVP;
    int order;
    void OnEnable()
    {
        if (!checkVP)
        {
            switch (gameObject.tag)
            {
                case "exp":
                    pointerGem = GameManagerControll.instance.posExp;
                    break;
                case "itemvp":
                    pointerGem = GameManagerControll.instance.posKho;
                    break;
            }
        }
    }

    void Start()
    {
        positionGem = new Vector3[numberPointsGem];
        DrawQuadraticCurveExp();
        StartCoroutine(waitRun());
        //order = (int)(transform.position.y * (-10));
        //objGem.GetComponent<SpriteRenderer>().sortingOrder = order + 5;
    }
    //set layout 
    void setLayoutObj(string namelayer)
    {
        if (namelayer == "Canvas")
        {
            objGem.GetComponent<SpriteRenderer>().sortingLayerName = "Canvas";
            objGem.GetComponent<SpriteRenderer>().sortingOrder = 17;
        }
    }
    //cho obj nhỏ lại
    float scale = 1;
    void setScaleObj()
    {
        StartCoroutine(scaleObj());
    }

    IEnumerator scaleObj()
    {
        objGem.transform.localScale = new Vector2(scale, scale);
        yield return new WaitForSeconds(0.2f);
        scale-= 0.3f;
        StartCoroutine(scaleObj());
    }
    //=================================
    void DrawQuadraticCurveExp()
    {
        for (int i = 1; i < numberPointsGem + 1; i++)
        {
            float t = i / (float)numberPointsGem;
            positionGem[i - 1] = CalculateQuadraticBezierPoint(t, transform.position, pointerBottom.position, pointerGem.position);
        }
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        p.z = 0;
        return p;
    }

    IEnumerator upSpeed()
    {
        yield return new WaitForSeconds(0.2f);
        speed += 3f;
        StartCoroutine(upSpeed());
    }

    IEnumerator waitRun()
    {
        yield return new WaitForSeconds(0.5f);
        isRun = true;
        StartCoroutine(upSpeed());
    }
    void ExpFly()
    {
        DrawQuadraticCurveExp();
        if (Vector3.Distance(objGem.transform.position, positionGem[indexArrayGem]) < 0.1f)
        {
            if (indexArrayGem < positionGem.Length - 1) indexArrayGem = indexArrayGem + 1;
        }
        //set layout in canvas
        if (gameObject.CompareTag("exp") && indexArrayGem == numberPointsGem - 5)
            setLayoutObj("Canvas");
        if (indexArrayGem == numberPointsGem - 6)
            setScaleObj();
        if (indexArrayGem == numberPointsGem - 1)
        {
            if (Vector3.Distance(objGem.transform.position, positionGem[numberPointsGem - 1]) < 0.01f)
            {
                //Gem.instance.ReciveGem(numberGem);
                if(gameObject.CompareTag("itemvp") && !checkVP)
                    GameManagerControll.instance.playAnimNhakho();
                Destroy(gameObject);
            }
        }
        objGem.transform.position = Vector3.MoveTowards(objGem.transform.position, positionGem[indexArrayGem], Time.deltaTime * speed);

    }

    void Update()
    {
        if (isRun == true) ExpFly();
    }
}
