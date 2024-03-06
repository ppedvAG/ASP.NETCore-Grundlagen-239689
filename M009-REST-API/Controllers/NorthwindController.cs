using M006_Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M009_REST_API.Controllers;

[ApiController]
[Route("api/northwind")]
[Produces("application/json")]
public class NorthwindController : Controller
{
	private readonly NorthwindContext db;

	public NorthwindController(NorthwindContext db) => this.db = db;

	[HttpGet("customers")]
	public IEnumerable<Customer> GetCustomers()
	{
		return db.Customers.AsEnumerable();
	}

	//[HttpGet]
	//[Route("customer/{id}")]
	[HttpGet("customer/{id}")]
	public Customer GetCustomerByID(string id)
	{
		return db.Customers.First(e => e.CustomerId == id);
	}

	[HttpPost("customer/submit")]
	public IActionResult PostCustomer(Customer customer)
	{
		try
		{
			db.Add(customer);
			db.SaveChanges();
		}
		catch (Exception)
		{
			return BadRequest();
		}
		return Ok();
	}
}
