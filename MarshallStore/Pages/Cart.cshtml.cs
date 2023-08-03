using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MarshallStore.Areas.MarshallStore.Models;

namespace MarshallStore.Pages
{
    public class CartModel : PageModel
    {
        public void OnGet()
        {
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
                                                <a href='/CMSCore/DashboardIndex' class='btn btn-default mt-1 ml-2'>
                                                    <i class='ni ni-cart'></i>
                                                    <span class='nav-link-inner--text'>
                                                        Cart
                                                    </span>
                                                    <span class='badge badge-white'>{ShoppingCartModel.CountByUserCreationId(UserId)}</span>
                                                </a>
                                            </li>";
            }
        }
    }
}
