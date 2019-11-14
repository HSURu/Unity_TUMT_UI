using UnityEngine;
using UnityEngine.Audio;    //引用 音頻 API
using UnityEngine.UI;       //引用 介面 API
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // 定義欄位 (宣告變數)
    //修飾詞 類型 名稱 結尾
    //public 公開、private 私人
    public AudioMixer mixer;
    public Text loadingText;    //文字
    public Slider loading;      //滑桿

    //定義方法(宣告函式)
    //修飾詞 類型 名稱 (參數) {敘述}
    public void SetVBGM(float value)
    {
        //音效管理器.設定浮點數("名稱" ，值);
        mixer.SetFloat("VBGM", value);
    }

    public void SetVSFX(float value)
    {
        mixer.SetFloat("VSFX", value);
    }


    public void Play()
    {
        //SceneManager.LoadScene("場景");
        StartCoroutine(Loading());
    }

    // 協同程序 Coroutine
    private IEnumerator Loading()
    {
        //print(" 測試 1 ");
        //yield return new WaitForSeconds(1);        //等待秒數(秒數);
        //print(" 測試 2 ");

        AsyncOperation ao = SceneManager.LoadSceneAsync("場景");    //取得場景資訊
        ao.allowSceneActivation = false;                            //取消載入場景

        //當 (場景仔入 == 未完成)
        while (ao.isDone == false)
        {
            // 更新介面並等待
            loadingText.text = ((ao.progress / 0.9f) * 100).ToString();
            loading.value = ao.progress / 0.9f;
            yield return new WaitForSeconds(0.0001f);

            //如果 載入進度 == 0.9 並且 按下任意鍵
            if (ao.progress == 0.9f  && Input.anyKey)
            {
                ao.allowSceneActivation = true;
            }
        }
    }
}


