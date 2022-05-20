using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioactivityDecayCalculator
{
    class NuclideManager
    {
        public ObservableCollection<Nuclide> Nuclides { get; }

        public event EventHandler NuclideChange;

        public NuclideManager()
        {
            Nuclides = new ObservableCollection<Nuclide>();
        }

        public void AddNuclide(Nuclide newNuclide)
        {
            Nuclides.Add(newNuclide);
            NuclideChange.Invoke(this, EventArgs.Empty);
        }

        public int GetCurrentMaxDecayTime()
        {
            return Nuclides.Max(n => n.FinishedDecay);
        }
    }
}
