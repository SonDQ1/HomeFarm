//using UnityEngine;
//using System.Collections;
//using GoogleMobileAds.Api;
//using UnityEngine.UI;

//public class MobileRewardVideoAd : MonoBehaviour
//{
//    public static MobileRewardVideoAd instance;
//    //ca-app-pub-3940256099942544/5224354917    //video test
//    //ca-app-pub-8290419972929222/6085913859
//    string IdRewardedVideoAndroid = "ca-app-pub-8290419972929222/6085913859";
//    string IdRewardedVideoIOs;
//    private RewardBasedVideoAd rewardBasedVideoAd;
//    int MaxWatch = 10;
//    private int numberWatchDay
//    {
//        get
//        {
//            if (!PlayerPrefs.HasKey("NumberWatchDay")) { PlayerPrefs.SetInt("NumberWatchDay", MaxWatch); PlayerPrefs.SetInt("DayNow", System.DateTime.Now.Day); }
//            else if (PlayerPrefs.HasKey("NumberWatchDay")) { if (PlayerPrefs.GetInt("DayNow") != System.DateTime.Now.Day) { PlayerPrefs.SetInt("NumberWatchDay", MaxWatch); PlayerPrefs.SetInt("DayNow", System.DateTime.Now.Day); } }
//            return PlayerPrefs.GetInt("NumberWatchDay");
//        }
//        set { PlayerPrefs.SetInt("NumberWatchDay", value); }
//    }
//    [SerializeField] GameObject Question;
//    [SerializeField] GameObject Congretulate;
//    [SerializeField] RewardVideo rewardVideo;
//    void Awake()
//    {
//        if (instance == null) instance = this;
//        else if (instance != this) Destroy(gameObject);

//        rewardBasedVideoAd = RewardBasedVideoAd.Instance;
//        if (numberWatchDay > 0) RequestRewardedVideo();
//    }
//    // Use this for initialization
//    void Start()
//    {

//    }

//    void RequestRewardedVideo()
//    {
//#if UNITY_ANDROID
//        string adUnitId = IdRewardedVideoAndroid;
//#elif UNITY_IPHONE
//        string adUnitId = IdRewardedVideoIOs;
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        AdRequest request = new AdRequest.Builder().Build();
//        rewardBasedVideoAd.LoadAd(request, adUnitId);
//        rewardBasedVideoAd.OnAdLoaded += OnAdLoaded;
//        rewardBasedVideoAd.OnAdFailedToLoad += OnAdFailedToLoad;
//        rewardBasedVideoAd.OnAdLoaded += OnAdLoaded;
//        rewardBasedVideoAd.OnAdStarted += OnAdStarted;
//        rewardBasedVideoAd.OnAdClosed += OnAdClosed;
//        rewardBasedVideoAd.OnAdRewarded += OnAdRewarded;

//    }

//    void OnAdLoaded(object sender, System.EventArgs eventArgs)
//    {

//    }

//    void OnAdFailedToLoad(object sender, System.EventArgs eventArgs)
//    {

//    }

//    void OnAdStarted(object sender, System.EventArgs eventArgs)
//    {

//    }

//    void OnAdClosed(object sender, System.EventArgs eventArgs)
//    {
//        rewardBasedVideoAd.OnAdLoaded -= OnAdLoaded;
//        rewardBasedVideoAd.OnAdFailedToLoad -= OnAdFailedToLoad;
//        rewardBasedVideoAd.OnAdLoaded -= OnAdLoaded;
//        rewardBasedVideoAd.OnAdStarted -= OnAdStarted;
//        rewardBasedVideoAd.OnAdClosed -= OnAdClosed;
//        rewardBasedVideoAd.OnAdRewarded -= OnAdRewarded;
//        if (numberWatchDay >= 0) RequestRewardedVideo();
//    }

//    void OnAdRewarded(object sender, System.EventArgs eventArgs)
//    {
//        numberWatchDay -= 1;
//        ShowReward();
//    }

//    public void ButtonGift(int id)
//    {
//        idBtn = id;
//        string str = Until.checkLanguage() ? "Bạn đã xem hết lượng trong ngày" : "You have finished viewing the day";
//        if (numberWatchDay > 0)
//        {
//            if (rewardBasedVideoAd.IsLoaded() == true)
//                Question.SetActive(true);
//            else
//            {
//                str = Until.checkLanguage() ? "Mời bạn thử lại lần khác, chúc bạn may mắn!" : "Please try again later, good luck!";
//                GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
//                    str, true);
//            }
//        }
//        else
//        {
//            GameManagerControll.instance.showObjText(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)),
//                    str, true);
//        }
//    }

//    public void ButtonYes()
//    {
//        Question.SetActive(false);
//        rewardBasedVideoAd.Show();
//    }

//    public void ButtonNo()
//    {
//        Question.SetActive(false);
//    }

//    Sprite icon;
//    int cgd = 0;
//    int idBtn = 0;
//    private void ShowReward()
//    {
//        Congretulate.SetActive(true);
//        Congretulate.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = icon;
//        Congretulate.transform.GetChild(0).GetChild(3).GetComponent<Image>().SetNativeSize();
//        Congretulate.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text = cgd+"";
//    }


//    public void ButtonConfirmReward()
//    {
//        Congretulate.SetActive(false);
//        Until.showPanel = false;
//        switch (idBtn)
//        {
//            case 0: PlayerPrefsController.instance.addExp(cgd); break;
//            case 1: PlayerPrefsController.instance.addGem(cgd); break;
//            case 2: PlayerPrefsController.instance.addCoin(cgd); break;
//            case 3: PlayerPrefsController.instance.addDiamod(cgd); break;
//        }
//        if (rewardVideo != null) rewardVideo.OnEnable();
//    }

//    public void setDialogConguration(int idbtn,Sprite icon, int exp, int gem, int coint, int diamod)
//    {
//        this.icon = icon;
//        idBtn = idbtn;
//        Debug.Log("setDialogConguration: " + exp + "-" + gem + "-" + coint + "-" + diamod);
//        switch (idbtn)
//        {
//            case 0: cgd = exp; break;
//            case 1: cgd = gem; break;
//            case 2: cgd = coint; break;
//            case 3: cgd = diamod; break;
//        }
//    }
//}
