//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Khamitova4432.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class TreatmentPlan
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public System.DateTime DateOfPlan { get; set; }
        public string Description { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
