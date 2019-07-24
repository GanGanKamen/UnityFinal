using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDriver : Driver
{
    const float ENGINE_TORQU = 0.7f;
    const float STEERING = 0.5f;
    const float DISTANCE_RANGE = 5.0f;

    public GameMain gameMain;       //<- CheckPoint の配列を取得するため
    public int indexCheckPoint;     //<- 配列から何番目のを取得するか？
    public Vector3 AimCross;        // モニター 外積
    public float AimDistance;       // モニター 距離

    // Start is called before the first frame update
    void Start()
    {
        indexCheckPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // myCar が関連づいているか？
        if (myCar == null ||
        // ゲームが開始されたか？
            GameMain.GamePhase != GameMain.eGamePhase.GAME){
            if (myCar != null){
                myCar.EngineTorqu = 0.0f;
                myCar.BreakTorqu = 1.0f;
            }
            return;
        }

        // 向かうチェックポイントを取得する
        CheckPoint aimPoint = 
            gameMain.CheckPointList[indexCheckPoint];

        // 目的に到達したか？を判断
        AimDistance = 
            Vector3.Distance(
                myCar.transform.position, 
                aimPoint.transform.position);
                
        if (AimDistance <= DISTANCE_RANGE){
            // 次のチェックポイントへ切り替え
            indexCheckPoint++;
            if (indexCheckPoint >= gameMain.CheckPointList.Length){
                // 最後までいったら最初から
                indexCheckPoint = 0;
            }
        }

        // アクセルを踏む
        myCar.EngineTorqu = ENGINE_TORQU;
        myCar.BreakTorqu = 0.0f;

        // ステアリングを操作
        Vector3 forward = myCar.transform.forward;
        Vector3 direction = 
            aimPoint.transform.position - myCar.transform.position;
        AimCross = Vector3.Cross(forward, direction).normalized;
        myCar.Steering = AimCross.y;
//        myCar.Steering = (AimCross.y > 0) ? STEERING : -STEERING;

    }
}
