// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
//
// namespace Chengzi
// {
//
//     /// <summary>
//     /// 设备数据访问层
//     /// </summary>
//     public class DeviceDao
//     {
//
//         public List<DeviceData> _deviceList { get; set; }
//
//         public void loadDeviceData()
//         {
//             if (_deviceList == null)
//                 _deviceList = new List<DeviceData>();
//
//             SReader reader = SReader.Create("Device/device_quality_data.xd");
//
//             for (int i = 0; i < reader.RecordCount; i++)
//             {
//                 DeviceData data = new DeviceData();
// #if UNITY_WEBGL
//                 data._id = reader.readByte();
//                 data._cpu = reader.ReadString();
//                 data._gpu = reader.ReadString();
//                 data._level = (QualityLevel)reader.readByte();
//                 data._isOpenFilter = reader.readBoolean();
// #else
//                 data = ReflectionUtil.dezelizebile(data, ref reader);
// #endif
//                 _deviceList.Add(data);
//             }
//             reader.Close();
//         }
//     }
// }
