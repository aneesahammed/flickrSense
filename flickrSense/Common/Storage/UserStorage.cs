using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace flickrSense.Common.Storage
{
    public class UserStorage
    {
        public static async Task<string> ReadTextFromFile(string fileName)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync(fileName);
                if (file != null)
                {
                    return await FileIO.ReadTextAsync(file);
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("UserStorage.ReadTextFromFile", ex);
            }
            return String.Empty;
        }

        public static async Task WriteText(string fileName, string content)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, content);
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("UserStorage.WriteText", ex);
            }
        }

        public static async Task DeleteFileIfExists(string fileName)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                if (file != null)
                {
                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("UserStorage.DeleteFileIfExists", ex);
            }
        }

        public static async Task AppendLineToFile(string fileName, string line)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                await FileIO.AppendLinesAsync(file, new List<string>() { line });
            }
            catch { /* Avoid any exception at this point. */ }
        }
    }
}
