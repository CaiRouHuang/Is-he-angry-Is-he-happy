using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 請確保引入 UnityEngine.UI 命名空間
using UnityEngine.SceneManagement;

public class ImageChanger : MonoBehaviour
{
    public Image emotionImage;
    public Sprite noneSprite;
    public Sprite happy1Sprite;
    public Sprite happy2Sprite;
    public Sprite angry1Sprite;
    public Sprite angry2Sprite;



    void Start()
    {
        emotionImage.sprite = noneSprite;
    }

    // Update is called once per frame
    void Update()
    {
        // 在每一幀更新時獲取最新的 joy 和 angry 值
        float joy = wordsenter2.joyValue;
        float angry = wordsenter2.angryValue;

        if (SceneManager.GetActiveScene().name == "1")
        {
            // 在 Scene1 中的邏輯
           
            ImageChange_scene1(joy,angry);
        }
        else if (SceneManager.GetActiveScene().name == "2")
        {
            // 在 Scene2 中的邏輯
            ImageChange_scene2(joy,angry);
        }
    }

    
    public void ImageChange_scene1(float joy, float angry)
    {
        if (angry < 5 && angry >= 0 && angry >= joy)
        {
            emotionImage.sprite = noneSprite;
             Debug.Log("none");
        }
        else if (angry < 8 && angry >= 5 && angry > joy)
        {
            emotionImage.sprite = angry1Sprite;
            Debug.Log("A");
        }
        else if (angry >= 8 && angry > joy)
        {
            emotionImage.sprite = angry2Sprite;
            Debug.Log("b");
        }

    }

        public void ImageChange_scene2(float joy, float angry)
    {
        if (joy < 5 && joy >= 0 && joy >= angry)
        {
            emotionImage.sprite = noneSprite;
        }
        else if (joy < 8 && joy >= 5 && joy > angry)
        {
            emotionImage.sprite = happy1Sprite;
        }
        else if (joy >= 8 && joy > angry)
        {
            emotionImage.sprite = happy2Sprite;
        }

    }

    

}
