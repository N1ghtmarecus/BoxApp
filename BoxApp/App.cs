using BoxApp.Services;

namespace BoxApp
{
    public class App(IUserCommunication userCommunication, IEventHandlerService eventHandlerService) : IApp
    {
        private readonly IUserCommunication _userCommunication = userCommunication;
        private readonly IEventHandlerService _eventHandlerService = eventHandlerService;

        public void Run()
        {
            // _eventHandlerService.SubscribeToEvents();
            // _userCommunication.UserChoice();
        }
    }
}