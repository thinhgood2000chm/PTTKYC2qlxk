
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Services
{

using System;
    using System.Collections.Generic;
    
public partial class KhachHang
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public KhachHang()
    {

        this.PhieuVes = new HashSet<PhieuVe>();

    }


    public int stt { get; set; }

    public string MaKH { get; set; }

    public string TenKH { get; set; }

    public string Sdt { get; set; }

    public Nullable<bool> GioiTinh { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<PhieuVe> PhieuVes { get; set; }

}

}
