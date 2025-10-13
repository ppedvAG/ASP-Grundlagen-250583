using M001.Models;
using Microsoft.AspNetCore.Mvc;

namespace M001.Controllers;

public class NorthwindController(NorthwindContext db) : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Customers()
	{
		IQueryable<Customer> customers = db.Customers;

		IEnumerable<string> countries = db.Customers.Select(e => e.Country).Distinct().OrderBy(e => e);
		ViewBag.Countries = countries;

		return View(customers);
	}

	public IActionResult BestellungenAnzeigen(string id)
	{
		IQueryable<Order> orders = db.Orders.Where(e => e.CustomerId == id);
		return View("Bestellungen", orders);
	}

	public IActionResult DetailsAnzeigen(int id)
	{
		IQueryable<DetailsProducts> od = db.OrderDetails
			.Where(e => e.OrderId == id)
			.Join(db.Products, //Andere Tabelle
				  e => e.ProductId, //Key von Tabelle 1
				  e => e.ProductId, //Key von Tabelle 2
				  (l, r) => new DetailsProducts() { ProductId = l.ProductId, ProductName = r.ProductName, Quantity = l.Quantity, UnitPrice = l.UnitPrice }); //Resultat
		
		return View("Details", od);
	}

	public IActionResult MitarbeiterAnzeigen(int id)
	{
		Employee e = db.Employees.First(e => e.EmployeeId == id);
		return View("Mitarbeiter", e);
	}

	public IActionResult KundenFiltern(string land)
	{
		IQueryable<Customer> c = db.Customers.Where(e => e.Country == land);

		IEnumerable<string> countries = db.Customers.Select(e => e.Country).Distinct().OrderBy(e => e);
		ViewBag.Countries = countries;

		return View("Customers", c);
	}
}
