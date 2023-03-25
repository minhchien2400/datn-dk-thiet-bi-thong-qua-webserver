using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Runtime.CompilerServices;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Service.Remote;

namespace WebApplication3.Service.Remote
{
    public class RemoteService : IRemoteService
    {
        private readonly SmartHomeDbContext _remoteDbContext;
        public RemoteService(SmartHomeDbContext remoteDbContext)
        {
            _remoteDbContext = remoteDbContext;
        }

        // hàm lấy giá trị của keyRemote khi click trên UI-remote và update vào database
        public async Task<int> setKeyRomote(int id, int key)
        {
            // update bulb
            if(key == 1)
            {
                updateStatusBulb(1, 1, 0);
            }
            if (key == 2)
            {
                updateStatusBulb(1, 0, 0);
            }
            if (key == 3)
            {
                updateStatusBulb(2, 1, 1);
            }
            if (key == 4)
            {
                updateStatusBulb(2, 0, 0);
            }
            if (key == 18)
            {
                updateStatusBulb(2, 1, 2);
            }
            if (key == 19)
            {
                updateStatusBulb(2, 1, 3);
            }

            //update speak
            if (key == 20) // on
            {
                updateStatusSpeak(1, 1, 2, 1); // id:1 - status:on - pause-not mute
            }
            if (key == 21) // off
            {
                updateStatusSpeak(1, 0, 0, 1);
            }
            if (key == 11) // play
            {
                updateStatusSpeak(1, 2, 1, 2);
            }
            if (key == 12) //pause
            {
                updateStatusSpeak(1, 2, 0, 2);
            }
            if (key == 15) // mute
            {
                updateStatusSpeak(1, 2, 2,0);
            }
            if (key == 22) // not mute
            {
                updateStatusSpeak(1, 2, 2, 1);
            }


            // update air
            if (key == 5) // on
            {
                updateStatusAir(1, 1, 5, 6, 0); // id:1 - on - mode old - speed old - temp old
            }
            if (key == 6) // off
            {
                updateStatusAir(1, 0, 5, 6, 0);
            }
            if (key == 7) // mode:auto
            {
                updateStatusAir(1, 2, 0, 6, 0);
            }
            if (key == 8) // mode: heat
            {
                updateStatusAir(1, 2, 1, 6, 0);
            }
            if (key == 9) // mode : cool
            {
                updateStatusAir(1, 2, 2, 6, 0);
            }
            if (key == 10) // mode dry
            {
                updateStatusAir(1, 2, 3, 6, 0);
            }
            if (key == 23) // mode: fan
            {
                updateStatusAir(1, 2, 4, 6, 0);
            }
            if (key == 24) // speed: auto
            {
                updateStatusAir(1, 2, 5, 0, 0);
            }
            if (key == 25) // speed: 1
            {
                updateStatusAir(1, 2, 5, 1, 0);
            }
            if (key == 26) // speed: 2
            {
                updateStatusAir(1, 2, 5, 2, 0);
            }
            if (key == 27) // speed: 3
            {
                updateStatusAir(1, 2, 5, 3, 0);
            }
            if (key == 28) // speed: 4
            {
                updateStatusAir(1, 2, 5, 4, 0);
            }
            if (key == 29) // speed: 5
            {
                updateStatusAir(1, 2, 5, 5, 0);
            }
            Remotes remotes = new Remotes();
            Remotes oldRemode = _remoteDbContext.Set<Remotes>().Where(r => r.Id == id).FirstOrDefault();    
            remotes.Id = id;
            remotes.KeyRemote = key;
            _remoteDbContext.Remove(oldRemode);
            _remoteDbContext.Add(remotes);
            _remoteDbContext.SaveChanges();
            return key;
        }

        // hàm lấy ra giá trị của keyRemote từ database cho ESP thực thi 
        public async Task<int> getKeyRomote(int id)
        {
            var keyRemote =_remoteDbContext.Set<Remotes>().Where(r => r.Id == id).Select(r => r.KeyRemote).FirstOrDefault();
            return keyRemote;
        }
        public async Task<Remotes> GetRemote(int id)
        {
            var remotes = _remoteDbContext.Set<Remotes>().Where(r => r.Id == id).FirstOrDefault();
            return remotes;
        }


        public async void updateStatusBulb(int id,int status,int color)
        {
            StatusBulbs newStatus = new StatusBulbs();
            StatusBulbs oldstatus = _remoteDbContext.Set<StatusBulbs>().Where(s => s.Id == id).FirstOrDefault();
            newStatus.Id = id;
            newStatus.Status = status;
            newStatus.Color = color;
            _remoteDbContext.Remove(oldstatus);
            _remoteDbContext.Add(newStatus);
            _remoteDbContext.SaveChanges();
        }

        public async void updateStatusSpeak(int id,int status, int playPause,int volume)
        {
            StatusSpeaks newStatus = new StatusSpeaks();
            StatusSpeaks oldstatus =_remoteDbContext.Set<StatusSpeaks>().Where(s => s.Id == id).FirstOrDefault();
            newStatus.Id = id;
            newStatus.Status = status !=2 ? status : oldstatus.Status;
            newStatus.PlayPause = playPause != 2 ? playPause : oldstatus.PlayPause;
            newStatus.Volume = volume != 2 ? volume : oldstatus.Volume;
            _remoteDbContext.Remove(oldstatus);
            _remoteDbContext.Add(newStatus);
            _remoteDbContext.SaveChanges();
        }

        public void updateStatusAir(int id, int status, int mode, int speed,int temp)
        {
            StatusAirs newStatus = new StatusAirs();
            StatusAirs oldstatus = _remoteDbContext.Set<StatusAirs>().Where(a => a.Id == id).FirstOrDefault();
            newStatus.Id= id;
            newStatus.Status = status != 2 ? status : oldstatus.Status;
            newStatus.Mode = mode == 5 ? oldstatus.Mode : mode;
            newStatus.Speed = speed == 6 ? oldstatus.Speed : speed;
            newStatus.Temp = temp == 0 ? oldstatus.Temp : temp;
            _remoteDbContext.Remove(oldstatus);
            _remoteDbContext.Add(newStatus);
            _remoteDbContext.SaveChanges();
        }
    }
}
