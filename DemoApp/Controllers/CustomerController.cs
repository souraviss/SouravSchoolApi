using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DemoApp.Dto;
using DemoApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        CustomerDto context;

        public CustomerController(CustomerDto db)
        {
            this.context = db;
           // context= HttpContext.RequestServices.GetService(typeof(DemoContext)) as DemoContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
           var result= this.context.ReadAllCustomer();
            return new OkObjectResult(result.Result);
            //DemoContext context = HttpContext.RequestServices.GetService(typeof(DemoContext)) as DemoContext;
            //return context.GetCustomers();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult FindByID(int id)
        {
            var result = this.context.FindOneAsync(id);
            return new OkObjectResult(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Customer body)
        {
           int result=await this.context.InsertAsync(body);
            if(result==0)
            {
                return new NotFoundResult();
            }
            ResponseStatus resp = new ResponseStatus();
            resp.Message = "Insert Successfully Done";
            resp.Status = 1;
            resp.LastInsertedId = result;
            return new OkObjectResult(resp);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody]Customer body,int id)
        {
            int result = await this.context.UpdateAsync(body, id);
            if (result == 0)
            {
                return new NotFoundResult();
            }
            ResponseStatus resp = new ResponseStatus();
            resp.Message = "Update Successfully Done";
            resp.Status = result;
            return new OkObjectResult(resp);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await this.context.DeleteAsync(Id);
            return new OkResult();
        }

    }
}