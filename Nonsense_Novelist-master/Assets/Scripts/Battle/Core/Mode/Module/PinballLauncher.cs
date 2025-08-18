using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;



public class PinBallLauncher : BattleModule
{
    public bool canShoot => mCanShoot;
    protected bool mCanShoot;
    public Vector3 launchPoint;
    private float currentChargeTime = 0f;
    private float chargeStartTime = 0f;
    private bool isCharging = false;
    List<Vector3> pathPoints = new List<Vector3>();
    private float trajectoryUpdateTime = 0.05f; // 设置轨迹更新的时间间隔
    private float lastTrajectoryUpdateTime = 0f;

    public PinBall nowBall;

    public LineRenderer trajectoryLine;


    public void CanShootSwitch(bool _canShoot)
    {
        mCanShoot = _canShoot;
    }

    /// <summary>
    /// 发射小球
    /// </summary>
    public void LaunchPinBall()
    {
        CreateNewBallObj();

    }


    void CreateNewBallObj()
    {

        PoolMgr.GetInstance().GetSOObj(prefabSOType.BattleObj, 0, (obj) =>
        {
            obj.transform.position = launchPoint;
            obj.transform.rotation = Quaternion.identity;

            nowBall.mBall.transform = obj.transform;
            SetNowBallVel();
            nowBall.ShootOut();
            nowBall = null;

            Battle.CardDeckManager.UseCurrentCard();
            CreateNewBall();
        });

    }

    void CreateNewBall()
    {
        WordTable.Data data = Battle.CardDeckManager.GetCurrentCard();
        PinBall pinball = Battle.ObjectFactory.CreatePinBall(data);
        GameObject temp = new GameObject();
        temp.transform.position = launchPoint;
        pinball.mBall.transform = temp.transform;
        pinball.mBall.wordData = data;
        pinball.mBall.velocity = Vector3.zero;
        pinball.mBall.radius = BattleConfig.mData.word.wordBallRadius;
        pinball.mBall.friction = BattleConfig.mData.word.wordBallFriction;
        pinball.mBall.energyLoss = BattleConfig.mData.word.wordBallCollisionLoss;
        pinball.mBall.hasShoot = false;
        nowBall = pinball;
    }

    void SetNowBallVel()
    {
        if (!nowBall.IsValid())
            return;
        
        nowBall.mBall.velocity = nowBall.mBall.preVelocity = CalculateLaunchVelocity();
    }

    private Vector2 CalculateLaunchVelocity()
    {
        float speed = Mathf.Lerp(
            BattleConfig.mData.word.wordBallMinSpeed,
            BattleConfig.mData.word.wordBallMaxSpeed,
            currentChargeTime / BattleConfig.mData.word.wordBallChargingMaxTime
        );

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector2 direction = (mouseWorldPosition - launchPoint).normalized;

        // 新增角度限制逻辑
        float maxAngle = BattleConfig.mData.word.maxAngle;
        float signedAngle = Vector2.SignedAngle(Vector2.up, direction);

        if (Mathf.Abs(signedAngle) > maxAngle)
        {
            float clampedAngle = Mathf.Clamp(signedAngle, -maxAngle, maxAngle);
            direction = Quaternion.Euler(0, 0, clampedAngle) * Vector2.up;
        }

        return direction * speed;
    }

    public override void LateUpdate(float deltaSec)
    {

    }

    bool isAngleValid = false;

    public override void Update(float deltaSec)
    {
        if (Input.GetMouseButton(0) && mCanShoot)
        {
            if (!isCharging)
            {
                CreateNewBall();
                isCharging = true;
                chargeStartTime = 0;
            }

            chargeStartTime += deltaSec;
            currentChargeTime = Mathf.Min(chargeStartTime, BattleConfig.mData.word.wordBallChargingMaxTime);

            lastTrajectoryUpdateTime += deltaSec;

            SetNowBallVel();
            if (lastTrajectoryUpdateTime >= trajectoryUpdateTime)
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPosition.z = 0;
                Vector2 direction = (mouseWorldPosition - launchPoint).normalized;
                float angle = Mathf.Abs(Vector2.Angle(direction, Vector2.up)); // 计算鼠标方向与Vector2.up的夹角

                if (angle > BattleConfig.mData.word.maxAngle)
                {
                    if (isAngleValid) // 如果之前的角度有效，保留当前的轨迹
                    {
                        isAngleValid = false;
                        // 保持轨迹不更新，保持之前的位置和轨迹
                        return;
                    }
                }
                else
                {
                    // 如果夹角在限制范围内，更新轨迹
                    isAngleValid = true;

                    lastTrajectoryUpdateTime = 0f;
                    ShowTrajectory(); // 定期更新轨迹

                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && mCanShoot)
        {
            if (isCharging)
            {
                LaunchPinBall();
                currentChargeTime = 0f;
                isCharging = false;

            }
        }
    }

    public void ShowTrajectory()
    {
        var preInfo = nowBall.mPreInfo;

        pathPoints.Clear();
        pathPoints.Add(launchPoint); // 起点
        for (int i = 0; i < preInfo.colPos.Count; i++)
        {
            pathPoints.Add(preInfo.colPos[i]);
        }

        if (nowBall.mBall.preTransform != null)
            pathPoints.Add(nowBall.mBall.preTransform.position); // 射线的延伸终点


        trajectoryLine.startWidth = BattleConfig.mData.word.wordBallRadius;
        trajectoryLine.endWidth = BattleConfig.mData.word.wordBallRadius;
        trajectoryLine.positionCount = Mathf.Min(pathPoints.Count, BattleConfig.mData.word.maxPreColTimes);

        for (int i = 0; i < Mathf.Min(pathPoints.Count, BattleConfig.mData.word.maxPreColTimes); i++)
        {
            trajectoryLine.SetPosition(i, pathPoints[i]);
        }

    }

    public override void Init()
    {
        base.Init();

        GameObject.Find("bulletroot").TryGetComponent<LineRenderer>(out trajectoryLine);
        launchPoint = GameObject.Find("bulletroot").transform.position;
    }
}
