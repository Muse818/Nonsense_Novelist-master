using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetLevelTo(int level)
    {

        anim.SetInteger("index", level);
    }

    #region ¶¯»­µ÷ÓÃ
    bool enterAnim = false;
    public void StartEndChangeLevel()
    {
        print("StartEndChangeLevel");
        if (!enterAnim)
        {
            print( "true");
            CharacterManager.instance.pause = true;
            enterAnim = true;
        }
        else
        {
            print(  "false");
            CharacterManager.instance.pause = false ;
            enterAnim = false;
        }

    }


    #endregion
}
