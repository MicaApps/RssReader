using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace RssReader.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(object activationArgs);
    }
}