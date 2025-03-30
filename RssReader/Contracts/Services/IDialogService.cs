using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace RssReader.Contracts.Services
{
    public interface IDialogService
    {
        Task<ContentDialogResult> ShowDialogAsync(string title, string content, string primaryButtonText, string secondaryButtonText);
    }
}