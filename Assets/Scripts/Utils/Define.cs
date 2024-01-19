using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region 아이템 데이터
    public class ItemTable
    {
        public int itemIndex;
        public int level;
        public bool isDebuff;
        public float effect;
        public string itemName;
        public string itemExplain;
    }
    public struct ItemKey
    {
        public int itemIndex { get; set; }
        public int level { get; set; }
        public bool isDebuff { get; set; }
        public ItemKey(int _itemIndex, int _level, bool _isDebuff)
        {
            this.itemIndex = _itemIndex;
            this.level = _level;
            this.isDebuff = _isDebuff;
        }
        public override int GetHashCode()
        {
            return itemIndex.GetHashCode() + level.GetHashCode() + isDebuff.GetHashCode();
        }
    }
    public class ItemData
    {
        public float effect;
        public string itemName;
        public string itemExplain;
        public ItemData(float _effect, string _itemName, string _itemExaplain)
        {
            this.effect = _effect;
            this.itemName = _itemName;
            this.itemExplain = _itemExaplain;
        }
    }
    #endregion
    public class ScoreData
    {
        public string player_ID;
        public int maxScore;
    }
    public class VolumeData
    {
        public float masterVolume;
        public float bgmVolume;
        public float sfxVolume;
        public VolumeData()
        {
            masterVolume = 0.7f;
            bgmVolume = 0.7f;
            sfxVolume = 0.7f;
        }
    }
    [System.Serializable]
    public class WholeGameData
    {
        public bool firstPlay;
        public int maxScore;

        public WholeGameData()
        {
            firstPlay = false;
            maxScore = 0;
        }
    }
    [System.Serializable]
    public class PatternDatas
    {
        public List<PatternData> patterns;
    }
    [System.Serializable]
    public class PatternData
    {
        public int firstObstacle;
        public int secondObstacle;
        public int thirdObstacle;
        public PatternData()
        {
            firstObstacle = 0;
            secondObstacle = 0;
            thirdObstacle = 0;
        }
    }
    public enum WorldObject
    {
        Unknown,
        Player,
        Enemy,
    }
    public enum State
    {
        Idle,
        Walk,
        Crouch
    }
    public enum UIEvent
    {
        Click,
        BeginDrag,
        Drag,
        DragEnd,
        PointerDown,
        PointerUP
    }
    public enum MouseEvent
    {
        Press,
        Click,
        End
    }
    public enum Scene
    {
        Unknown,
        TitleScene,
        MainScene,
        GameScene
    }
    public enum Sound
    {
        Master,
        BGM,
        SFX,
        MaxCount
    }
}