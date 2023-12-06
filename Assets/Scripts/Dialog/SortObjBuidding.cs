using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortObjBuidding : MonoBehaviour {

    [SerializeField] GameObject[] count;
    private void OnEnable()
    {
        GameObject[] countOrdered = count.OrderBy(go => go.GetComponent<UnlockItem>().levelUnlock).ToArray();
        for (int i = 0; i < countOrdered.Length; i++)
        {
            countOrdered[i].transform.SetSiblingIndex(i);
        }
    }
}
