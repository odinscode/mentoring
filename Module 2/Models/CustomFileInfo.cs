namespace FileSystemVisitorApp.Models
{
    public class CustomFileInfo : CustomFileItem
    {
        public CustomFileInfo(string fullName)
        {
            FullName = fullName;
            Name = System.IO.Path.GetFileName(fullName);
        }
    }
}
