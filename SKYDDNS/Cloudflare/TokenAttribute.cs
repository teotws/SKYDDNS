using System.Threading.Tasks;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace SKYDDNS.Cloudflare
{
    public class TokenAttribute : ApiActionAttribute
    {
        public override Task OnRequestAsync(ApiRequestContext context)
        {
            var token = "nfFbr7H4YhYQFVSP0KPqT-KqYTiPwIPDujuLkAxT";
            context.HttpContext.RequestMessage.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");
            return Task.CompletedTask;
        }
    }
}
