using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afdian.Action.Utils
{
    public class JsonUtil
    {
        #region JsonStr2Obj
        public static T JsonStr2Obj<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        #endregion

        #region Obj2JsonStr
        public static string Obj2JsonStr(object jsonObj)
        {
            return JsonConvert.SerializeObject(jsonObj);
        }
        #endregion
    }
}
