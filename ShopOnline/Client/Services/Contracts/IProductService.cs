using ShopOnline.Models.Dtos;

namespace ShopOnline.Client.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
    }
}
