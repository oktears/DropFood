using System.Collections.Generic;

namespace Chengzi
{

    /// <summary>
    /// I18N数据访问层
    /// </summary>
    public class I18NDao
    {

        public Dictionary<int, I18NData> _i18NDict { get; set; }

        public void loadI18NData()
        {
            if (_i18NDict == null)
                _i18NDict = new Dictionary<int, I18NData>();

            SReader reader = SReader.Create("System/i18n_data.xd");

            for (int i = 0; i < reader.RecordCount; i++)
            {
                I18NData data = new I18NData();
                data = ReflectionUtil.dezelizebile(data, ref reader);
                _i18NDict.Add(data._textId, data);
            }
            reader.Close();
        }
    }
}
