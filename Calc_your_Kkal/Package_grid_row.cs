using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Project
{
    public class Package_grid_row
    {
        private String name;
        private Double kkal;
        private Double in_1g;

        void Package_grid()
        {

        }

        public Package_grid_row(string name, double kkal, double in_1g)
        {
            this.name = name;
            this.kkal = kkal;
            this.In_1g = in_1g;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public double Kkal
        {
            get
            {
                return kkal;
            }

            set
            {
                kkal = value;
            }
        }

        public double In_1g
        {
            get
            {
                return in_1g;
            }

            set
            {
                in_1g = value;
            }
        }
    }
}
