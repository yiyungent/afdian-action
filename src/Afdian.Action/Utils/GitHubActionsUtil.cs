using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afdian.Action.Utils
{
    public class GitHubActionsUtil
    {
        public enum GitHubEnvKeyEnum
        {
            GITHUB_WORKSPACE = 0,
            GITHUB_ACTION_PATH = 1
        }

        public static string GetEnv(string name)
        {
            if (!name.StartsWith("INPUT_"))
            {
                name = $"INPUT_{name}";
            }
            name = name.ToUpper();

            // 注意: 当没有这个环境变量时, 不会报错, 而是返回 null
            return Environment.GetEnvironmentVariable(name);
        }

        public static string GitHubEnv(GitHubEnvKeyEnum gitHubEnvKeyEnum)
        {
            string envKey = gitHubEnvKeyEnum.ToString();

            // 注意: 当没有这个环境变量时, 不会报错, 而是返回 null
            return Environment.GetEnvironmentVariable(envKey);
        }

        public static void SetOutput(string name, string value)
        {
            Console.WriteLine($"::set-output name={name}::{value}");
        }
    }
}
