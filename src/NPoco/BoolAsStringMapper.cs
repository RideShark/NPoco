using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace NPoco
{
    class BoolAsStringMapper : DefaultMapper 
    {
        public override Func<object, object> GetFromDbConverter(Type destType, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                if (destType  == typeof(bool))
                {
                    return delegate(object src)
                    {
                        string castSrc = (string)src;
                        if (castSrc == "True" || castSrc == "true" || castSrc.ToLower() == "true")
                        {
                            return true;
                        }
                        return false;
                    };
                }
                if (destType  == typeof(bool?))
                {
                    return delegate(object src)
                    {
                        string castSrc = (string)src;
                        if (String.IsNullOrWhiteSpace(castSrc))
                        {
                            return null;
                        }
                        if (castSrc == "True" || castSrc == "true" || castSrc.ToLower() == "true")
                        {
                            return true;
                        }
                        if (castSrc == "False" || castSrc == "false" || castSrc.ToLower() == "false")
                        {
                            return false;
                        }
                        return null;
                    };
                }
            }
            return null;
        }

        public override Func<object, object> GetToDbConverter(Type destType, Type sourceType)
        {
            if (destType == typeof (bool) || destType == typeof(bool?))
            {
                if (sourceType == typeof (bool))
                {
                    return delegate(object src)
                    {
                        bool castSrc = (bool)src;
                        return castSrc ? "True" : "False";
                    };
                }
                if (sourceType == typeof (bool?))
                {
                    return delegate(object src)
                    {
                        bool? castSrc = (bool?) src;
                        if (castSrc.HasValue)
                        {
                            return castSrc.Value ? "True": "False";
                        }
                        return "";
                    };
                }
            }
            return null;
        }
    }
}
