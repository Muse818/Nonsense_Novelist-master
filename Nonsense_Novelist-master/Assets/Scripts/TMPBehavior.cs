
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;
public class TMPBehavior : PlayableBehaviour
{
    public string Text;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //通过下面的这句代码，即可在绑定过对象的轨道上进行传递参数的操作。playData就是我们放在轨道上面自定义的Asset。
        TextMeshProUGUI sayHello = playerData as TextMeshProUGUI;
        if (sayHello != null)
        {
            //对轨道绑定的对象进行值的传递。
            sayHello.text = Text;
        }
    }

}

