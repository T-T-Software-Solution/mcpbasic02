# App.Console

App.Console is a .NET 8 console application that acts as an interactive AI chat client, integrating with Azure OpenAI and a Model Context Protocol (MCP) server. It enables conversational AI with tool-augmented responses, leveraging both LLM and MCP server tools.

## Features
- Connects to Azure OpenAI for chat completions
- Integrates with an MCP server via SSE (Server-Sent Events)
- Discovers and lists available MCP tools
- Interactive conversational loop with streaming AI responses
- Supports tool-augmented answers (e.g., customer, order, starship operations)
- User secrets and configuration support for sensitive data

## Project Structure
- `Program.cs` — Main entry point, configuration, and chat loop
- `appsettings.json` — Application configuration (non-secret)

## Getting Started

### Prerequisites
- .NET 8 SDK
- Access to Azure OpenAI (endpoint, key, deployment)
- Running MCP server (see `App.Mcp`)

### Configuration
1. Set up `appsettings.json` with the MCP SSE endpoint:
   ```json
   {
     "Mcp": {
       "SseEndpoint": "http://localhost:5103/sse"
     }
   }
   ```
2. Store Azure OpenAI credentials and deployment in user secrets:
   Use the following command to set user secrets:
   ```powershell
   dotnet user-secrets init
   dotnet user-secrets set "AzureOpenAI:Endpoint" "<your-endpoint>"
   dotnet user-secrets set "AzureOpenAI:Key" "<your-key>"
   dotnet user-secrets set "AzureOpenAI:Deployment" "<your-deployment>"
   ```

### Build and Run
```powershell
# Navigate to the App.Console project directory
cd Presentations\App.Console

# Restore dependencies
 dotnet restore

# Build the project
 dotnet build

# Run the application
 dotnet run
```

## Usage
- On startup, the app lists all available MCP tools.
- Enter prompts at the console. The AI will answer, using MCP tools as needed.
- Type `exit` or `quit` to leave the chat loop.

## Notes
- The application requires both Azure OpenAI and MCP server endpoints to be configured.
- All secrets should be stored securely using user secrets.

## License
This project is for demonstration and educational purposes.
