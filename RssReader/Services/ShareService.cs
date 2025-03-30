using RssReader.Contracts.Services;
using Windows.ApplicationModel.DataTransfer;

namespace RssReader.Services
{
    public class ShareService : IShareService
    {
        public void ShareLink(string link)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(link);
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(dataPackage);
            // 可以进一步实现分享到其他应用的逻辑
        }
    }
}