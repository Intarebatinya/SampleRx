using SampleRx.Dal;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SampleRx.Web.Models
{
    [DataContract, KnownType(typeof(RxDataEntry))]
    public class RxDataResponse : RxBaseResponse
    {
        [DataMember]
        public List<RxDataEntry> Data { get; set; } = new List<RxDataEntry>();
    }
}