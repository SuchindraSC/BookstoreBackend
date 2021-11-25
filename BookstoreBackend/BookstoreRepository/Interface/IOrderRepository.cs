using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.Interface
{
    public interface IOrderRepository
    {
        bool PlaceTheOrder(List<Order> orderdetails);
        List<OrderModel> GetOrderList(int userId);
    }
}
