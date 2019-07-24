using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownText : MonoBehaviour
{
    public UnityEngine.UI.Text textCountDown;
    Animator animText;

    // Start is called before the first frame update
    void Awake(){
        animText = textCountDown.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool IsEnd()
    {
        var info = animText.GetCurrentAnimatorStateInfo(0);
        return info.IsName("End");
    }

    public void Replay(string text)
    {
        //　テキスト差し替え
        textCountDown.text = text;
        // アニメーションをリプレイ
        animText.enabled = false;
        animText.Play("countdown_anim", 0, 0.0f);
        animText.enabled = true;
    }

}
