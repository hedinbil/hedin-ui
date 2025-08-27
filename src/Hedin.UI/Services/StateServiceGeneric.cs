using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.UI.Services;

public abstract class StatefulComponentBase<TState> : StatefulComponentBase where TState : new()
{
    protected TState Model { get; set; } = new TState();

    protected override void LoadState()
    {
        Model = StateService.GetState<TState>(PageKey) ?? new TState();
    }

    protected override void SaveState()
    {
        StateService.SaveState(PageKey, Model);
    }

    protected override void ClearState()
    {
        StateService.ClearState(PageKey); 
    }

}
