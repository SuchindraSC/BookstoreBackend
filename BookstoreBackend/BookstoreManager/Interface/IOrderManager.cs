using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Interface
{
    public interface IOrderManager
    {
        bool PlaceTheOrder(List<Order> orderdetails);
        List<OrderModel> GetOrderList(int userId);
    }
}
