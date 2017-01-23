using System;
using System.Collections.Generic;
using System.Text;

namespace MakeConfig.Bean
{
    public class TestPlan
    {
        public int i_Idx = -1;
        public string str_Os = string.Empty;
        public bool b_NeedCat = true;
        public List<TestRcd> lst_TestRcd = new List<TestRcd>();
        public TestPlan(int iIdx)
        {
            this.i_Idx = iIdx;
        }
    }
}
