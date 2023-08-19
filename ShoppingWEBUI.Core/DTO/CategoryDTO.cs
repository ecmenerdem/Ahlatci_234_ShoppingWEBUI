using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Core.DTO
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public Guid? Guid { get; set; }
        public bool? IsActive { get; set; }
    }
}
