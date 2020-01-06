﻿using SmartStore.Core.Domain.Customers;
using SmartStore.Core.Domain.Shipping;
using SmartStore.Services.Common;

namespace SmartStore.Services.Cart.Rules.Impl
{
    public class ShippingMethodRule : ListRuleBase<int>
    {
        protected override int GetValue(CartRuleContext context)
        {
            int? shippingMethodId = context.Customer.GetAttribute<ShippingOption>(SystemCustomerAttributeNames.SelectedShippingOption, context.Store.Id)?.ShippingMethodId;

            return shippingMethodId ?? 0;
        }
    }
}
