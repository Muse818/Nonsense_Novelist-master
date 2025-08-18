using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
///<summary>
///抽奖盒动画
///</summary>
class BoxAnim : MonoBehaviour
{
    /// <summary>附加在盒子上的动画片段名称</summary>
    private string anim1;
    private string anim2;
    private string anim3;
    private string anim4;
    private string anim5;
    private string anim6;
    private string anim7;
    /// <summary>动画行为类</summary>
    public AnimationActor animActor;
    /// <summary>动画片段名称的集合</summary>
    private List<string> animNames=new List<string>();
    /// <summary>随机生成的6个随机数</summary>
    private int[] nums = new int[6];
    /// <summary>词条预制体</summary>
    public GameObject wordPrefab;
    /// <summary>父物体变换组件</summary>
    public Transform parentTF;
    /// <summary>判断是否是第一次点击</summary>
    public bool isFirst=false;
    private void Awake()
    {
        animActor = new AnimationActor(GetComponent<Animation>());
        animNames.AddRange(new string[] { anim1, anim2, anim3, anim4, anim5, anim6, anim7 });
        DontDestroyOnLoad(this.gameObject);
    }
    /// <summary>
    /// 抽取6个词条动画
    /// </summary>
    /// <returns></returns>
    public void NumberRandom()
    {
        UnityEngine.Random.InitState((int)Time.time);
        nums[0] = UnityEngine.Random.Range(0, animNames.Count);
        for (int i = 1; i < nums.Length;)
        {
            UnityEngine.Random.InitState((int)Time.time);
            nums[i] = UnityEngine.Random.Range(0, animNames.Count);
            Useable(i);
        }
    }
    /// <summary>
    /// 递归（返回一个未重复的元素）
    /// </summary>
    /// <param name="a">当前的元素</param>
    /// <returns></returns>
    private int Useable(int a)
    {
        if (IsUseable(a))
        {
            a++;
        }
        else
        {
            nums[a] = UnityEngine.Random.Range(0, animNames.Count);
            Useable(a);
        }
        return nums[a];
    }
    /// <summary>
    /// 判断是否重复
    /// </summary>
    /// <param name="length">当前的元素</param>
    /// <returns></returns>
    public bool IsUseable(int length)
    {
        for (int j = 0; j < length; j++)
        {
            if (animNames[nums[length]] == animNames[nums[j]])
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 点击盒子播放动画
    /// </summary>
    public void BoxPlay()
    {
        for (int i = 0; i < nums.Length; i++)
        {
            //animActor.Play(animNames[nums[i]]);
        }
    }
    /// <summary>
    /// 随机生成6个新的词条
    /// </summary>
    public void CreateWord()
    {
        if (!isFirst)
        {
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.name == "MainCanvas")
                {
                    for (int i = 0; i < nums.Length; i++)
                    {
                        GameObject word = Instantiate(wordPrefab, canvas.transform);
                        Type absWord = AllSkills.CreateSkillWord();
                        //将技能储存，加载到下一个场景
                        AllSkills.absWords[i] = absWord;
                        word.AddComponent(absWord);
                        //让button的text显示技能文字
                        word.GetComponent<Image>().sprite = Resources.Load<Sprite>("FirstStageLoad/" + word.GetComponent<AbstractWord0>().wordName);
                        word.transform.SetParent(parentTF);
                    }
                }
                isFirst = true;
            }
        }       
    }
}
