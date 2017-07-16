namespace SampleRx.Dal
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [DataContract(IsReference =true)]
    public partial class RxDataEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public int EntryId { get; set; }
        [DataMember]
        public int EntryCompanyId { get; set; }
        [DataMember]
        public int EntryDrugId { get; set; }
        [DataMember]
        public int Quantiy { get; set; }
        [DataMember]
        public decimal Price { get; set; }

        public virtual RxCompany RxCompany { get; set; }
        [DataMember]
        public virtual RxDrug RxDrug { get; set; }
    }
}
