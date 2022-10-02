namespace Server_Test_Project.FIleStorage.Interfaces
{
    public interface IImageStorage
    {
        string Create(Stream stream, string extension, string folder);
        void Delete(string name, string extension, string folder);
    }
}
