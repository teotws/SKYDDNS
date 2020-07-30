using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SKYDDNS.Cloudflare;
using SKYDDNS.Cloudflare.Dtos;
using SKYDDNS.Common;
using SKYDDNS.Const;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace SKYDDNS
{
    public class MainServices : IMainServices
    {
        public MainServices(IConfiguration configuration, ICloudflareApi api, ILogger<MainServices> logger, ICommonApi commonApi)
        {
            _taskOptions = configuration.Get<TaskOptions>();
            _api = api;
            _logger = logger;
            _commonApi = commonApi;
        }
        private readonly TaskOptions _taskOptions;
        private readonly ICloudflareApi _api;
        private readonly ILogger<MainServices> _logger;
        private readonly ICommonApi _commonApi;


        private static Timer timer;
        public async Task<bool> InitTaskAsnyc()
        {
            //先执行一次
            await Handler(_taskOptions);
            //设置Timer，循环执行
            timer = new Timer(_taskOptions.Interval * 1000);
            timer.Elapsed += async (sender, e) => await Handler(_taskOptions);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();

            return true;
        }

        private async Task Handler(TaskOptions options)
        {
            if (options.Names == null)
            {
                _logger.LogError("请配置需要DDNS的域名（Names）");
                return;
            }
            var names = options.Names.Split(",").Where(a => a != null).ToList();
            if (names == null || names.Count == 0)
            {
                _logger.LogError("未正确配置Names");
                return;
            }
            _logger.LogInformation("开始");
            var recordsResult = await _api.GetDNSRecordsAsync(options.ZoneId);
            if (!recordsResult.Success)
            {
                _logger.LogInformation($"查询DNS记录失败：{recordsResult.Messages}");
                return;
            }
            var ip = string.Empty;
            try
            {
                ip = await _commonApi.GetIPAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"获取公网IP失败：{ex.Message}");
                return;
            }
            ip = ip.Replace("\n", "").Trim();
            _logger.LogInformation($"当前公网IP：{ip}");

            foreach (var item in names)
            {
                var record = recordsResult.Result.Where(a => a.name.Equals(item, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if (record != null)
                {
                    if (record.content == ip)
                    {
                        _logger.LogInformation($"{item} DNS记录为{record.content},无需更新");
                        continue;
                    }
                    else
                    {
                        _logger.LogInformation($"{item} DNS记录为{record.content},即将更新");
                        var model = new DNSRecordInput()
                        {
                            type = DNSType.A,
                            name = record.name,
                            content = ip
                        };
                        try
                        {
                            var res = await _api.UpdateDNSRecordAsync(options.ZoneId, record.id, model);
                            if (res.Success)
                            {
                                _logger.LogInformation($"{item}记录更新成功");
                                continue;
                            }
                            else
                            {
                                _logger.LogInformation($"{item}记录更新失败:{res.Messages}");
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation($"{item}记录更新失败:{ex.Message}");
                            continue;
                        }

                    }
                }
                else
                {
                    _logger.LogInformation($"DNS记录：{item}不存在，即将添加");
                    var model = new DNSRecordInput()
                    {
                        type = DNSType.A,
                        name = item,
                        content = ip
                    };
                    try
                    {
                        var res = await _api.CreateDNSRecordAsync(options.ZoneId, model);
                        if (res.Success)
                        {
                            _logger.LogInformation($"{item}记录添加成功");
                            continue;
                        }
                        else
                        {
                            _logger.LogInformation($"{item}记录添加失败:{res.Messages}");
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"{item}记录添加失败:{ex.Message}");
                    }
                }
            }
        }
    }
}
