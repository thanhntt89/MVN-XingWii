using System.Collections.Generic;
using System.Linq;

namespace WiiObjects
{
    public class FilesCollection
    {
        private List<FileEntity> listFiles = new List<FileEntity>();

        public void AddItem(FileEntity item)
        {
            listFiles.Add(item);
        }

        public List<FileEntity> GetListItem()
        {
            return listFiles;
        }

        public FileEntity FindByInputName(string inputName)
        {
            var query = from item in listFiles where item.InputName.Equals(inputName) select item;
            if (query == null)
                return null;

            return query.Count() > 0 ? query.First() : null;
        }

        public void UpdateFilePathByFileName(string fileName, string filePath)
        {
            var query = listFiles.Where(item => item.InputName.Equals(fileName));
            if (query != null)
                query.First().FullFilePath_FolderWork = filePath;
        }
    }

    public class FileEntity
    {
        public string InputName { get; set; }
        public string FileName { get; set; }

        public string FullFilePath_FolderWork { get; set; }

        public string FileOutPutPath { get; set; }
    }
}
