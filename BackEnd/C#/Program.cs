using Microsoft.AspNetCore.Builder;
using ServerSide;

public class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("Starting...");

        Server server = new();
        server.Start();

        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");
        app.MapGet("/rooms", () => server.GetRooms());
        app.Run();
    }

}