using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DINGSOMETHING.Models.DataAccess;
using DINGSOMETHING.Models.Helper;

namespace DINGSOMETHING.Models.BusinessAccess
{
    public class Order : IOrderRepository
    {
        private readonly Connection _connection;
        private FileHelper fileHelper;

        
        public Order(){
            fileHelper = new FileHelper();
            _connection = new Connection();
        }

        #region Property
        public Guid Id { get; set; }

        public Guid PId { get; set; }

        public string OrderNo { get; set; }

        public string PName { get; set; }

        public string OrderMemo { get; set; }

        public DateTime CreateDate { get; set; }

        public int IsEnable { get; set; }

        public bool IsClose { get; set; }

        #endregion

        public int Create(Order order)
        {
            if(order == null){
                throw new ArgumentNullException();
            }

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Execute(fileHelper.GetScriptFromFile("CreateOrder"),
                        new { OrderNo = order.OrderNo , OrderMemo = order.OrderMemo , 
                                PId = order.PId , PName = order.PName , IsClose = order.IsClose });
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public IEnumerable<Order> Get()
        {
            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Query<Order>(fileHelper.GetScriptFromFile("GetOrder"),null).ToList();
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public Order GetById(string id)
        {
            Guid guidKey = Guid.Parse(id);

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.QuerySingle<Order>(fileHelper.GetScriptFromFile("GetOrderById"),
                        new { Id = guidKey });
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public string GetMaxNo()
        {
            string data;

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    data = conn.QuerySingle<string>(fileHelper.GetScriptFromFile("GetMaxOrder"),null);
                    return this.GenerateNewOrderNo(data);
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public int Update(Order order)
        {
            throw new NotImplementedException();
        }

        private string GenerateNewOrderNo(string maxNo){
            string result = string.Empty;
            
            if(string.IsNullOrEmpty(maxNo)){
                result = string.Concat(
                    DateTime.Now.Year.ToString().Substring(2),
                    DateTime.Now.Month.ToString().PadLeft(2,'0'),
                    "01");
            }else{
                int no = int.Parse(maxNo.Substring(4)) + 1;
                result = string.Concat(maxNo.Substring(0,4),no.ToString().PadLeft(2,'0'));
            }

            return result;
        }
    }

}