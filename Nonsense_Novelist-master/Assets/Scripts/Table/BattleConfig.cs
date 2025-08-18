
public class BattleConfig : JsonTable<BattleConfig.Data>
{
    [System.Serializable]
    public class WordConfig
    {
        public int maxVerbNum = 3;
        public int maxPreColTimes = 3;
        public float verbPowerInterval = 5;
        public float wordBallRadius = 3;
        public float wordBallMinSpeed = 3;
        public float wordBallMaxSpeed = 3;
        public float wordBallFriction = 3;
        public float wordBallCollisionLoss = 3;
        public float wordBallChargingMaxTime=3;
        public float maxAngle = 45f;
        public int initLoadCount = 3;
        public int maxLoadCount = 5;
        public float loadSec = 1.0f;
    }
    
    [System.Serializable]
    public class Data
    {
        public WordConfig word;
    }

    public override string AssetName => "BattleConfig";
}