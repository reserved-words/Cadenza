using TestConsoleApp;

Console.WriteLine("Starting");

//var service = new UpdateFromBase64ToFilepaths();
//await service.Run("cadenza-test");

//var service = new StripBaseDirectoryFromFilepath();
//await service.Run("cadenza-test", "C:\\Cadenza-Test\\Music\\");

var service = new UpdateArtistCompareNames();
await service.Run("cadenza-test");

Console.WriteLine("-------------");
Console.WriteLine("Finished");

Console.ReadKey();