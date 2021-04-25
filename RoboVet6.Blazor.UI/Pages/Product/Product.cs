using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces;
using RoboVet6.Blazor.UI.Interfaces.Services;

namespace RoboVet6.Blazor.UI.Pages.Product
{
    public partial class Product
    {

        public List<Models.Product> Products { get; set; } = new List<Models.Product>();

        public List<Models.Product> ProductsList { get; set; } = new List<Models.Product>();


        private bool _authenticated;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            _authenticated = user.Identity?.IsAuthenticated ?? false;

            if (!_authenticated)
            {
                return;
            }

            Products = (await ProductDataService.GetAllProducts(SearchTerm)).ToList();
            ProductsList = Products;

        }

        [Inject]
        public IProductDataService ProductDataService { get; set; }

        public string SearchTerm { get; set; }


        private void FilterChanged()
        {

            ProductsList = Products.Where(x => x.Name.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

    }

}
