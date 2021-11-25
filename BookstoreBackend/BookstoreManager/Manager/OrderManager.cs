using BookstoreManager.Interface;
using BookstoreModel;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository repository;

        public OrderManager(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public List<OrderModel> GetOrderList(int userId)
        {
            try
            {
                return this.repository.GetOrderList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlaceTheOrder(List<Order> orderDetails)
        {
            try
            {
                return this.repository.PlaceTheOrder(orderDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
