using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PresentationTest;

internal class TextErrorInformer : IErrorInformer
{
    private string _recentMessage;

    public TextErrorInformer()
    {
        this._recentMessage = string.Empty;
    }

    public void InformError(string message)
    {
        this._recentMessage = message;
    }

    public void InformSuccess(string message)
    {
        this._recentMessage = message;
    }

    public string GetRecentMessage()
    {
        return this._recentMessage;
    }
}
