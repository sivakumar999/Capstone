using BlogTrackerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;

namespace BlogTrackerApplication.Controllers
{
    public class AdminController : Controller
    {
       Uri baseAddress = new Uri("https://localhost:44329/api");
        HttpClient client;

        public AdminController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult EmpList()
        {
            List<EmpInfo> emps = new List<EmpInfo>();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/EmpInfoes").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    emps = JsonConvert.DeserializeObject<List<EmpInfo>>(data);
                }
                else
                {
                    TempData["ErrorMessage"] = "Error retrieving employee data from the API.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }

            return View(emps);
        }





        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmpInfo emps)
        {
            string data = JsonConvert.SerializeObject(emps);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responce = client.PostAsync(client.BaseAddress + "/EmpInfoes", content).Result;
            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("EmpList");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmpInfo emps = new EmpInfo();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/EmpInfoes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                emps = JsonConvert.DeserializeObject<EmpInfo>(data);
            }
            return View(emps);
        }

        [HttpPost]
        public ActionResult Edit(EmpInfo emp)
        {
            try
            {
                string data = JsonConvert.SerializeObject(emp);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/EmpInfoes/" + emp.Id, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EmpList");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating emp.");
                    return View(emp);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(emp);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                EmpInfo emps = new EmpInfo();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/EmpInfoes/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    emps = JsonConvert.DeserializeObject<EmpInfo>(data);
                }
                return View(emps);
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/EmpInfoes/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EmpList");
                }
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
            return View();
        }
    }
}
