//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: CommonResponse.proto
namespace Chengzi
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CommonResponse")]
  public partial class CommonResponse : global::ProtoBuf.IExtensible
  {
    public CommonResponse() {}
    
    private string _url;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"url", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string url
    {
      get { return _url; }
      set { _url = value; }
    }
    private string _code;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"code", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string code
    {
      get { return _code; }
      set { _code = value; }
    }
    private string _message;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"message", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string message
    {
      get { return _message; }
      set { _message = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}