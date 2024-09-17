public class ActivityTrigger
{
    [FunctionName("ValidateOrder")]
    public static bool ValidateOrder([ActivityTrigger] OrderInput input, ILogger log)
    {
        // Perform order validation logic
        return true; // Assume validation passed for simplicity
    }


    [FunctionName("ChargeCustomer")]
    public static void ChargeCustomer([ActivityTrigger] OrderInput input, ILogger log)
    {
        // Perform customer charging logic
    }

    [FunctionName("SendConfirmationEmail")]
    public static void SendConfirmationEmail([ActivityTrigger] OrderInput input, ILogger log)
    {
        // Send a confirmation email to the customer
    }
}