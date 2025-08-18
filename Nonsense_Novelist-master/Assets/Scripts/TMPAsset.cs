
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;
[System.Serializable]
public class TMPAsset : PlayableAsset
{
    public string Text;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        //通过下面的两行代码进行创建一个新的Playable（Script类型），然后通过GetBehavior来访问刚刚创建的
        var scriptPlayable = ScriptPlayable<TMPBehavior>.Create(graph);
        //上方create实际接受两个参数，第一个参数是graph，第二个参数是我们创建的这个Playable接受几个输入，默认不填写那么就是0个输入。
        var scriptBehavior = scriptPlayable.GetBehaviour();
        scriptBehavior.Text = Text;

        //返回刚刚创建出来的Playable，Unity会帮助我们自动的连线。
        return scriptPlayable;
    }
}
