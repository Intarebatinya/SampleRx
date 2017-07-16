namespace SampleRx.Dal
{
    using System.ComponentModel.DataAnnotations;

    public partial class RxCompanyLogin
    {
        [Key]
        [StringLength(50)]
        public string RxLoginEmail { get; set; }

        public int RxLoginCompanyId { get; set; }

        public bool RxLoginActive { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] RxLoginHash { get; set; }

        //public virtual RxCompany RxCompany { get; set; }
    }
}
