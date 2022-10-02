using Server_Test_Project.FIleStorage.Interfaces;

namespace Server_Test_Project.FIleStorage.Implimentations
{
    public class ImageStorage : IImageStorage
    {
        private const string fileFolder = "Images";
        private readonly string filePath;

        public ImageStorage(string rootFolder)
        {
            filePath = Path.Combine(rootFolder, fileFolder);
        }

        public string Create(Stream stream, string extension, string folder)
        {
            var guid = Guid.NewGuid().ToString("N");

            using (var fileStream = new FileStream(Path.Combine(filePath, $"{folder}/{guid}{extension}"), FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }

            return guid;
        }

        public void Delete(string name, string extension, string folder)
        {
            var path = Path.Combine(filePath, folder, name + extension);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
