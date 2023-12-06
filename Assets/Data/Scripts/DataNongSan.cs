using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNongSan : ScriptableObject {
    [System.Serializable]
    public struct NongSan
    {
        public string nameV, nameE;
        public int levelUnlock;
        public int timept;
        public int giamua;
        public int giaban;
        public int kcpt;
        public Sprite hinhanh;
        public Sprite iconMake;
        public Caytrong caytrong;
    }
    public NongSan[] data;
}
