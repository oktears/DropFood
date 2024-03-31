using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    public static class DictionaryEx
    {
        /// <summary>
        /// 提供一个方法遍历所有value值
        /// </summary>
        public static void Foreach<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TKey, TValue> action)
        {
            dic.Foreach(action, 1000);
        }

        /// <summary>
        /// 提供一个方法遍历所有项
        /// </summary>
        public static void Foreach<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TKey, TValue> action, int maxCount)
        {
            if (action == null) return;
            var enumerator = dic.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < maxCount)
            {
                action(enumerator.Current.Key, enumerator.Current.Value);
            }
        }

        /// <summary>
        /// 提供一个方法遍历所有value值
        /// </summary>
        public static void ForeachKey<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TKey> action)
        {
            dic.ForeachKey(action, 1000);
        }

        /// <summary>
        /// 提供一个方法遍历所有key值
        /// </summary>
        public static void ForeachKey<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TKey> action, int maxCount)
        {
            if (action == null) return;
            var enumerator = dic.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < maxCount)
            {
                action(enumerator.Current.Key);
            }
        }

        /// <summary>
        /// 提供一个方法遍历所有value值
        /// </summary>
        public static void ForeachValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TValue> action)
        {
            dic.ForeachValue(action, 1000);
        }

        /// <summary>
        /// 提供一个方法遍历所有value值
        /// </summary>
        public static void ForeachValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TValue> action, int maxCount)
        {
            if (action == null) return;
            var enumerator = dic.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < maxCount)
            {
                action(enumerator.Current.Value);
            }
        }
    }

}
