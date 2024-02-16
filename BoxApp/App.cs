using BoxApp.Data;
using BoxApp.Services;

namespace BoxApp;

public class App: IApp
{
    private readonly IUserCommunication _userCommunication;
    private readonly IEventHandlerService _eventHandlerService;
    private readonly BoxAppDbContext _boxAppDbContext;

    public App(IUserCommunication userCommunication, IEventHandlerService eventHandlerService, BoxAppDbContext boxAppDbContext) 
    {
        _userCommunication = userCommunication;
        _eventHandlerService = eventHandlerService;
        _boxAppDbContext = boxAppDbContext;
        _boxAppDbContext.Database.EnsureCreated();
    }

    public void Run()
    {
        _eventHandlerService.SubscribeToEvents();
        _userCommunication.UserChoice();
    }
}