using TestConsoleApp;

Console.WriteLine("Starting");

Console.WriteLine("Enter API key");
var apiKey = Console.ReadLine();

Console.WriteLine("Enter username");
var username = Console.ReadLine();

var service = new ScrobbleImporter();

await service.Run(apiKey, username);

Console.WriteLine("-------------");
Console.WriteLine("Finished");

Console.ReadKey();