using WebApi.Models;

namespace WebApi.Services
{
    public interface ICallCenterService
    {
         string ProcessCall(CallEvent callEvent);
    }
}