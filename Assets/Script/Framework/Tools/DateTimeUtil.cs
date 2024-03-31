using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 日期时间工具类
    /// </summary>
    public class DateTimeUtil
    {

        //标准时间戳
        private readonly static DateTime s_timestampBaseDateTime = DateTime.Parse("01/01/1970 00:00:00");
        //北京时区和世界标准调整时区的时间差(单位：毫秒)
        private readonly static long BEIJING_TIMEZONE_UTC_OFFSET_TRICKS_MS = 8 * 60 * 60 * 1000;

        /// <summary>
        /// 获取当前距1970年1月1日的秒数
        /// </summary>
        /// <returns></returns>
        public static long GetTimestampSecond()
        {
            long ticks = DateTime.UtcNow.Ticks - s_timestampBaseDateTime.Ticks;
            ticks = Decimal.ToInt64(Decimal.Divide(ticks, 10000000));
            return ticks;
        }

        /// <summary>
        /// 获取当前距1970年1月1日的毫秒数
        /// </summary>
        /// <returns></returns>
        public static long GetTimestampMS()
        {
            long ticks = DateTime.UtcNow.Ticks - s_timestampBaseDateTime.Ticks;
            ticks = Decimal.ToInt64(Decimal.Divide(ticks, 10000));
            return ticks;
        }

        /// <summary>
        /// 获取指定日期的距离1970年1月1日的秒数
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <param name="h">时</param>
        /// <param name="mm">分</param>
        /// <param name="s">秒</param>
        /// <returns></returns>
        public static long GetThisTimestampSecond(int y, int m, int d, int h, int mm, int s)
        {
            DateTime _dateTime = new DateTime(y, m, d, h, m, s).AddTicks(-BEIJING_TIMEZONE_UTC_OFFSET_TRICKS_MS * 10000); // convert from beijing timezone to utc
            long ticks = _dateTime.Ticks - s_timestampBaseDateTime.Ticks;
            ticks /= 10000000;
            return ticks;
        }

        /// <summary>
        /// 距1970年1月1日的的毫秒数转时间戳（计算日期）
        /// </summary>
        /// <param name="tickms"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeByTick(long tickms)
        {
            DateTime time = s_timestampBaseDateTime.AddTicks((tickms + BEIJING_TIMEZONE_UTC_OFFSET_TRICKS_MS) * 10000);
            return time;
        }

        /// <summary>
        /// 通过秒数换算成一天当中的时间字符串
        /// </summary>
        /// <param name="tickms">秒数</param>
        /// <returns></returns>
        public static string GetTimeBySecond(long tickms)
        {
            int _ticks = (int)(tickms / 1000);
            if (_ticks < 1)
            {
                return "00:00:00";
            }

            int _hour = _ticks / 3600;
            _ticks -= _hour * 3600;
            int _minute = _ticks / 60;
            _ticks -= _minute * 60;
            int _second = _ticks;

            string _hourStr = "" + _hour;
            if (_hour < 10)
                _hourStr = "0" + _hour;

            string _minuteStr = "" + _minute;
            if (_minute < 10)
                _minuteStr = "0" + _minute;

            string _secondStr = "" + _second;
            if (_second < 10)
                _secondStr = "0" + _second;

            return _hourStr + ":" + _minuteStr + ":" + _secondStr;
        }

        /// <summary>
        /// 获取当前天的数字 20170314
        /// </summary>
        /// <returns></returns>
        public static int getTodayNum()
        {
            string month = string.Format("{0:00}", DateTime.Today.Month);
            string day = string.Format("{0:00}", DateTime.Today.Day);
            return Convert.ToInt32(Convert.ToString(DateTime.Today.Year) + month + day);
        }


        /// <summary>
        /// 获取当前天的数字 2017.03.14
        /// </summary>
        /// <returns></returns>
        public static string getTodayByString()
        {
            return Convert.ToString(Convert.ToString(DateTime.Today.Year) + "." + Convert.ToString(DateTime.Today.Month) + "." + Convert.ToString(DateTime.Today.Day));
        }

        /// <summary>
        /// 格式化时间00:00:00
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getSecondToFormatTime(float seconds)
        {
            float h = Mathf.FloorToInt(seconds / 3600f);
            float m = Mathf.FloorToInt(seconds / 60f - h * 60f);
            float s = Mathf.FloorToInt(seconds - m * 60f - h * 3600f);
            return h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
        }

        /// <summary>
        /// 格式化时间00:00 mm:ss
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getSecondToFormatTimeM_S(float seconds, StringBuilder str)
        {
            TimeSpan ts = TimeSpan.FromSeconds(seconds);
            float seconds0 = (float)Math.Round(seconds, 2) - (float)((int)Math.Round(seconds, 2));
            int seconds1 = Convert.ToInt32(seconds0 * 100);
            str.Remove(0, str.Length);

            do
            {
                if (seconds < 0) break;
                if (ts.Minutes + ts.Hours * 60 < 10)
                {
                    str.Append("0");
                }
                str.Append(ts.Minutes + ts.Hours * 60).Append(":");
                if (ts.Seconds < 10)
                {
                    str.Append("0");
                }
                str.Append(ts.Seconds);
                //.Append(":");
                //if (seconds1 < 10)
                //{
                //    str.Append("0");
                //}
                //str.Append(seconds1);
            } while (false);

            return str.ToString();
        }

    }
}
