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
    public class RamMetricsAgentClient : IRamMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IRamMetricsAgentClient> _logger;
        private readonly IMapper _mapper;

        public RamMetricsAgentClient(HttpClient httpClient, ILogger<IRamMetricsAgentClient> logger, IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _logger = logger;
        }
        public GetByPeriodRamMetricsApiResponse GetRamMetrics(RamMetricApiGetRequest request)
        {
            try
            {
                var generatedClient = new NsSwagLib.Client(request.ClientBaseAddress, _httpClient);
                var response = generatedClient.ApiMetricsRamFromTo(request.FromTime, request.ToTime);
                var apiResponse = _mapper.Map<GetByPeriodRamMetricsApiResponse>(response);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        //public GetByPeriodRamMetricsApiResponse GetRamMetrics(RamMetricApiGetRequest request)
        //{
        //    var dateFormat = "yyyy-MM-ddTHH:mm:ss";
        //    var fromParameter = request.FromTime.ToString(dateFormat);
        //    var toParameter = request.ToTime.ToString(dateFormat);
        //    var httpRequest = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        $"{request.ClientBaseAddress}/api/metrics/ram/from/{fromParameter}/to/{toParameter}");
        //    try
        //    {
        //        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

        //        using var responseStream = response.Content.ReadAsStreamAsync().Result;
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //        };
        //        return JsonSerializer.DeserializeAsync<GetByPeriodRamMetricsApiResponse>(responseStream, options).Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}
    }
}