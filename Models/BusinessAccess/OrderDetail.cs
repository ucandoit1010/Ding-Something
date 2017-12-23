using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DINGSOMETHING.Models.DataAccess;
using DINGSOMETHING.Models.Helper;

namespace DINGSOMETHING.Models.BusinessAccess
{
    public class OrderDetail : IOrderDetailRepository
    {

        private readonly Connection _connection;
        private FileHelper fileHelper;

        public OrderDetail(){
            this._connection = new Connection();
            this.fileHelper = new FileHelper();
        }

        #region Property
        public string Item { get; set; }

        public int Price { get; set; }

        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public int IsEnable { get; set; }

        #endregion

        public int Create(OrderDetail detail)
        {
            if(detail == null){
                throw new ArgumentNullException();
            }

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Execute(fileHelper.GetScriptFromFile("CreateOrderDetail"),
                        new { OrderId = detail.OrderId , UserId = detail.UserId , 
                                UserName = detail.UserName , Item = detail.Item , 
                                Price = detail.Price });
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public IEnumerable<OrderDetail> GetByOrder(string id)
        {
            if(string.IsNullOrEmpty(id)){
                throw new ArgumentNullException();
            }

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Query<OrderDetail>(
                        fileHelper.GetScriptFromFile("GetDetailByOrder"),new { OrderId = id });
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public int UpdateIsEnable(OrderDetail detail)
        {
            if(detail == null){
                throw new ArgumentNullException();
            }

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Execute(
                        fileHelper.GetScriptFromFile("UpdateOrderDetail"),
                            new { Id = detail.Id , IsEnable = detail.IsEnable });
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }
    }


}