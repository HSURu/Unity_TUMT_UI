using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp = 100;        //血量
    public Slider hpSlider;     //血量滑桿

    //觸發事件 : 碰到勾選 IsTrigger 物件會執行一次
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "陷阱")  //如果 碰到,標籤 等於 "陷阱"
        {
            int d = other.GetComponent<Trap>().damage;  //取得元件<泛型>().成員
            hp -= d;                                    //血量 扣 10
            hpSlider.value = hp;                        //血量滑桿.值 = 血量
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "陷阱")  //如果 碰到,標籤 等於 "陷阱"
        {
            int d = other.GetComponent<Trap>().damage;  //取得元件<泛型>().成員
            hp -= d;                                    //血量 扣 10
            hpSlider.value = hp;                        //血量滑桿.值 = 血量
        }
    }
}
