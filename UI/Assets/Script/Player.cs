using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("血量與血條")]
    public int hp = 100;        //血量
    public Slider hpSlider;     //血量滑桿
    [Header("雞腿區域")]
    public Text textChicken;    //雞腿文字解面
    public int chickenCount;    //雞腿數量
    public int chickenTotal;    //雞腿總數
    [Header("時間區域")]
    public Text textTime;       //時間文字介面
    public float gameTime;      //時間
    [Header("結束畫布")]
    public GameObject final;
    public Text textBest;
    public Text textCurrent;

    //觸發事件 : 碰到勾選 IsTrigger 物件會執行一次
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "陷阱")                          //如果 碰到,標籤 等於 "陷阱"
        {
            int d = other.GetComponent<Trap>().damage;    //取得元件<泛型>().成員
            hp -= d;                                      //血量 扣 10
            hpSlider.value = hp;                          //血量滑桿.值 = 血量
            if (hp <= 0) Dead();
        }

        if (other.tag == "雞腿")
        {
            chickenCount++;
            textChicken.text = "CHICKEN : " + chickenCount + " / " + chickenTotal;
            Destroy(other.gameObject);
        }
        
        //如果 碰到.名稱 為 "終點" 並且 雞腿數量 等於 雞腿總數
        if (other.name == "終點" && chickenCount == chickenTotal)
        {
            GameOver();
        }
    }

    //粒子碰撞事件 : 碰到勾選 send collision Messages 粒子會執行一次
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "陷阱")  //如果 碰到,標籤 等於 "陷阱"
        {
            int d = other.GetComponent<Trap>().damage;    //取得元件<泛型>().成員
            hp -= d;                                      //血量 扣 10
            hpSlider.value = hp;                          //血量滑桿.值 = 血量
            if (hp <= 0) Dead();
        }
    }

    private void Start()
    {
        //預設最佳紀錄為 0 改為 99999 
        if (PlayerPrefs.GetFloat("最佳紀錄") == 0)
        {
            PlayerPrefs.SetFloat("最佳紀錄", 99999);
        }

        chickenTotal = GameObject.FindGameObjectsWithTag("雞腿").Length;         //雞腿總數 = 遊戲物件.透過標籤搜尋找負數物間("標籤名稱".數量)
        textChicken.text = "CHICKEN : 0/ " + chickenTotal;                       //雞腿文字介面.文字 = "CHICKEN : 0 / " + 雞腿總數
    }

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        gameTime += Time.deltaTime;
        textTime.text = gameTime.ToString("F1");
    }

    private void Dead()
    {
        final.SetActive(true);                                          //顯示結束畫布
        textCurrent.text = "TIME : " + gameTime.ToString("F1");         //顯示目前時間
        textBest.text = "BEST : " + PlayerPrefs.GetFloat("最佳紀錄").ToString("F1");        //顯示最佳時間
        Cursor.lockState = CursorLockMode.None;     //解除鎖定滑鼠

        GetComponent<FPSControllerLPFP.FpsControllerLPFP>().enabled = false;
        enabled = false;
    }

    private void GameOver()
    {
        final.SetActive(true);                                          //顯示結束畫布
        textCurrent.text = "TIME : " + gameTime.ToString("F1");         //顯示目前時間

        //目前時間 < 最佳時間 100 < 99999
        //最佳紀錄 = 目前時間

        if (gameTime < PlayerPrefs.GetFloat("最佳紀錄"))
        {
            PlayerPrefs.SetFloat("最佳紀錄", gameTime);
        }

        textBest.text = "BEST : " + PlayerPrefs.GetFloat("最佳紀錄").ToString("F1");        //顯示最佳時間

        Cursor.lockState = CursorLockMode.None;     //解除鎖定滑鼠
    }
}
