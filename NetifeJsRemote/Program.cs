// See https://aka.ms/new-console-template for more information

using Grpc.Core;
using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using NetifeJsRemote;

//if (args.Length != 2)
//{
//    return;
//}

var services = new ServiceCollection();
services.AddNodeJS();
services.Configure<NodeJSProcessOptions>(op => op.Port = 7893);
ServiceProvider serviceProvider = services.BuildServiceProvider(); 

new Server
{
    Services =
    {
        NetifeMessage.NetifePost.BindService(new NetifePostImpl(serviceProvider.GetRequiredService<INodeJSService>()))
    },
    Ports = { new ServerPort("0.0.0.0", 7892, ServerCredentials.Insecure) }
}.Start();
Console.ReadLine();