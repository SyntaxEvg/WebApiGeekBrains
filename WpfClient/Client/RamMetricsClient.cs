using System;
using System.Net.Http;
using AutoMapper;
using WpfClient.Client.Interfaces;

using WpfClient.Requests;
using WpfClient.Responses;
using Microsoft.Extensions.Logging;
using MetricsManagerClient.Data.Interfaces;

namespace WpfClient.Client
{
    public class RamMetricsClient : IRamMetricsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IRamMetricsClient> _logger;
        private readonly IMapper _mapper;
        private readonly IAppModel _appModel;


        public RamMetricsClient(
            HttpClient httpClient,
            ILogger<IRamMetricsClient> logger,
            IMapper mapper,
            IAppModel model)
        {
            _httpClient = httpClient;
            _logger = logger;
            _mapper = mapper;
            _appModel = model;
        }

        public GetByPeriodRamMetricsClientResponse GetMetricsFromAgent(GetRamMetricsFromAgentRequest request)
        {
            try
            {
                var generatedClient = new global::Client.GeneratedManager.Client(
                    _appModel.ManagerBaseAddress, _httpClient);
                var response =  generatedClient.ApiMetricsRamAgentFromTo(
                    request.AgentId, request.FromTime, request.ToTime);
                var apiResponse = _mapper.Map<GetByPeriodRamMetricsClientResponse>(response);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public GetByPeriodRamMetricsClientResponse GetMetricsFromAllCluster(GetAllRamMetricsRequest request)
        {
            try
            {
                var generatedClient = new global::Client.GeneratedManager.Client(
                    _appModel.ManagerBaseAddress, _httpClient);
                var response =  generatedClient.ApiMetricsRamClusterFromTo(
                    request.FromTime, request.ToTime);
                var apiResponse = _mapper.Map<GetByPeriodRamMetricsClientResponse>(response);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}