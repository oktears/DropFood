using System;
using System.Reflection;

namespace Chengzi
{

    /// <summary>
    /// 反射工具类
    /// </summary>
    public static class ReflectionUtil
    {

        public static Type[] getPropertyTypes(Object obj)
        {
            Type t = obj.GetType();
            Type[] typeArr = new Type[t.GetProperties().Length];

            for (int i = 0; i < t.GetProperties().Length; i++)
            {
                PropertyInfo pi = t.GetProperties()[i];
                typeArr[i] = pi.PropertyType;
            }
            return typeArr;
        }

        public static T dezelizebile<T>(T entity, ref SReader reader) where T : new()
        {
            Type type = typeof(T);
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                object obj = null;
                bool isContinue = false;
                switch (item.PropertyType.ToString())
                {
                    case "System.Byte":
                        obj = reader.readByte();
                        break;
                    case "System.Int16":
                        obj = reader.readShort();
                        break;
                    case "System.Int32":
                        obj = reader.readInt();
                        break;
                    case "System.Int64":
                        obj = reader.readLong();
                        break;
                    case "System.String":
                        obj = reader.readString();
                        break;
                    case "System.Single":
                        obj = reader.readFloat();
                        break;
                    case "System.Double":
                        obj = reader.readFloat();
                        break;
                    case "System.Boolean":
                        obj = reader.readBoolean();
                        break;
                    default:
                        if (item.PropertyType.BaseType.ToString() == "System.Enum")
                        {
                            obj = reader.readEnum(item.PropertyType);
                        }
                        else
                        {
                            isContinue = true;
                        }
                        break;
                }
                if (isContinue)
                    continue;

                //UnityEngine.Debug.Log(item.PropertyType.ToString() + "," + Convert.ChangeType(obj, item.PropertyType));
                item.SetValue(entity, Convert.ChangeType(obj, item.PropertyType), null);
            }
            return entity;
        }

        /// <summary>
        /// 拷贝对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object copy(this object obj)
        {
            Object targetDeepCopyObj;
            Type targetType = obj.GetType();
            //值类型  
            if (targetType.IsValueType == true)
            {
                targetDeepCopyObj = obj;
            }
            //引用类型   
            else
            {
                targetDeepCopyObj = System.Activator.CreateInstance(targetType);   //创建引用对象   
                System.Reflection.MemberInfo[] memberCollection = obj.GetType().GetMembers();

                foreach (System.Reflection.MemberInfo member in memberCollection)
                {
                    if (member.MemberType == System.Reflection.MemberTypes.Field)
                    {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
                        Object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(targetDeepCopyObj, copy(fieldValue));
                        }

                    }
                    else if (member.MemberType == System.Reflection.MemberTypes.Property)
                    {
                        System.Reflection.PropertyInfo myProperty = (System.Reflection.PropertyInfo)member;
                        MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null)
                        {
                            object propertyValue = myProperty.GetValue(obj, null);
                            if (propertyValue is ICloneable)
                            {
                                myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                            }
                            else
                            {
                                myProperty.SetValue(targetDeepCopyObj, copy(propertyValue), null);
                            }
                        }

                    }
                }
            }
            return targetDeepCopyObj;
        }


    }
}
