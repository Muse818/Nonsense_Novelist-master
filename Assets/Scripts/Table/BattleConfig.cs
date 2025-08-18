
public class BattleConfig : JsonTable<BattleConfig.Data>
{
    [System.Serializable]
    public class WordConfig
    {
        public int maxVerbNum = 3;
        public int maxPreColTimes = 3;
        public float verbPowerInterval = 5;
        public float wordBallRadius = 3;
        public float wordBallMinSpeed = 0.1f;
        public float wordBallMaxSpeed = 3;
        public float wordBallFriction = 3;
        public float wordBallCollisionLoss = 3;
        public float wordBallChargingMinTime = 0.5f;
        public float wordBallChargingMaxTime = 3;
        public float maxAngle = 45f;
        public int initLoadCount = 3;
        public int maxLoadCount = 5;
        public float loadSec = 1.0f;
    }

    [System.Serializable]
    public class UnitConfig
    {
        public float scale = 0.8f;
        public int nounInitSlotNum = 7;
        public int nounMaxSlotNum = 10;
        public int verbDefaultSlotNum = 3;
        public int verbMaxSlotNum = 5;
        public int servantDefaultSlotNum = 3;
        public int servantMaxSlotNum = 4;
    }

    [System.Serializable]
    public class CampConfig
    {
        public float initCampHp = 100;
    }

    [System.Serializable]
    public class Data
    {
        public WordConfig word;
        public UnitConfig unit;
        public CampConfig camp;
    }

    public override string AssetName => "BattleConfig";
}