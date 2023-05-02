using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Jering.Javascript.NodeJS;
using NetifeJsRemote.Model;
using NetifeMessage;

namespace NetifeJsRemote;

public class NetifePostImpl : NetifeMessage.NetifePost.NetifePostBase
{

    private INodeJSService _nodeJsService;
    
    public NetifePostImpl(INodeJSService nodeJsService)
    {
        _nodeJsService = nodeJsService;
    }
    
    public override async Task<NetifeMessage.NetifeProbeResponse> 
            UploadRequest(NetifeMessage.NetifeProbeRequest request, ServerCallContext context)
    {
        Request innerRequest = new Request();
        string[] set = request.Uuid.Split(";");
        innerRequest.UUID = set[0];
        innerRequest.RequestType = request.RequestType.ToString();
        innerRequest.Pid = request.HasPid ? request.Pid : "";
        innerRequest.Protocol = request.Protocol.ToString();
        innerRequest.ApplicationType = request.ApplicationType.ToString();
        innerRequest.IsRawText = request.IsRawText;
        innerRequest.ProcessName = request.HasProcessName ? request.ProcessName : "";
        innerRequest.DstIpAddr = request.DstIpAddr;
        innerRequest.DstIpPort = request.DstIpPort;
        innerRequest.SrcIpPort = request.SrcIpPort;
        innerRequest.SrcIpAddr = request.SrcIpAddr;
        innerRequest.UUIDSub = request.HasUuidSub ? request.UuidSub : 0;
        innerRequest.RawText = request.RawText;
        NetifeMessage.NetifeProbeResponse probeResponse = new NetifeProbeResponse();
        var res = await _nodeJsService.InvokeFromFileAsync<Response>(
            Path.Join(Path.Join("scripts","bin"), set[1] + ".js"), set[2],args: new[] { JsonSerializer.Serialize(innerRequest) });
        probeResponse.Uuid = res.UUID;
        probeResponse.DstIpAddr = res.DstIpAddr;
        probeResponse.DstIpPort = res.DstIpPort;
        probeResponse.ResponseText = res.ResponseText;
        return probeResponse;
    }
}