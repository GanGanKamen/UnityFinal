using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public enum eGamePhase {
        START_CAMERA,
        START_COUNTDOWN,
        GAME,
        GAMEOVER,
        RESULT,
    }

    public static eGamePhase GamePhase = eGamePhase.START_CAMERA;

    public Camera MainCamera;
    public GameObject PrefabCountDown;
    public PlayerDriver PlayerDriver;

    public UnityEngine.UI.Text textTime;
    public CheckPoint[] CheckPointList;

    bool isStarted = false;
    float startDateTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (textTime != null){
            if (isStarted){
                // 残りタイムを表示
                var time = Time.time - startDateTime;
                float seconds = (int)time;
                float milisec = (int)((time * 100.0f) % 100.0f);
                textTime.text = 
                    (seconds/60).ToString("00") + ":" +
                    (seconds%60).ToString("00") + ":" +
                    milisec.ToString("00");
            }
        }
    }

    IEnumerator Main()
    {
        // CAMERA DEMO
        PlayerDriver.enabled = false;
        GamePhase = eGamePhase.START_CAMERA;
        {
            var animCamera = MainCamera.GetComponent<Animator>();
            animCamera.enabled = true;
            while(true) {
                var info = animCamera.GetCurrentAnimatorStateInfo(0);
                // Animator のステートがEndになったら修了
                if (info.IsName("End")){
                    break;  //-> Loop を抜ける
                }
                yield return null;
            }
            animCamera.enabled = false;
        }

        // カウントダウン
        GamePhase = eGamePhase.START_COUNTDOWN;
        {
            var newObj = Instantiate(PrefabCountDown);
            CountDownText script = newObj.GetComponent<CountDownText>();
            script.Replay("3");
            yield return null;
            while(script.IsEnd() == false){
                yield return null;
            }
            script.Replay("2");
            yield return null;
            while(script.IsEnd() == false){
                yield return null;
            }
            script.Replay("1");
            yield return null;
            while(script.IsEnd() == false){
                yield return null;
            }
            script.Replay("GO!");
            yield return null;
            while(script.IsEnd() == false){
                yield return null;
            }
            Destroy(newObj);
        }

        // ゲーム開始！
        GamePhase = eGamePhase.GAME;
        PlayerDriver.enabled = true;
        PlayerDriver.SwitchView(0);  // Driver のビューを変更

        // タイマーを開始する
        isStarted = true;
        startDateTime = Time.time;

        // チェックポイントをまわったか？
        int max = CheckPointList.Length;
        int cnt;
        do {
            cnt = 0;
            foreach (var item in CheckPointList){
                if (item.IsPassed == true) cnt++;
            }
            yield return null;
        } while(cnt<max);

        // ゴール！
        isStarted = false;
        {
            var newObj = Instantiate(PrefabCountDown);
            CountDownText script = newObj.GetComponent<CountDownText>();
            script.Replay("GOAL");
            yield return null;
            while(script.IsEnd() == false){
                yield return null;
            }
        }

        yield return null;
    }

}
