namespace AboHalalBackend.FilesServices
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
    }
}
