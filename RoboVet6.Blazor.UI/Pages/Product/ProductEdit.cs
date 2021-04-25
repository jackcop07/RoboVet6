using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorStrap.Util;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using RoboVet6.Blazor.UI.Interfaces.Helpers;
using RoboVet6.Blazor.UI.Interfaces.Services;

namespace RoboVet6.Blazor.UI.Pages.Product
{
    public partial class ProductEdit
    {
        [Parameter]
        public int ProductId { get; set; }

        public Models.Product Product { get; set; } = new Models.Product();

        [Inject] 
        public IProductDataService ProductDataService { get; set; }


        [Inject]
        protected IMatToaster Toaster { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IProductHelper ProductHelper { get; set; }

        public decimal PriceExcVat { get; set; }


        private string _priceIncVat;

        public string PriceIncVat
        {
            get
            {
                return _priceIncVat;
            }
            set
            {
                _priceIncVat = value;

                if (decimal.TryParse(PriceIncVat, out var priceIncVat))
                {
                    Product.PriceIncVat = priceIncVat;
                }
                else
                {
                    Product.PriceIncVat = -1.00m;
                }

                UpdatePriceExcVat();
                StateHasChanged();
            }
        }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private bool _authenticated;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            _authenticated = user.Identity?.IsAuthenticated ?? false;

            if (_authenticated && ProductId != 0)
            {
                Product = await ProductDataService.GetProductById(ProductId);
                PriceExcVat = Product.PriceExcVat;
                PriceIncVat = Product.PriceIncVat.ToString();
            }
        }

        protected void HandleInvalidSubmit()
        {
            Toaster.Add("Please correct validation errors.", MatToastType.Danger);
        }

        protected async Task HandleValidSubmit()
        {

            if (Product.Id == 0) //new
            {
                var addedProduct = await ProductDataService.AddProduct(Product);

                if (addedProduct != null)
                {
                    Toaster.Add("product created successfully.", MatToastType.Success);
                    NavigationManager.NavigateTo($"/productdetail/{addedProduct.Id}");
                }
                else
                {
                    Toaster.Add("Product not created. Please try again.", MatToastType.Danger);
                }
            }
            else
            {
                await ProductDataService.UpdateProduct(Product);

                Toaster.Add("Product updated successfully", MatToastType.Success);
                NavigationManager.NavigateTo($"/productdetail/{Product.Id}");
            }
        }

        private void UpdatePriceExcVat()
        {
            if (decimal.TryParse(PriceIncVat, out var priceIncVat))
            {
                PriceExcVat = ProductHelper.CalculatePriceExcVat(20, priceIncVat);
            }

            PriceExcVat = ProductHelper.CalculatePriceExcVat(20, priceIncVat);
            StateHasChanged();
        }
    }
}
