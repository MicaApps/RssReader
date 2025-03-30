using System;

namespace RssReader.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);
    }
}