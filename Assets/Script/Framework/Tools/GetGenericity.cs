using System.Collections.Generic;
using System;
using System.Reflection;

namespace Chengzi
{

    public class GetGenericity<T> where T : new()
    {
        public static T get()
        {
            T ret = new T();
            return ret;
        }
    }

    public class GenericityUtil
    {
        public static T convertType<T>(string str)
        {
            System.Object obj = new Object();
            Type t = typeof(T);
            switch (t.ToString())
            {
                case "System.Byte":
                    obj = Convert.ToByte(str);
                    break;
                case "System.Int16":
                    obj = Convert.ToInt16(str);
                    break;
                case "System.Int32":
                    obj = Convert.ToInt32(str);
                    break;
                case "System.Int64":
                    obj = Convert.ToInt64(str);
                    break;
                case "System.String":
                    obj = Convert.ToString(str);
                    break;
                case "System.Single":
                    obj = Convert.ToSingle(str);
                    break;
                case "System.Double":
                    obj = Convert.ToDouble(str);
                    break;
                default:
                    if (t.BaseType.ToString() == "System.Enum")
                    {
                        obj = Enum.Parse(t, str);
                    }
                    break;
            }
            return (T)obj;
        }

    }

}
