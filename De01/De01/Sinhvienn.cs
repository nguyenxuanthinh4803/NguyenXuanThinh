namespace De01
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sinhvienn")]
    public partial class Sinhvienn
    {
        [Key]
        [StringLength(6)]
        public string MaSV { get; set; }

        [StringLength(50)]
        public string HotenSV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(3)]
        public string MaLop { get; set; }

        public virtual Sinhvienn Sinhvienn1 { get; set; }

        public virtual Sinhvienn Sinhvienn2 { get; set; }
    }
}
