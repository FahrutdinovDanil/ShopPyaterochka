//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopPyaterochka
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductIntakeProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductIntakeId { get; set; }
        public int Count { get; set; }
        public decimal PriceUnit { get; set; }
        public int StatusIntakeId { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual ProductIntake ProductIntake { get; set; }
        public virtual StatusIntake StatusIntake { get; set; }
    }
}
