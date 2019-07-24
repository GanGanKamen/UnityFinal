using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    [Range (-1.0f, +1.0f)]
    public float Steering = 0.0f;

    [Range (-1.0f, 1.0f)]
    public float EngineTorqu = 0.0f;

    public float BreakTorqu = 0.0f;

    public float MaxAngle = 30.0f;

    public float MaxTorqu = 100.0f;

    public float MaxBreakTorqu = 200.0f;

    // カメラの注視点
    public GameObject CameraLookAt;

    // カメラの位置の配列
    public GameObject [] CameraPositions;


    /// <summary>
    /// 駆動タイプ
    /// </summary>
    public enum eDriveType {
        [Tooltip ("前輪駆動")]
        Front,
        [Tooltip ("後輪駆動")]
        Rear,
        [Tooltip ("四輪駆動")]
        FrontRear,
    };

    public eDriveType DriveType;

    public WheelCollider [] FrontWheel;
    public WheelCollider [] RearWheel;

    // Use this for initialization
    void Start ()
    {

    }

    void Update ()
    {
        if (FrontWheel == null || FrontWheel.Length < 2
             || RearWheel == null || RearWheel.Length < 2) {
            // タイヤが参照されていないときは何もしない
            return;
        }

        // ハンドルを切る
        float angleWheel = MaxAngle * Steering;
        foreach (var wheel in FrontWheel) {
            wheel.steerAngle = angleWheel;
            // タイヤの角度を変える
            var rotate = wheel.transform.localRotation;
            rotate = Quaternion.AngleAxis (angleWheel, Vector3.up);
            wheel.transform.localRotation = rotate;
        }

        // タイヤを駆動させる
        float torqu = MaxTorqu * EngineTorqu;
        switch (DriveType) {
            case eDriveType.Front:
                foreach (var wheel in FrontWheel) {
                    wheel.motorTorque = torqu;
                }
                break;
            case eDriveType.Rear:
                foreach (var wheel in RearWheel) {
                    wheel.motorTorque = torqu;
                }
                break;
            case eDriveType.FrontRear:
                foreach (var wheel in FrontWheel) {
                    wheel.motorTorque = torqu;
                }
                foreach (var wheel in RearWheel) {
                    wheel.motorTorque = torqu;
                }
                break;
        }

        // ブレーキ
        float breakTorqu = MaxBreakTorqu * BreakTorqu;

        // 前後のタイヤにブレーキ
        foreach (var wheel in FrontWheel) {
            wheel.brakeTorque = breakTorqu;
        }
        foreach (var wheel in RearWheel) {
            wheel.brakeTorque = breakTorqu;
        }
    }
}
