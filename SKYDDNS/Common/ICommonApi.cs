using System.Threading.Tasks;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace SKYDDNS.Common
{
    public interface ICommonApi : IHttpApi
    {
        [HttpHost("http://ip.cip.cc/")]
        [HttpGet]
        Task<string> GetIPAsync();
    }
}
