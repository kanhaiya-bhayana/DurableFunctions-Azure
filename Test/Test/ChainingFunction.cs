namespace Test
{
    public class ChainingFunction
    {
        private readonly ILogger<FetchToolsFunction> _logger;

        public ChainingFunction(ILogger<FetchToolsFunction> logger)
        {
            _logger = logger;
        }

        [Function("ChainingFunction_HttpStart")]
        public async Task<HttpResponseData> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get","post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext context)
        {
            _logger.LogInformation($"Started: {context.FunctionDefinition.Name}");

            var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(ChainingFunction));
            return await client.CreateCheckStatusResponseAsync(req, instanceId);
        }


        [Function(nameof(ChainingFunction))]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            if (!context.IsReplaying)
            {

                _logger.LogInformation($"Started: {nameof(ChainingFunction)}");
            }
            var toolsTask = context.CallActivityAsync<ToolsResponse>(nameof(FetchToolsFunction.FetchTools));
            var partsTask = context.CallActivityAsync<PartsResponse>(nameof(FetchPartsFunction.FetchParts));
            await Task.WhenAll(toolsTask, partsTask);

            var coffee = await context.CallActivityAsync<bool>(nameof(HaveCoffeeFunction.HaveCoffee));



            _logger.LogInformation("Completed");
        }
    }
}
