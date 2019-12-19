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

    //觸發事件 : 碰到勾選 IsTrigger 物件會執行一次
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "陷阱")                          //如果 碰到,標籤 等於 "陷阱"
        {
            int d = other.GetComponent<Trap>().damage;    //取得元件<泛型>().成員
            hp -= d;                                      //血量 扣 10
            hpSlider.value = hp;                          //血量滑桿.值 = 血量
        }

        if (other.tag == "雞腿")
        {
            chickenCount++;
            textChicken.text = "CHICKEN : " + chickenCount + " / " + chickenTotal;
            Destroy(other.gameObject);
        }
        
        if (other.name == "終點" && chickenCount == chickenTotal)
        {
            print("過關");
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
        }
    }

    private void Start()
    {
        chickenTotal = GameObject.FindGameObjectsWithTag("雞腿").Length;
        textChicken.text = "CHICKEN : 0/ " + chickenTotal;
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
}
