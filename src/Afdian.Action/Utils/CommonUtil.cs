using System;
using System.Collections.Generic;
using System.Text;

namespace Afdian.Action.Utils
{
    public class CommonUtil
    {

        public static string PrettyFileSize(long byteSize)
        {
            var num = 1024.00; // byte

            if (byteSize < num)
                return byteSize + "B";
            if (byteSize < Math.Pow(num, 2))
                return (byteSize / num).ToString("f2") + "KB"; // KB
            if (byteSize < Math.Pow(num, 3))
                return (byteSize / Math.Pow(num, 2)).ToString("f2") + "MB"; // MB
            if (byteSize < Math.Pow(num, 4))
                return (byteSize / Math.Pow(num, 3)).ToString("f2") + "GB"; // GB

            return (byteSize / Math.Pow(num, 4)).ToString("f2") + "TB"; // TB
        }

    }
}
