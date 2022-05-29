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
    public class NetworkMetricsAgentClient : INetworkMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<INetworkMetricsAgentClient> _logger;
        private readonly IMapper _mapper;

        public NetworkMetricsAgentClient(HttpClient httpClient, ILogger<INetworkMetricsAgentClient> logger, IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _logger = logger;
        }
        public GetByPeriodNetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricApiGetRequest request)
        {
            try
            {
                var generatedClient = new global::Client.GeneratedManager.Client(request.ClientBaseAddress, _httpClient);
                var response = generatedClient.ApiMetricsNetworkFromTo(request.FromTime, request.ToTime);
                var apiResponse = _mapper.Map<GetByPeriodNetworkMetricsApiResponse>(response);
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new GetByPeriodNetworkMetricsApiResponse();
                _logger.LogError(ex.Message);
            }
            return null;
        }



        //public GetByPeriodNetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricApiGetRequest request)
        //{
        //    var dateFormat = "yyyy-MM-ddTHH:mm:ss";
        //    var fromParameter = request.FromTime.ToString(dateFormat);
        //    var toParameter = request.ToTime.ToString(dateFormat);
        //    var httpRequest = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        $"{request.ClientBaseAddress}/api/metrics/network/from/{fromParameter}/to/{toParameter}");
        //    try
        //    {
        //        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

        //        using var responseStream = response.Content.ReadAsStreamAsync().Result;
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //        };
        //        return JsonSerializer.DeserializeAsync<GetByPeriodNetworkMetricsApiResponse>(responseStream, options).Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}
    }
}