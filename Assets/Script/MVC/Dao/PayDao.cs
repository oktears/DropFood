using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// 支付数据访问层
    /// </summary>
    public class PayDao
    {

        public List<PayInfoData> _payInfoList { get; set; }


        public void loadPayInfoData()
        {
            if (_payInfoList == null)
                _payInfoList = new List<PayInfoData>();

            SReader reader = SReader.Create("Pay/payInfo_data.xd");
            for (int i = 0; i < reader.RecordCount; i++)
            {
                PayInfoData data = new PayInfoData();
                data = ReflectionUtil.dezelizebile(data, ref reader);
                _payInfoList.Add(data);
            }
            reader.Close();
        }
    }
}
