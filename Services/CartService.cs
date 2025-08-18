using JoiDelivery.Dtos;
using JoiDelivery.Models;
using JoiDelivery.Seed;

namespace JoiDelivery.Services;

public class CartService(ProductService productService)
{
    private readonly Dictionary<string, Cart> _userCarts = SeedData.CartForUsers;

    public CartProductInfo AddProductToCartForUser(AddProductRequest addProductRequest)
    {
        var cart = _userCarts.GetValueOrDefault(addProductRequest.UserId);
        var product = productService.GetProduct(addProductRequest.ProductId, addProductRequest.OutletId);

        cart.Products ??= [];

        cart.Products.Add(product);

        return new CartProductInfo(cart, product, product.SellingPrice);
    }
    
    public Cart? GetCartForUser(string userId) => _userCarts.GetValueOrDefault(userId);
}