using Flashcards.Data;
using Flashcards.Models;
using Flashcards.Repositoties.Abstract;
using Flashcards.Repositoties.Bases;
using Flashcards.Services.Abstracts;
using Flashcards.Services.Bases;
using Flashcards.ViewModels;
using Flashcards.ViewModels.UserControls;
using Flashcards.ViewModels.Windows;
using Flashcards.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Flashcards.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            #region [ Windows ]
            services
                .AddScoped<MainWindow>()
                .AddTransient<EditWindow>();
            #endregion

            #region [ ViewModels ]
            services
                .AddSingleton<MainWindowViewModel>()
                .AddSingleton<EditWindowViewModel>()
                .AddSingleton<CSViewModel>()
                .AddTransient<SetsViewModel>();

            services
                .AddSingleton<Func<SetEntity, SetViewModel>>
                (sp => set => new SetViewModel(
                    set,
                    sp.GetRequiredService<INavigationService>(),
                    sp.GetRequiredService<IWordRepository>(),
                    sp.GetRequiredService<Func<ObservableCollection<WordEntity>, bool, SelectedSetViewModel>>()
                    )
                );

            services
                .AddSingleton<Func<ObservableCollection<WordEntity>, bool, SelectedSetViewModel>>
                (sp => (words, ch) => new SelectedSetViewModel(
                    words,
                    ch,
                    sp.GetRequiredService<IWordRepository>(),
                    sp.GetRequiredService<Func<WordEntity, EditWindowViewModel>>()
                    )
                );

            services
                .AddSingleton<Func<WordEntity, EditWindowViewModel>>
                (sp => word => new EditWindowViewModel(
                    word,
                    sp.GetRequiredService<IWordRepository>()
                    )
                );
            #endregion

            #region [ Repositories ]
            services
                .AddSingleton<ISetRepository, SetRepository>()
                .AddSingleton<IWordRepository, WordRepository>();
            #endregion

            #region [ Services ]
            services
                .AddSingleton<INavigationService, NavigationService>();
            #endregion

            #region [ DBContext ]
            services.AddSingleton<DesignTimeDbContextFactory>();
            #endregion

            return services;
        }
    }
}
