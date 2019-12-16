using UnityEngine;
using UnityEngine.UI;

public class PauseControl: MonoBehaviour
{
    [Header("暫停介面")]
    public Image imagePause;
    public Sprite spritePause, spritePlay;
    [Header("暫停")]
    public bool pause;

    /// <summary>
    /// 暫停方法
    /// </summary>
    private void Pause()
    {
        //如果 按下 ESC 就執行{}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("暫停~"); 
            pause = !pause;         // ! 相反 : 將布林值改為相反

            //條件運算子
            //布林值 ? 結果一 : 結果二
            //布林值 true 會執行解果一
            //布林值 salse 會執行解果二

            //暫停介面.圖片 = 暫停布林值 ? 打勾時換開始圖片 : 取消時換暫停圖片;
            imagePause.sprite = pause ? spritePlay : spritePause;
            //時間大小 = 暫停布林值 ? 打勾時暫停 0 :取消時開始 1;
            Time.timeScale = pause ? 0 : 1;
        }
    }
    
    //更新:一秒直行約60次
    private void Update()
    {
        Pause();
    }
}
