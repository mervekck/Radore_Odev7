using CodeFirstRadoreOrnek.Data;
using CodeFirstRadoreOrnek.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstRadoreOrnek.Controllers
{
    //endpoint 
    //localhost:7079/radore/Kitap
    [Route("radore/[controller]")]
    [ApiController]
    public class KitapController : ControllerBase
    {

        ApplicationDbContext _context;

        //Constructor injection 
        public KitapController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Kitap Controlleri swagger postman yada tarayıcıdan çağırdığım zaman ilk olarak bu metot tetiklenecek
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kitap>>> kitaplariGetir()
        {
            List<Kitap> kitapListesi;
            //Select * from Kitap
            kitapListesi = await _context.Kitap.ToListAsync();

            return kitapListesi;
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<Kitap>>> kitapEkle(Kitap kitap)
        {
            try
            {
                //insert into Kitap (fiyat,sayfaSayisi,KitapAdi) values(400,480,'phpile ');
                _context.Kitap.Add(kitap);
               await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Kitap>> kitapDetay(int id)
        {
            try
            {
                var kitap = await _context.Kitap.FindAsync(id);
                return Ok(kitap);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Kitap detayları getirilirken bir hata oluştu", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var kitap = await _context.Kitap.FindAsync(id);

                _context.Kitap.Remove(kitap);
                await _context.SaveChangesAsync();

                return Ok("işlem başarılı");
            }
            catch (Exception ex)
            {
                return BadRequest($"İşlem Başarısız...Hata Mesajı=>{ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> KitapGuncelle(int id, Kitap kitap)
        {
            // İlgili kitabı bul
            var mevcutKitap = await _context.Kitap.FindAsync(id);

            if (mevcutKitap == null)
            {
                return NotFound(new { message = "Kitap bulunamadı!" });
            }

            try
            {
                // Mevcut kitabı güncelle
                mevcutKitap.kitapAdi = kitap.kitapAdi;
                mevcutKitap.fiyat = kitap.fiyat;
                mevcutKitap.sayfaSayisi = kitap.sayfaSayisi;

                _context.Kitap.Update(mevcutKitap);
                await _context.SaveChangesAsync();

                return Ok("Kitap başarıyla güncellendi!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Kitap güncellenirken bir hata oluştu", error = ex.Message });
            }
        }

    }

}
