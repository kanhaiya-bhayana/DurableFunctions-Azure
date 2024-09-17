namespace DurabelFunctionDemo
{
    internal class OrchestratorFunction
    {
        [FunctionName("OrderOrchestrator")]
        public static async Task<string> OrchestratorFunctionCore(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var input = context.GetInput<OrderInput>();

            // Call the validation activity
            bool isValid = await context.CallActivityAsync<bool>("ValidateOrder", input);

            if (!isValid)
            {
                throw new Exception("Order is not valid");
            }

            // Call the charge activity
            await context.CallActivityAsync("ChargeCustomer", input);

            // Call the email activity
            await context.CallActivityAsync("SendConfirmationEmail", input);

            return "Order processed successfully";
        }

    }
}
