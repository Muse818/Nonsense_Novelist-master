using TMPro;
using UnityEngine;
using System.Collections.Generic;

    public enum FloatWordType
    {
        physics = 0,
        psychic = 1,
        heal = 2,
        healMax = 3,
        removeWord = 4,
        getWord = 5,

    }


public class BattleFloatTextUI : BattleUI
{
    public BattleFloatTextUI(string context,Vector3 pos,float startTime, FloatWordType type,RectTransform uiText)
    { 
        mainCamera = Camera.main;
        mFloatTexts=new FloatTextInfo(context,pos,startTime,type,uiText);
    } 
    protected override void CreateUIPanel()
    {
        mUIPanel=ResMgr.GetInstance().Load<GameObject>("SecondStageLoad/floatWord");
        
    }
    public class FloatTextInfo
    {
        public string text;
        public Vector3 worldPosition;
        public float startTime;
        public RectTransform uiText;
        public FloatWordType type;

        public FloatTextInfo(string text, Vector3 worldPosition, float startTime, FloatWordType type,RectTransform uiText )
        {
            this.text = text;
            this.worldPosition = worldPosition;
            this.startTime = startTime;
            this.uiText = uiText;
            this.type = type;
        }
    }

    protected FloatTextInfo mFloatTexts;
    public FloatTextInfo floatTexts=>mFloatTexts;
    protected Camera mainCamera;
    protected string PrefabPath = "";
    protected float displayDuration = 3.0f;

  /*

   
    public void AddFloatText(string text, Vector3 worldPosition,FloatWordType type)
    {
        TextMeshProUGUI newText = CreateTextObject();
        newText.text = text;
        newText.color = SetColor(type);
        newText.fontSize = 24;
        floatTexts.Add(new FloatTextInfo(text, worldPosition, Time.time, type,newText.rectTransform));
    }

  
    public override void Update(float deltaTime)
    {
        float currentTime = deltaTime;

        for (int i = floatTexts.Count - 1; i >= 0; i--)
        {
            FloatTextInfo floatText = floatTexts[i];

            if (currentTime - floatText.startTime > displayDuration)
            {
                PoolMgr.GetInstance().PushObj(PrefabPath, floatText.uiText.gameObject);
                floatTexts.RemoveAt(i);
                continue;
            }

            UpdateFloatTextPosition(floatText);
        }
    }

    protected void UpdateFloatTextPosition(FloatTextInfo floatText)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(floatText.worldPosition);
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            floatText.uiText.parent as RectTransform,
            screenPosition,
            mainCamera,
            out localPosition
        );
        floatText.uiText.localPosition = localPosition + new Vector2(0, (Time.time - floatText.startTime) * 20); 
    }


    private TextMeshProUGUI CreateTextObject()
    {
        var obj=PoolMgr.GetInstance().GetObj(PrefabPath);
        TextMeshProUGUI tmp;
        if (!obj.TryGetComponent<TextMeshProUGUI>(out tmp)) return null;


       



        return tmp;
    }



    protected virtual void OnRegistered() { }

    protected virtual void OnEnabled() { }

    protected virtual void OnDisabled() { }

    public override void Init()
    {
        
    }


    public override void LateUpdate(float deltaTime) { }

   

    protected Color SetColor(FloatWordType _style)

    {
        switch (_style)
        {
            case FloatWordType.physics:
                {
                    return Color.white;

                }
            case FloatWordType.psychic:
                {
                    return Color.blue;

                }
            case FloatWordType.heal:
                {
                    return Color.green;

                }
            case FloatWordType.healMax:
                {
                    return (Color.green + Color.white * 0.4f);

                }
            case FloatWordType.removeWord:
                {
                    return Color.grey;

                }
            case FloatWordType.getWord:
                {
                    return Color.yellow;

                }
        }
        return Color.white;
    }
    */
}
