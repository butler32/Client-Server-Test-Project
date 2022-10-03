namespace Server_Test_Project.FIleStorage.Interfaces
{
    public interface IImageStorage
    {
        string Create(Stream stream);
        void Delete(string name);
    }
}
