using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{
    public class GameDao
    {

        public Dictionary<int, ItemData> _itemDataDict { get; set; }

        public Dictionary<int, ItemSeqData> _itemSeqDataDict { get; set; }

        public Dictionary<int, ItemSeqData> _mspoItemSeqDataDict { get; set; }

        public void loadItemData()
        {
            if (_itemDataDict == null)
                _itemDataDict = new Dictionary<int, ItemData>();

            SReader reader = SReader.Create("Game/item_data.xd");
            for (int i = 0; i < reader.RecordCount; i++)
            {
                ItemData data = new ItemData();
                data = ReflectionUtil.dezelizebile(data, ref reader);
                _itemDataDict.Add(data._itemId, data);
            }
            reader.Close();
        }

        public void loadItemSeqData()
        {
            if (_itemSeqDataDict == null)
                _itemSeqDataDict = new Dictionary<int, ItemSeqData>();

            SReader reader = SReader.Create("Game/item_seq_data.xd");
            for (int i = 0; i < reader.RecordCount; i++)
            {
                ItemSeqData data = new ItemSeqData();
                data = ReflectionUtil.dezelizebile(data, ref reader);
                data._itemPropDict = new Dictionary<int[], float>();

                string[] probArr = XDParseUtil.parseStringArray(data._prob, '|');
                for (int j = 0; j < probArr.Length; j++)
                {
                    string str = probArr[j];
                    string[] itemArr = XDParseUtil.parseStringArray(str, ';');
                    int[] itemIdArr = XDParseUtil.parseIntArray(itemArr[0]);
                    float prob = float.Parse(itemArr[1]);
                    data._itemPropDict.Add(itemIdArr, prob);
                }

                data._itemPropArr = new float[data._itemPropDict.Count];
                int k = 0;
                foreach (var item in data._itemPropDict)
                {
                    data._itemPropArr[k] = item.Value;
                    k++;
                }

                _itemSeqDataDict.Add(data._groupId, data);
            }

            reader.Close();
        }

        public void loadMspoItemSeqData()
        {
            if (_mspoItemSeqDataDict == null)
                _mspoItemSeqDataDict = new Dictionary<int, ItemSeqData>();

            SReader reader = SReader.Create("Game/item_seq_mspo.xd");
            for (int i = 0; i < reader.RecordCount; i++)
            {
                ItemSeqData data = new ItemSeqData();
                data = ReflectionUtil.dezelizebile(data, ref reader);
                data._itemPropDict = new Dictionary<int[], float>();

                string[] probArr = XDParseUtil.parseStringArray(data._prob, '|');
                for (int j = 0; j < probArr.Length; j++)
                {
                    string str = probArr[j];
                    string[] itemArr = XDParseUtil.parseStringArray(str, ';');
                    int[] itemIdArr = XDParseUtil.parseIntArray(itemArr[0]);
                    float prob = float.Parse(itemArr[1]);
                    data._itemPropDict.Add(itemIdArr, prob);
                }

                data._itemPropArr = new float[data._itemPropDict.Count];
                int k = 0;
                foreach (var item in data._itemPropDict)
                {
                    data._itemPropArr[k] = item.Value;
                    k++;
                }
                _mspoItemSeqDataDict.Add(data._groupId, data);
            }

            reader.Close();
        }
    }
}
