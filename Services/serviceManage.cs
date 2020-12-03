using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class serviceManage
    {
        public List<TaiKhoan> getAllAcc()
        {
            QuanLyXeKhachEntities data = new QuanLyXeKhachEntities();
            var list = data.TaiKhoans.ToList();
            return list;
        }
        public List<NhanVien> getAllEmp()
        {
            QuanLyXeKhachEntities dataNV = new QuanLyXeKhachEntities();
            var list = dataNV.NhanViens.ToList();
            return list;
        }
        public List<KhachHang> getAllCustomer()
        {
            QuanLyXeKhachEntities dataCus = new QuanLyXeKhachEntities();
            var list = dataCus.KhachHangs.ToList();
            return list;
        }
        public List<XeKhach> getAllvehicle()
        {
            QuanLyXeKhachEntities dataVeh = new QuanLyXeKhachEntities();
            var list = dataVeh.XeKhaches.ToList();
            return list;
        }
        public List<PhieuVe> getAllTicket()
        {
            QuanLyXeKhachEntities dataTicket = new QuanLyXeKhachEntities();
            var list = dataTicket.PhieuVes.ToList();
            return list;
        }
        public List<HuongDanVien> getAllTourGuild()
        {
            QuanLyXeKhachEntities dataTourGuild = new QuanLyXeKhachEntities();
            var list = dataTourGuild.HuongDanViens.ToList();
            return list;
        }
        public List<TaiXe> getAllDriver()
        {
            QuanLyXeKhachEntities dataDriver = new QuanLyXeKhachEntities();
            var list = dataDriver.TaiXes.ToList();
            return list;
        }
        public List<DiaDiem> getAllLocations()
        {
            QuanLyXeKhachEntities dataLocation = new QuanLyXeKhachEntities();
            var list = dataLocation.DiaDiems.ToList();
            return list;
        }
        public List<T>
    }
}
