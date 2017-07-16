using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SampleRx.Dal
{
    [DataContract]
    public partial class RxDrug
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RxDrug()
        {
            RxDataEntries = new HashSet<RxDataEntry>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RxDrugId { get; set; }

        [Required]
        [DataMember]
        [StringLength(50)]
        public string RxDrugName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RxDataEntry> RxDataEntries { get; set; }
    }
}
