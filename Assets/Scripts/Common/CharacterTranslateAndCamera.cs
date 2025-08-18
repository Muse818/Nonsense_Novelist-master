using UnityEngine;
using UnityEngine.UI;

///<summary>
///��ɫ�ƶ�+������ƶ�(���������
///</summary>
class CharacterTranslateAndCamera : MonoBehaviour
{
    /// <summary>��ǰ�ؿ����н�ɫ</summary>
    private GameObject[] characters1;
    /// <summary>�ƶ��������</summary>
    private Camera camera_;
    /// <summary>�ƶ��ٶ�</summary>
    public float moveSpeed = 0.1f;
    /// <summary>��ͣ�ĵ�</summary>
    private GameObject[] targets;
    /// <summary>��ǰ�ؿ����</summary>
    public int guanQiaNum = 0;
    /// <summary>��ǰ�½����</summary>��Ҫ��inspector������ݵ�ǰ�½ڸ�ֵ
    public int chapterNum = 2;
    /// <summary>װ��Ʒ</summary>
    public GameObject[] zhuangShiPin;
    /// <summary>�ؿ�ͼƬ</summary>
    public GameObject[] chapterName;
    

    private void Start()
    {
        camera_ = GameObject.Find("MainCamera").GetComponent<Camera>();
        targets = new GameObject[3];
        targets[1] = GameObject.Find("target2");
        targets[2] = GameObject.Find("target3");
        
    }
    private void FixedUpdate()
    {
        /*if (UIManager.nextQuanQia && characters1.Length != 0)
        {   
            //������ƶ�
            camera_.transform.Translate(Vector3.right*moveSpeed);
            //ʣ���ɫ�ƶ�
            foreach(var item in characters1)
            {
                item.transform.Translate(Vector3.right * moveSpeed);
            }
        }*/
        EndMove();
    }
    public void BeginMove()
    {
        AbstractCharacter[] tempCharacters = GameObject.Find("SelfCharacter").GetComponentsInChildren<AbstractCharacter>();
        characters1 = new GameObject[tempCharacters.Length];
        for(int i=0; i < tempCharacters.Length; i++)
        {
            characters1[i] = tempCharacters[i].gameObject;
        }
    }
    public void EndMove()
    {
        if (guanQiaNum<=1&&Vector3.Distance(camera_.transform.position, targets[guanQiaNum+1].transform.position) <= 0.5f)
        {
            camera_.transform.position = targets[guanQiaNum+1].transform.position;
            //UIManager.nextQuanQia = false;
            //װ��Ʒ�͹ؿ���ͼƬ�л�
            zhuangShiPin[guanQiaNum + 1].SetActive(true);
            zhuangShiPin[guanQiaNum].SetActive(false);
            chapterName[guanQiaNum + 1].SetActive(true);
            chapterName[guanQiaNum].SetActive(false);

            guanQiaNum++;//��һ����Զ���������
        }
    }
}
