using SKYDDNS.Cloudflare.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace SKYDDNS.Cloudflare
{
    [Token]
    [HttpHost("https://api.cloudflare.com/client/v4/")]
    [JsonReturn(EnsureSuccessStatusCode = false)]
    public interface ICloudflareApi : IHttpApi
    {
        /// <summary>
        /// 获取DNS记录
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("zones/{zoneId}/dns_records")]
        Task<ApiResult<List<DNSRecordDto>>> GetDNSRecordsAsync([Required] string zoneId, int page = 1, int per_page = 100, CancellationToken cancellationToken = default);

        /// <summary>
        /// 创建DNS记录
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        [HttpPost("zones/{zoneId}/dns_records")]
        Task<ApiResult> CreateDNSRecordAsync([Required] string zoneId, [JsonContent]DNSRecordInput model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新DNS记录
        /// </summary>
        /// <param name="zoneId">区域ID</param>
        /// <param name="dnsId">DNSID</param>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("zones/{zoneId}/dns_records/{dnsId}")]
        Task<ApiResult> UpdateDNSRecordAsync([Required] string zoneId, [Required] string dnsId, [JsonContent] DNSRecordInput model, CancellationToken cancellationToken = default);

    }
}
