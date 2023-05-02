// See https://aka.ms/new-console-template for more information

using Grpc.Core;
using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using NetifeJsRemote;

if (args.Length != 3)
{
    return;
}

var services = new ServiceCollection();
services.AddNodeJS();
ServiceProvider serviceProvider = services.BuildServiceProvider(); 

new Server
{
    Services =
    {
        NetifeMessage.NetifePost.BindService(new NetifePostImpl(serviceProvider.GetRequiredService<INodeJSService>()))
    },
    Ports = { new ServerPort(args[1], int.Parse(args[2]), ServerCredentials.Insecure) }
}.Start();