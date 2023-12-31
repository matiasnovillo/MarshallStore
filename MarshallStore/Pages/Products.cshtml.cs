using MarshallStore.Areas.CMSCore.Models;
using MarshallStore.Areas.MarshallStore.Models;
using MarshallStore.Areas.MarshallStore.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

//Last modification on: 01/08/2023 19:39:01

namespace MarshallStore.Areas.MarshallStore.Pages
{
    /// <summary>
    /// Stack:             9 <br/>
    /// Name:              C# Razor Page. <br/>
    /// Function:          Allow you to show HTML files using Razor Page technology. <br/>
    /// Last modification: 01/08/2023 19:39:01
    /// </summary>
    public partial class ProductsModel : PageModel
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
                                                <a href='/Cart' class='btn btn-default mt-1 ml-2'>
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