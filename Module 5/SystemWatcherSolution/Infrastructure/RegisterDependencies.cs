using System.Collections.Generic;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;
using SystemWatcherSolution.Services.Converting;
using SystemWatcherSolution.Services.Validation;
using Unity;

namespace SystemWatcherSolution.Infrastructure
{
    public static class RegisterDependencies
    {
        private static IUnityContainer container;

        public static IUnityContainer Container
        {
            get { return container; }
        }

        public static void SetupContainer()
        {
            #region Validation

            container.RegisterType<IElementValidation<RuleElement>, RuleElementValidation>();
            container.RegisterType<IElementValidation<WatchedDirectoryElement>, WatchedDirectoryElementValidation>();

            #endregion

            #region Converting

            container.RegisterType<IElementConverter<RuleElement, Rule>, RuleConverter>();
            container.RegisterType<IElementConverter<WatchedDirectoryElement, WatchedDirectory>, WatchedDirectoryElementConverter>();
            container.RegisterType<IElementConverter<SystemWatcherConfigurationSection, SystemWatcher>, SystemWatcherConfigurationSectionConverter>();
            container.RegisterType<IElementConverter<RuleElementCollection, IEnumerable<Rule>>, RuleElementCollectionConverter>();
            container.RegisterType<IElementConverter<WatchedDirectoryElementCollection, IEnumerable<WatchedDirectory>>, WatchedDirectoryElementCollectionConverter>();

            #endregion
        }
    }
}
