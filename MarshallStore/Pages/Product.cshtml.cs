using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarshallStore.Areas.MarshallStore.Models;
using Microsoft.AspNetCore.Http;

namespace MarshallStore.Pages
{
    public class ProductByIdModel : PageModel
    {
        public void OnGet(int ProductId)
        {
            ProductModel ProductModel = new ProductModel(ProductId);
            ViewData["ProductId"] = ProductModel.ProductId;
            ViewData["Producer"] = ProductModel.Producer;
            ViewData["Model"] = ProductModel.Model;
            ViewData["Price"] = ProductModel.Price;
            ViewData["Description"] = ProductModel.Description;
            ViewData["Image1"] = ProductModel.Image1;
            ViewData["Image2"] = ProductModel.Image2;
            ViewData["Image3"] = ProductModel.Image3;

            //Get UserId from Session
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;

            if (UserId == 0)
            {
                //User not found
                ViewData["EnterButton"] = $@"<li class='nav-item'>
                                                <a href='/Login' class='btn btn-default mt-1 ml-2'>
                                                    <i class='fas fa-user'></i> 
                                                    <span class='nav-link-inner--text'>
                                                        Login
                                                    </span>
                                                </a>
                                            </li>";
            }
            else
            {
                //User found
                ViewData["EnterButton"] = $@"<li class='nav-item'>
                                                <a href='/CMSCore/DashboardIndex' class='btn btn-default mt-1 ml-2'>
                                                    <i class='fas fa-user'></i> 
                                                    <span class='nav-link-inner--text'>
                                                        Enter dashboard
                                                    </span>
                                                </a>
                                            </li>";

                ShoppingCartModel ShoppingCartModel = new ShoppingCartModel();

                ViewData["ShoppingCart"] = $@"<li class='nav-item'>
                                                <a href='/Cart' class='btn btn-default mt-1 ml-2'>
                                                    <i class='ni ni-cart'></i>
                                                    <span class='nav-link-inner--text'>
                                                        Cart
                                                    </span>
                                                    <span class='badge badge-white'>{ShoppingCartModel.CountByUserCreationId(UserId)}</span>
                                                </a>
                                            </li>";

                ViewData["AddToCartButton"] = $@"<button type='submit' class='btn btn-warning mt-4'>Add to Cart &nbsp;<i class='ni ni-cart'></i></button>";

            }
        }
    }
}
