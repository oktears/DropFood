using System.Collections.Generic;

namespace Chengzi
{

    /// <summary>
    /// 关卡数据访问层
    /// </summary>
    public class LevelDao
    {

        public Dictionary<byte, LevelData> _leveDict { get; set; }

        public void loadLevelData()
        {
            if (_leveDict == null)
                _leveDict = new Dictionary<byte, LevelData>();

            SReader reader = SReader.Create("System/level_data.xd");

            for (int i = 0; i < reader.RecordCount; i++)
            {
                LevelData data = new LevelData();
#if UNITY_WEBGL
                data._id = reader.ReadByte();
                data._chapterId = reader.ReadByte();
                data._name = reader.ReadString();
                data._levelType = (LevelType)reader.ReadByte();
                data._unlockLevels = reader.ReadString();
                data._unlockLevelList = XDParseUtil.parseByteList(data._unlockLevels);
                data._prefab = reader.readString();
                data._bgmIn = reader.ReadInt();
                data._bgmLoop = reader.ReadInt();
                data._bgmOut = reader.ReadInt();
                data._isOpenBloom = reader.readBoolean();
                data._isOpenLomo = reader.readBoolean();
                data._isOpenTiltShift = reader.readBoolean();
#else
                data = ReflectionUtil.dezelizebile(data, ref reader);
                data._unlockLevelList = XDParseUtil.parseByteList(data._unlockLevels);
#endif
                _leveDict.Add(data._id, data);
            }
            reader.Close();
        }
    }
}
