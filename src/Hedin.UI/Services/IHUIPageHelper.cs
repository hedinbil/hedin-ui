using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.UI.Services
{
    public interface IHUIPageHelper
    {
        Task<List<HUIMenuItem>> GetMenuItems(System.Reflection.Assembly[] assembles);

    }
}
