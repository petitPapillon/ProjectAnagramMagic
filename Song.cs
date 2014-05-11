using domasno.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace domasno
{
    public class Song
    {
        SoundPlayer sp;
        public Song()
        {
            sp = new SoundPlayer(Properties.Resources.AnagramMagic);
            playSong();
        }
        public void playSong()
        {
            sp.PlayLooping();
        }
        public void stopSong()
        {
            sp.Stop();
        }
    }
}
