using System.IO;
using SystemWatcherSolution.Models.Configuration;

namespace SystemWatcherSolution.Services.Validation
{
    public class WatchedDirectoryElementValidation : IElementValidation<WatchedDirectoryElement>
    {
        public void CheckValidity(WatchedDirectoryElement validatable)
        {
            if (!Directory.Exists(validatable.DirectoryPath))
                throw new DirectoryNotFoundException(string.Format(Resources.Exceptions.DirtectoryNotFound, validatable.DirectoryPath));
        }
    }
}
