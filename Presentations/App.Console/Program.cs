using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using ModelContextProtocol.Client;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddUserSecrets<Program>()
    .Build();

// Retrieve secrets with null checks
string? endpointStr = config["AzureOpenAI:Endpoint"];
string? key = config["AzureOpenAI:Key"];
string? deployment = config["AzureOpenAI:Deployment"];

if (string.IsNullOrWhiteSpace(endpointStr) || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(deployment))
{
    Console.WriteLine("Azure OpenAI configuration is missing. Please set Endpoint, Key, and Deployment in user secrets.");
    return;
}

var endpoint = new Uri(endpointStr);

// Create an IChatClient using Azure OpenAI with the settings from user secrets
IChatClient client =
    new ChatClientBuilder(
        new AzureOpenAIClient(endpoint, new Azure.AzureKeyCredential(key))
            .GetChatClient(deployment).AsIChatClient())
    .UseFunctionInvocation()
    .Build();

// Retrieve MCP SSE endpoint from configuration
string? mcpSseEndpoint = config["Mcp:SseEndpoint"];
if (string.IsNullOrWhiteSpace(mcpSseEndpoint))
{
    Console.WriteLine("MCP SSE endpoint is missing. Please set 'Mcp:SseEndpoint' in appsettings or user secrets.");
    return;
}

// Wrap the MCP client in an 'await using' block to ensure it's disposed correctly
await using (IMcpClient mcpClient = await McpClientFactory.CreateAsync(
    new SseClientTransport(new()
    {
        Endpoint = new Uri(mcpSseEndpoint)
    })))
{
    // List all available tools from the MCP server.
    Console.WriteLine("Available tools:");
    IList<McpClientTool> tools = await mcpClient.ListToolsAsync();
    foreach (McpClientTool tool in tools)
    {
        Console.WriteLine($"{tool.Name} - {tool.Description}");
    }
    Console.WriteLine();

    // Conversational loop that can utilize the tools via prompts.
    List<ChatMessage> messages = [
        new(ChatRole.System, "If you cannot find a customer, please get all customers to see them. Answer in language of the user question. Translate question to English to understand it better before answering. Answer all questions."),
    ];

    while (true)
    {
        Console.Write("Prompt: ");
        var userInput = Console.ReadLine();
        if (string.IsNullOrEmpty(userInput) || userInput.Trim() == "exit" || userInput.Trim() == "quit")
        {
            // Allow the user to exit the loop by typing 'exit' or 'quit'
            Console.WriteLine("Exiting the conversation loop.");
            break;
        }
        else if (string.IsNullOrWhiteSpace(userInput))
        {
            // When the user presses Enter on an empty line, break the loop
            break;
        }
        messages.Add(new(ChatRole.User, userInput));

        Console.WriteLine();
        Console.WriteLine("AI Answer: ");        

        List<ChatResponseUpdate> updates = [];
        await foreach (ChatResponseUpdate update in client
            .GetStreamingResponseAsync(messages, new() { Tools = [.. tools] }))
        {
            Console.Write(update);
            updates.Add(update);
        }
        Console.WriteLine();
        Console.WriteLine();

        messages.AddMessages(updates);
    }
} // <-- mcpClient is automatically disposed here, and the child process is terminated.

Console.WriteLine("\nApplication exited."); // Added for clarity