using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DINGSOMETHING.Models.BusinessAccess;
using DINGSOMETHING.Models;
using DINGSOMETHING.Models.Helper;
using System.Security.Claims;

namespace DINGSOMETHING.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private Order orderOperation;
        private OrderDetail orderDetailOperation;
        private Product productOperation;

        private UserHelper user;

        public OrderController(){


            orderOperation = new Order();
            productOperation = new Product();
            orderDetailOperation = new OrderDetail();
        }


        public IActionResult Create(string id) {

            if(string.IsNullOrEmpty(id)){
                return RedirectToAction("Index","Product");
            }
            
            ViewData["Product"] = productOperation.GetById(id.ToUpper());
            
            return View();
        }


        public IActionResult Index(){

            ViewData["Order"] = orderOperation.Get();

            return  View();
        }

        public IActionResult Checkout(string id) {
            ViewData["Order"] = orderOperation.GetById(id);
            ViewData["Detail"] = orderDetailOperation.GetByOrder(id);

            user = new UserHelper((ClaimsIdentity)User.Identity);
            ViewData["User"] = user.GetUserName();

            return View();
        }

        [HttpPost]
        public IActionResult SaveDetail(string item , string price , string val){
            Response resp = new Response();
            
            if(string.IsNullOrEmpty(item) || string.IsNullOrEmpty(price)){
                resp.Code = -1;
                resp.Content = new ArgumentNullException().Message;
                return Json(resp);
            }

            user = new UserHelper((ClaimsIdentity)User.Identity);

            OrderDetail detail = new OrderDetail();
            detail.Price = int.Parse(price);
            detail.Item = item;
            detail.UserId = new Guid(EncryptHelper.DecryptString(user.GetUserId()));
            detail.UserName = user.GetUserName();
            detail.OrderId = new Guid(EncryptHelper.DecryptString(val));
            
            orderDetailOperation.Create(detail);
            
            resp.Code = 1;

            return Json(resp);
        }

        [HttpPost]
        public IActionResult Update(string key){
            Response response = new Response();
            OrderDetail detail = new OrderDetail();

            detail.Id = new Guid(EncryptHelper.DecryptString(key));
            detail.IsEnable = 0;
            orderDetailOperation.UpdateIsEnable(detail);

            response.Code = 1;

            return Json(response);
        }

        [HttpPost]
        public IActionResult Save(Order order){

            string tmpId = EncryptHelper.DecryptString(order.PName);
            order.OrderNo = orderOperation.GetMaxNo();
            order.PName = productOperation.GetById(tmpId).Name;
            order.PId = new Guid(tmpId);

            orderOperation.Create(order);

            return RedirectToAction("Index");
        }


    }


}