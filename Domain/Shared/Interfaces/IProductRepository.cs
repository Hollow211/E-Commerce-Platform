using Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetById(int id);

        Task<ICollection<Product>> GetAllProducts();

        Task CreateProduct(Product product);

        Task SaveChanges();
    }
}
