namespace NetifeJsRemote.Model;

public class Request
{
    public string UUID { get; set; }
    
    public string RequestType { get; set; }
    
    public string ApplicationType { get; set; }
    
    public string Protocol { get; set; }
    
    public string DstIpAddr { get; set; }
    
    public string DstIpPort { get; set; }
    
    public string SrcIpAddr { get; set; }
    
    public string SrcIpPort { get; set; }
    
    public bool IsRawText { get; set; }

    public int UUIDSub { get; set; }
    
    public string RawText { get; set; }

    public string Pid { get; set; }

    public string ProcessName { get; set; }
}