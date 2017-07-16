using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SampleRx.Web.Models
{
    [DataContract]
    public class RxBaseResponse
    {
        [DataMember]
        public List<string> Logs { get; set; } = new List<string>();
    }
}