using HotelMamagement.DAL;
using HotelMamagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMamagement.Controllers
{
    public class RoomsController : Controller
    {
        private readonly RoomDAL _roomDAL;
        public RoomsController()
        {
            _roomDAL = new RoomDAL();
        }
        public IActionResult Index()
        {
            List<Room> rooms = _roomDAL.GetAllRooms();
            return View(rooms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            _roomDAL.InsertRooms(room);
            return View(room);
        }

        public IActionResult Edit(int id)
        {
            Room room = _roomDAL.GetRoomById(id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            _roomDAL.UpdateRoom(room);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Room room = _roomDAL.GetRoomById(id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Delete(Room room)
        {   
            _roomDAL.Delete(room);
            return RedirectToAction("Index");
        }

    }
}
