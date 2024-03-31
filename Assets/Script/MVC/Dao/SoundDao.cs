
using System.Collections.Generic;

namespace Chengzi
{

    /// <summary>
    /// 音频数据访问层
    /// </summary>
    public class SoundDao
    {

        public List<SoundData> _soundList { get; private set; }

        public void loadSoundData()
        {
            _soundList = new List<SoundData>();
            SReader reader = SReader.Create("Sound/sound_data.xd");

            for (int i = 0; i < reader.RecordCount; i++)
            {
                SoundData data = new SoundData();
                data._soundId = reader.ReadShort();
                data._soundPath = reader.ReadString();
                if (reader.ReadSByte() == 1)
                {
                    data._isLoop = true;
                }
                else
                {
                    data._isLoop = false;
                }
                data._volume = reader.ReadFloat();
                _soundList.Add(data);
            }
            reader.Close();
        }
    }

}

