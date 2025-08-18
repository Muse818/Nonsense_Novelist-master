using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;



public class PinBallLauncher : BattleModule
{
    protected static int mMaxSimTimes = 60;
    
    protected bool mCanShoot;
    public Vector3 mLaunchPoint;
    public float mCurrentChargeTime = 0f;
    protected List<Vector3> mPathPoints = new List<Vector3>();
    protected float mTrajectoryUpdateTime = 0.05f; // 设置轨迹更新的时间间隔
    protected float mLastTrajectoryUpdateTime = 0f;
    protected bool mIsAngleValid = false;
    
    public enum State
    {
        Idle,
        PreCharge,
        Charging,
    }

    protected State mChargeState = State.Idle;
    public bool IsCharging => mChargeState == State.Charging;
    
    public bool CanShoot
    {
        get
        {
            if (!mCanShoot)
                return false;
            
            return Battle.CardDeckManager.HasLoadCard();
        }
    }

    public PinBall mNowBall;

    public LineRenderer mTrajectoryLine;


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
            obj.transform.position = mLaunchPoint;
            obj.transform.rotation = Quaternion.identity;

            mNowBall.mBall.mTransform = obj.transform;
            mNowBall.ShootOut();
            mNowBall = null;

            Battle.CardDeckManager.UseCurrentCard();
        });

    }

    void CreateNewBall()
    {
        WordTable.Data data = Battle.CardDeckManager.GetCurrentCard();
        PinBall pinball = Battle.ObjectFactory.CreatePinBall(data);
        
        GameObject empty = new GameObject
        {
            transform =
            {
                position = mLaunchPoint,
                rotation = Quaternion.identity
            }
        };
        
        pinball.mBall.mTransform = empty.transform;
        pinball.IsTickEnable = false;

        mNowBall = pinball;
    }

    private float CalculateCurrentAngle()
    {
        // 计算鼠标方向与Vector2.up的夹角
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector2 direction = (mouseWorldPosition - mLaunchPoint).normalized;
        return Vector2.SignedAngle(Vector2.up, direction);
    }

    private Vector2 CalculateLaunchVelocity(float chargeTime, float angle)
    {
        float speed = Mathf.Lerp(
            BattleConfig.mData.word.wordBallMinSpeed,
            BattleConfig.mData.word.wordBallMaxSpeed,
            chargeTime / BattleConfig.mData.word.wordBallChargingMaxTime
        );
        
        // 新增角度限制逻辑
        float maxAngle = BattleConfig.mData.word.maxAngle;
        float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
        var direction = Quaternion.Euler(0, 0, clampedAngle) * Vector2.up;
        
        return direction * speed;
    }

    public override void LateUpdate(float deltaSec)
    {

    }

    public override void Update(float deltaSec)
    {
        if (!CanShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Can not shot");
            }
        }
        else
        {
            if (mNowBall == null)
            {
                CreateNewBall();
            }

            if (mChargeState == State.Idle)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mIsAngleValid = true;
                    mCurrentChargeTime = 0;
                    mChargeState = State.PreCharge;
                    mTrajectoryLine.enabled = true;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    mCurrentChargeTime += deltaSec;
                        
                    mNowBall.mBall.mShootAngle = CalculateCurrentAngle();
                    mNowBall.mBall.mVelocity = CalculateLaunchVelocity(mCurrentChargeTime, mNowBall.mBall.mShootAngle);

                    if (mChargeState == State.PreCharge)
                    {
                        if (mCurrentChargeTime >= BattleConfig.mData.word.wordBallChargingMinTime)
                        {
                            mChargeState = State.Charging;
                        }
                    }
                    
                    mLastTrajectoryUpdateTime += deltaSec;
                    if (mLastTrajectoryUpdateTime >= mTrajectoryUpdateTime)
                    {
                        mLastTrajectoryUpdateTime -= mTrajectoryUpdateTime;
                            
                        float angle = Mathf.Abs(mNowBall.mBall.mShootAngle);
                        if (angle > BattleConfig.mData.word.maxAngle)
                        {
                            if (mIsAngleValid)
                            {
                                mIsAngleValid = false;
                                ShowTrajectory();
                            }
                        }
                        else
                        {
                            mIsAngleValid = true;
                            ShowTrajectory();
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (mChargeState == State.Charging)
                    {
                        LaunchPinBall();
                        mTrajectoryLine.enabled = false;
                    }
                    
                    mCurrentChargeTime = 0f;
                    mChargeState = State.Idle;
                }
            }
        }
    }

    public override void LateFixedUpdate(float deltaSec)
    {
        switch (mChargeState)
        {
            case State.PreCharge:
            case State.Charging:
                mNowBall.TickSimulation(mMaxSimTimes, deltaSec);
                break;
            
            default:
                break;
        }
    }

    public void ShowTrajectory()
    {
        var simulateInfo = mNowBall.mSimulateInfo;

        mPathPoints.Clear();
        mPathPoints.Add(mLaunchPoint); // 起点
        
        for (int i = 0; i < simulateInfo.colPos.Count; i++)
        {
            mPathPoints.Add(simulateInfo.colPos[i]);
        }


        mTrajectoryLine.startWidth = BattleConfig.mData.word.wordBallRadius;
        mTrajectoryLine.endWidth = BattleConfig.mData.word.wordBallRadius;
        mTrajectoryLine.positionCount = Mathf.Min(mPathPoints.Count, BattleConfig.mData.word.maxPreColTimes);

        for (int i = 0; i < Mathf.Min(mPathPoints.Count, BattleConfig.mData.word.maxPreColTimes); i++)
        {
            mTrajectoryLine.SetPosition(i, mPathPoints[i]);
        }

    }

    public override void Init()
    {
        base.Init();

        GameObject.Find("bulletroot").TryGetComponent<LineRenderer>(out mTrajectoryLine);
        mLaunchPoint = GameObject.Find("bulletroot").transform.position;
    }
}
