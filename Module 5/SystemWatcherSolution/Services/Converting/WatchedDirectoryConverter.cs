using System.IO;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;
using SystemWatcherSolution.Services.Validation;

namespace SystemWatcherSolution.Services.Converting
{
    public class WatchedDirectoryConverter : IElementConverter<WatchedDirectoryElement, WatchedDirectory>
    {
        private readonly IElementValidation<WatchedDirectoryElement> validationService;

        public WatchedDirectoryConverter(IElementValidation<WatchedDirectoryElement> validationService)
        {
            this.validationService = validationService;
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
