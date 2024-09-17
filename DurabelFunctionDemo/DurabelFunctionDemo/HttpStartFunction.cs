namespace DurabelFunctionDemo
{
    public class HttpStartFunction
    {
        [FunctionName("HttpStartFuncMain")]
        public static async Task<IActionResult> HttpStartFuncMain(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            OrderInput input = JsonConvert.DeserializeObject<OrderInput>(requestBody);

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("OrderOrchestrator", input);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
