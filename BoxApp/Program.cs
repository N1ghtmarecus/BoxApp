﻿using BoxApp;
using BoxApp.Components.DataProviders;
using BoxApp.Data;
using BoxApp.Data.Entities;
using BoxApp.Data.Repositories;
using BoxApp.Helpers;
using BoxApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();
services.AddSingleton<IFilterBoxesProvider, FilterBoxesProvider>();
services.AddSingleton<IBoxesProvider, BoxesProvider>();
services.AddSingleton<IRepository<Box>, SqlRepository<Box>>();
services.AddSingleton<InputHelper>();
services.AddSingleton<BoxCalculator>();
services.AddDbContext<BoxAppDbContext>(options => options
    .UseSqlServer("Data Source=DESKTOP-I615PSF\\SQLEXPRESS;Initial Catalog=AppBoxStorage;Integrated Security=True;Trust Server Certificate=True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();

app?.Run();