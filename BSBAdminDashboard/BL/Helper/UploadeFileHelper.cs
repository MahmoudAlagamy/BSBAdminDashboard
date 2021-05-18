using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BSBAdminDashboard.BL.Helper
{
    public static class UploadeFileHelper
    {
        public static string SaveFile(IFormFile FileUrl, string FolderPath)
        {
            #region Save Image

            // Get Directory
            string FilePath = Directory.GetCurrentDirectory() + "/wwwroot/Files/" + FolderPath;

            // Get File Name
            // Guid.NewGuid() use to not replace files have same name and givit random code
            string FileName = Guid.NewGuid() + Path.GetFileName(FileUrl.FileName);

            // Merge The Directory With File Name
            // if forget '/' use 'Path.Combine'
            string FinalPath = Path.Combine(FilePath, FileName);

            // Save Your Files As Stream "Data Over Time"
            using (var stream = new FileStream(FinalPath, FileMode.Create))
            {
                FileUrl.CopyTo(stream);
            }

            #endregion
            return FileName;
        }
    
        public static void RemoveFile(string FolderName, string RemovedFileName)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/"+ FolderName + RemovedFileName))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Files/" + FolderName + RemovedFileName);
            }
        }

    }
}
