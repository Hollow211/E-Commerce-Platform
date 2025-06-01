using Domain.Aggregates;
using Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetById(int id);

        Task<List<Product>> GetAllProducts();

        Task CreateProduct(Product product);

        Task<List<Product>> GetProductsFromIds(List<int> ids);

        Task SaveChanges();
    }
}
