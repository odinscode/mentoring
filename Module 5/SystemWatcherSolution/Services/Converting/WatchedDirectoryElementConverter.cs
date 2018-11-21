using System.IO;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;
using SystemWatcherSolution.Services.Validation;

namespace SystemWatcherSolution.Services.Converting
{
    public class WatchedDirectoryElementConverter : IElementConverter<WatchedDirectoryElement, WatchedDirectory>
    {
        private readonly IElementValidation<WatchedDirectoryElement> validationService;

        public WatchedDirectoryElementConverter(IElementValidation<WatchedDirectoryElement> validationService)
        {
            this.validationService = validationService ??
                throw new System.ArgumentNullException("IElementValidation", "IElementValidation wasn't specified");
        }

        public WatchedDirectory Convert(WatchedDirectoryElement source)
        {
            validationService.CheckValidity(source);

            WatchedDirectory watchedDirectory = new WatchedDirectory()
            {
                DirectoryInfo = new DirectoryInfo(source.DirectoryPath)
            };

            return watchedDirectory;
        }
    }
}
