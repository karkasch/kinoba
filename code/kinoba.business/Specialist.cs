//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kinoba.business
{
    using System;
    using System.Collections.Generic;
    
    public partial class Specialist
    {
        public Specialist()
        {
            this.SpecialistProfessionLinks = new HashSet<SpecialistProfessionLink>();
            this.SpecialistSchoolLinks = new HashSet<SpecialistSchoolLink>();
            this.SpecialistMedias = new HashSet<SpecialistMedia>();
            this.SpecialistPhotos = new HashSet<SpecialistPhoto>();
        }
    
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Slug { get; set; }
        public Nullable<int> CityId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> CastingDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte Sex { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<short> Height { get; set; }
        public Nullable<short> ClothesSize { get; set; }
        public Nullable<short> ShoeSize { get; set; }
        public Nullable<short> EyeColor { get; set; }
        public Nullable<short> HairLength { get; set; }
        public Nullable<short> HairColor { get; set; }
        public string Passport { get; set; }
        public string Notes { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<SpecialistProfessionLink> SpecialistProfessionLinks { get; set; }
        public virtual ICollection<SpecialistSchoolLink> SpecialistSchoolLinks { get; set; }
        public virtual ICollection<SpecialistMedia> SpecialistMedias { get; set; }
        public virtual ICollection<SpecialistPhoto> SpecialistPhotos { get; set; }
        public virtual User User { get; set; }
    }
}
