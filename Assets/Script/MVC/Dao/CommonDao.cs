using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chengzi
{

    /// <summary>
    /// 收藏品数据访问层
    /// </summary>
    public class CommonDao
    {

        public List<TipData> _tipList { get; private set; }

        public void loadTipData()
        {
            _tipList = new List<TipData>();
            SReader reader = SReader.Create("System/tip_data.xd");
            for (int i = 0; i < reader.RecordCount; i++)
            {
                TipData data = new TipData();
                data = ReflectionUtil.dezelizebile<TipData>(data, ref reader);
                _tipList.Add(data);
            }
            reader.Close();
        }
    }
}
