

using UnityEngine;

public class BattleScene : BattleModule
{

    
    public override void Init() 
    {
        FindAllWall();
    }

    public void FindAllWall()//临时的，先把场上放的墙壁读进来
    { 
        var A= GameObject.Find("WallCol").GetComponentsInChildren<Collider2D>();
        foreach(var aSS in A)
        {
        
            Battle.ObjectFactory.CreateWall<NormarWall>(aSS);
        }
    }
    public override void Update(float deltaSec)
    {
       
    }
}