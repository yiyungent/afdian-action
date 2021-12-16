using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Afdian.Action.Utils
{
    public class FileUtil
    {
        /// <summary>
        /// 程序一启动为 根目录
        /// </summary>
        public static readonly string AppDir;

        static FileUtil()
        {
            AppDir = Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 获取文件夹下所有文件
        /// </summary>
        /// <param name="directory">文件夹路径</param>
        /// <param name="pattern">文件类型</param>
        /// <param name="list">集合</param>
        public static void GetFiles(string directory, string pattern, ref List<string> list)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            foreach (FileInfo info in directoryInfo.GetFiles(pattern))
            {
                list.Add(info.FullName);
            }
            foreach (DirectoryInfo info in directoryInfo.GetDirectories())
            {
                GetFiles(info.FullName, pattern, ref list);
            }
        }


        public static async Task<string> ReadStringAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath, Encoding.UTF8);
        }


        /// <summary>
        /// 相对路径 转 绝对路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="currentDirectory">(要比较的) 当前路径</param>
        /// <returns></returns>
        public static string RelativePathToAbsolutePath(string relativePath, string currentDirectory)
        {
            string originDir = AppDir;
            //Console.WriteLine($"currentDirectory: {currentDirectory}");
            Directory.SetCurrentDirectory(currentDirectory);
            string absolutePath = Path.GetFullPath(relativePath);
            //Console.WriteLine($"absolutePath: {absolutePath}");

            // Fixed: TODO: 这样做有弊端, 导致最后一次执行后，currentDirectory 变化
            // 执行完成后, 重新设置回原来 dir, 防止污染
            Directory.SetCurrentDirectory(originDir);

            return absolutePath;
        }

        public static void GetLocalImageInfo(string imagePath, out long byteSize, out long width, out long height)
        {
            try
            {
                using (FileStream imageStream = File.Open(imagePath, FileMode.Open))
                {
                    byteSize = imageStream.Length;
                }
                //System.Drawing.Image mImage = System.Drawing.Image.FromFile(imagePath);
                var mImage = Image.Load(imagePath);
                width = mImage.Width;
                height = mImage.Height;
            }
            catch (Exception ex)
            {
                byteSize = 0;
                width = 0;
                height = 0;
            }
        }


        /// <summary> 
        /// 获取网络图片的大小和尺寸 
        /// </summary> 
        /// <param name="imageUrl">图片url</param> 
        /// <param name="byteSize">图片大小 (Byte)</param> 
        /// <param name="widthxHeight">图片尺寸（WidthxHeight）</param> 
        public static void GetRemoteImageInfo(string imageUrl, out long byteSize, out long width, out long height)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(200);
                var result = httpClient.GetAsync(imageUrl).Result;
                if (result.IsSuccessStatusCode)
                {
                    using (var stream = result.Content.ReadAsStreamAsync().Result)
                    {
                        byteSize = (stream.Length / 1024);
                        var mImage = Image.Load(stream);
                        width = mImage.Width;
                        height = mImage.Height;
                    }
                }
                else
                {
                    byteSize = 0;
                    width = 0;
                    height = 0;
                }
            }
            catch (Exception ex)
            {
                byteSize = 0;
                width = 0;
                height = 0;
            }
        }

    }
}
