using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using TodoList.Database;
using TodoListWPF.Views;

namespace TodoListWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<TodoListDbContext>();
        }

        protected override Window CreateShell()
        {
            var database = new TodoListDbContext();
            database.Database.EnsureCreated();
            DatabaseLocator.Database = database;

            var w = Container.Resolve<MainWindow>();
            return w;
        }
    }
}
