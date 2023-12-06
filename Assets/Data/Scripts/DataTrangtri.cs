using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTrangtri : ScriptableObject {
    [System.Serializable]
    public struct Trangtri
    {
        public string nameV, nameE;
        public int giamua;
        public int giakc;
        public Sprite hinhanh;
    }
    public Trangtri[] data;
}
