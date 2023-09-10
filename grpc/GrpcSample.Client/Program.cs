using Grpc.Net.Client;
using GrpcSample.Client;

var channel = GrpcChannel.ForAddress("https://localhost:7227");
var client = new Greeter.GreeterClient(channel);

var response = client.SayHello(new HelloRequest() { Name = "Sam" });

Console.WriteLine(response.Message);