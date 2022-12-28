//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Restaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurant()
        {
            this.Calisans = new HashSet<Calisan>();
            this.RestaurantMalzemeDetays = new HashSet<RestaurantMalzemeDetay>();
        }
    
        public int RestaurantID { get; set; }
        public string RestaurantAdi { get; set; }
        public string Sehir { get; set; }
        public string Adres { get; set; }
        public string Tel { get; set; }
        public Nullable<int> MenuID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calisan> Calisans { get; set; }
        public virtual Menu Menu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantMalzemeDetay> RestaurantMalzemeDetays { get; set; }
    }
}
