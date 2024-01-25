using BoxApp;
using BoxApp.DataProviders;
using BoxApp.Entities;
using BoxApp.Repositories;
using BoxApp.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();
services.AddSingleton<IFilterBoxesProvider, FilterBoxesProvider>();
services.AddSingleton<IBoxesProvider, BoxesProvider>();
services.AddSingleton<IRepository<Box>, JsonRepository<Box>>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;

app.Run();