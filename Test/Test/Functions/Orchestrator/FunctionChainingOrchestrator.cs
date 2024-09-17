using Test.Functions.Actitivity;

namespace Test.Functions.Orchestrator
{
    public class FunctionChainingOrchestrator(ILogger<FunctionChainingOrchestrator> _logger)
    {
        [Function(nameof(FunctionChainingOrchestrator))]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            if (!context.IsReplaying)
            {

                _logger.LogInformation($"Started: {nameof(FunctionChainingOrchestrator)}");
            }
            var toolsTask = context.CallActivityAsync<ToolsResponse>(nameof(FetchToolsFunction.FetchTools));
            var partsTask = context.CallActivityAsync<PartsResponse>(nameof(FetchPartsFunction.FetchParts));
            await Task.WhenAll(toolsTask, partsTask);

            var coffee = await context.CallActivityAsync<bool>(nameof(HaveCoffeeFunction.HaveCoffee));



            _logger.LogInformation("Completed");
        }
    }
}
