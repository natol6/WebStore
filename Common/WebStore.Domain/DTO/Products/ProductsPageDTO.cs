using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Domain.DTO.Products
{
    public record ProductsPageDTO(IEnumerable<ProductDTO> Products, int TotalCount);

    
}
