using System;
using System.Net.Http;
using System.Text.Json;
using AutoMapper;
using MetricsManager.Client.Interfaces;
using MetricsManager.Requests;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Client
{
    public class CpuMetricsAgentClient : ICpuMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ICpuMetricsAgentClient> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsAgentClient(HttpClient httpClient, ILogger<ICpuMetricsAgentClient> logger, IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _logger = logger;
        }


        public GetByPeriodCpuMetricsApiResponse GetCpuMetrics(CpuMetricApiGetRequest request)
        {
            try
            {
                var generatedClient = new global::Client.GeneratedManager.Client(request.ClientBaseAddress, _httpClient);
                var response = generatedClient.ApiMetricsCpuFromTo(request.FromTime, request.ToTime);
                var apiResponse = _mapper.Map<GetByPeriodCpuMetricsApiResponse>(response);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        //public GetByPeriodCpuMetricsApiResponse GetCpuMetrics(CpuMetricApiGetRequest request)
        //{
        //    var dateFormat = "yyyy-MM-ddTHH:mm:ss";
        //    var fromParameter = request.FromTime.ToString(dateFormat);
        //    var toParameter = request.ToTime.ToString(dateFormat);
        //    var httpRequest = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        $"{request.ClientBaseAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
        //    try
        //    {
        //        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

        //        using var responseStream = response.Content.ReadAsStreamAsync().Result;
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //        };
        //        return JsonSerializer.DeserializeAsync<GetByPeriodCpuMetricsApiResponse>(responseStream, options).Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}
    }
}