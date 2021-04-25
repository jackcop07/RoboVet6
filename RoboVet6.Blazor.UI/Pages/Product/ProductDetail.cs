using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;

namespace RoboVet6.Blazor.UI.Pages.Product
{
    public partial class ProductDetail
    {
        public Models.Product Product { get; set; }

        [Inject]
        public IProductDataService ProductDataService { get; set; }

        [Parameter]
        public int ProductId { get; set; }

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

            Product = await ProductDataService.GetProductById(ProductId);

        }

        

    }
}
