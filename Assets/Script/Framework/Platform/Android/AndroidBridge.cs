using UnityEngine;

namespace Chengzi
{
     
#if UNITY_ANDROID || UNITY_STANDALONE_WIN

    /// <summary>
    /// Unity与Andorid交互类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndroidBridge
    {

        /// <summary>
        /// Java 包名.类名
        /// </summary>
        protected virtual string javaClassName
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// 调用Java静态方法（无返回值）
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数列表</param>
        protected void callStatic(string methodName, params object[] args)
        {
            using (AndroidJavaClass ajc = new AndroidJavaClass(javaClassName))
            {
                ajc.CallStatic(methodName, args);
            }
        }

        /// <summary>
        /// 调用Java静态方法
        /// </summary>
        /// <typeparam name="ReturnType">返回值</typeparam>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数表</param>
        /// <returns>返回值</returns>
        protected T callStatic<T>(string methodName, params object[] args)
        {
            using (AndroidJavaClass ajc = new AndroidJavaClass(javaClassName))
            {
                return ajc.CallStatic<T>(methodName, args);
            }
        }

        /// <summary>
        /// 调用Java静态方法（无参）
        /// </summary>
        /// <typeparam name="ReturnType">返回值</typeparam>
        /// <param name="methodName">方法名</param>
        /// <returns>返回值</returns>
        protected T callStatic<T>(string methodName)
        {
            using (AndroidJavaClass ajc = new AndroidJavaClass(javaClassName))
            {
                return ajc.CallStatic<T>(methodName);
            }
        }


    }
#endif

}

