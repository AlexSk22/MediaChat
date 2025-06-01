using Microsoft.AspNetCore.Builder;
using ServerSide;

public class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("Starting...");

        Server server = new();

        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");
        app.MapGet("/rooms", () => server.GetRooms());
        app.Run();
    }

}