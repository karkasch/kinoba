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
    
    public partial class User
    {
        public User()
        {
            this.Specialists = new HashSet<Specialist>();
        }
    
        public int Id { get; set; }
        public short UserType { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
    
        public virtual ICollection<Specialist> Specialists { get; set; }
    }
}
