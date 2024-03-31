using Newtonsoft.Json;
using UnityEngine;

namespace Chengzi
{
    public static class PlayerPrefsHelper
    {
        public static T Get<T>(string key) where T : class
        {
            T t = null;
            string saveString = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(saveString))
            {
                t = JsonConvert.DeserializeObject<T>(saveString);
            }
            return t;
        }

        public static bool Save<T>(string key, T t)
        {
            if (string.IsNullOrEmpty(key) || t == null)
            {
                return false;
            }

            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
            }

            string saveString = JsonConvert.SerializeObject(t);
            //Debug.Log(saveString);

            if (!string.IsNullOrEmpty(saveString))
            {
                PlayerPrefs.SetString(key, saveString);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除所有
        /// </summary>
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public static void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 1 ? true : false;
        }

        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public static void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

    }
}