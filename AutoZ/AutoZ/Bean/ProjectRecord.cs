//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using AutoZKernel.Bean;

namespace AutoZ.Bean
{
    public class ProjectRecord : Cloneable
    {
        public int i_index = -1;
        public string str_path = null;
        public bool b_hasSet = false;
        public List<Lang> lst_supportedLang = new List<Lang>();
        public int i_packageNum = 1;
        public bool b_useDifferentBAT = false;
        public bool b_haveENUDisk = false;
        public bool b_haveSpecialOEM = false;
        public List<OEM> lst_specialOEM_all = new List<OEM>();
        public List<Lang_OEMs> lst_specials = new List<Lang_OEMs>();
        
        public ProjectRecord(int idx, string path)
        {
            this.i_index = idx;
            this.str_path = path;
        }

        public void cloneLang(Lang record)
        {
            this.lst_supportedLang.Add(Clone<Lang>(record));
        }
        public void cloneOEM(OEM record)
        {
            this.lst_specialOEM_all.Add(Clone<OEM>(record));
        }
        public void cloneLang_OEMs(Lang_OEMs record)
        {
            this.lst_specials.Add(Clone<Lang_OEMs>(record));
        }
    }
}
