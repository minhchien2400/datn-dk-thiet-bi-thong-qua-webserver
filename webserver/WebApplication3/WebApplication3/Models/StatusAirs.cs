namespace WebApplication3.Models
{
    public class StatusAirs
    {  // các thuộc tính của đối tượng "điều hòa"
        public int Id { get; set; } // mã id
        public int Status { get; set; } // trạng thái hiện tại (On/Off)
        public int Mode { get; set; } // Chế độ hiện tại
        public int Speed { get; set; } // tốc độ hiện tại
        public int Temp { get; set; } // nhiệt độ hiện tại
    }
}
