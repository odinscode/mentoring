using System.Collections.Generic;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services.Converting
{
    public class WatchedDirectoryElementCollectionConverter 
        : IElementConverter<WatchedDirectoryElementCollection, IEnumerable<WatchedDirectory>>
    {
        private readonly IElementConverter<WatchedDirectoryElement, WatchedDirectory> directoryConvertionService;

        public WatchedDirectoryElementCollectionConverter(IElementConverter<WatchedDirectoryElement, WatchedDirectory> directoryConverterService)
        {
            this.directoryConvertionService = directoryConverterService ??
                throw new System.ArgumentNullException("WatchedDirectoryElementConverter", "WatchedDirectoryElementConverter is not specified");
        }

        public IEnumerable<WatchedDirectory> Convert(WatchedDirectoryElementCollection source)
        {
            var watchedDirectories = new List<WatchedDirectory>();

            foreach (WatchedDirectoryElement watchedDirectoryElement in source)
            {
                var watchedDirectory = this.directoryConvertionService.Convert(watchedDirectoryElement);
                watchedDirectories.Add(watchedDirectory);
            }

            return watchedDirectories;
        }
    }
}
