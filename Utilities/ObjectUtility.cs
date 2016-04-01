using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities
{
    public static class ObjectUtility
    {

        /// <summary>
        /// Clones a object via shallow copy
        /// </summary>
        /// <typeparam name="T">Object Type to Clone</typeparam>
        /// <param name="obj">Object to Clone</param>
        /// <returns>New Object reference</returns>
        public static T ShadowClone<T>(this T obj) where T : class, new()
        {
            if (obj == null) return null;
            T newObject = new T();
            var propertyInfoArray = newObject.GetType().GetProperties(BindingFlags.Public |
                                    BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfoArray)
            {
                try
                {
                    //make sure not to copy reference
                    //var propertyVal = propertyInfo.GetValue(obj, null);
                    //if (propertyVal is int || propertyVal is string || propertyVal is DateTime || propertyVal is bool || propertyVal is float
                    //    || propertyVal is Nullable<int> || propertyVal is Nullable<float> || propertyVal is Nullable<DateTime> || propertyVal is Nullable<bool>)
                    //if (propertyVal == null) continue;
                    //if (propertyVal.GetType().Namespace == "System")
                    if(propertyInfo.PropertyType.Namespace == "System")
                    {
                        var propertyVal = propertyInfo.GetValue(obj, null);
                        propertyInfo.SetValue(newObject, propertyVal);
                    }
                }
                catch (Exception e)
                {

                }
            }
            return (T)newObject;
        }

        /// <summary>
        /// Clones an object with every property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj) where T : class ,new()
        {
            if (obj == null) return default(T);
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// Merge two objects. If there is a value(not equal to default value) in obj for a property, the value will be used, otherwise look at obj2 to check if there is a value to use.
        /// Obj and Obj2 should be the same class.
        /// This method returns a new object rather than updating obj.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static T MergeObject<T>(this T obj, T obj2) where T : class, new()
        {
            if (obj == null) return null;
            if (obj2 == null) return obj;
            T newObject = new T();
            var propertyInfoArray = obj.GetType().GetProperties(BindingFlags.Public |
                                    BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfoArray)
            {
                //get value from obj, if no value, then try get from obj2
                var propertyVal = propertyInfo.GetValue(obj, null);

                if (!propertyVal.EqualsToDefault())
                {
                    propertyInfo.SetValue(newObject, propertyVal);
                }
                else
                {
                    propertyVal = propertyInfo.GetValue(obj2, null);
                    if (!propertyVal.EqualsToDefault())
                    {
                        propertyInfo.SetValue(newObject, propertyVal);
                    }
                }
            }
            return newObject;
        }

        /// <summary>
        /// check if a value is a default value of its type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool EqualsToDefault<T>(this T obj)
        {
            var myType = obj.GetType();
            var defaultValue = GetDefaultValue(myType);
            return object.Equals(obj, (T)defaultValue);
        }

        /// <summary>
        /// if value type,return the default value of the type. 
        /// if class type,return an new instance of the class.
        /// if failed to return the new instance, return null.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object GetDefaultValue(this Type t)
        {
            try
            {
                return Activator.CreateInstance(t);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Convert a JsonObject into whatever type provided. If failed, return null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static T JsonToViewModel<T>(this JObject jsonObject) where T : class
        {
            var serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,//.Auto,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            try
            {
                var resultModel = JsonConvert.DeserializeObject<T>(jsonObject.ToString(), serializerSettings);
                return resultModel;
            }
            catch
            {
                return null;
            }
        }

        public static object GetObjectProperty<ObjectType>(object instance, string property_name)
        {
            PropertyInfo property_info = typeof(ObjectType).GetProperty(property_name);
            return property_info.GetValue(instance);
        }

        public static PropertyInfo GetProperty<objectType>(string property_name)
        {
            return typeof(objectType).GetProperty(property_name);
        }

    }
}
