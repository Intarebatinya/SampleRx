using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SampleRx.Dal
{
    [DataContract(IsReference = true)]
    public partial class RxCompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RxCompany()
        {
            RxDataEntries = new HashSet<RxDataEntry>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public int RxCompanyId { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string RxCompanyName { get; set; }
        [DataMember]
        public bool RxCompanyActive { get; set; }

        [Required]
        [StringLength(32)]
        [DataMember]
        public string RxCompanyAccessKey { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RxDataEntry> RxDataEntries { get; set; }
    }
}
