using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AI
{
    /// <summary>
    /// ����״̬��ԭ����״̬��
    /// </summary>
    class DeadState :AbstractState
    {
        //private UIManager uIManager;
        private AudioPlay audioPlay;
        private void Start()
        {
            if (GameObject.Find("UIManager")!=null)
            {
                //uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

            }
            audioPlay = GameObject.Find("AudioSource").GetComponent<AudioPlay>();

        }
        override public void Awake()
        {

            base.Awake();
            id = StateID.dead;
            triggers.Add(gameObject.AddComponent<ReLifeTrigger>());
            map.Add(TriggerID.ReLife, StateID.idle);

        }
        public override void Action(MyState0 myState)
        {

            //AbstractCharacter myc = this.transform.parent.GetComponent<AbstractCharacter>();
            //bool lastOne = true;
            //if (this.transform.parent.GetComponent<AbstractCharacter>())
            //{
            //      foreach (var _ac in CharacterManager.instance.GetComponentsInChildren<AbstractCharacter>())
            //    {
            //        if ((_ac.camp == myc.camp)&& (_ac != myc))lastOne = false;
            //    } 
            //}


            //if (lastOne&&(myc.camp!=CampEnum.stranger))
            //{


            //    CharacterManager.instance.EndGame();
            //  }
           

            ////��ʱ������Ч��
            //SpriteRenderer sp;
            //if (this.TryGetComponent<SpriteRenderer>(out sp))
            //{
            //    sp.color = Color.black;
            //}

            var _sa = this.transform.parent.GetComponent<ServantAbstract>();
            //�������ӣ���������
            if (_sa != null)
            {
                _sa.masterNow.DeleteServant(_sa.gameObject);
            }
            //��ʱȥ�������if
            //if (myState.character.charaAnim.IsEnd(AnimEnum.dead))
            //{ 
            //�����궯��������
            Destroy(this.transform.parent.gameObject);
            //}
        }


        public override void EnterState(MyState0 myState)
        {
            var _ac = this.transform.parent.GetComponent<AbstractCharacter>();
            if (_ac.Camp == CampEnum.right)
            {
                CharacterManager.charas_right.Remove(this.transform.parent.GetComponent<AbstractCharacter>());
                if (CharacterManager.charas_right.Count == 0) CharacterManager.instance.EndGame();
                return;
            }

            if (_ac.Camp == CampEnum.left)
            {
                CharacterManager.charas_left.Remove(this.transform.parent.GetComponent<AbstractCharacter>());
                if (CharacterManager.charas_left.Count == 0) CharacterManager.instance.EndGame();
                return; 
            }
            if (myState.character.Camp == CampEnum.stranger)
            {
                myState.character.Camp = CampEnum.left;
                CharacterManager.instance.RefreshStanger();
                //�жϹ���orBoss����
                audioPlay.RandomPlay();

                Destroy(myState.character.gameObject);
                
            }



            myState.character.charaAnim.Play(AnimEnum.dead);
            //AbstractBook.afterFightText += myState.character.LowHPText();
            AbstractBook.afterFightText += myState.character.DieText();
            

            ////����
            //if (GameObject.Find("LeftAll").GetComponentsInChildren<AbstractCharacter>().Length <= 1 || GameObject.Find("RightAll").GetComponentsInChildren<AbstractCharacter>().Length <= 1)
            //// if (myState.character.camp == CampEnum.friend)
            //{
            //    UIManager.LoseEnd();
            //}
            /*
            if (myState.character.camp == CampEnum.enemy&& UIManager.enemyParentF[uIManager.transAndCamera.guanQiaNum].transform.childCount <= 1)
            {
                if (uIManager.transAndCamera.guanQiaNum == 0)
                {
                    UIManager.charaTransAndCamera.BeginMove();
                    UIManager.nextQuanQia = true;
                    UIManager.WinEnd();
                    for (int i = 0; i < UIManager.enemyParentF[1].transform.childCount; i++)
                    {
                        UIManager.enemyParentF[1].transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                if(uIManager.transAndCamera.guanQiaNum == 1)
                {
                    UIManager.charaTransAndCamera.BeginMove();
                    UIManager.nextQuanQia = true;
                    UIManager.WinEnd();
                    for (int i = 0; i < UIManager.enemyParentF[2].transform.childCount; i++)
                    {
                        UIManager.enemyParentF[2].transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                if(uIManager.transAndCamera.guanQiaNum == 2)
                {
                    UIManager.WinEnd();
                }
            }*/

        }
        public override void Exit(MyState0 myState)
        {
            //SpriteRenderer sp;
            //if (this.TryGetComponent<SpriteRenderer>(out sp))
            //{
            //    sp.color = Color.white;
            //}
        }
    }

}