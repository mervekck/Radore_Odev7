using Microsoft.AspNetCore.Mvc;
using MvcRadoreOrnek.Models;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using System.Text;

namespace MvcRadoreOrnek.Controllers
{
    public class KitapController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Kitap> kitapList = new List<Kitap>();
            //chrome bir sekme açıyorum ve oraya gitmek istediğim sayfayı yazıyorum
            using(var httpClient = new HttpClient())
            {
                using (var gelenYanit = await httpClient.GetAsync("http://localhost:5181/radore/Kitap"))
                {
                    string gelenString = await gelenYanit.Content.ReadAsStringAsync();
                    kitapList = JsonConvert.DeserializeObject<List<Kitap>>(gelenString);

                }
            }

            return View(kitapList);
        }

        public ViewResult KitapOlustur() => View();

       // public ViewResult KitapOlustur2()
        //{
           // return View();
        //}
        [HttpPost]
        public async Task<IActionResult> KitapOlustur(Kitap kitap)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent serializeEdilecekFilm = new StringContent(JsonConvert.SerializeObject(kitap), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://localhost:5181/radore/Kitap", serializeEdilecekFilm))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            //işlem başarılı mesajını göster
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

                return RedirectToAction("Index");

        }
        public async Task<IActionResult> KitapDetay(int id)
        {
            Kitap kitap = null;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var gelenYanit = await httpClient.GetAsync($"http://localhost:5181/radore/Kitap/{id}"))
                    {
                        if (gelenYanit.StatusCode == HttpStatusCode.OK)
                        {
                            string gelenString = await gelenYanit.Content.ReadAsStringAsync();
                            kitap = JsonConvert.DeserializeObject<Kitap>(gelenString);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Kitap detayları yüklenirken bir hata oluştu!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Veriler yüklenirken bir hata oluştu: {ex.Message}";
                }
            }

            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }
        public async Task<IActionResult> KitapSil(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync($"http://localhost:5181/radore/Kitap/{id}"))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            TempData["SuccessMessage"] = "Kitap başarıyla silindi!";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Kitap silinirken bir hata oluştu!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> KitapGuncelle(int id)
        {
            Kitap kitap = null;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var gelenYanit = await httpClient.GetAsync($"http://localhost:5181/radore/Kitap/{id}"))
                    {
                        if (gelenYanit.StatusCode == HttpStatusCode.OK)
                        {
                            string gelenString = await gelenYanit.Content.ReadAsStringAsync();
                            kitap = JsonConvert.DeserializeObject<Kitap>(gelenString);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Kitap detayları yüklenirken bir hata oluştu!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Veriler yüklenirken bir hata oluştu: {ex.Message}";
                }
            }
            return View(kitap);
        }
        [HttpPost]
        public async Task<IActionResult> KitapGuncelle(Kitap kitap)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent serializeEdilecekKitap = new StringContent(JsonConvert.SerializeObject(kitap), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"http://localhost:5181/radore/Kitap/{kitap.Id}", serializeEdilecekKitap))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            TempData["SuccessMessage"] = "Kitap başarıyla güncellendi!";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Kitap güncellenirken bir hata oluştu!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
