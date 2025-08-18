using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

///<summary>
///技能词条的圆圈加载显示
///</summary>
class SkillsLoadingCircle : MonoBehaviour
{
    /// <summary>动词圆圈预制体 </summary>
    public GameObject verbLoadingPrefab;    
    /// <summary>角色头顶动词圆圈的父物体位置 </summary>
    private Transform[] verbLoadingPoints=new Transform[4];
    /// <summary>剩余加载时间数值 </summary>
    private List<Image> verbCD = new List<Image>();
    /// <summary>角色位置 </summary>
    private List<GameObject> skillUIbar = new List<GameObject>();
    /// <summary>获取该角色 </summary>
    private AbstractCharacter charaComponent;
    [Tooltip("手动设置，不设的话默认HP")]
    /// <summary>条位置 </summary>
    public Transform barPoint;
    //
    private int count;

    public void Start()
    {
        charaComponent = gameObject.GetComponent<AbstractCharacter>();

        if(barPoint==null)
            barPoint = gameObject.transform.Find("HP");

        for(int i = 0; i < barPoint.childCount; i++)
        {
            verbLoadingPoints[i] = barPoint.GetChild(i);
        }
        count = charaComponent.skills.Count;
    }
    public void FixedUpdate()
    {
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "UIcanvas")
            {
                if(charaComponent.skills.Count>count)
                {
                    //charaComponent.realSkills = charaComponent.GetComponents<AbstractVerbs>();
                    count = charaComponent.skills.Count;
                    skillUIbar.Add(Instantiate(verbLoadingPrefab, canvas.transform));
                    verbCD.Add(skillUIbar[count-1].transform.GetChild(0).GetComponent<Image>()) ;
                    skillUIbar[count-1].transform.position= verbLoadingPoints[count - 1].position;
                }
            }
        }   


        if (skillUIbar.Count != 0&&charaComponent!=null)
        {            
            for (int i = 0; i < count; i++)
            {
                //获取到该角色身上的skills库中的动词技能
                float percent = charaComponent.skills[i].CD / charaComponent.skills[i].needCD;
                verbCD[i].fillAmount = percent;
                //实时更新位置
                skillUIbar[i].transform.position = verbLoadingPoints[i].position;
            }
        }
        if (charaComponent.hp <= 0)
        {
            for(int i = 0; i < skillUIbar.Count; i++)
            {
                Destroy(skillUIbar[i]);
            }
        }
    }
}
