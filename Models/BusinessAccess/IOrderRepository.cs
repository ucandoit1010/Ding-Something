using System;
using System.Collections.Generic;


namespace DINGSOMETHING.Models.BusinessAccess
{
    public interface IOrderRepository
    {
        int Create(Order order);
        
        Order GetById(string id);

        IEnumerable<Order> Get();

        string GetMaxNo();

        int Update(Order order);

        
    }

    public interface IOrderDetailRepository {

        int Create(OrderDetail detail);

        IEnumerable<OrderDetail> GetByOrder(string id);

        int UpdateIsEnable(OrderDetail detail);
    }

}