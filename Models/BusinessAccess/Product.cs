using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DINGSOMETHING.Models.DataAccess;
using DINGSOMETHING.Models.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DINGSOMETHING.Models.BusinessAccess
{
    public class Product : IProductRepository
    {
        private readonly Connection _connection;
        private FileHelper fileHelper;

        public Product() :base() {
            fileHelper = new FileHelper();
            _connection = new Connection();
        }

        #region Property
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CatalogId { get; set; }

        public string CatalogName { get; set; }

        public DateTime CreateDate { get; set; }

        public string Img { get; set; }

        public int IsEnable { get; set; }

        #endregion


        public int Create(Product product)
        {
            if(product == null){
                throw new ArgumentNullException();
            }

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Execute(fileHelper.GetScriptFromFile("CreateProduct"),
                        new { Name = product.Name , CatalogId = product.CatalogId , 
                        CatalogName = product.CatalogName , Img = product.Img });
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public IEnumerable<Product> Get()
        {
            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Query<Product>(
                        fileHelper.GetScriptFromFile("GetProduct") ,null).ToList();
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public Product GetById(string id)
        {
            if(string.IsNullOrEmpty(id)) {
                throw new ArgumentNullException();
            }

            Guid guidKey = Guid.Parse(id);

            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.QuerySingle<Product>(
                        fileHelper.GetScriptFromFile("GetProductById") ,new { Id = guidKey });
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public List<SelectListItem> GetCatalogSelectList(){
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem option;
            var catalogList = this.GetCatalog();

            foreach (ProductCatalog item in catalogList)
            {
                option = new SelectListItem();
                option.Text = item.Name;
                option.Value = 
                    EncryptHelper.EncryptString(item.Id.ToString().ToUpper());
                
                list.Add(option);
            }

            return list;
        }


        public List<SelectListItem> GetProductByCatalog(string catalogId) {

            var list = new List<SelectListItem>();
            SelectListItem option;
            var productList = this.Get().Where(p => p.CatalogId == catalogId.ToUpper()).ToList();
            
            foreach (Product item in productList)
            {
                option = new SelectListItem();
                option.Text = item.Name;
                option.Value = 
                    EncryptHelper.EncryptString(item.Id.ToString());
                
                list.Add(option);
            }

            return list;
        }

        public List<SelectListItem> GetEmptyList(){

            return new List<SelectListItem>();
        }

        public IEnumerable<ProductCatalog> GetCatalog()
        {
            using (var conn = _connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    return conn.Query<ProductCatalog>(
                        fileHelper.GetScriptFromFile("GetProductCatalog") ,null);
                }
                catch (Exception err)
                {
                    throw err;
                }

            }
        }

        public int Update(Product product)
        {
            throw new NotImplementedException();
        }


    }

    public class ProductCatalog {

        public string Name { get; set; }

        public Guid Id { get; set; }

    }
}