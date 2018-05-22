using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using GenesisWinForm.MathG3D;

namespace GenesisWinForm
{

    /// <summary>
    /// 从v1 收缩到 v2 的 收缩价
    /// </summary>
    public class ContractileValue
    {
        public ContractileValue(float _value,Vertex _v1) {
            value = _value;
            v1 = _v1;
           
        }


        public ContractileValue(Vertex _v1)
        {
            value = _v1.cost;
            v1 = _v1;

        }

        public float value;
        public Vertex v1;
       
    }
}
